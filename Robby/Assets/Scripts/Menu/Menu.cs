using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    private static int UISize = 2;
    public readonly string[] UISizeNames = { "Small", "Medium", "Big" };

    public static float UIScale 
    { 
        get { return UISize / 4.0f + 0.5f; } 
    }
    
    private GameObject lastSelect;

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

    void Update () 
    {
        // Workaround to prevent mouse clicks from deselecting objects        
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
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
