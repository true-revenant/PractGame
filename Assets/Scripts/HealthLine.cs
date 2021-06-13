using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthLine : MonoBehaviour
{
    private Image healthLine;
    private void Awake()
    {
        healthLine = GetComponent<Image>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseHealthlineValue(float points)
    {
        healthLine.fillAmount += (float)Math.Round(points / 100, 2);
    }

    public void DecreaseHealthlineValue(float points)
    {
        healthLine.fillAmount -= (float)Math.Round(points / 100, 2);
    }
}
