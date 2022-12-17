using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCircle : MonoBehaviour
{

    [SerializeField] ParticleSystem pointEffect;
    [SerializeField] ParticleSystem ringEffect;

    SpriteRenderer sprite;
    Animator anim;

    Color curColor;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void ChangeColor(Color color)
    {
        curColor = color;

        CirclePop();
        sprite.color = curColor;
    }

    public void CirclePop()
    {
        anim.SetTrigger("pop");
    }

    int comboCount = 0;
    bool isCombo = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {

            if(collision.GetComponent<Ball>().thisColor != curColor)
            {
                comboCount = 0;
                isCombo = false;
                InGameManager.Instance.curHp -= 1f;
                return;
            }

            comboCount++;
            isCombo = true;

            InGameManager.Instance.GetScore(5);
            PlayEffect();
        }


        void PlayEffect()
        {
            CirclePop();

            PointEffectColor();
            pointEffect.Play();
            ringEffect.Play();
        }

        void PointEffectColor()
        {
            var pointMain = pointEffect.main;
            var ringMain = ringEffect.main;

            pointMain.startColor = curColor;
            ringMain.startColor = curColor;
        }
    }


}
