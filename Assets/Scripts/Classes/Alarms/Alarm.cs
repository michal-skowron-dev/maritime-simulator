using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Alarm
{
    #region OPIS KLASY

    /*
     * Klasa "Alarm" sluzy glownie do wykonywania wszystkich operacji zwiazanych z przeliczaniem odleglosci statku od obiektow, uwzgledniania bledu pomiarowego przy pomocy klasy "BoxMuller", a takze przeksztalcanie wspolrzednych na zapis macierzowy przy pomocy klasy "Matrix".
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    GameObject alarm, sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    readonly ushort id;
    public Vector3 direction;
    public Transform shipTransform;
    ushort limit;

    BoxMuller boxMuller = new BoxMuller(0.3, 0);
    double boxMullerError;

    Matrix matrix;
    List<object> mixedList = new List<object>();

    double[,] echoPointGlobal;
    double[] angles;

    /*
     * Opis poszczegolnych zmiennych:
     * 
     * "alarm"              - Jest to czujnik, ktory rejestruje odleglosc okreslonych obiektow od statku. Zmienna ta jest wykorzystywana przede wszystkim do przekazywania aktualnej pozycji obiektu, z ktorego wychodzi promien.
     * "sphere"             - Jest to sfera, ktora ma byc generowana w swiecie gry na podstawie wyznaczonych wartosci z klasy "Matrix".
     * 
     * "id"                 - Jest to identyfikator alarmu z ktorego wychodzi promien.
     * 
     * "direction"          - Jest to kierunek, w ktorym ma byc skierowany promien wychodzacy z czujnika. Zmienna jest publiczna, poniewaz jej wartosc bedzie stale aktualizowana przy pomocy klasy "ALarm_Manager".
     * "shipTransform"      - Zmienna zawierajca dane o pozycji i obrocie statku w swiecie gry. Zmienna jest publiczna, poniewaz jej wartosc bedzie stale aktualizowana przy pomocy klasy "ALarm_Manager".
     * 
     * "limit"              - Jest to zmienna, w ktorej zapisana jest wartosc odleglosci, ponizej ktorej ma wyswielac sie ostrzezenie dotyczace kolizji statku z danym obiektem.
     * 
     * "boxMuller"          - Jest to obiekt klasy "BoxMuller", ktora sluzy do generowania bledu pomiarowego przy pomocy transformacji Boxa-Mullera.
     * "boxMullerError"     - Jest to zmienna przechowywujaca aktualna wartosc bledu pomiarowego.
     * 
     * "matrix"             - Jest to obiekt klasy "Matrix", ktora sluzy do zapisu macierzowego wspolrzednych.
     * "mixedList"          - Jest to lista obiektow, ktora jest niezbedna do wyznaczenia wspolrzednych sfery. Lista jest typu "object", poniewaz niezbedne bedzie wypakowanie zmiennych o roznych typach razem przy zwracaniu wartosci z metody "GetCalculatedValues(Transform shipTransform, GameObject alarm)" z klasy "Matrix".
     * 
     * "echoPointGlobal"    - Jest to zmienna niezbedna do wyznaczenia pozycji sfery w swiecie gry. Jest to rowniez pierwsza zmienna wypakowywana z listy obiektow pozyskanej z klasy "Matrix".
     * "angles"             - Jest to zmienna niezbedna do wyznaczenia obrotu sfery w swiecie gry. Jest to rowniez druga zmienna wypakowywana z listy obiektow pozyskanej z klasy "Matrix".
    */

    #endregion

    #region OPIS METOD

    #region OPIS KONSTRUKTORA

    public Alarm(InputField input, GameObject alarm, ushort id, Vector3 direction, Transform shipTransform, ushort limit)
    {
        input.onEndEdit.AddListener(delegate{UpdateInput(input);});

        this.alarm = alarm;
        this.id = id;
        this.direction = direction;
        this.shipTransform = shipTransform;
        this.limit = limit;

        matrix = new Matrix(id);
    }

    /*
     * Przy tworzeniu obiektu wymagane jest podanie takich parametrow jak pole tekstowe w menu opcji zwiazane z danym alarmem, sam obiekt czujnika ze swiata gry, identyfikator czujnika, kierunek z ktorego wychodzi promien z czujnika, aktualne dane o pozycji statku oraz wartosc limitu ponizej ktorej ma sie wyswietlac ostrzezenie o kolizji.
     * Aby pole tekstowe prawidlowo dzialalo po zakonczeniu edytowania wartosci przez uzytkownika, niezbedny jest delegat, ktory umozliwi reakcje na zmienione wartosci limitu dla danego alarmu w menu opcji poprzez funkcje "UpdateInput(InputField input)".
     * Koncowym poleceniem w konstruktorze jest przydzielenie obiektowi typu "Matrix" numeru id okreslonego czujnika.
    */

    #endregion
    
    #region OPIS METODY "UPDATE_INPUT(INPUTFIELD INPUT)"
    
    void UpdateInput(InputField input)
    {
        try{limit = ushort.Parse(input.text);} catch{input.text = limit.ToString();}
    }

    /*
     * Wewnatrz bezargumentowej funkcji "UpdateInput(InputField input)" wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     * Jesli uzytkownik wprowadzi wartosc mniejsza od 0, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "limit" zostanie wpisana w pole tekstowe.
     * Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion
    
    #region OPIS METODY "FORCE_UPDATE()"
    
    public void ForceUpdate()
    {
        // Debug.DrawRay(alarm.transform.position, direction * 1000);

        if (Alarm_Manager.BoxMuller)
            boxMullerError = boxMuller.BoxMullerCalc();

        else if (!Alarm_Manager.BoxMuller && boxMullerError != 0)
            boxMullerError = 0;

        RaycastHit hit;

        if (Physics.Raycast(new Ray(alarm.transform.position, direction), out hit))
        {
            if (hit.collider.tag.Equals("Terrain"))
            {
                Alarm_Manager.distance[id] = hit.distance + boxMullerError;

                if (Alarm_Manager.distance[id] < limit)
                    Alarm_Manager.danger[id] = true;

                else
                    Alarm_Manager.danger[id] = false;
            }
        }

        mixedList = matrix.GetCalculatedValues(shipTransform, alarm);
        echoPointGlobal = (double[,])mixedList[0];
        angles = (double[])mixedList[1];

        sphere.transform.position = new Vector3((float)echoPointGlobal[0, 3], (float)echoPointGlobal[1, 3], (float)echoPointGlobal[2, 3]);
        sphere.transform.rotation = Quaternion.Euler((float)angles[0], (float)angles[1], (float)angles[2]);
    }

    /*
     * Jako ze klasa "Alarm" nie dziedziczy z klasy "MonoBehaviour", co powoduje brak metody "Update()", ktora wykonuje polecenia w niej zawarte co 1 klatke, niezbedne bylo znalezienie innego sposobu na zaktualizowanie danych z klasy "Alarm".
     * Problem udalo sie rozwiazac poprzez wywolanie metody "ForceUpdate()" wewnatrz funkcji "Update()" w klasie "Alarm_Manager", ktora juz jednak dziedziczy po "MonoBehaviour".
     * Wewnatrz funkcji "ForceUpdate()" sprawdzane jest, czy opcja uwzgledniajaca blad pomiaru jest wlaczona. Jesli stan opcji zapisany w klasie "Alarm_Manager" ma wartosc "true", to generowana jest nowa wartosc bledu pomiarowego poprzez metode zawarta w klasie "BoxMuller".
     * Jesli jednak dodawanie bledu pomiarowego jest wylaczone w menu opcji, to wartosc zmiennej "boxMullerError" jest zerowana (sprawdzane jest rowniez, czy zmienna nie przyjela juz wartosci 0, aby nie zerowac jej ponownie) i do wyniku koncowego odleglosci czujnika od trafionego obiektu jest dodawana wartosc 0.
     * W kolejnej instrukcji warunkowej sprawdzane jest, czy promien wychodzacy z czujnika trafil w obiekt z ustawionym tagiem "Terrain". Jesli warunek jest prawdziwy, to zapisywana jest informacja o odleglosci czujnika od trafionego obiektu do zmiennej, znajdujacej sie w klasie "Alarm_Manager".
     * Dodatkowo sprawdzane jest, czy dany alarm ma wyswietlic ostrzezenie o kolizji, jesli wartosc bedzie mniejsza od wartosci zapisanej w zmiennej "limit".
     * Kolejne polecenia w funkcji, to obliczenie wspolrzednych w formie zapisu macierzowego przy pomocy klasy "Matrix".
     * Wyliczone wartosci musza zostac nastepnie wypakowane, poniewaz potrzebne dane sa zapisane w roznych typach zmiennych.
     * Pierwsza wartoscia w zwroconej liscie jest tablica wielowymiarowa o rozmiarze 4 x 4, natomiast 2 wypakowywana zmienna jest tablica jednowymiarowa, zawierajaca dane o katach obrotu sfery.
     * Na koniec ustawiana jest pozycja i obrot sfery w swiecie gry na podstawie uzyskanych wynikow.
    */

    #endregion

    #endregion
}