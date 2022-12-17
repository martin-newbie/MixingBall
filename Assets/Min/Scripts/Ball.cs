using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public Color thisColor;
    SpriteRenderer sprite;

    public void Init(Vector3 target, Color color, float duration = 4f)
    {
        sprite = GetComponent<SpriteRenderer>();

        thisColor = color;
        sprite.color = thisColor;
        StartCoroutine(MoveLogic(target, duration));
    }

    IEnumerator MoveLogic(Vector3 target, float duration) 
    {
        float timer = 0f;
        Vector2 originPos = transform.position;
        while (timer < duration)
        {
            transform.position = Vector3.Lerp(originPos, target, 1 - Mathf.Cos(((timer / duration) * Mathf.PI) / 2));
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        yield break;
    }
}
