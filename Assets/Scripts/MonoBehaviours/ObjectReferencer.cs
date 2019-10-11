using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ObjectReferencer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI informationText;
    [SerializeField]
    private TextMeshProUGUI conclusionText;
    [SerializeField]
    private CountdownTimer timer;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Hand playerHand;

    public TextMeshProUGUI InformationText { get => informationText; }
    public TextMeshProUGUI ConclusionText { get => conclusionText; }
    public CountdownTimer Timer { get => timer; }
    public Player Player { get => player; }
    public Hand PlayerHand { get => playerHand; }

    public static event UnityAction<ObjectReferencer> OnReferencerActive;
    public static event UnityAction<ObjectReferencer> OnReferencerDestroyed;

    private void OnEnable()
    {
        OnReferencerActive?.Invoke(this);
    }

    private void Awake()
    {
        OnReferencerActive?.Invoke(this);
    }

    private void OnDestroy()
    {
        OnReferencerDestroyed?.Invoke(this);
    }
}