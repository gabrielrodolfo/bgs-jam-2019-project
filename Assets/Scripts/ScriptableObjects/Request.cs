using UnityEngine;

[CreateAssetMenu(menuName="GameAssets/Request")]
public class Request : ScriptableObject
{
    [SerializeField]
    private GameObject requestItem;
    [SerializeField]
    private GameObject reward;
    [SerializeField]
    private Request nextRequest;

    public GameObject RequestItem { get => requestItem; }
    public GameObject Reward { get => reward; }
    public Request NextRequest { get => nextRequest; }

}