using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class GameManager : Manager
{
    [SerializeField]
    private string gameEndWinPhrase = "Você entregou todos os itens possíveis!\r\nParabéns!";
    [SerializeField]
    private string gameEndPhrase = "Você entregou {0} itens!";

    public static Hand PlayerHand { get; private set; }
    public static Player Player { get; private set; }
    public static CountdownTimer Timer { get; private set; }
    public static TextMeshProUGUI InfoText { get; private set; }
    public static TextMeshProUGUI ConclusionText { get; private set; }

    public static event UnityAction<GameManager> OnObjectsReference;
    public static event UnityAction OnObjectsDereference;

    private List<RequestingEntity> requesters = new List<RequestingEntity>();
    

    private int requestsFulfilled = 0;
    private bool gameHasEnded = false;

    public void Awake()
    {
        ObjectReferencer.OnReferencerActive += OnReferencerActive;
        ObjectReferencer.OnReferencerDestroyed += OnReferencerDestroyed;
        RequestingEntity.OnFinishedRequesting += OnRequesterFinishedRequesting;
        RequestingEntity.OnRequestFulfilled += OnRequestersRequestFulfilled;
    }

    private void OnReferencerActive(ObjectReferencer orf)
    {
        PlayerHand = orf.PlayerHand;
        Player = orf.Player;
        Timer = orf.Timer;
        InfoText = orf.InformationText;
        ConclusionText = orf.ConclusionText;

        Time.timeScale = 1f;
        requesters = FindObjectsOfType<RequestingEntity>().ToList();

        InfoText.text = "";
        ConclusionText.text = "";

        Timer.OnTimerReachedZero.AddListener(FinishTheGame);

        OnObjectsReference?.Invoke(this);
    }

    private void OnReferencerDestroyed(ObjectReferencer orf)
    {
        PlayerHand = null;
        Player = null;
        Timer = null;
        InfoText = null;
        ConclusionText = null;
    }

    private void OnRequesterFinishedRequesting(RequestingEntity re)
    {
        if (requesters.Contains(re)) requesters.Remove(re);

        if (requesters.Count == 0) WinTheGame();
    }

    private void OnRequestersRequestFulfilled(RequestingEntity re, Request request)
    {
        requestsFulfilled++;
    }

    private void ResetVariables()
    {
        requestsFulfilled = 0;
    }

    private void WinTheGame()
    {
        Time.timeScale = 0f;
        ConclusionText.text = gameEndWinPhrase;
        gameHasEnded = true;
    }

    private void FinishTheGame()
    {
        Time.timeScale = 0f;
        ConclusionText.text = string.Format(gameEndPhrase, requestsFulfilled);
        gameHasEnded = true;
    }

    private void Update()
    {
        if (gameHasEnded)
        {
            if (Input.GetButtonDown("Restart"))
            {
                ResetVariables();
                Toolbox.GetManager<GameSceneManager>().ReloadScene();
            }
        }
    }
}