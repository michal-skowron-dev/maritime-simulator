using UnityEngine;
using System.Collections.Generic;
using DLL_Matrix;

#region OPIS BIBLIOTEKI "DLL_Matrix"

/*
 * "DLL_Matrix" - Samodzielnie napisana biblioteka zawierajaca metody operacji na macierzach o rozmiarach 4 x 4.
 *  Metody te sluza do szybkiego tworzenia macierzy 4 x 4 o zadanych parametrach, wymnazaniu ich, czy sprawdzaniu poprawnosci ich rozmiaru.
 *  Projekt biblioteki znajduje sie w folderze "Resources".
*/

#endregion

public class Matrix
{
    #region OPIS KLASY

    /*
     * Klasa "Matrix" sluzy do tworzenia macierzy oraz wykonywania na nich odpowiednich dzialan przy pomocy biblioteki "DLL_Matrix" w celu wyznaczenia zapisu macierzowego wspolrzednych.
     * Obliczone wartosci posluza jako wspolrzedne do utworzenia sfery, ktora bedzia tworzona w zaleznosci od obiektu trafionego przez promien wychodzacy z danego czujnika.
    */

    #endregion
    
    #region OPIS ZMIENNYCH
    
    List<object> mixedList = new List<object>();
    double[,] ship, echo, echoGlobal, echoPoint, echoPointGlobal;
    double[] parameters = new double[6];
    readonly ushort id;

    /*
     * Opis poszczegolnych zmiennych:
     * 
     * "mixedList"          - Jest to lista obiektow, ktora jest niezbedna do wyznaczenia wspolrzednych sfery. Lista jest typu "object", poniewaz niezbedne jest spakowanie zmiennych o roznych typach razem przy zwracaniu wartosci z metody "GetCalculatedValues(Transform shipTransform, GameObject alarm)".
     * 
     * "ship"               - Jest to macierz o rozmiarach 4 x 4. Wykonywane sa na niej operacje mnozenia przez inne macierze o rozmiarach 4 x 4 z odpowiednio ustawionymi paremetrami dla danego obiektu.
     * "echo"               - Jest to macierz o rozmiarach 4 x 4. Wykonywane sa na niej operacje mnozenia przez inne macierze o rozmiarach 4 x 4 z odpowiednio ustawionymi paremetrami dla danego obiektu.
     * "echoGlobal"         - Jest to macierz o rozmiarach 4 x 4. Wykonywane sa na niej operacje mnozenia przez inne macierze o rozmiarach 4 x 4 z odpowiednio ustawionymi paremetrami dla danego obiektu.
     * "echoPoint"          - Jest to macierz o rozmiarach 4 x 4. Wykonywane sa na niej operacje mnozenia przez inne macierze o rozmiarach 4 x 4 z odpowiednio ustawionymi paremetrami dla danego obiektu.
     * "echoPointGlobal"    - Jest to macierz o rozmiarach 4 x 4. Wykonywane sa na niej operacje mnozenia przez inne macierze o rozmiarach 4 x 4 z odpowiednio ustawionymi paremetrami dla danego obiektu.
     * 
     * "parameters"         - Jest to tablica o rozmiarze 6 elementow, przechowywujaca pozycje i obrot okreslonego obiektu (statku lub alarmu) kolejno pozyskanych z 3 osi wspolrzednych x, y, z.
     * 
     * "id"                 - Jest to identyfikator alarmu z ktorego wychodzi promien. Zmienna ta ma na celu przekazanie zmiennej "parameters" danych o odleglosci trafionego przez promien obiektu.
    */

    #endregion
    
    #region OPIS METOD
    
    #region OPIS KONSTRUKTORA
    
    public Matrix(ushort id)
    {
        this.id = id;
    }

    /*
     * Przy tworzeniu obiektu wymagany jest identyfikator alarmu, aby byc w stanie obliczyc pozycje sfery na podstawie zarejestrowanej odleglosci z danego alarmu.
    */

    #endregion
    
