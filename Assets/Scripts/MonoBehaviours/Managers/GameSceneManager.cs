using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : Manager
{
    private Scene currentLoadedScene;
    
    public event UnityAction<Scene, LoadSceneMode> OnSceneLoaded
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
    public event UnityAction<Scene> OnSceneUnloaded
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

    public AsyncOperation UnloadCurrentSceneAsync(){
        if (currentLoadedScene != null) return SceneManager.UnloadSceneAsync(currentLoadedScene);
        return null;
    }

    public AsyncOperation LoadSceneAsync(string name)
    {
        currentLoadedScene = SceneManager.GetSceneByName(name);
        return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }
}