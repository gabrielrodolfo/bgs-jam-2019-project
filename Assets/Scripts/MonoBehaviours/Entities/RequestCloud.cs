using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestCloud : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer requestItem;
    [SerializeField]
    private ParticleSystem cloudPuffs;

    public void UpdateItemSprite(Sprite item)
    {
        requestItem.sprite = item;
    }

    private void DisablePuffs(){
        cloudPuffs.Stop();
    }

    private void EnablePuffs(){
        cloudPuffs.Play();
    }
}
