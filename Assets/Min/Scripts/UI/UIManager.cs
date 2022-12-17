using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Image[] outlineButtons;

    public void ChangeRed()
    {
        InGameManager.Instance.ChangeColor(true, false, false);
        SetOutlineActive(0);
    }

    public void ChangeBlue()
    {
        InGameManager.Instance.ChangeColor(false, true, false);
        SetOutlineActive(1);
    }

    public void ChangeGreen()
    {
        InGameManager.Instance.ChangeColor(false, false, true);
        SetOutlineActive(2);
    }

    void SetOutlineActive(int index)
    {
        outlineButtons[index].gameObject.SetActive(!outlineButtons[index].gameObject.activeInHierarchy);
    }
}
