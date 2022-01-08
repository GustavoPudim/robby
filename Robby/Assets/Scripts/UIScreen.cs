using UnityEngine.EventSystems;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    private GameObject lastSelect;

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

}
