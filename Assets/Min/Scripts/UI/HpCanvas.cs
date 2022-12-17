using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCanvas : MonoBehaviour
{
    public Image hpImage;

    float targetFill;
    Color targetColor;
    bool isHp = false;

    private void Update()
    {
        if (!isHp)
            hpImage.color = Color.Lerp(Color.red, Color.green, targetFill);
        else
        {
            hpImage.color = targetColor;
        }

        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, targetFill, Time.deltaTime * 15f);
    }

    public HpCanvas SetHPGauge(float fill, bool _isHp = false)
    {
        targetFill = fill;
        isHp = _isHp;

        return this;
    }

    public HpCanvas SetGaugeColor(Color color)
    {
        targetColor = color;
        return this;
    }
}
