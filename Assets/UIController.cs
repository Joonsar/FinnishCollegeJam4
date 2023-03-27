using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider levelSlider;
    public TMP_Text levelText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AdjustLevelSlider(float amount)
    {
        levelSlider.value += amount;

    }

    public void ChangeLevelText(string text)
    {
        levelText.text = text;
    }

    public void SetLevelSlider(float v)
    {
        levelSlider.value = v;
    }
}
