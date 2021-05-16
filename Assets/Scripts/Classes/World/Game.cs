using UnityEngine;
using UnityEngine.PostProcessing;

public class Game : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa "Game" odpowiedzialna jest w glownej mierze za wstrzymywanie symulacji oraz przelaczanie miedzy kamerami przy uruchomionej symulacji za pomoca okreslonych klawiszy.
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    public GameObject tppCamera, fppCamera, simulator, pause;
    public AudioSource bellSound;
    public PostProcessingProfile tppCameraPostProcessingProfile;

    /*
     * Opis poszczegolnych zmiennych:
     * 
     * "tppCamera"                          - Third-person perspective camera (kamera z perspektywy trzeciej osoby). Kamera podazajaca za statkiem z mozliwoscia kontrolowania jej pozycji.
     * "fppCamera"                          - First-person perspective camera (kamera z perspektywy pierwszej osoby). Kamera umieszczona na mostku nawigacyjnym w celu zmiany predkosci i kursu statku.
     * "simulator"                          - Jest to obiekt zawierajacy wszystkie komponenty, ktore umozliwiaja przeprowadzenie symulacji. Wszystkie zwiazane dzieci z tym obiektem (takie jak teren, statek, czy sterownia) sa wlaczane po wybraniu opcji startu z menu glownego.
     * "pause"                              - Jest to obiekt, ktory wyswietla menu glowne wraz ze wszystkimi opcjami dotyczacymi symulatora, ktore uzytkownik moze zmienic. Dzieci tego obiektu to przede wszystkim opcje, podopcje, checkboxy, slidery, itd.
     * 
     * "bellSound"                          - Dzwiek dzwonka, ktory jest odtwarzany, gdy statek jest bliski kolizji.
     * 
     * "tppCameraPostProcessingProfile"     - Odniesienie do profilu post processingu. Jest ono potrzebne z powodu koniecznosci wylaczenia wszystkich efektow post processingu przed uruchomieniem symulatora.
    */

    #endregion

    #region OPIS METOD

    #region OPIS METODY "PAUSE()"

    void Pause()
    {
        Time.timeScale = 0;

        simulator.SetActive(false);
        pause.SetActive(true);
    }

    /*
     * Bezargumentowa funkcja "Pause()" jest wywolywana gdy uzytkownik bedzie chcial zatrzymac symulator i przejsc do menu glownego poprzez nacisniecie klawisza "Escape":
     * 
     * "Time.timeScale = 0"         - Sluzy do wstrzymania symulacji poprzez zatrzymanie czasu gry. Statek jak i woda przestaja sie wowczas poruszac.
     * 
     * "simulator.SetActive(false)" - Symulacja zostaje wylaczona przez zdezaktywowanie obiektu. Dzieci obiektu sa rowniez wylaczane, jesli byly wczesniej wlaczone.
     * "pause.SetActive(true)"      - Wlaczone zostaje menu pauzy przez uaktywnienie obiektu. Dzieci obiektu sa rowniez wlaczane, jesli przed zdezaktywowaniem rodzica byly wlaczone.
    */

    #endregion
    
    #region OPIS METODY "START()"
    
    void Start()
    {
        tppCameraPostProcessingProfile.fog.enabled = false;
        tppCameraPostProcessingProfile.antialiasing.enabled = false;
        tppCameraPostProcessingProfile.ambientOcclusion.enabled = false;
        tppCameraPostProcessingProfile.screenSpaceReflection.enabled = false;
        tppCameraPostProcessingProfile.depthOfField.enabled = false;
        tppCameraPostProcessingProfile.motionBlur.enabled = false;
        tppCameraPostProcessingProfile.eyeAdaptation.enabled = false;
        tppCameraPostProcessingProfile.bloom.enabled = false;
        tppCameraPostProcessingProfile.colorGrading.enabled = false;
        tppCameraPostProcessingProfile.userLut.enabled = false;
        tppCameraPostProcessingProfile.chromaticAberration.enabled = false;
        tppCameraPostProcessingProfile.grain.enabled = false;
        tppCameraPostProcessingProfile.vignette.enabled = false;
        tppCameraPostProcessingProfile.dithering.enabled = false;

        Pause();
    }

    /*
     * W funkcji "Start()" wylaczane sa wszystkie efekty post processingu, poniewaz po wylaczeniu aplikacji, wartosci post processingu w inspektorze nie zostaja resetowane.
     * Przy pierwszym uruchomieniu symulatora funkcja "Pause()" zostaje wywolana.
    */

    #endregion

    #region OPIS METODY "UPDATE()"

    void Update()
    {
        if (!pause.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Pause();

            if (Input.GetKeyDown(KeyCode.C))
            {
                tppCamera.SetActive(!tppCamera.activeSelf);
                fppCamera.SetActive(!fppCamera.activeSelf);
            }
        }
        else
        {
            if (bellSound.isPlaying)
                bellSound.Stop();
        }
    }

    /*
     * W metodzie "Update()" sprawdzane jest, czy menu pauzy jest wylaczone. Jesli menu jest wlaczone, to dzwiek dzwonka (jesli jest odtwarzany) zostaje zatrzymany.
     * Jesli menu pauzy nie jest aktywne, to wowczas sa rejestrowane nacisniete przez uzytkownika klawisze. Uzytkownik w takim wypadku moze wlaczyc menu pauzy klawiszem "Escape".
     * Podczas uruchomionej symulacji uzytkownik moze rowniez przelaczac miedzy kamerami (pierwszoosobowa lub trzecioosobowa) przy pomocy klawisza "C".
    */

    #endregion

    #endregion
}