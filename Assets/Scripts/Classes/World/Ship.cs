using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ship : Nomoto
{
    #region OPIS KLASY

    /*
     * Klasa "Ship" sluzy do wykrywania kolizji statku z obiektami oraz do wyswielania wartosci takich jak predkosc i kurs statku uzytkownikowi.
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    public Text targetCourseLabel, targetSpeedLabel, tppCameraCourseLabel, tppCameraSpeedLabel, fppCameraCourseLabel, fppCameraSpeedLabel;
    public GameObject collisionMessage, pause;
    public Button courseButton, speedButton;

    bool wait;

    /*
     * Na poczatek nalezy zrobic referencje do potrzebnych obiektow na scenie w celu mozliwosci manipulowania ich wartosciami z poziomu skryptu.
     * Zmienna "wait" sluzy jako zmienna sprawdzajaca, czy odczekany zostal czas, po ktorym statek przestal dotykac obiekt, z ktorym sie zderzyl.
    */

    #endregion
    
    #region OPIS METOD
    
    #region OPIS METODY "ON_COLLISION_ENTER(COLLISION COLLISION)"
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Terrain"))
        {
            ship.isKinematic = true;
            collisionMessage.SetActive(true);
        }
    }

    /*
     * Funkcja "OnCollisionEnter" sluzy do wykrywania kolizji statku z obiektem o tagu "Terrain".
     * Jesli zostanie wykryte zderzenie, to ruch statku zostaje wylaczony przez wlasciwosc "isKinematic" oraz wlaczane zostaje ostrzezenie informujace o zderzeniu statku z obiektem.
    */

    #endregion

    #region OPIS METODY "ON_COLLISION_EXIT(COLLISION COLLISION)"

    void OnCollisionExit(Collision collision)
    {
        if (!wait)
        {
            wait = true;
            StartCoroutine(Wait());
        }
    }

    /*
     * Funkcja "OnCollisionExit" sprawdza, czy statek przestal sie stykac z obiektem, po ktorym doszlo do kolizji.
     * Jesli nie zostanie wykryta interakcja statku z obiektem, to po odczekaniu 5 sekund statek znowu zacznie sie poruszac, a ostrzezenie o kolizji zniknie.
    */

    #endregion

    #region OPIS IENUMERATORA "WAIT()"

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);

        ship.isKinematic = false;
        collisionMessage.SetActive(false);
        wait = false;
    }

    /*
     * Interfejs "IEnumerator Wait()" posluzyl do odczekania 5 sekund zanim mozna bylo przywrocic zmiennej "wait" wartosc "false".
     * Wyrazenie "yield" zatrzymuje tymczasowe wykonanie metody i wysyla jedna wartosc do wywolywacza.
     * Dalej jest przywracana mozliwosc ruchu statku oraz ostrzezenie o kolizji jest wylaczane.
    */

    #endregion

    #region OPIS METODY "CHANGE_COURSE(FLOAT VALUE)"

    public void ChangeCourse(float value)
    {
        targetCourseLabel.text = string.Format("{0:0.00}°", value);
    }

    /*
     * "ChangeCourse(float value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci slidera kursu.
     *  Po zmianie wartosci aktualizowana jest wartosc etykiety, ktora wyswietla uzytkownikowi zadany przez niego kurs.
    */

    #endregion

    #region OPIS METODY "CHANGE_SPEED(FLOAT VALUE)"

    public void ChangeSpeed(float value)
    {
        targetSpeedLabel.text = string.Format("{0:0.00}kts", value * 21 * 0.01);
    }

    /*
    * "ChangeSpeed(float value)" jest funkcja, ktora jest wywolywana po zdarzeniu zmiany wartosci slidera predkosci.
    *  Po zmianie wartosci aktualizowana jest wartosc etykiety, ktora wyswietla uzytkownikowi zadana przez niego predkosc.
   */

    #endregion

    #region OPIS METODY "UPDATE()"

    void Update()
    {
        if (!pause.activeSelf)
        {
            if (Language.Lang)
            {
                tppCameraCourseLabel.text = string.Format("Course: {0:0.00}°", xAxis);
                tppCameraSpeedLabel.text = string.Format("Speed: {0:0.00} knots", zAxis * 21 * 0.01);
            }

            else
            {
                tppCameraCourseLabel.text = string.Format("Kurs: {0:0.00}°", xAxis);
                tppCameraSpeedLabel.text = string.Format("Prędkość: {0:0.00} knots", zAxis * 21 * 0.01);
            }

            fppCameraCourseLabel.text = string.Format("{0:0.00}°", xAxis);
            fppCameraSpeedLabel.text = string.Format("{0:0.00}kts", zAxis * 21 * 0.01);

            if (fppCameraCourseLabel.text.Equals(targetCourseLabel.text))
                Button_Manager.SetButtonColor(courseButton, Color.green);

            else
                Button_Manager.SetButtonColor(courseButton, Color.red);

            if (fppCameraSpeedLabel.text.Equals(targetSpeedLabel.text))
                Button_Manager.SetButtonColor(speedButton, Color.green);

            else
                Button_Manager.SetButtonColor(speedButton, Color.red);
        }
    }

    /*
     * W funkcji "Update()" aktualizowane sa etykiety informujace uzytkownika o obecnej wartosci kursu i predkosci w zaleznosci od ustawionego jezyka.
     * Jesli obecna wartosc bedzie rowna z wartoscia docelowa, to etykiety zmienia swoj kolor na kolor zielony. W innym wypadku etykiety przyjma barwe czerwona.
    */

    #endregion

    #endregion
}