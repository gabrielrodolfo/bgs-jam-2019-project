using UnityEngine;

[CreateAssetMenu(menuName="GameAssets/Request")]
public class Request : ScriptableObject
{
    [SerializeField]
    private RequestItem requestItem;
    [SerializeField]
    private RequestItem reward;
    [SerializeField]
    private Request nextRequest;

    public RequestItem RequestItem { get => requestItem; }
    public RequestItem Reward { get => reward; }
    public Request NextRequest { get => nextRequest; }

}