using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public bool start;

    private TextMeshProUGUI timerText;
    private TimerData data;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        data = new TimerData(0f, 0f); //Конструктор задает значения 0 всем полям
    }

    /*
     * По стандарту обновление раз в 2 м/сек 
    */
    void FixedUpdate()
    {
        if(start)
        {
            data.addSeconds(Time.fixedDeltaTime);
            timerText.text = data.minutes + ":" + data.seconds.ToString("#.##");
        }

    }
}
