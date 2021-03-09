using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVariance : MonoBehaviour
{
    public Slider slider;

    public float ChangeValue(float value)
    {
        return slider.value = value;
    }

    float calculPercentage(float value, float valueTotal)
    {
        return (value / valueTotal);
    }
}
