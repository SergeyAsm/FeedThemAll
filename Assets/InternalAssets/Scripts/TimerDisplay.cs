using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public TMPro.TMP_Text minutes;
    public TMPro.TMP_Text seconds;

    // Update is called once per frame
    void Update()
    {
        var avalableTime = GameController.instance.level.time;
        int minutesTime = (int)(avalableTime / 60);
        int secondsTime = (int)(avalableTime % 60);
        minutes.text = minutesTime.ToString();
        seconds.text = secondsTime.ToString();
    }
}
