using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpCanvas : MonoBehaviour
{
    public Image hpImage;

    float targetFill;

    private void Update()
    {
        hpImage.color = Color.Lerp(Color.red, Color.green, targetFill);
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, targetFill, Time.deltaTime * 15f);
    }

    public void SetHPGauge(float fill)
    {
        targetFill = fill;
    }
}
