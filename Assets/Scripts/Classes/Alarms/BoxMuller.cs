public class BoxMuller
{
    #region OPIS KLASY

    /*
     * Klasa "BoxMuller" sluzy do generowania bledu pomiarowego przy pomocy transformacji Boxa-Mullera.
     * Transformacja Boxa-Mullera to metoda generowania liczb losowych o rozkladzie normalnym, na podstawie dwoch wartosci zmiennej o rozkladzie jednostajnym na przedziale (0,1].
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    System.Random random = new System.Random();
    readonly double standardDeviation, average;
    double u1, u2;

    /*
     * Opis poszczegolnych zmiennych:
     * 
     * "random"             - Utworzenie obiektu niezbednego do losowania liczb.
     * 
     * "standardDeviation"  - Wartosc odchylenia standardowego.
     * "average"            - Wartosc sredniej.
     * 
     * "u1"                 - Pierwsza wylosowana liczba.
     * "u2"                 - Druga wylosowana liczba.
    */

    #endregion
    
    #region OPIS METOD

    #region OPIS KONSTRUKTORA

    public BoxMuller(double standardDeviation, double average)
    {
        this.standardDeviation = standardDeviation;
        this.average = average;
    }

    /*
     * Przy tworzeniu nowego obiektu wymagane jest podanie wartosci odchylenia standardowego oraz sredniej, ktore sa uwzgledniane przy wyznaczaniu bledu pomiarowego.
    */

    #endregion
    
    #region OPIS METODY "BOX_MULLER_CALC()"
    
    public double BoxMullerCalc()
    {
        u1 = 1.0 - random.NextDouble();
        u2 = 1.0 - random.NextDouble();
        return (System.Math.Sqrt(-2.0 * System.Math.Log(u1)) * System.Math.Sin(2.0 * System.Math.PI * u2) * standardDeviation) + average;
    }

    /*
     * Funkcja "BoxMullerCalc()" zwraca wartosc obliczonego bledu pomiarowego wedlug podanego wzoru.
     * Jako ze, metoda "NextDouble()" zwraca losowa liczbe zmiennoprzecinkowa, ktora jest wieksza lub rowna 0.0 i mniejsza niz 1.0, to niezbedne okazalo sie odjecie 1 od wylosowanej wartosci, w celu zachowania liczby mieszczacej sie w przedziale (0,1].
    */

    #endregion

    #endregion
}