using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCanvas : MonoBehaviour
{
    public Image hpImage;

    Color targetColor;
    bool isHp = false;

    private void Update()
    {
        if (!isHp)
            hpImage.color = Color.Lerp(Color.red, Color.green, hpImage.fillAmount);
        else
        {
            hpImage.color = targetColor;
        }
    }

    public HpCanvas SetHPGauge(float fill, bool _isHp = false)
    {
        hpImage.fillAmount = fill;
        isHp = _isHp;

        return this;
    }

    public HpCanvas SetGaugeColor(Color color)
    {
        targetColor = color;
        return this;
    }
}
