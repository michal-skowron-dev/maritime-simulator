using UnityEngine;
using UnityEngine.UI;

public class Button_Manager
{
    #region OPIS KLASY

    /*
     * Klasa "Button_Manager" jest uzywana do manipulowania wlasciwosciami przyciskow umieszczonych w menu symulatora. Poki co znajduje sie w niej jedynie funkcja zmianiajaca kolor tla okreslonego przycisku.
    */

    #endregion

    #region OPIS METOD

    #region OPIS METODY "SET_BUTTON_COLOR(BUTTON BUTTON, COLOR COLOR)"

    public static void SetButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = color;
        colors.pressedColor = color;
        button.colors = colors;
    }

    /*
     * Funkcja "SetButtonColor(Button button, Color color)" sluzy do ustawienia danemu przyciskowi wybrany kolor tla.
     * Ustawienia koloru sa rowniez zatwierdzane dla podswietlania przycisku kursorem myszy oraz przy naciskaniu przycisku.
    */

    #endregion

    #endregion
}