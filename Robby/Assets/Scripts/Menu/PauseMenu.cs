using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{

    void OnEnable()
    {
        mainSectionSelect.GetComponent<Button>().Select();
        mainSectionSelect.GetComponent<Button>().OnSelect(null); // Makes sure the button is highlighted
    }

    public void Resume()
    {
        Manager.instance.player.TogglePause();
    }

    public void SaveAndQuit()
    {
        Manager.instance.player.TogglePause();
        SceneManager.LoadScene("MainMenu");
    }

}
