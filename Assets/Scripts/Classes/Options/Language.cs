using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa "Language" sluzy do ustawiania wybranego przez uzytkownika jezyka w menu opcji dla wszystkich elementow, z ktorymi uzytkownik moze miec interakcje.
     * Lista rozwijana z efetkami post processingu nie zostala przetlumaczona ze wzgledu na trudne znalezienie odpowiednich okreslen dla poszczegolnych efektow.
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    public static bool Lang {get; set;}
    public static bool Paused {get; set;}

    public Text mainMenuTitleLabel, startButtonLabel, restartButtonLabel, optionsButtonLabel, informationButtonLabel, quitButtonLabel,
                informationTitleLabel, descriptionTitleLabel, descriptionLabel, informationBackButtonLabel,
                optionsTitleLabel, volumeDescriptionLabel, timeDescriptionLabel, alarmsButtonLabel, cameraButtonLabel, resolutionButtonLabel, othersButtonLabel, optionsBackButtonLabel,
                alarmsTitleLabel, alarmsDescriptionLabel, boxMullerSwitchLabel, frontAlarmPlaceholder, leftAlarmPlaceholder, rightAlarmPlaceholder, rearAlarmPlaceholder, echosounderPlaceholder, alarmsBackButtonLabel,
                cameraTitleLabel, cameraDescriptionLabel, minimalDistanceLabel, maximumDistanceLabel, minimalDeflectionLabel, maximumDeflectionLabel, remotenessSensitivityLabel, xyAxisSensitivityLabel, minimalDistancePlaceholder, maximumDistancePlaceholder, minimalDeflectionPlaceholder, maximumDeflectionPlaceholder, remotenessSensitivityPlaceholder, xyAxisSensitivityPlaceholder, cameraBackButtonLabel,
                resolutionTitleLabel, widthLabel, heightLabel, screenModeLabel, refreshRateLabel, widthPlaceholder, heightPlaceholder, screenModePlaceholder, refreshRatePlaceholder, applySettingsButtonLabel, resolutionBackButtonLabel,
                othersTitleLabel, languageSwitchLabel, terrainLightingSwitchLabel, shipLightingSwitchLabel, changeSceneButtonLabel, othersBackButtonLabel,

                warningMessage, collisionMessage, breakdown, reparation;

    public Dropdown timeDropdown, effectsDropdown, scenarioDropdown, shipDropdown;

    const string EnglishBack = "BACK", EnglishPlaceholder = "Enter value..", PolishBack = "WSTECZ", PolishPlaceholder = "Wprowadź wartość..";
    readonly string[] EnglishTimeOptions = new string[2] {"Custsom Time", "System Time" }, PolishTimeOptions = new string[2] {"Czas Użytkownika", "Czas Systemowy"},
                      EnglishScenarioOptions = new string[2] {"Scenario 1", "Scenario 2" }, PolishScenarioOptions = new string[2] {"Scenariusz 1", "Scenariusz 2"},
                      EnglishShipOptions = new string[2] {"Default Ship", "My Ship" }, PolishShipOptions = new string[2] {"Statek Domyślny", "Własny Statek"};

    /*
     * Wlasciwosc "Lang" sluzy do przechowywania wartosci aktualnie ustawionego jezyka, natomiast wlasciwosc "Paused" ma za zadanie zapisac moment, w ktorym pierwszy raz uzytkownik uruchomi symulator.
     * Jest konieczne zapisanie tej wartosci w pamieci z powodu pozniejszej mozliwosci zmiany napisu na przycisku z napisem "START" na "KONTYNUUJ" przy tlumaczeniu aplikacji.
     * Bez zapamietanej wartosci po zapauzowaniu symulatora, zmiana napisu przy tlumaczeniu zachodzila by nieprawidlowo.
     * Dalej znajduja sie wszystkie odniesienia do kontrolek, w ktorych jest wyswietlany tekst uzytkownikowi.
     * Na koniec znajduja sie stale, ktore dosc czesto powtarzaja sie dla niektorych kontrolek oraz tablica stringow, ktora jest wykorzystywana do zapelniania list rozwijanych przy pomocy funkcji.
    */

    #endregion
    
    #region OPIS METOD
    
    #region OPIS METODY "START()"
    
    void Start() {Pause.LanguageSwitchEvent += PauseLanguageSwitchEvent;}

    /*
     * W funkcji "Start" subskrypowane jest zdarzenie ze skryptu "Pause". Jako ze, wykorzystano "delegat" w skrypcie "Pause" przekazujacy wartosc typu "bool", to zdarzenie "PauseLanguageSwitchEvent" bedzie przenosilo wartosc boolowska, oznaczajaca wybor danego jezyka.
    */

    #endregion

    #region OPIS METODY "ON_DESTROY()"

    void OnDestroy() {Pause.LanguageSwitchEvent -= PauseLanguageSwitchEvent;}

    /*
     * Po zresetowaniu symulatora przyciskiem w menu glownym nalezy odsubskrybowac zdarzenia. W innym wypadku checkbox odpowiedzialny za zmiane jezyka przestanie dzialac po restarcie symulatora ze wzgledu na brak referencji.
    */

    #endregion

    #region OPIS METODY "PAUSE_LANGUAGE_SWITCH_EVENT(BOOL VALUE)"

    void PauseLanguageSwitchEvent(bool value)
    {
        Lang = value;

        if (value)
        {
            mainMenuTitleLabel.text = "Maritime Simulator";

            if (!Paused)
                startButtonLabel.text = "START";
            else
                startButtonLabel.text = "CONTINUE";

            restartButtonLabel.text = "RESTART";
            optionsButtonLabel.text = "OPTIONS";
            informationButtonLabel.text = "INFORMATION";
            quitButtonLabel.text = "QUIT";

            informationTitleLabel.text = "Information";
            descriptionTitleLabel.text = "Links to sources and tutorials:";
            descriptionLabel.text = string.Format("{0}{7}{1}{7}{2}{7}{3}{7}{4}{7}{5}{7}{6}", "Picture:    https://forum.unity.com/attachments/upload_2015-10-9_6-57-57-png.157470", "Sound:     https://www.youtube.com/watch?v=Xfks6jiS_iI", "Physics:   https://www.youtube.com/watch?v=FtcdkfvrQv0", "Camera:   https://www.youtube.com/watch?v=Ta7v27yySKs", "Ship:         https://assetstore.unity.com/packages/3d/vehicles/sea/brig-sloop-sailing-ship-77862", "Terrain:    Unity Standard Assets", "Support:   Bartosz Muczyński and Mateusz Bilewski                Michał Skowron - IT Technology 3 year", "\n");
            informationBackButtonLabel.text = EnglishBack;

            optionsTitleLabel.text = "Options";
            volumeDescriptionLabel.text = "Sound";
            timeDescriptionLabel.text = "Time";
            alarmsButtonLabel.text = "Alarms";
            cameraButtonLabel.text = "Camera";
            resolutionButtonLabel.text = "Resolution";
            othersButtonLabel.text = "Others";
            optionsBackButtonLabel.text = EnglishBack;
            CreateNewDropdown(timeDropdown, EnglishTimeOptions);

            alarmsTitleLabel.text = "Alarms";
            alarmsDescriptionLabel.text = "Display warnings below a given value.";
            boxMullerSwitchLabel.text = "Include Box–Muller transform.";
            frontAlarmPlaceholder.text = EnglishPlaceholder;
            leftAlarmPlaceholder.text = EnglishPlaceholder;
            rightAlarmPlaceholder.text = EnglishPlaceholder;
            rearAlarmPlaceholder.text = EnglishPlaceholder;
            echosounderPlaceholder.text = EnglishPlaceholder;
            alarmsBackButtonLabel.text = EnglishBack;

            cameraTitleLabel.text = "Camera";
            cameraDescriptionLabel.text = "Moving camera while holding ALT key.";
            minimalDistanceLabel.text = "Minimal distance";
            maximumDistanceLabel.text = "Maximum distance";
            minimalDeflectionLabel.text = "Minimal deflection";
            maximumDeflectionLabel.text = "Maximum deflection";
            remotenessSensitivityLabel.text = "Remoteness sensitivity";
            xyAxisSensitivityLabel.text = "XY axis sensitivity";
            minimalDistancePlaceholder.text = EnglishPlaceholder;
            maximumDistancePlaceholder.text = EnglishPlaceholder;
            minimalDeflectionPlaceholder.text = EnglishPlaceholder;
            maximumDeflectionPlaceholder.text = EnglishPlaceholder;
            remotenessSensitivityPlaceholder.text = EnglishPlaceholder;
            xyAxisSensitivityPlaceholder.text = EnglishPlaceholder;
            cameraBackButtonLabel.text = EnglishBack;

            resolutionTitleLabel.text = "Resolution";
            widthLabel.text = "Width";
            heightLabel.text = "Height";
            screenModeLabel.text = "Fullscreen mode (1 / 0)";
            refreshRateLabel.text = "Refresh rate";
            widthPlaceholder.text = EnglishPlaceholder;
            heightPlaceholder.text = EnglishPlaceholder;
            screenModePlaceholder.text = EnglishPlaceholder;
            refreshRatePlaceholder.text = EnglishPlaceholder;
            applySettingsButtonLabel.text = "APPLY SETTINGS";
            resolutionBackButtonLabel.text = EnglishBack;

            othersTitleLabel.text = "Others";
            languageSwitchLabel.text = "English translation.";
            terrainLightingSwitchLabel.text = "Turn on terrain lighting.";
            shipLightingSwitchLabel.text = "Turn on ship lighting.";
            changeSceneButtonLabel.text = "CHANGE SCENE";
            othersBackButtonLabel.text = EnglishBack;
            CreateNewDropdown(scenarioDropdown, EnglishScenarioOptions);
            CreateNewDropdown(shipDropdown, EnglishShipOptions);



            warningMessage.text = "HIGH RISK OF COLLISION";
            collisionMessage.text = "SHIP HAS HIT AN OBJECT";
            breakdown.text = "Stop Ship Movement";
            reparation.text = "R";
        }

        else
        {
            mainMenuTitleLabel.text = "Symulator Morski";

            if (!Paused)
                startButtonLabel.text = "ROZPOCZNIJ";
            else
                startButtonLabel.text = "KONTYNUUJ";

            restartButtonLabel.text = "RESETUJ";
            optionsButtonLabel.text = "OPCJE";
            informationButtonLabel.text = "INFORMACJE";
            quitButtonLabel.text = "WYJŚCIE";

            informationTitleLabel.text = "Informacje";
            descriptionTitleLabel.text = "Linki do źródeł i tutoriali:";
            descriptionLabel.text = string.Format("{0}{7}{1}{7}{2}{7}{3}{7}{4}{7}{5}{7}{6}", "Obraz:      https://forum.unity.com/attachments/upload_2015-10-9_6-57-57-png.157470", "Dźwięk:    https://www.youtube.com/watch?v=Xfks6jiS_iI", "Fizyka:      https://www.youtube.com/watch?v=FtcdkfvrQv0", "Kamera:   https://www.youtube.com/watch?v=Ta7v27yySKs", "Statek:      https://assetstore.unity.com/packages/3d/vehicles/sea/brig-sloop-sailing-ship-77862", "Teren:       Unity Standard Assets", "Pomoc:     Bartosz Muczyński i Mateusz Bilewski                Michał Skowron - Informatyka 3 rok", "\n");
            informationBackButtonLabel.text = PolishBack;

            optionsTitleLabel.text = "Opcje";
            volumeDescriptionLabel.text = "Dźwięk";
            timeDescriptionLabel.text = "Godzina";
            alarmsButtonLabel.text = "Alarmy";
            cameraButtonLabel.text = "Kamera";
            resolutionButtonLabel.text = "Rozdzielczość";
            othersButtonLabel.text = "Inne";
            optionsBackButtonLabel.text = PolishBack;
            CreateNewDropdown(timeDropdown, PolishTimeOptions);

            alarmsTitleLabel.text = "Alarmy";
            alarmsDescriptionLabel.text = "Wyświetlaj ostrzeżenia poniżej danej wartości.";
            boxMullerSwitchLabel.text = "Uwzględnij transformację Boxa-Mullera.";
            frontAlarmPlaceholder.text = PolishPlaceholder;
            leftAlarmPlaceholder.text = PolishPlaceholder;
            rightAlarmPlaceholder.text = PolishPlaceholder;
            rearAlarmPlaceholder.text = PolishPlaceholder;
            echosounderPlaceholder.text = PolishPlaceholder;
            alarmsBackButtonLabel.text = PolishBack;

            cameraTitleLabel.text = "Kamera";
            cameraDescriptionLabel.text = "Poruszanie kamerą po przetrzymaniu klawisza ALT.";
            minimalDistanceLabel.text = "Minimalne oddalenie";
            maximumDistanceLabel.text = "Maksymalne oddalenie";
            minimalDeflectionLabel.text = "Minimalne wychylenie";
            maximumDeflectionLabel.text = "Maksymalne wychylenie";
            remotenessSensitivityLabel.text = "Czułość oddalania";
            xyAxisSensitivityLabel.text = "Czułość osi XY";
            minimalDistancePlaceholder.text = PolishPlaceholder;
            maximumDistancePlaceholder.text = PolishPlaceholder;
            minimalDeflectionPlaceholder.text = PolishPlaceholder;
            maximumDeflectionPlaceholder.text = PolishPlaceholder;
            remotenessSensitivityPlaceholder.text = PolishPlaceholder;
            xyAxisSensitivityPlaceholder.text = PolishPlaceholder;
            cameraBackButtonLabel.text = PolishBack;

            resolutionTitleLabel.text = "Rozdzielczość";
            widthLabel.text = "Szerokość";
            heightLabel.text = "Wysokość";
            screenModeLabel.text = "Tryb pełnoekranowy (1 / 0)";
            refreshRateLabel.text = "Częstotliwość odświeżania";
            widthPlaceholder.text = PolishPlaceholder;
            heightPlaceholder.text = PolishPlaceholder;
            screenModePlaceholder.text = PolishPlaceholder;
            refreshRatePlaceholder.text = PolishPlaceholder;
            applySettingsButtonLabel.text = "ZASTOSUJ USTAWIENIA";
            resolutionBackButtonLabel.text = PolishBack;

            othersTitleLabel.text = "Inne";
            languageSwitchLabel.text = "Angielskie tłumaczenie.";
            terrainLightingSwitchLabel.text = "Włącz oświetlenie terenu.";
            shipLightingSwitchLabel.text = "Włącz oświetlenie statku.";
            changeSceneButtonLabel.text = "ZMIEŃ SCENĘ";
            othersBackButtonLabel.text = PolishBack;
            CreateNewDropdown(scenarioDropdown, PolishScenarioOptions);
            CreateNewDropdown(shipDropdown, PolishShipOptions);



            warningMessage.text = "WYSOKIE RYZYKO KOLIZJI";
            collisionMessage.text = "STATEK UDERZYŁ W OBIEKT";
            breakdown.text = "Awaryjne Zatrzymanie Statku";
            reparation.text = "N";
        }
    }

    /*
     * Funkcja "PauseLanguageSwitchEvent" podmienia tekst na kontrolkach w zaleznosci od parametru "value", ktory jest ustawiany w skrypcie "Pause" po odznaczeniu lub zaznaczeniu checkboxa odpowiadzialnego za tlumaczenie aplikacji w menu opcji.
    */

    #endregion

    #region OPIS METODY "ADD_NEW_OPTIONS(STRING[] ARRAY)"

    void CreateNewDropdown(Dropdown dropdown, string[] options)
    {
        dropdown.options.Clear();

        foreach (string option in options)
            dropdown.options.Add(new Dropdown.OptionData(option));

        dropdown.captionText.text = dropdown.options[dropdown.value].text;
    }

    /*
     * Funkcja "CreateNewDropdown" sluzy do zapelniania wybranej listy rozwijanej opcjami, podanymi w postaci tablicy stringow.
     * Lista jest czyszczona, w petli "foreach" kazdy element w tablicy stringow jest dodawany do listy rozwijanej, a na koniec ustawiany jest tekst dla aktualnie wybranej opcji w liscie, ktory ma sie wyswietlic uzytkownikowi.
    */

    #endregion

    #endregion
}