    #region OPIS METODY "GET_CALCULATED_VALUES(TRANSFORM SHIP_TRANSFORM, GAMEOBJECT ALARM)"
    
    public List<object> GetCalculatedValues(Transform shipTransform, GameObject alarm)
    {
        parameters = MatrixCalc.SetParameters(shipTransform.position.x, shipTransform.position.y, shipTransform.position.z, shipTransform.rotation.x, shipTransform.rotation.y, shipTransform.rotation.z);
        ship = MatrixCalc.MultiplyMatrix(MatrixCalc.CreateMatrix("RZ", parameters), MatrixCalc.CreateMatrix("RY", parameters));
        ship = MatrixCalc.MultiplyMatrix(ship, MatrixCalc.CreateMatrix("RX", parameters));
        ship = MatrixCalc.MultiplyMatrix(ship, MatrixCalc.CreateMatrix("BASE", parameters));

        parameters = MatrixCalc.SetParameters(alarm.transform.position.x, alarm.transform.position.y, alarm.transform.position.z, alarm.transform.rotation.x, alarm.transform.rotation.y, alarm.transform.rotation.z);
        echo = MatrixCalc.MultiplyMatrix(MatrixCalc.CreateMatrix("RZ", parameters), MatrixCalc.CreateMatrix("RY", parameters));
        echo = MatrixCalc.MultiplyMatrix(echo, MatrixCalc.CreateMatrix("RX", parameters));
        echo = MatrixCalc.MultiplyMatrix(echo, MatrixCalc.CreateMatrix("BASE", parameters));

        echoGlobal = MatrixCalc.MultiplyMatrix(ship, echo);

        parameters = MatrixCalc.SetParameters(0, 0, 0, 0, 0, 0);
        echoPoint = MatrixCalc.MultiplyMatrix(MatrixCalc.CreateMatrix("RZ", parameters), MatrixCalc.CreateMatrix("RY", parameters));
        echoPoint = MatrixCalc.MultiplyMatrix(echoPoint, MatrixCalc.CreateMatrix("RX", parameters));

        if (id == 1 || id == 2)
            parameters = MatrixCalc.SetParameters(Alarm_Manager.distance[id], 0, 0, 0, 0, 0);
        else if (id == 4)
            parameters = MatrixCalc.SetParameters(0, Alarm_Manager.distance[id], 0, 0, 0, 0);
        else
            parameters = MatrixCalc.SetParameters(0, 0, Alarm_Manager.distance[id], 0, 0, 0);

        echoPoint = MatrixCalc.MultiplyMatrix(echoPoint, MatrixCalc.CreateMatrix("BASE", parameters));

        echoPointGlobal = MatrixCalc.MultiplyMatrix(echoGlobal, echoPoint);

        mixedList.Add(echoPointGlobal);
        mixedList.Add(MatrixCalc.GetAngles(echoPointGlobal));

        return mixedList;
    }

    /*
     * "GetCalculatedValues(Transform shipTransform, GameObject alarm)" jest funkcja, ktora zwraca liste obiektow potrzebnych do wyznaczenia pozycji i obrotu sfery.
     *  W funkcji nastepuje tworzenie odpowiednio przygotowanych macierzy o rozmiarach 4 x 4 o wartosciach pozyskanych z tablicy "parameters".
     *  Tak utworzone macierze sa nastepnie wymnazane przez siebie w celu uzyskania zapisu macierzowego wspolrzednych.
     *  Instrukcja warunkowa wewnatrz funkcji sprawdza zarejestrowana odleglosc z danego czujnika. Identyfikator alarmu jest podawany przy tworzeniu obiektu klasy.
     *  Koncowym etapem dzialania funkcji jest dodanie kluczowych wartosci do listy obiektow "mixedList" i zwrocenie jej wartosci. Do listy sa dodawane wartosci macierzy 4 x 4 ze zmiennej "echoPointGlobal" oraz tablica jednowymiarowa z wyznaczonymi katami obrotu sfery.
    */

    #endregion

    #endregion
}