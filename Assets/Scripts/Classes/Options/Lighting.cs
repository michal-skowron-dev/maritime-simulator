using UnityEngine;

public class Lighting : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa "Lighting" odpowiada za przechowywanie stanow checkBoxow dotyczacych oswietlenia z menu opcji oraz za wlaczanie zadanych ustawien.
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    public GameObject frontLight, leftLight, rightLight, rearLight, echosounderLight, terrainLight;

    /*
     * Opis poszczegolnych zmiennych i wlasciwosci:
     * 
     * "frontLight"               - Jest to fizycznie ustawione swiatlo punktowe wydobywajace sie z czujnika umieszczonego na statku.
     * "leftLight"                - Jest to fizycznie ustawione swiatlo punktowe wydobywajace sie z czujnika umieszczonego na statku.
     * "rightLight"               - Jest to fizycznie ustawione swiatlo punktowe wydobywajace sie z czujnika umieszczonego na statku.
     * "rearLight"                - Jest to fizycznie ustawione swiatlo punktowe wydobywajace sie z czujnika umieszczonego na statku.
     * "echosounderLight"         - Jest to fizycznie ustawione swiatlo punktowe wydobywajace sie z czujnika umieszczonego na statku.
     * 
     * "terrainLight"             - Sa to 4 swiatla kierunkowe ustawione w swiecie gry w celu oswietlenia terenu.
     * 
    */

    #endregion

    #region OPIS METOD

    #region OPIS METODY "START()"

    void Start()
    {
        Pause.TerrainLightingSwitchEvent += PauseTerrainLightingSwitchEvent;
        Pause.ShipLightingSwitchEvent += PauseShipLightingSwitchEvent;
    }

    /*
     * W funkcji "Start" subskrypowane sa zdarzenie ze skryptu "Pause". Jako ze, wykorzystano "action" w skrypcie "Pause" nie przekazujacy zadnej wartosci, to zdarzenia nie beda przenosily zadnych wartosci.
    */

    #endregion

    #region OPIS METODY "ON_DESTROY()"

    void OnDestroy()
    {
        {
            Pause.TerrainLightingSwitchEvent -= PauseTerrainLightingSwitchEvent;
            Pause.ShipLightingSwitchEvent -= PauseShipLightingSwitchEvent;
        }
    }

    /*
     * Po zresetowaniu symulatora przyciskiem w menu glownym nalezy odsubskrybowac zdarzenia. W innym wypadku checkboxy odpowiedzialne za zmiane stanu oswietlenia przestana dzialac po restarcie symulatora ze wzgledu na brak referencji.
    */

    #endregion

    #region OPIS METODY "PAUSE_TERRAIN_LIGHTING_SWITCH_EVENT()"

    void PauseTerrainLightingSwitchEvent()
    {
        terrainLight.SetActive(!terrainLight.activeSelf);
    }

    /*
     * Funkcja "PauseTerrainLightingSwitchEvent()" sluzy do wlaczania, badz wylaczania oswietlenia terenu.
    */

    #endregion

    #region OPIS METODY "PAUSE_SHIP_LIGHTING_SWITCH_EVENT()"

    void PauseShipLightingSwitchEvent()
    {
        frontLight.SetActive(!frontLight.activeSelf);
        leftLight.SetActive(!leftLight.activeSelf);
        rightLight.SetActive(!rightLight.activeSelf);
        rearLight.SetActive(!rearLight.activeSelf);
        echosounderLight.SetActive(!echosounderLight.activeSelf);
    }

    /*
     * Funkcja "PauseShipLightingSwitchEvent" sluzy do wlaczania, badz wylaczania swiatel punktowych statku.
    */

    #endregion

    #endregion
}