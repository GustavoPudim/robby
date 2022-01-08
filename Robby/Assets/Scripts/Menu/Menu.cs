using UnityEngine;
using UnityEngine.UI;

public class Menu : UIScreen
{

    private static int UISize = 2;
    public readonly string[] UISizeNames = { "Small", "Medium", "Big" };

    public static float UIScale 
    { 
        get { return UISize / 4.0f + 0.5f; } 
    }

    [Header("References")]
    public GameObject mainSection;
    public GameObject optionsSection;

    // First button to select in section
    public Button mainSectionSelect; 
    public Button optionsSectionSelect;

    public Slider UISizeSlider;
    public Text UISizeText;
    
    
    void Start ()
    {
        HidePointer();

        SetUIScale();
        UISizeSlider.SetValueWithoutNotify(UISize);
        UpdateUISizeName();

        mainSectionSelect.Select();
    }

    public void HidePointer()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenOptions()
    {
        optionsSection.SetActive(true);
        mainSection.SetActive(false);

        optionsSectionSelect.Select();
    }

    public void CloseOptions()
    {
        optionsSection.SetActive(false);
        mainSection.SetActive(true);

        mainSectionSelect.Select();
    }  

    public void UpdateUISize()
    {
        UISize = Mathf.FloorToInt(UISizeSlider.value);

        UpdateUISizeName();
        SetUIScale();
    }

    public void UpdateUISizeName()
    {
        UISizeText.text = "UI Size: " + UISizeNames[UISize];
    }

    public void SetUIScale()
    {
        transform.localScale = Vector3.one * UIScale;
    }

}
