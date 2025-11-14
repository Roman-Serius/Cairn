using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isPaused = false;
    private bool isTransitioning = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Only toggle if not already switching
        if (isTransitioning) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                StartCoroutine(ResumeRoutine());
            else
                StartCoroutine(PauseRoutine());
        }
    }

    private IEnumerator PauseRoutine()
    {
        if (isTransitioning) yield break;
        isTransitioning = true;

        isPaused = true;
        Time.timeScale = 0f;

        // Load PauseMenu scene additively
        AsyncOperation loadOp = SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
        while (!loadOp.isDone)
            yield return null;

        if (CameraController.instance != null)
            CameraController.instance.enabled = false;

        isTransitioning = false;
    }

    private IEnumerator ResumeRoutine()
    {
        if (isTransitioning) yield break;
        isTransitioning = true;

        // Re-enable camera before unpausing
        if (CameraController.instance != null)
            CameraController.instance.enabled = true;

        // Start unloading the pause scene
        AsyncOperation unloadOp = SceneManager.UnloadSceneAsync("PauseMenu");
        if (unloadOp == null)
        {
            Debug.LogWarning("PauseMenu scene not found or not loaded.");
            isTransitioning = false;
            yield break;
        }

        // Wait for unload to finish even when paused
        while (!unloadOp.isDone)
            yield return null;

        isPaused = false;
        Time.timeScale = 1f;
        isTransitioning = false;
    }

    public void QuitToStart()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("StartScreen");
    }
}