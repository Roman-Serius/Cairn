using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // Press ESC to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        SceneController.Instance.LoadAdditive("PauseMenu");
        Time.timeScale = 0f; // freeze physics + movement
        isPaused = true;
    }

    public void ResumeGame()
    {
        SceneController.Instance.UnloadAdditive("PauseMenu");
        Time.timeScale = 1f; // unfreeze
        isPaused = false;
    }

    public void QuitToStart()
    {
        Time.timeScale = 1f;
        SceneController.Instance.LoadScene("StartScreen");
    }
}
