using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using DLL_Time;

#region OPIS BIBLIOTEKI "DLL_TIME"

/*
 * "DLL_Time" - Samodzielnie napisana biblioteka zawierajaca 2 metody "TimeCalc.Hours(float sliderValue)" oraz "TimeCalc.Minutes(float sliderValue)".
 *  Metody te sluza do szybkiego zamieniania wartosci slidera wedlug wczesniej przyjetej skali odpowiednio na same godziny lub na same minuty w zaleznosci od uzytej metody.
 *  Projekt biblioteki znajduje sie w folderze "Resources".
*/

#endregion

public class Pause : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa "Pause" sluzy do zarzadznia takimi opcjami jak poziom glosnosci efektow dzwiekowych, czy zmiany pory dnia, w ktorej przeprowadzana jest symulacja.
     * Znajduja sie tutaj rowniez wszelakie metody, ktore zostaja wywolywane po nacisnieciu w odpowiednie przyciski, zmiane wartosci listy, czy tez stanow checkBoxow w menu.
     * Zarejestrowane zmiany stanow checkBoxow sa z tej klasy przekazywane do innych klas scisle odpowiedzialnych za dane obiekty.
    */

    #endregion

    #region OPIS ZMIENNYCH

    public GameObject simulator, pause;
    public Text volumeLabel, timeLabel, startButtonLabel;
    public AudioSource seaSound, bellSound;
    public Slider timeSlider;

    public delegate void BoolDelegate(bool value);
    public static event BoolDelegate LanguageSwitchEvent;

    public static event Action TerrainLightingSwitchEvent, ShipLightingSwitchEvent;

    public bool scenarioChange, shipChange;

    const ushort Range = 0, SunPositionCorrection = 268;

    ushort onlyHours, onlyMinutes, minutes;
    string clock, temp;

    /*
     * Opis poszczegolnych zmiennych:
     * 
     * "simulator"                      - Jest to obiekt zawierajacy wszystkie komponenty, ktore umozliwiaja przeprowadzenie symulacji. Wszystkie zwiazane dzieci z tym obiektem (takie jak teren, statek, czy sterownia) sa wlaczane po wybraniu opcji startu z menu glownego.
     * "pause"                          - Jest to obiekt, ktory wyswietla menu glowne wraz ze wszystkimi opcjami dotyczacymi symulatora, ktore uzytkownik moze zmienic. Dzieci tego obiektu to przede wszystkim opcje, podopcje, checkboxy, slidery, itd.
     * 
     * "volumeLabel"                    - Etykieta wyswietlana w menu opcji. Wskazuje aktualnie ustawiona wartosc poziomu dzwieku.
     * "timeLabel"                      - Etykieta wyswietlana w menu opcji. Wskazuje aktualnie ustawiona wartosc (czasu) pory dnia.
     * "startButtonLabel"               - Jest to napis, ktory jest wyswietlany na przycisku menu glownego sluzacego do uruchomienia symulatora. Jesli uzytkownik wlaczy ponownie menu glowne, to zamiast napisu "START" ukaze sie napis "KONTYNUUJ" .
     * 
     * "seaSound"                       - Zapetlone dzwieki morza.
     * "bellSound"                      - Dzwiek dzwonka, ktory jest odtwarzany, gdy statek jest bliski kolizji.
     * 
     * "timeSlider"                     - Aby moc manipulowac wartosciami slidera czasu z poziomu skryptu, nalezy dodac zmienna o typie Slider i dokonac referencji z zadanym sliderem.
     * 
     * "LanguageSwitchEvent"            - Przy pomocy delegatu bedzie przekazywany parametr stanu ustawionego checkboxa, okreslajacego aktualnie ustawiony jezyk aplikacji do skryptu "Language", w ktorym jest zasubskrybowane to zdarzenie.
     * 
     * "TerrainLightingSwitchEvent"     - Przy pomocy "Action" bedzie wywolywane zdarzenie w skrypcie "Lighting". Jako ze uzyto "Action", zdarzenie nie bedzie przekazywalo zadnych parametrow.
     * "ShipLightingSwitchEvent"        - Przy pomocy "Action" bedzie wywolywane zdarzenie w skrypcie "Lighting". Jako ze uzyto "Action", zdarzenie nie bedzie przekazywalo zadnych parametrow.
     * 
     * "scenarioChange"                 - Zmienna sluzaca do zapisywania aktualnie ustawionej wartosci z listy rozwijanej w menu opcji, odpowiedzialnej za wybor scenariusza.
     * "shipChange"                     - Zmienna sluzaca do zapisywania aktualnie ustawionej wartosci z listy rozwijanej w menu opcji, odpowiedzialnej za wybor modelu statku.
     * 
     * "Range"                          - Stala modyfikujaca zakres przesuwanych wartosci na sliderze czasu. Przy wartosci domyslnej rownej 0 poczatkiem zakresu slidera czasu jest godzina 0:00.
     * "SunPositionCorrection"          - Aby ustawic slonce w zadanym punkcie na swiecie niezbedne bylo zastosowanie korekcji pozycji na osi x. Przy godzinie 6:00 slonce jest ustawione w taki sposob, aby idealnie zasymulowac wschod slonca.
     * 
     * "onlyHours"                      - Zmienna przechowywujaca pelne godziny (bez uwzgledniania minut) przekonwertowane z wartosci ze slidera.
     * "onlyMinutes"                    - Zmienna przechowywujaca same minuty (bez wliczania pelnych godzin) przekonwertowane z wartosci ze slidera.
     * "minutes"                        - Zmienna przechowywujaca aktualny czas zamieniony na minuty.
     * 
     * "clock"                          - Zmienna przechowywujaca aktualny czas systemowy pod postacia zmiennej string.
     * "temp"                           - Zmienna tymczasowa, ktora ma na celu aktualizowanie wartosci slidera czasu i etykiety informujacej o aktualnym czasie co minute. Zmienna jest wykorzystywana w instrukcji warunkowej do porownania zmian w czasie.
    */

    #endregion

    #region OPIS METOD

    #region OPIS METODY "CHANGE_VOLUME_LEVEL(FLOAT VALUE)"

    public void ChangeVolumeLevel(float value)
    {
        seaSound.volume = (float)(value * 0.01);
        bellSound.volume = (float)(value * 0.01);
        volumeLabel.text = string.Format("{0}%", value);
    }

    /*
     * "ChangeVolumeLevel(float value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci slidera, odpowiedzialnego za poziom glosnosci dzwieku symulatora.
     *  Wewnatrz funkcji zmieniany jest poziom glosnosci dzwieku morza, jak i dzwieku dzwonka w zaleznosci od ustawionej na sliderze wartosci. Poziom glosnosci efektow dzwiekowych moze przyjmowac wartosci od 0.00 do 1.00 (od 0% do 100%).
     *  Po zmianie wartosci slidera aktualizowany jest napis informujacy o ustawionym poziomie dzwieku, wyrazony w procentach.
    */

    #endregion
    
    #region OPIS METODY "CHANGE_TIME(FLOAT VALUE)"
    
    public void ChangeTime(float value)
    {
        onlyHours = TimeCalc.Hours(value);
        onlyMinutes = TimeCalc.Minutes(value);

        if (onlyHours <= 23 - Range)
            onlyHours += Range;

        else
        {
            onlyHours = (ushort)(onlyHours - 24 + Range);

            if (onlyHours >= 24)
                onlyHours -= 24;
        }

        transform.rotation = Quaternion.Euler(value + SunPositionCorrection, transform.rotation.y, transform.rotation.z);
        timeLabel.text = string.Format("{0:00}:{1:00}", onlyHours, onlyMinutes);
    }

    /*
     * "ChangeTime(float value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci slidera, odpowiedzialnego za aktualnie ustawiona pore dnia (czas).
     *  Wewnatrz funkcji zamieniane sa wartosci ze slidera osobno na godziny i minuty wedlug wczesniej przyjetej skali przy pomocy dolaczonych metod z biblioteki "DLL_Time".
     *  Instrukcja warunkowa zawarta wewnatrz funkcji ma na celu zapobiegnieciu wykroczenia godziny poza prawidlowy zakres. Nie moze dojsc do sytuacji, w ktorej ustawiony czas na etykiecie bedzie mial wartosc godzinowa na przyklad "24:32" (zamiast prawidlowego formatu "00:32").
     *  Nastepne polecenia sluza do zmiany pozycji slonca na niebie przy pomocy funkcji "Quaternion.Euler" oraz wyswietlenie wartosci aktualnie ustawionego przez uzytkownika czasu na etykiecie.
    */

    #endregion
    
    #region OPIS METODY "SELECT_TIME_MODE(INT INDEX)"

    public void SelectTimeMode(int index)
    {
        if (index == 0)
            timeSlider.enabled = true;

        else
        {
            timeSlider.enabled = false;
            temp = null;
        }
    }

    /*
     * "SelectTimeMode(int index)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany pozycji na liscie rozwijanej.
     *  Jesli zostanie wybrana pierwsza opcja na liscie, to slider czasu zostanie wlaczony. Uzytkownik bedzie mogl samodzielnie wybrac odpowiednia pore dnia (czas). Wartosc pory dnia nie bedzie w takim przypadku automatycznie aktualizowana.
     *  Jesli zostanie wybrana druga opcja na liscie, to slider czasu zostanie wylaczony, co oznaczac bedzie, ze symulator bedzie stale aktualizowal czas wedlug czasu sytemowego. Ustawienie wartosci "null" dla zmiennej "temp" ma na celu natychmiastowej zmiany godziny na czas systemowy.
    */

    #endregion

    #region OPIS METODY "UPDATE()"

    void Update()
    {
        if (!timeSlider.enabled)
        {
            clock = DateTime.Now.ToString("HH:mm");

            if (clock != temp)
            {
                if (Convert.ToUInt16(clock.Substring(3, 2)) % 4 == 0 || temp == null)
                {
                    temp = clock;

                    minutes = (ushort)((DateTime.Now.Hour * 60 + DateTime.Now.Minute) * 0.25);

                    timeSlider.value = minutes;
                    transform.rotation = Quaternion.Euler(minutes + SunPositionCorrection, transform.rotation.y, transform.rotation.z);
                }

                timeLabel.text = clock.ToString();
            }
        }
    }

    /*
     * W funkcji "Update()" sprawdzane jest, czy slider czasu jest wylaczony. Jesli slider jest wylaczony, to pobierana bedzie wartosc czasu systemowego i zapisywana do zmiennej "clock".
     * W kolejnej instrukcji warunkowej sprawdzane jest, czy czas systemowy sie zmienil. Jesli nastapila zmiana w czasie, to wykorzystywana jest funkcja "Substring", ktora wyciaga ze zmiennej clock dane dotyczace minut.
     * Pozyskane minuty sa konwertowane z typu string na typ ushort, a nastepnie dzielone przez modulo 4. Powodem tej operacji jest ograniczenie wykonywania sie zbyt czestego zmiany wartosci.
     * Insrukcja warunkowa przyjmie wartosc "true" jedynie wtedy, gdy wyciagniete minuty beda mialy odpowiednio wartosci 04, 08, 12, 16, 20, 24, itd. lub gdy uzytkownik przestawi opcje z listy rozwijanej na czas systemowy.
     * Natepnie wartosc zmiennej "clock" jest przepisywana do zmiennej "temp", zamieniana jest aktualna godzina na minuty, ktore sa od razu przeskalowane przez podzielenie tej wartosci przez 4 (mnozenie przez 0.25 jest operacja szybsza), aby bylo mozliwe ustawienie zadanej wartosci na sliderze czasu.
     * Kolejnym krokiem jest przesuniecie pozycji slonca na niebie przy pomocy funkcji "Quaternion.Euler" oraz wyswietlenie wartosci aktualnego czasu na etykiecie.
    */

    #endregion

    #region OPIS METODY "START_SIMULATOR()"

    public void StartSimulator()
    {
        Time.timeScale = 1;

        pause.SetActive(false);
        simulator.SetActive(true);

        if (!Language.Paused)
        {
            Language.Paused = true;

            if (Language.Lang)
                startButtonLabel.text = "CONTINUE";

            else
                startButtonLabel.text = "KONTYNUUJ";
        }
    }

    /*
     * Bezargumentowa funkcja "StartSimulator()" jest funkcja, ktora jest wywolywana po zdarzeniu nacisniecia przycisku startu symulatora w menu glownym.
     * 
     * "Time.timeScale = 1"                     - Sluzy do wznowienia symulacji poprzez wznowienie czasu gry. Statek jak i woda zaczynaja sie ponownie poruszac.
     * 
     * "pause.SetActive(false);"                - Menu glowne zostaje wylaczona przez zdezaktywowanie obiektu. Dzieci obiektu sa rowniez wylaczane, jesli byly wczesniej wlaczone.
     * "simulator.SetActive(true)"              - Wlaczony zostaje symulator przez uaktywnienie obiektu. Dzieci obiektu sa rowniez wlaczane, jesli przed zdezaktywowaniem rodzica byly wlaczone.
     * 
     * "startButtonLabel.text = "KONTYNUUJ""    - Po pierwszym uruchomieniu symulacji, przycisk w menu glownym zmienia swoj napis z napisu "START" na napis "KONTYNUUJ" w zaleznosci od ustawionego jezyka aplikacji.
    */

    #endregion

    #region OPIS METODY "RESTART_SIMULATOR()"

    public void RestartSimulator() {SceneManager.LoadScene(0);}

    /*
     * Bezargumentowa funkcja "RestartSimulator()" jest funkcja, ktora jest wywolywana po zdarzeniu nacisniecia przycisku restartu symulatora w menu glownym.
     * Cala scena jest ladowana ponownie.
    */

    #endregion

    #region OPIS METODY "QUIT()"

    public void Quit() {Application.Quit();}

    /*
     * Bezargumentowa funkcja "Quit()" jest funkcja, ktora jest wywolywana po zdarzeniu nacisniecia przycisku wyjscia z symulatora w menu glownym.
     * Aplikacja jest zymykana.
    */

    #endregion

    #region OPIS METODY "BOX_MULLER_SWITCH(BOOL VALUE)"

    public void BoxMullerSwitch(bool value) {Alarm_Manager.BoxMuller = value;}

    /*
     * "BoxMullerSwitch(bool value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany stanu checkBoxa w menu opcji.
     *  Stan checkBoxa jest przekazywany do odpowiedniego pola w klasie "Alarm_Manager" poprzez wlasciwosc.
     *  Stan pola jest odpowiedzalny za uwzglednienie bledu pomiarowego metoda transformacji Boxa Mullera.
    */

    #endregion

    #region OPIS METODY "ALT_CAMERA_CONTROL(BOOL VALUE)"

    public void AltCameraControl(bool value) {TPP_Camera.AltCameraControl = value;}

    /*
     * "AltCameraControl(bool value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany stanu checkBoxa w menu opcji.
     *  Stan checkBoxa jest przekazywany do odpowiedniego pola w klasie "TPP_Camera" poprzez wlasciwosc.
     *  Stan pola jest odpowiedzalny za sterowanie kamery trzecioosobowej przez uzytkownika przy dodatkowym przetrzymaniu klawisza "ALT".
    */

    #endregion
    
    #region OPIS METODY "LANGUAGE_SWITCH(BOOL VALUE)"
    
    public void LanguageSwitch(bool value)
    {
        if (LanguageSwitchEvent != null)
            LanguageSwitchEvent(value);
    }

    /*
     * "LanguageSwitch(bool value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany stanu checkBoxa w menu opcji.
     *  Stan checkBoxa jest przekazywany do skryptu "Language" przy pomocy "delegatu".
     *  Przekazywana wartosc jest odpowiedzalny za tlumaczenie aplikacji na jezyk angielski.
    */

    #endregion

    #region OPIS METODY "TERRAIN_LIGHTING_SWITCH()"

    public void TerrainLightingSwitch()
    {
        if (TerrainLightingSwitchEvent != null)
            TerrainLightingSwitchEvent();
    }

    /*
     * "TerrainLightingSwitch()" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany stanu checkBoxa w menu opcji.
     *  Stan checkBoxa jest przekazywany do skryptu "Lighting" przy pomocy "action".
     *  Stan pola jest odpowiedzalny za wlaczenie statycznych (Baked) swiatel kierunkowych terenu.
    */

    #endregion

    #region OPIS METODY "SHIP_LIGHTING_SWITCH()"

    public void ShipLightingSwitch()
    {
        if (ShipLightingSwitchEvent != null)
            ShipLightingSwitchEvent();
    }

    /*
     * "ShipLightingSwitch()" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany stanu checkBoxa w menu opcji.
     *  Stan checkBoxa jest przekazywany do skryptu "Lighting" przy pomocy "action".
     *  Stan pola jest odpowiedzalny za wlaczenie dynamicznych (Realtime) swiatel punktowych statku.
    */

    #endregion

    #region OPIS METODY "SELECT_SCENARIO()"

    public void SelectScenario()
    {
        scenarioChange = !scenarioChange;
    }

    /*
     * Funkcja "SelectScenario" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci listy rozwijanej odpowiedzialenej za wybor scenariusza w menu opcji.
    */

    #endregion

    #region OPIS METODY "SELECT_SHIP()"

    public void SelectShip()
    {
        shipChange = !shipChange;
    }

    /*
     * Funkcja "SelectShip" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci listy rozwijanej odpowiedzialenej za wybor statku w menu opcji.
    */

    #endregion

    #region OPIS METODY "CHANGE_SCENE()"

    public void ChangeScene()
    {
        if (!scenarioChange && !shipChange)
            SceneManager.LoadScene(0);

        else if (!scenarioChange && shipChange)
            SceneManager.LoadScene(1);

        else if (scenarioChange && !shipChange)
            SceneManager.LoadScene(2);

        else
            SceneManager.LoadScene(3);
    }

    /*
     * Bezargumentowa funkcja "ChangeScene()" jest funkcja, ktora jest wywolywana po zdarzeniu nacisniecia przycisku zmiany sceny w menu opcji symulatora.
     * W zaleznosci od wybranego scenariusza i sceny ladowany jest odpowiedni przypadek symulacji.
    */

    #endregion

    #endregion
}