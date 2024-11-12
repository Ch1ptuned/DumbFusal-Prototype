using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private GameObject g_MainMenu;
    [SerializeField] private GameObject g_SettingsMenu;

    private bool settingsOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsOpen)
            {
                Settings();
                Time.timeScale = 1.0f;
            }
            else
            {
                Settings();
                Time.timeScale = 0f;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResumeGame()
    {
        g_SettingsMenu.SetActive(false);
        g_MainMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        if (settingsOpen)
        {
            g_SettingsMenu.SetActive(false);
            g_MainMenu.SetActive(true);
            settingsOpen = false;
        }
        else
        {
            g_SettingsMenu.SetActive(true);
            g_MainMenu.SetActive(false);
            settingsOpen = true;
        }
    }
}