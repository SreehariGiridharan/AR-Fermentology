using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseManager : MonoBehaviour
{
    private bool isPaused = false;
    private string pauseSceneName = "Chatbot"; // Change to your pause menu scene name

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
        }
    }

    public void PauseGame()
    {
        // Pause game time
        Time.timeScale = 0f;
        AudioListener.pause = true;
        isPaused = true;

        // Load pause scene additively
        SceneManager.LoadScene(pauseSceneName, LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        // Resume game time
        Time.timeScale = 1f;
        AudioListener.pause = false;
        isPaused = false;

        // Unload pause scene
        SceneManager.UnloadSceneAsync(pauseSceneName);
    }
}
