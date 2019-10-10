using UnityEngine;

public class RequestingEntity : MonoBehaviour
{
    [SerializeField]
    private Request initialRequest;
    [SerializeField]
    private RequestCloud cloud;

    public Request CurrentRequest { get; private set; }

    private void Start()
    {
        if (cloud == null) cloud = GetComponentInChildren<RequestCloud>();

        CurrentRequest = initialRequest;
        cloud.UpdateItemSprite(CurrentRequest.RequestItem.RepresentingImage);
        cloud.gameObject.SetActive(false);
    }

    public void FulfillRequest(){
        CurrentRequest = CurrentRequest.NextRequest;

        if (CurrentRequest == null) FinishRequesting();
    }

    public void FinishRequesting(){
        Destroy(this);
    }
}