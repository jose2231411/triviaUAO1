using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TimerFill : MonoBehaviour
{
    public float max;
    
    public float current;

    public UnityEngine.UI.Image bar;

    private void Update()
    {
        current += Time.deltaTime;
        getCurrentFill();
    }
    public void resetFill() 
    {
        current = 0;
    }
    void getCurrentFill()
    {
        float fillAmount = current / max;
        bar.fillAmount = fillAmount;
    }
}
