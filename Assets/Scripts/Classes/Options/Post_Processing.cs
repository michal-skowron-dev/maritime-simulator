using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class Post_Processing : MonoBehaviour
{
    #region OPIS KLASY

    /*
     * Klasa "Post_Processing" sluzy do ustawiania efektow post processing dla kamery z perspektywy trzeciej osoby.
     * Ponizej znajduje sie przyklad manipulacji efektem "bloom" dla kamery z perspektywy trzeciej osoby.
     * Na poczatek pobierane sa aktualne ustawienia post processingu do zmiennej "bloomSettings".
     * Nastepnie ustawiana jest w tej zmiennej intensywnosc efektu "bloom", ktora wynosi 10.
     * Na koniec nadpisuje sie ustawienia post processingu przy pomocy tej zmiennej.
     * 
     * BloomModel.Settings bloomSettings = tppCameraPostProcessingProfile.bloom.settings;
     * bloomSettings.bloom.intensity = 10;
     * tppCameraPostProcessingProfile.bloom.settings = bloomSettings;
     * 
     * Przeanalizowanie powyzszego przykladu pozwolilo mi na wlaczanie, badz wylaczanie zadanych efektow post processingu przy pomocy listy rozwijanej w menu opcji.
    */

    #endregion

    #region OPIS ZMIENNYCH

    public PostProcessingProfile tppCameraPostProcessingProfile;
    public Dropdown addEffectDropdown, removeEffectDropdown;

    ushort i;

    /*
     * Do "tppCameraPostProcessingProfile", "addEffectDropdown" i "removeEffectDropdown" dokonano referencji, w celu mozliwosci manipulowania tymi obiektami.
     * Zmianna "i" jest zapisana jako zmienna globalna w celach optymalizacyjnych. Jest wykorzystywana jedynie w petli "for".
    */

    #endregion

    #region OPIS METOD

    #region OPIS METODY "ADD_EFFECT(INT INDEX)"

    public void AddEffect(int index)
    {
        if (index != 0)
        {
            Apply(addEffectDropdown.options[addEffectDropdown.value].text);

            removeEffectDropdown.options.Add(new Dropdown.OptionData(addEffectDropdown.options[addEffectDropdown.value].text));
            RemoveOption(addEffectDropdown, addEffectDropdown.options[addEffectDropdown.value].text);

            addEffectDropdown.value = 0;
            addEffectDropdown.captionText.text = addEffectDropdown.options[addEffectDropdown.value].text;
        }
    }

    /*
     * Funkcja "AddEffect" sluzy do wlaczania danego efektu post processingu dla kamery, dodawaniu wybranej opcji do listy rozwijanej, w ktorej znajduja sie wykorzystywane w danej chwili efekty oraz usuwanie danej opcji efektu do wyboru.
     * Index listy jest ustawiany na pierwszy element, czyli na znak "+" oraz ustawiane jest jego wyswietlanie.
    */

    #endregion

    #region OPIS METODY "REMOVE_EFFECT(INT INDEX)"

    public void RemoveEffect(int index)
    {
        if (index != 0)
        {
            Apply(removeEffectDropdown.options[removeEffectDropdown.value].text);

            addEffectDropdown.options.Add(new Dropdown.OptionData(removeEffectDropdown.options[removeEffectDropdown.value].text));
            RemoveOption(removeEffectDropdown, removeEffectDropdown.options[removeEffectDropdown.value].text);

            removeEffectDropdown.value = 0;
            removeEffectDropdown.captionText.text = removeEffectDropdown.options[removeEffectDropdown.value].text;
        }
    }

    /*
     * Funkcja "RemoveEffect" sluzy do wylaczania danego efektu post processingu dla kamery, usuwaniu wybranej opcji z listy rozwijanej, w ktorej znajduja sie wykorzystywane w danej chwili efekty oraz dodawanie danej opcji efektu do wyboru.
     * Index listy jest ustawiany na pierwszy element, czyli na znak "-" oraz ustawiane jest jego wyswietlanie.
    */

    #endregion

    #region OPIS METODY "APPLY()"

    void Apply(string option)
    {
        switch (option)
        {
            case "Fog":
                tppCameraPostProcessingProfile.fog.enabled = !tppCameraPostProcessingProfile.fog.enabled;
                break;

            case "Antialiasing":
                tppCameraPostProcessingProfile.antialiasing.enabled = !tppCameraPostProcessingProfile.antialiasing.enabled;
                break;

            case "Ambient Occlusion":
                tppCameraPostProcessingProfile.ambientOcclusion.enabled = !tppCameraPostProcessingProfile.ambientOcclusion.enabled;
                break;

            case "Screen Space Reflection":
                tppCameraPostProcessingProfile.screenSpaceReflection.enabled = !tppCameraPostProcessingProfile.screenSpaceReflection.enabled;
                break;

            case "Depth Of Field":
                tppCameraPostProcessingProfile.depthOfField.enabled = !tppCameraPostProcessingProfile.depthOfField.enabled;
                break;

            case "Motion Blur":
                tppCameraPostProcessingProfile.motionBlur.enabled = !tppCameraPostProcessingProfile.motionBlur.enabled;
                break;

            case "Eye Adaptation":
                tppCameraPostProcessingProfile.eyeAdaptation.enabled = !tppCameraPostProcessingProfile.eyeAdaptation.enabled;
                break;

            case "Bloom":
                tppCameraPostProcessingProfile.bloom.enabled = !tppCameraPostProcessingProfile.bloom.enabled;
                break;

            case "Color Grading":
                tppCameraPostProcessingProfile.colorGrading.enabled = !tppCameraPostProcessingProfile.colorGrading.enabled;
                break;

            case "User Lut":
                tppCameraPostProcessingProfile.userLut.enabled = !tppCameraPostProcessingProfile.userLut.enabled;
                break;

            case "Chromatic Aberration":
                tppCameraPostProcessingProfile.chromaticAberration.enabled = !tppCameraPostProcessingProfile.chromaticAberration.enabled;
                break;

            case "Grain":
                tppCameraPostProcessingProfile.grain.enabled = !tppCameraPostProcessingProfile.grain.enabled;
                break;

            case "Vignette":
                tppCameraPostProcessingProfile.vignette.enabled = !tppCameraPostProcessingProfile.vignette.enabled;
                break;

            case "Dithering":
                tppCameraPostProcessingProfile.dithering.enabled = !tppCameraPostProcessingProfile.dithering.enabled;
                break;
        }
    }

    /*
     * Funkcja "Apply" sluzy do przelaczania miedzy opcjami post processingu przy pomocy switcha, ktoremu przekazywany jest parametr z funkcji "AddEffect" lub "RemoveEffect".
    */

    #endregion

    #region OPIS METODY "REMOVE_OPTION(DROPDOWN DROPDOWN, STRING OPTION)"

    void RemoveOption(Dropdown dropdown, string option)
    {
        for (i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text.Equals(option))
            {
                dropdown.options.RemoveAt(i);
                break;
            }
        }
    }

    /*
     * Funkcja "RemoveOption" sluzy do usuwania wybranej opcji dla okreslonej przez uzytkownika listy rozwijanej.
     * Przez petle "for" sprawdzane sa kolejne elementy listy dopoki szukana wartosc zostanie znaleziona i usunieta z listy.
     * Gdy wartosc zostanie znaleziona, to nie ma potrzeby dalszego przeszukiwania listy, wiec petla zostaje przerwana przez "break", a funkcja konczy swoje dzialanie.
    */

    #endregion

    #endregion
}