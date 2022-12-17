using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private static UIManager instance = null;
    public static UIManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public Image[] outlineButtons;

    [Header("Score Text")]
    public Text scoreText;
    public Animator scoreTextPop;

    public void ChangeScore(int score)
    {
        scoreText.text = string.Format("{0:0,##}", score);
        scoreTextPop.SetTrigger("pop");
    }

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
