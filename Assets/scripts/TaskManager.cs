using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    private const string Task1Key = "TaskCompleted_Scene1";
    private const string Task2Key = "TaskCompleted_Scene2";
    private const string Task3Key = "TaskCompleted_Scene3";
    public GameObject canvas;
    public menu_changer menuChanger;

    void Start()
    {
        // Check if all tasks are completed when the scene starts
        CheckAllTasksCompleted();
    }

    // Call this function when a task is completed in the scene
    public void MarkTaskAsCompleted()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "5 deg Reaction")
            PlayerPrefs.SetInt(Task1Key, 1);
        else if (currentScene == "35 deg Reaction")
            PlayerPrefs.SetInt(Task2Key, 1);
        else if (currentScene == "100 deg Reaction")
            PlayerPrefs.SetInt(Task3Key, 1);

        PlayerPrefs.Save(); // Save the data persistently

        // Re-check if all tasks are completed
        CheckAllTasksCompleted();
    }

    private void CheckAllTasksCompleted()
    {
        bool task1 = PlayerPrefs.GetInt(Task1Key, 0) == 1;
        bool task2 = PlayerPrefs.GetInt(Task2Key, 0) == 1;
        bool task3 = PlayerPrefs.GetInt(Task3Key, 0) == 1;

        if (task1 && task2 && task3)
        {
            OnAllTasksCompleted();
        }
    }

    private void OnAllTasksCompleted()
    {
        menuChanger.SolutionShower(); // Call the function
        // canvas.SetActive(false);
        ResetTaskProgress();
        // Add your function here (e.g., unlocking a new feature, playing an animation, etc.)
    }

    public void ResetTaskProgress()
    {
        PlayerPrefs.DeleteKey(Task1Key);
        PlayerPrefs.DeleteKey(Task2Key);
        PlayerPrefs.DeleteKey(Task3Key);
        PlayerPrefs.Save();

        Debug.Log("Task progress has been reset.");
    }
   
}
