using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CountdownTimer : MonoBehaviour {

    [SerializeField] 
    private TextMeshProUGUI countdownText;
    [SerializeField]
    private float startingTimeInSeconds = 5f;

    private int currentMinutes;
    private float currentSeconds;

    private string minutesAsString
    {
        get => currentMinutes.ToString("D2");
    }
    private string secondsAsString
    {
        get => Mathf.FloorToInt(currentSeconds).ToString("D2");
    }
    private string time
    {
        get => minutesAsString + ":" + secondsAsString;
    }
    public UnityEvent OnTimerReachedZero { get; set; } = new UnityEvent();

	void Start () {
        currentSeconds = startingTimeInSeconds;
        CalculateTime();
	}
	
	void Update () {
        CalculateTime();
        countdownText.text = time;

        if (currentMinutes <= 0 && Mathf.FloorToInt(currentSeconds) == 0)
        {
            OnTimerReachedZero.Invoke();
        }
	}

    private void CalculateTime()
    {
        currentSeconds -= Time.deltaTime;
        if (currentSeconds < 0)
        {
            currentMinutes += Mathf.FloorToInt(currentSeconds / 60);
            currentSeconds = 60 + (currentSeconds % 60f);
        }

        if (currentSeconds > 59)
        {
            currentMinutes += Mathf.FloorToInt(currentSeconds / 60);
            currentSeconds %= 60f;
        }
    }
}
