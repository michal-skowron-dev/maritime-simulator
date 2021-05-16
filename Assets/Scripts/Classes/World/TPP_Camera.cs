using UnityEngine;
using UnityEngine.UI;
using System;

public class TPP_Camera : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa "TPP_Camera" sluzy do ustawiania i zarzadzania kamera z perspektywy trzeciej osoby.
    */

    #endregion

    #region OPIS ZMIENNYCH

    public static bool AltCameraControl {get; set;}

    public InputField minimalDistanceInput, maximumDistanceInput, minimalDeflectionInput, maximumDeflectionInput, remotenessSensitivityInput, xyAxisSensitivityInput;
    public Transform tppCamera, ship;

    float minimalDistance = 10.0f, maximumDistance = 500.0f, minimalDeflection = 30.0f, maximumDeflection = 85.0f, remotenessSensitivity = 8.0f, xyAxisSensitivity = 8.0f, zoom = 200.0f, x = 1.0f, y = 65.0f, temp;

    Vector3 vector;
    Quaternion rotation;

    /*
     * Opis poszczegolnych zmiennych i wlasciwosci:
     * 
     * "AltCameraControl"               - Wlasciwosc zapisujaca wybrany przez uzytkownika sposob kierowania kamera z menu opcji.
     * 
     * "minimalDistanceInput"           - Pole tekstowe, w ktore uzytkownik podaje zadana wartosc w menu opcji.
     * "maximumDistanceInput"           - Pole tekstowe, w ktore uzytkownik podaje zadana wartosc w menu opcji.
     * 
     * "minimalDeflectionInput"         - Pole tekstowe, w ktore uzytkownik podaje zadana wartosc w menu opcji.
     * "maximumDeflectionInput"         - Pole tekstowe, w ktore uzytkownik podaje zadana wartosc w menu opcji.
     * 
     * "remotenessSensitivityInput"     - Pole tekstowe, w ktore uzytkownik podaje zadana wartosc w menu opcji.
     * "xyAxisSensitivityInput"         - Pole tekstowe, w ktore uzytkownik podaje zadana wartosc w menu opcji.
     * 
     * "tppCamera"                      - Kamera z perspektywy trzeciej osoby.
     * "ship"                           - Statek, na ktory kamera ma byc skierowana.
     * 
     * "minimalDistance"                - Zapisana wartosc minimalnego oddalenia.
     * "maximumDistance"                - Zapisana wartosc maksymalnego oddalenia.
     * 
     * "minimalDeflection"              - Zapisana wartosc minimalnego wychylenia.
     * "maximumDeflection"              - Zapisana wartosc maksymalnego wychylenia.
     * 
     * "remotenessSensitivity"          - Zapisana wartosc czulosci oddalania.
     * "xyAxisSensitivity"              - Zapisana wartosc czulosci osi XY.
     * 
     * "zoom"                           - Zapisana wartosc zblizenia kamery.
     * 
     * "x"                              - Zapisana wartosc czulosci myszy po osi X.
     * "y"                              - Zapisana wartosc czulosci myszy po osi Y.
     * 
     * "temp"                           - Zmienna tymczasowa, sluzaca do optymalizacji.
    */

    #endregion

    #region OPIS METOD

    #region OPIS METODY "CHANGE_MINIMAL_DISTANCE(STRING VALUE)"

    public void ChangeMinimalDistance(string value)
    {
        try
        {
            temp = float.Parse(value);

            if (temp >= 0 && temp <= maximumDistance)
                minimalDistance = temp;

            else
                throw new Exception();
        }
        catch {minimalDistanceInput.text = minimalDistance.ToString();}
    }

    /*
     * "ChangeMinimalDistance(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci pola tekstowego w menu opcji.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     *  Jesli uzytkownik wprowadzi wartosc mniejsza od 0 lub wieksza od wartosci maksymalnej oddalenia, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "minimalDistance" zostanie wpisana w pole tekstowe.
     *  Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion

    #region OPIS METODY "CHANGE_MAXIMUM_DISTANCE(STRING VALUE)"

    public void ChangeMaximumDistance(string value)
    {
        try
        {
            temp = float.Parse(value);

            if (temp >= 0 && temp >= minimalDistance)
                maximumDistance = temp;

            else
                throw new Exception();
        }
        catch {maximumDistanceInput.text = maximumDistance.ToString();}
    }

    /*
     * "ChangeMaximumDistance(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci pola tekstowego w menu opcji.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     *  Jesli uzytkownik wprowadzi wartosc mniejsza od 0 lub mniejsza od wartosci minimalnej oddalenia, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "maximumDistance" zostanie wpisana w pole tekstowe.
     *  Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion

    #region OPIS METODY "CHANGE_MINIMAL_DEFLECTION(STRING VALUE)"

    public void ChangeMinimalDeflection(string value)
    {
        try
        {
            temp = float.Parse(value);

            if (temp >= 0 && temp <= maximumDeflection)
                minimalDeflection = temp;

            else
                throw new Exception();
        }
        catch {minimalDeflectionInput.text = minimalDeflection.ToString();}
    }

    /*
     * "ChangeMinimalDeflection(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci pola tekstowego w menu opcji.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     *  Jesli uzytkownik wprowadzi wartosc mniejsza od 0 lub wieksza od wartosci maksymalnego wychylenia, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "minimalDeflection" zostanie wpisana w pole tekstowe.
     *  Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion

    #region OPIS METODY "CHANGE_MAXIMUM_DEFLECTION(STRING VALUE)"

    public void ChangeMaximumDeflection(string value)
    {
        try
        {
            temp = float.Parse(value);

            if (temp >= 0 && temp >= minimalDeflection)
                maximumDeflection = temp;

            else
                throw new Exception();
        }
        catch {maximumDeflectionInput.text = maximumDeflection.ToString();}
    }

    /*
     * "ChangeMaximumDeflection(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci pola tekstowego w menu opcji.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     *  Jesli uzytkownik wprowadzi wartosc mniejsza od 0 lub mniejsza od wartosci minimalnego wychylenia, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "maximumDeflection" zostanie wpisana w pole tekstowe.
     *  Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion

    #region OPIS METODY "CHANGE_REMOTENESS_SENSITIVITY(STRING VALUE)"

    public void ChangeRemotenessSensitivity(string value)
    {
        try
        {
            temp = float.Parse(value);

            if (temp >= 0)
                remotenessSensitivity = temp;

            else
                throw new Exception();
        }
        catch {remotenessSensitivityInput.text = remotenessSensitivity.ToString();}
    }

    /*
     * "ChangeRemotenessSensitivity(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci pola tekstowego w menu opcji.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     *  Jesli uzytkownik wprowadzi wartosc mniejsza od 0, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "remotenessSensitivity" zostanie wpisana w pole tekstowe.
     *  Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion

    #region OPIS METODY "CHANGE_XY_AXIS_SENSITIVITY(STRING VALUE)"

    public void ChangeXyAxisSensitivity(string value)
    {
        try
        {
            temp = float.Parse(value);

            if (temp >= 0)
                xyAxisSensitivity = temp;

            else
                throw new Exception();
        }
        catch {xyAxisSensitivityInput.text = xyAxisSensitivity.ToString();}
    }

    /*
     * "ChangeXyAxisSensitivity(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci pola tekstowego w menu opcji.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     *  Jesli uzytkownik wprowadzi wartosc mniejsza od 0, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "xyAxisSensitivity" zostanie wpisana w pole tekstowe.
     *  Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion

    #region OPIS METODY "CONTROL_CAMERA()"

    void ControlCamera()
    {
        x += Input.GetAxis("Mouse X") * xyAxisSensitivity;
        y += Input.GetAxis("Mouse Y") * xyAxisSensitivity;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            zoom -= remotenessSensitivity;

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            zoom += remotenessSensitivity;

        y = Mathf.Clamp(y, minimalDeflection, maximumDeflection);
        zoom = Mathf.Clamp(zoom, minimalDistance, maximumDistance);
    }

    /*
     * W funkcji "ControlCamera()" przechwytywane sa ruchy myszy i naciskane przyciski (kolko myszy), a nastepnie uwzgledniane sa wczesniej ustawione wartosci szybkosci odpowiednich operacji takich jak powiekszanie, czy ruchy myszy po osi X i Y.
     * Funkcja "Mathf.Clamp" przyjmuje trzy argumenty. Pierwszy argument to wartosc, ktora ma byc sprawdzana z drugim i trzecim argumentem. Funkcja ma ta na celu zapobiegniecie wykroczenia wartosci argumentu poza zakres zdefiniowany przez drugi i trzeci argument.
     * Wartosc minimalna jaka wartosc moze przyjac wynosi wartosc argumentu drugiego, natomiast wartosc maksymalna jaka wartosc moze przyjac wynosi wartosc argumentu trzeciego.
    */

    #endregion

    #region OPIS METODY "START()"

    void Start() {tppCamera = transform;}

    #endregion
    
    #region OPIS METODY "UPDATE()"
    
    void Update()
    {
        if (!AltCameraControl)
            ControlCamera();

        else if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            ControlCamera();
    }

    /*
     * Sprawdzane zostaje, czy uzytkownik wybral sterowanie kamera klawiszem "ALT". Jesli wylaczona jest funkcja sterowania kamera klawiszem "ALT" w menu opcji, to uzytkownik moze swobodnie sterowac kamera.
     * Jesli jednak wlaczona zostala funkcja sterowania kamera klawiszem "ALT", to uzytkownik bedzie mogl poruszac kamera dopiero po przetrzymaniu lewego lub prawego klawisza "ALT".
    */

    #endregion
    
    #region OPIS METODY "LATE_UPDATE()"
    
    void LateUpdate()
    {
        vector = new Vector3(0, 0, -zoom);
        rotation = Quaternion.Euler(y, x, 0);
        tppCamera.position = ship.position + rotation * vector;

        tppCamera.LookAt(ship.position);
    }

    #endregion

    #endregion
}