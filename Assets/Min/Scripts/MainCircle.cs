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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball") && collision.GetComponent<Ball>().thisColor == curColor)
        {
            CirclePop();

            PointEffectColor();
            pointEffect.Play();
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
