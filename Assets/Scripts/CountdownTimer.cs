using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 30f;
    public TMP_Text countdownText;

    private EndGameManager endManager;

    private bool _isOutOfTime = false;

    private bool _stopCountdown = false;

    // Start is called before the first frame update
    void Start()
    {
        endManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<EndGameManager>();
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 5)
        {
            countdownText.color = Color.red;
        }
        if(_stopCountdown == false)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
        }

        // Player has survived, stop timer and end game
        if(currentTime <= 0)
        {
          if(_isOutOfTime == false)
          {
                endManager.EndGame(true);
                _isOutOfTime = true;
          }
           currentTime = 0;
        }
    }

    public bool isOutOfTime
    {
        get { return _stopCountdown; }
        set { _stopCountdown = value; }
    }
}
