using UnityEngine;
using UnityEngine.UI;

public class Nomoto : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa "Nomoto" sluzy do obliczen parametrow kursu oraz predkosci statku na podstawie modelu "Nomoto".
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    public Rigidbody ship;
    public Slider courseSlider, speedSlider;
    public float zAxis, xAxis;

    const float T = 48.5f, K = 0.1232f, StabilityFactor = 0.6f, ForceMultiplier = 9;

    Vector3 currentSpeed;
    float radians;

    /*
     * Do "ship", "courseSlider" i "speedSlider" dokonano referencji, w celu mozliwosci manipulowania tymi obiektami.
     * Ponizej znajduja sie stale wspolczynniki "T" i "K" wykorzystywane w modelu "Nomoto", wspolczynnik stabilnosci oraz odpowiednio dobrany mnoznik sily.
    */

    #endregion

    #region OPIS METOD

    #region OPIS METODY "FIXED_UPDATE()"

    void FixedUpdate()
    {
        zAxis = Mathf.MoveTowards(zAxis, speedSlider.value, StabilityFactor * Time.deltaTime);
        xAxis = Mathf.MoveTowards(xAxis, courseSlider.value, StabilityFactor * Time.deltaTime);
        ChangeSpeed();
        ChangeCourse();
    }

    /*
     * Metoda "Mathf.MoveTowards" przyjmuje trzy argumenty. Pierwszy argument to obecna wartosc, drugi argument to wartosc, do ktorej dazy funkcja, natomiast trzeci arguement to maksymalna wartosc jaka moze zostac dodana do wartosci zmiennej.
    */

    #endregion

    #region OPIS METODY "CHANGE_SPEED()"

    void ChangeSpeed()
    {
        currentSpeed = new Vector3(0.0f, 0.0f, zAxis) * ForceMultiplier;
        ship.AddRelativeForce(currentSpeed);
    }

    /*
     * W funkcji "ChangeSpeed()" tworzony jest nowy wektor, ktorego wartosc jest mnozona przez mnoznik sily. Na statek jest dodawana sila, ktora popycha statek do przodu (AddRelativeForce).
    */

    #endregion

    #region OPIS METODY "CHANGE_COURSE()"

    void ChangeCourse()
    {
        radians = (K * zAxis * Time.deltaTime - xAxis * (Time.deltaTime - T)) / T;
        ship.transform.rotation = Quaternion.Euler(0.0f, radians * 180f * Time.deltaTime, 0.0f);
    }

    /*
     * Funkcja "ChangeCourse()" odpowiada za zmiane kursu statku na podstawie wyliczonych wartosci dla modelu "Nomoto".
    */

    #endregion

    #endregion
}