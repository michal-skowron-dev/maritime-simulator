using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa "Resolution" zawiera metody sluzace do zmiany takich ustawien jak szerokosc i wysokosc okna, ustawienie trybu pelnoekranowego lub ustawienie zadanej czestotliwosci odswiezania.
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    public InputField widthInput, heightInput, fullScreenModeInput, refreshRateInput;

    ushort width = (ushort)Screen.width, height = (ushort)Screen.height, refreshRate = 60;
    bool fullScreenMode;

    /*
     * Opis poszczegolnych zmiennych:
     * 
     * "widthInput"             - Jest to pole tekstowe w menu opcji odpowiedzialne za zmiane wartosci szerokosci okna aplikacji.
     * "heightInput"            - Jest to pole tekstowe w menu opcji odpowiedzialne za zmiane wartosci wysokosci okna aplikacji.
     * "fullScreenModeInput"    - Jest to pole tekstowe w menu opcji odpowiedzialne za przelaczanie aplikacji w tryb pelnoekranowy badz okienkowy.
     * "refreshRateInput"       - Jest to pole tekstowe w menu opcji odpowiedzialne za zmiane wartosci czestotliwosci odswiezania.
     * 
     * "width"                  - Jest to zmienna przechowywujaca ustawiona szerokosc okna aplikacji.
     * "height"                 - Jest to zmienna przechowywujaca ustawiona wysokosc okna aplikacji.
     * "refreshRate"            - Jest to zmienna przechowywujaca ustawiona wartosc czestotliwosci odswiezania.
     * 
     * "fullScreenMode"         - Jest to zmienna przechowywujaca stan uruchomionej aplikacji (tryb pelnoekranowy badz okienkowy).
    */

    #endregion

    #region OPIS METOD
    
    #region OPIS METODY "CHANGE_WIDTH(STRING VALUE)"
    
    public void ChangeWidth(string value)
    {
        try{width = ushort.Parse(value);} catch{widthInput.text = width.ToString();}
    }

    /*
     * "ChangeWidth(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zakonczenia edytowania wartosci w polu tekstowym przez uzytkownika.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     *  Jesli uzytkownik wprowadzi wartosc mniejsza od 0, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "width" zostanie wpisana w pole tekstowe.
     *  Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion
    
    #region OPIS METODY "CHANGE_HEIGHT(STRING VALUE)"

    public void ChangeHeight(string value)
    {
        try{height = ushort.Parse(value);} catch{heightInput.text = height.ToString();}
    }

    /*
     * "ChangeHeight(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zakonczenia edytowania wartosci w polu tekstowym przez uzytkownika.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     *  Jesli uzytkownik wprowadzi wartosc mniejsza od 0, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "height" zostanie wpisana w pole tekstowe.
     *  Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion
    
    #region OPIS METODY "CHANGE_SCREEN_MODE(STRING VALUE)"
    
    public void ChangeScreenMode(string value)
    {
        try
        {
            if (value.Equals("1") || value.Equals("true") || value.Equals("yes") || value.Equals("tak"))
                fullScreenMode = true;

            else if (value.Equals("0") || value.Equals("false") || value.Equals("no") || value.Equals("nie"))
                fullScreenMode = false;

            else
            {
                if (fullScreenMode)
                    fullScreenModeInput.text = "1";

                else
                    fullScreenModeInput.text = "0";
            }
        }
        catch { }
    }

    /*
     * "ChangeScreenMode(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zakonczenia edytowania wartosci w polu tekstowym przez uzytkownika.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu porownanie wartosci wpisanej przez uzytkownika w polu tekstowym z odpowiednimi frazami.
     *  Funkcja ma ta na celu przyjmowanie scisle zdefiniowanych ciagow znakow w celu zmiany wartosci zmiennej "fullScreenMode".
     *  Jesli uzytkownik wpisze w polu tekstowym wartosci "1", "true", "yes" lub "tak", to pole tekstowe zaakceptuje te frazy i zmienna "fullScreenMode" zmieni swoja wartosc na "true".
     *  Jesli uzytkownik wpisze w polu tekstowym wartosci "0", "false", "no" lub "nie", to pole tekstowe zaakceptuje te frazy i zmienna "fullScreenMode" zmieni swoja wartosc na "false".
     *  Jesli uzytkownik wpisze w polu tekstowym jakakolwiek inna wartosc, to na podstawie obecnie ustawionej wartosci zmiennej "fullScreenMode" w pole tekstowe zostanie wpisane slowo "tak" badz "nie".
    */

    #endregion
    
    #region OPIS METODY "CHANGE_REFRESH_RATE(STRING VALUE)"
    
    public void ChangeRefreshRate(string value)
    {
        try{refreshRate = ushort.Parse(value);} catch{refreshRateInput.text = refreshRate.ToString();}
    }

    /*
     * "ChangeRefreshRate(string value)" jest funkcja, ktora jest wywolywana po zdarzeniu zakonczenia edytowania wartosci w polu tekstowym przez uzytkownika.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu przekonwertowanie wartosci wpisanej przez uzytkownika w polu tekstowym na liczbe.
     *  Jesli uzytkownik wprowadzi wartosc mniejsza od 0, to wystapi wyjatek, ktory zostanie obsluzony w taki sposob, ze ostatnia prawidlowa wartosc zmiennej "refreshRate" zostanie wpisana w pole tekstowe.
     *  Wyjatek wystapi rowniez, jesli uzytkownik poda liczbe wychodzaca poza zakres zmiennej typu "ushort", badz gdy wpisana wartosc w polu tekstowym bedzie nieprawidlowym ciagiem znakow.
    */

    #endregion
    
    #region OPIS METODY "APPLY_SETTINGS()"
    
    public void ApplySettings()
    {
        try{Screen.SetResolution(width, height, fullScreenMode, refreshRate);} catch{}
    }

    /*
     * "ApplySettings()" jest funkcja, ktora jest wywolywana po zdarzeniu klikniecia kursorem myszy w przycisk zatwierdzajacy zmiany wprowadzone przez uzytkownika.
     *  Wewnatrz funkcji wykonywany jest blok try catch, majacy na celu zatwierdzenie wprowadzonych przez uzytkownika zmian.
    */

    #endregion

    #endregion
}