using System.Collections;
using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    [Header("Stone building toggle")]
    public string stoneSceneName = "StoneBuildingScene";   // <-- set actual scene name in Inspector
    public KeyCode stoneToggleKey = KeyCode.B;             // <-- key to open/close
    private bool stoneSceneOpen = false;

    private void Awake()
    {
        // Singleton pattern
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
        // Don't allow toggling while the game is paused
        if (Time.timeScale == 0f)
            return;

        if (Input.GetKeyDown(stoneToggleKey))
        {
            if (!stoneSceneOpen)
                OpenStoneScene();
            else
                CloseStoneScene();
        }
    }

    private void OpenStoneScene()
    {
        LoadAdditive(stoneSceneName);

        // Optional: disable world camera so the stone scene can control its own
        if (CameraController.instance != null)
            CameraController.instance.enabled = false;

        stoneSceneOpen = true;
    }

    private void CloseStoneScene()
    {
        UnloadAdditive(stoneSceneName);

        // Re-enable world camera
        if (CameraController.instance != null)
            CameraController.instance.enabled = true;

        stoneSceneOpen = false;
    }

    // Basic load
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("I am pressing the button");
    }

    // Additive load (like "BuildingArea" or "PauseMenu")
    public void LoadAdditive(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    // Unload additive scenes
    public void UnloadAdditive(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
    }

    // Optional: transition with fade or delay
    public void LoadSceneWithDelay(string sceneName, float delay = 1f)
    {
        StartCoroutine(DelayedLoad(sceneName, delay));
    }

    private IEnumerator DelayedLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(sceneName);
    }

    // Quit for Start or Pause menus
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}