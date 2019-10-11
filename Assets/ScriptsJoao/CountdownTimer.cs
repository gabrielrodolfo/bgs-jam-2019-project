using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

    string g = "Game Over";
    float currentTime = 0f;
    float startingTime = 5f;

    [SerializeField] Text countdownText;
	// Use this for initialization
	void Start () {
        currentTime = startingTime;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime -= 1f * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
             currentTime = 0;
            countdownText.text ="Game Over";

        }
	}
}
