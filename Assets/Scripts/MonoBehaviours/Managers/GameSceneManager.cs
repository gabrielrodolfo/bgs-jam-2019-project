using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : Manager
{
    [SerializeField]
    private string firstSceneToLoad = "Level";
    private string currentLoadedSceneName;
    private Coroutine currentReloadCoroutine = null;
    
    public static event UnityAction<Scene, LoadSceneMode> OnSceneLoaded
    {
        add
        {
            SceneManager.sceneLoaded += value;
        }
        remove
        {
            SceneManager.sceneLoaded -= value;
        }
    }
    public static event UnityAction<Scene> OnSceneUnloaded
    {
        add
        {
            SceneManager.sceneUnloaded += value;
        }
        remove
        {
            SceneManager.sceneUnloaded -= value;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene loadedScene = SceneManager.GetSceneAt(i);
            if (loadedScene.name != "Base")
            {
                currentLoadedSceneName = loadedScene.name;
                break;
            }
        }

        if (currentLoadedSceneName == null) LoadSceneAsync(firstSceneToLoad);
    }

    public AsyncOperation UnloadCurrentSceneAsync(){
        if (!string.IsNullOrEmpty(currentLoadedSceneName)) {
            SceneManager.UnloadSceneAsync(currentLoadedSceneName);
            currentLoadedSceneName = null;
        }
        return null;
    }

    public AsyncOperation LoadSceneAsync(string name)
    {
        currentLoadedSceneName = name;
        return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }

    [ContextMenu("Reload Loaded Scene")]
    public void ReloadScene()
    {
        if (currentReloadCoroutine == null)
        {
            currentReloadCoroutine = StartCoroutine(ReloadRoutine());
        }
    }

    private IEnumerator ReloadRoutine()
    {
        AsyncOperation asyo = SceneManager.UnloadSceneAsync(currentLoadedSceneName);
        
        while (!asyo.isDone) yield return null;

        asyo = SceneManager.LoadSceneAsync(currentLoadedSceneName, LoadSceneMode.Additive);

        while (!asyo.isDone) yield return null;

        currentReloadCoroutine = null;
    }
}