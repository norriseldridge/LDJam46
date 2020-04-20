using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;

    public static void Pause()
    {
        if (!Paused)
        {
            Paused = true;
            Time.timeScale = 0;
            SceneManager.LoadScene(Scenes.PAUSE_MENU, LoadSceneMode.Additive);
        }
        else
        {
            Resume();
        }
    }

    public static void Resume()
    {
        Paused = false;
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(Scenes.PAUSE_MENU);
    }

    public void OnClickResume()
    {
        // Just close the current popup
        Resume();
    }

    public void OnClickQuit()
    {
        SceneManager.LoadScene(Scenes.MAIN_MENU);
    }
}
