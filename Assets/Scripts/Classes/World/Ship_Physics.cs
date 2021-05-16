using UnityEngine;

public class Ship_Physics : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa sluzaca do symulowania fizyki wody dzialajacej na statek.
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    public Rigidbody ship;

    public float waterLevel = 100.0f, level = 2.0f, waterDensity = 0.125f, pressureForce = 4.0f;
    float forceFactor;
    Vector3 force;

    /*
     * Na poczatek utworzyc nalezy referencje dla statku, na ktory ma dzialac fizyka wody.
     * Podane nizej wartosci okreslaja poziom wody, gestosc i cisnienie wody.
    */

    #endregion
    
    #region OPIS METOD
    
    #region OPIS METODY "FIXED_UPDATE()"
    
    void FixedUpdate()
    {
        forceFactor = 1.0f - ((transform.position.y - waterLevel) / level);
        if (forceFactor > 0.0f)
        {
            force = -Physics.gravity * ship.mass * (forceFactor - ship.velocity.y * waterDensity);
            force += new Vector3(0.0f, -pressureForce * ship.mass, 0.0f);
            ship.AddForce(force, ForceMode.Force);
        }
    }

    /*
     * W funkcji "FixedUpdate()" obliczana jest sila, ktora ma oddzialywac na statek.
    */

    #endregion

    #endregion
}