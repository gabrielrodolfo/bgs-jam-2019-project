using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager
{
    public static Hand PlayerHand { get; private set; }

    public void Start()
    {
        GameSceneManager.OnSceneLoaded += SearchMainObjects;
        GameSceneManager.OnSceneUnloaded += DereferenceMainObjects;

        PlayerHand = FindObjectOfType<Hand>();
    }

    private void SearchMainObjects(Scene scene, LoadSceneMode mode)
    {
        PlayerHand = FindObjectOfType<Hand>();
    }

    private void DereferenceMainObjects(Scene scene)
    {
        PlayerHand = null;
    }
}