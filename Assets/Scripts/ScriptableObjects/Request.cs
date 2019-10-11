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
    [Space]
    [SerializeField]
    private AudioClip acceptSfx;
    [SerializeField]
    private float volume = 1f;
    [SerializeField]
    private float pitch = 1f;
    [SerializeField]
    private bool sfxIs3D = true;

    public RequestItem RequestItem { get => requestItem; }
    public RequestItem Reward { get => reward; }
    public Request NextRequest { get => nextRequest; }
    public AudioClip AcceptSFX { get => acceptSfx; }
    public float Volume { get => volume; }
    public float Pitch { get => pitch; }
    public bool SFXis3D { get => sfxIs3D; }
}