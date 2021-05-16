using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;

public class Logs : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa "Logs" sluzy do generowania pliku dziennika, w ktorej zawarte beda takie informacje jak numer komunikatu, czas wystapienia komunikatu, pozycja statku w swiecie gry oraz zarejestrowana wartosc kursu i predkosci.
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    public GameObject ship;
    public Text courseLabel, speedLabel;

    string clock, log;
    ushort counter;
    bool wait;

    /*
     * Opis poszczegolnych zmiennych:
     * 
     * "ship"           - Jest to fizyczny obiekt statku ustawiony w swiecie gry.
     * 
     * "courseLabel"    - Jest to napis, ktory wyswietla aktualna wartosc kursu statku.
     * "speedLabel"     - Jest to napis, ktory wyswietla aktualna wartosc predkosci statku.
     * 
     * "clock"          - Jest to zmienna przechowywujaca aktualny czas systemowy pod postacia zmiennej string.
     * "log"            - Jest to zmienna, ktora przechowywuje pojedynczy komunikat o numerze komunikatu, czasie systemowym, pozycji statku, wartosci kursu i jego predkosci.
     * 
     * "counter"        - Jest to zmienna sluzaca do zapamietywania aktualnego numeru komunikatu.
     * 
     * "wait"           - Jest to zmienna sprawdzajaca kiedy moze zostac zapisany kolejny komunikat.
    */

    #endregion
    
    #region OPIS METOD
    
    #region OPIS METODY "UPDATE()"
    
    void Update()
    {
        if (!wait)
        {
            counter++;
            clock = DateTime.Now.ToString("dd/MM/yyyy|HH:mm:ss");

            log = string.Format("[N:{0}][D:{1}][X:{2}][Y:{3}][Z:{4}][C:{5}][S:{6}]", counter, clock, ship.transform.position.x, ship.transform.position.y, ship.transform.position.z, courseLabel.text, speedLabel.text);
            // Debug.Log(log);

            SaveLogs();

            wait = true;
            StartCoroutine(Wait());
        }
    }

    /*
     * Wewnatrz funkcji "Update()" sprawdzana jest na poczatek mozliwosc zapisania komunikatu.
     * Instrukcja warunkowa obecna w funkcji jest na tyle istotna, ze zapobiega zapisywaniu sie wartosci do pliku co 1 klatke dzialania aplikacji.
     * Wewnatrz instrukcji warunkowej przygotowywane sa dane do zapisu takie jak numer komunikatu, czy aktualny czas systemowy.
     * W zmiennej "log" wartosci sa dodawane w okreslonym porzadku, ktory zapewni wieksza czytelnosc po zapisie danego komunikatu do pliku.
     * Nastepnym etapem jest nadpisanie badz utworzenie pliku dziennika z nowo dodanymi wartosciami poprzez funkcje "SaveLogs()".
     * Ostatnie 2 polecenia w funkcji sluza do odczekania 5 sekund przed zapisaniem kolejnego komunikatu do pliku.
    */

    #endregion
    
    #region OPIS IENUMERATORA "WAIT()"
    
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        wait = false;
    }

    /*
     * Interfejs "IEnumerator Wait()" posluzyl do odczekania 5 sekund zanim mozna bylo przywrocic zmiennej "wait" wartosc "false".
     * Wyrazenie "yield" zatrzymuje tymczasowe wykonanie metody i wysyla jedna wartosc do wywolywacza.
    */

    #endregion
    
    #region OPIS METODY "SAVE_LOGS()"
    
    void SaveLogs()
    {
        StreamWriter save = new StreamWriter(Application.dataPath + "/Logs.txt", true);
        save.WriteLine(log);
        save.Close();
    }

    /*
     * Funkcja "SaveLogs()" sluzy do zapisywania kolejnych komunikatow do pliku dziennika.
     * Obiekt "save" przechowywuje sciezke zapisu pliku dziennika wraz z wlaczona opcja dodawania kolejnych wpisow do juz istniejacego pliku.
    */

    #endregion

    #endregion
}