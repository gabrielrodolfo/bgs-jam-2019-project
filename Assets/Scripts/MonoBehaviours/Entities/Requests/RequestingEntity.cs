using UnityEngine;
using System;
using System.Collections;

public class RequestingEntity : MonoBehaviour
{
    [SerializeField]
    private Request initialRequest;
    [SerializeField]
    private RequestCloud cloud;
    [SerializeField]
    private CloudTriggerArea cloudTriggerArea;
    [SerializeField]
    private RequestItemDropArea dropArea;
    [SerializeField]
    private Transform rewardSpawnPoint;

    public Request CurrentRequest 
    { get; private set; }

    private void Start()
    {
        if (cloud == null) cloud = GetComponentInChildren<RequestCloud>();

        CurrentRequest = initialRequest;
        cloud.UpdateItemSprite(CurrentRequest.RequestItem.RepresentingImage);
        cloud.gameObject.SetActive(false);

        cloudTriggerArea.OnPlayerEnter += OnPlayerEnter;
        cloudTriggerArea.OnPlayerExit += OnPlayerExit;
        dropArea.OnItemReceived += OnItemReceived;
    }

    public void FulfillRequest(){
        if (CurrentRequest.Reward != null)
        {
            Instantiate(CurrentRequest.Reward.gameObject, rewardSpawnPoint.position, Quaternion.identity);
        }

        if (CurrentRequest.SFXis3D) 
        {
            Toolbox.GetManager<AudioManager>()
                .PlaySFXAt(CurrentRequest.AcceptSFX, transform.position, CurrentRequest.Volume, CurrentRequest.Pitch);
        }
        else
        {
            Toolbox.GetManager<AudioManager>()
                .PlaySFX(CurrentRequest.AcceptSFX, CurrentRequest.Volume, CurrentRequest.Pitch);
        }
        CurrentRequest = CurrentRequest.NextRequest;
        

        if (CurrentRequest == null) 
        {
            StartCoroutine(FinishRequesting());
        }
        else 
        {
            cloud.UpdateItemSprite(CurrentRequest.RequestItem.RepresentingImage);
            cloud.EmitStars();
        }
    }

    public IEnumerator FinishRequesting(){
        cloud.DisablePuffs();
        cloud.ShowCloud(false);
        cloud.EmitHearts();
        yield return new WaitForSeconds(2f);

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Destroy(this);
    }

    private void OnPlayerEnter()
    {
        cloud.gameObject.SetActive(true);
    }

    private void OnPlayerExit()
    {
        cloud.gameObject.SetActive(false);
    }

    private void OnItemReceived(RequestItem item)
    {
        if (CurrentRequest == null) return;

        if (item.ItemName.Equals(CurrentRequest.RequestItem.ItemName))
        {
            Destroy(item.gameObject);
            FulfillRequest();
        }
    }
}