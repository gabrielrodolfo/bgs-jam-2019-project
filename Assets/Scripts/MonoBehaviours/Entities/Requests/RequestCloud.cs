using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCloud : MonoBehaviour
{
    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private SpriteRenderer requestItem;
    [SerializeField]
    private ParticleSystem cloudPuffs;
    [SerializeField]
    private ParticleSystem starEmitter;
    [SerializeField]
    private ParticleSystem heartEmitter;

    public void UpdateItemSprite(Sprite item)
    {
        requestItem.sprite = item;
    }

    private void TurnCloudAtPlayer()
    {
        cloud.transform.LookAt(GameManager.Player.PlayerCamera.transform);
    }

    public void DisablePuffs(){
        cloudPuffs.Stop();
    }

    public void EnablePuffs(){
        cloudPuffs.Play();
    }

    public void ShowCloud(bool show)
    {
        cloud.SetActive(show);
    }

    public void EmitStars()
    {
        starEmitter.Play();
    }

    public void EmitHearts()
    {
        heartEmitter.Play();
    }

    private void Update()
    {
        TurnCloudAtPlayer();
    }

    private void OnEnable()
    {
        EnablePuffs();
    }

    private void OnDisable()
    {
        DisablePuffs();
    }
}
