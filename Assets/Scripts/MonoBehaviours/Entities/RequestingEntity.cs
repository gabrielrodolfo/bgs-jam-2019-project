using UnityEngine;

public class RequestingEntity : MonoBehaviour
{
    [SerializeField]
    private Request initialRequest;

    public Request CurrentRequest { get; private set; }

    private void Start()
    {
        CurrentRequest = initialRequest;
    }

    public void FulfillRequest(){
        CurrentRequest = CurrentRequest.NextRequest;

        if (CurrentRequest == null) FinishRequesting();
    }

    public void FinishRequesting(){
        Destroy(this);
    }
}