using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Alarm_Manager : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * W klasie "Alarm_Manager" sa zapisywane wszelkie dane o ustawieniach alarmow takie jak uwzglednianie bledu pomiarowego do wynikow, zapisywanie stanu o wystapieniu ostrzezenia o kolizji z danego alarmu, czy wartosci odleglosci miedzy alarmem, a trafionym obiektem przez promien wychodzacy z danego czujnika.
     * Znajduja sie tutaj rowniez wszystkie zmienne publiczne, do ktorych trzeba bylo zrobic referencje do obiektow ustawionych na scenie gry.
     * W tej klasie tworzone sa takze wszystkie alarmy o okreslonych przez uzytkownika parametrach oraz wyswietlane zostaja informacje uzytkownikowi takie jak odleglosc czujnika od trafionego obiektu oraz ostrzezenia o kolizji.
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    Dictionary<string, Alarm> alarm = new Dictionary<string, Alarm>();

    public static bool BoxMuller {get; set;}

    public static bool[] danger = new bool[5];
    public static double[] distance = new double[5];

    public GameObject frontAlarm, leftAlarm, rightAlarm, rearAlarm, echosounder, warning, ship;
    public InputField frontAlarmInput, leftAlarmInput, rightAlarmInput, rearAlarmInput, echosounderInput;
    public Button frontAlarmButton, leftAlarmButton, rightAlarmButton, rearAlarmButton, echosounderButton;
    public Text frontAlarmText, leftAlarmText, rightAlarmText, rearAlarmText, echosounderText;

    public AudioSource bellSound;

    /*
     * Opis poszczegolnych zmiennych i wlasciwosci:
     * 
     * "alarm"                  - Jest to slownik utworzonych obiektow z klasy "Alarm".
     * 
     * "BoxMuller"              - Jest to wlasciwosc, ktora przechowuje stan checkBoxa z menu opcji.
     * 
     * "danger"                 - Jest to tablica jednowymiarowa przechowywujaca stany danych alarmow odnosnie ostrzezenia o przekroczonej wartosci.
     * "distance"               - Jest to tablica jednowymiarowa przechowywujaca wartosci odleglosci miedzy trafionymi obiektami, a promieniami wychodzacymi z czujnikow.
     * 
     * "frontAlarm"             - Jest to fizyczny obiekt czujnika ustawionego w swiecie gry, z ktorego ma wychodzic promien.
     * "leftAlarm"              - Jest to fizyczny obiekt czujnika ustawionego w swiecie gry, z ktorego ma wychodzic promien.
     * "rightAlarm"             - Jest to fizyczny obiekt czujnika ustawionego w swiecie gry, z ktorego ma wychodzic promien.
     * "rearAlarm"              - Jest to fizyczny obiekt czujnika ustawionego w swiecie gry, z ktorego ma wychodzic promien.
     * "echosounder"            - Jest to fizyczny obiekt czujnika ustawionego w swiecie gry, z ktorego ma wychodzic promien.
     * "warning"                - Jest to wiadomosc tekstowa ostrzezajaca uzytkownika przed kolizja.
     * "ship"                   - Jest to fizyczny obiekt statku ustawiony w swiecie gry.
     * 
     * "frontAlarmInput"        - Jest to pole tekstowe w menu opcji odpowiedzialne za zmiane wartosci limitu, ponizej ktorej ma sie wyswietlac ostrzezenie dotyczace kolizji statku z obiektem.
     * "leftAlarmInput"         - Jest to pole tekstowe w menu opcji odpowiedzialne za zmiane wartosci limitu, ponizej ktorej ma sie wyswietlac ostrzezenie dotyczace kolizji statku z obiektem.
     * "rightAlarmInput"        - Jest to pole tekstowe w menu opcji odpowiedzialne za zmiane wartosci limitu, ponizej ktorej ma sie wyswietlac ostrzezenie dotyczace kolizji statku z obiektem.
     * "rearAlarmInput"         - Jest to pole tekstowe w menu opcji odpowiedzialne za zmiane wartosci limitu, ponizej ktorej ma sie wyswietlac ostrzezenie dotyczace kolizji statku z obiektem.
     * "echosounderInput"       - Jest to pole tekstowe w menu opcji odpowiedzialne za zmiane wartosci limitu, ponizej ktorej ma sie wyswietlac ostrzezenie dotyczace kolizji statku z obiektem.
     * 
     * "frontAlarmButton"       - Jest to przycisk, ktory graficznie przedstawia wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * "leftAlarmButton"        - Jest to przycisk, ktory graficznie przedstawia wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * "rightAlarmButton"       - Jest to przycisk, ktory graficznie przedstawia wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * "rearAlarmButton"        - Jest to przycisk, ktory graficznie przedstawia wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * "echosounderButton"      - Jest to przycisk, ktory graficznie przedstawia wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * 
     * "frontAlarmText"         - Jest to napis na przycisku, ktory wyswietla aktualna wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * "leftAlarmText"          - Jest to napis na przycisku, ktory wyswietla aktualna wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * "rightAlarmText"         - Jest to napis na przycisku, ktory wyswietla aktualna wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * "rearAlarmText"          - Jest to napis na przycisku, ktory wyswietla aktualna wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * "echosounderText"        - Jest to napis na przycisku, ktory wyswietla aktualna wartosc odleglosci miedzy trafionym obiektem, a promieniem wychodzacym z czujnika.
     * 
     * "bellSound"              - Jest to dzwiek dzwonka, ktory zostaje odtwarzany w petli, gdy statek jest bliski kolizji z obiektem.
    */

    #endregion

    #region OPIS METOD

    #region OPIS METODY "START()"

    void Start()
    {
        alarm.Add("front", new Alarm(frontAlarmInput, frontAlarm, 0, frontAlarm.transform.forward, ship.transform, 500));

        alarm.Add("left", new Alarm(leftAlarmInput, leftAlarm, 1, -leftAlarm.transform.right, ship.transform, 200));
        alarm.Add("right", new Alarm(rightAlarmInput, rightAlarm, 2, rightAlarm.transform.right, ship.transform, 200));

        alarm.Add("rear", new Alarm(rearAlarmInput, rearAlarm, 3, -rearAlarm.transform.forward, ship.transform, 200));

        alarm.Add("echosounder", new Alarm(echosounderInput, echosounder, 4, Vector3.down, ship.transform, 10));
    }

    /*
     * Wewnatrz funkcji "Start()" sa dodawane wszelkie ustawione na statku czujniki alarmow do slownika, aby mozna bylo latwiej nimi zarzadzac.
     * Klucze slownika sa typu "string" i posiadaja prosta nazwe, dzieki ktorej mozna szybko znalezc okreslony alarm.
     * Alarmy sa tworzone bezposrednio o okreslonych parametrach przed dodaniem ich do slownika.
    */

    #endregion
    
    #region OPIS METODY "UPDATE_ALARM_CLASS()"
    
    void UpdateAlarmClass()
    {
        foreach (string key in alarm.Keys)
        {
            alarm[key].shipTransform = ship.transform;

            if (!key.Equals("echosounder"))
            {
                if (key.Equals("front"))
                    alarm[key].direction = frontAlarm.transform.forward;

                else if (key.Equals("left"))
                    alarm[key].direction = -leftAlarm.transform.right;

                else if (key.Equals("right"))
                    alarm[key].direction = rightAlarm.transform.right;

                else
                    alarm[key].direction = -rearAlarm.transform.forward;
            }

            alarm[key].ForceUpdate();
        }
    }

    /*
     * Funkcja "UpdateAlarmClass()" sluzy do zaktualizowania poszczegolnych wartosci zmiennych w obiektach utworzonych z klasy "Alarm".
     * Poprzez petle "foreach" wyszukiwane sa okreslone alarmy po kluczu, a nastepnie do kazdego znalezionego obiektu przekazywana jest nowa wartosc pozycji statku.
     * W instrukcji warunkowej sprawdzany jest po kolei kazdy alarm po jego kluczu w celu indywidualnego wprowadzenia nowych wartosci dotyczacych pozycji danego alarmu.
     * Na koniec wywolywana jest metoda "ForceUpdate()" dla kazdego utworzonego alarmu dodanego do slownika.
    */

    #endregion
    
    #region OPIS METODY "UPDATE()"
    
    void Update()
    {
        UpdateAlarmClass();

        frontAlarmText.text = string.Format("{0:000}", distance[0]);
        leftAlarmText.text = string.Format("{0:000}", distance[1]);
        rightAlarmText.text = string.Format("{0:000}", distance[2]);
        rearAlarmText.text = string.Format("{0:000}", distance[3]);
        echosounderText.text = string.Format("{0:000.00}", distance[4]);

        if (danger[0] || danger[1] || danger[2] || danger[3] || danger[4])
        {
            if (!bellSound.isPlaying)
            {
                warning.SetActive(true);
                bellSound.Play();
            }

            if (danger[0])
                Button_Manager.SetButtonColor(frontAlarmButton, Color.red);
            else
                Button_Manager.SetButtonColor(frontAlarmButton, Color.green);

            if (danger[1])
                Button_Manager.SetButtonColor(leftAlarmButton, Color.red);
            else
                Button_Manager.SetButtonColor(leftAlarmButton, Color.green);

            if (danger[2])
                Button_Manager.SetButtonColor(rightAlarmButton, Color.red);
            else
                Button_Manager.SetButtonColor(rightAlarmButton, Color.green);

            if (danger[3])
                Button_Manager.SetButtonColor(rearAlarmButton, Color.red);
            else
                Button_Manager.SetButtonColor(rearAlarmButton, Color.green);

            if (danger[4])
                Button_Manager.SetButtonColor(echosounderButton, Color.red);
            else
                Button_Manager.SetButtonColor(echosounderButton, Color.green);
        }

        if (!danger[0] && !danger[1] && !danger[2] && !danger[3] && !danger[4])
        {
            warning.SetActive(false);

            Button_Manager.SetButtonColor(frontAlarmButton, Color.green);
            Button_Manager.SetButtonColor(leftAlarmButton, Color.green);
            Button_Manager.SetButtonColor(rightAlarmButton, Color.green);
            Button_Manager.SetButtonColor(rearAlarmButton, Color.green);
            Button_Manager.SetButtonColor(echosounderButton, Color.green);

            bellSound.Stop();
        }
    }

    /*
     * W metodzie "Update()" co 1 klatke wywolywana jest funkcja "UpdateAlarmClass()", majaca zaktualizowac wartosci obiektow utworzonych w klasie "Alarm".
     * Poza tym, wewnatrz funkcji "Update()" aktualizowane sa napisy wyswietlane uzytkownikowi, dotyczace aktualnej wartosci odleglosci miedzy trafionymi obiektami, a promieniami wychodzacymi z czujnikow.
     * Nastepnie w instrukcji warunkowej sprawdzane sa stany alarmow dotyczace ryzyka kolizji statku z obiektem w swiecie gry.
     * Jesli ktorykolwiek alarm zglosi niebezpieczenstwo w postaci wystapienia kolizji, to zaczyna rozbrzmiewac zapetlony dzwiek dzwonka oraz wyswietlana zostaje specjalnie przygotowane wiadomosc dotyczaca ostrzezenia o kolizji.
     * W instrukcji warunkowej sprawdzane jest, ktore alarmy zglaszaja niebezpieczenstwo oraz odpowiednio nadaja barwe przyciskowi w zaleznosci od wykrytego zagrozenia.
     * Ostatnia instrukcja warunkowa w funkcji sprawdza, czy kazdy z ustawionych alarmow nie zglasza stanu zagrozenia. Jesli wszystkie alarmy nie zglaszaja niebezpieczenstwa, to ostrzezenie o kolizji zostaje wylaczone, wszystkie przyciski informujace o zagrozeniu przybieraja barwe zielona oraz zatrzymywany jest dzwiek dzwonka.
    */

    #endregion

    #endregion
}