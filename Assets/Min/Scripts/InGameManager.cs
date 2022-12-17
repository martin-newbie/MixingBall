using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    RED,
    BLUE,
    GREEN,
    MAGENTA,
    YELLOW,
    TEAL,
    WHITE,
    BLACK
}

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance = null;
    public static InGameManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public MainCircle mainCircle;
    public Ball ballPrefab;

    bool red, blue, green;
    bool isGameActive;

    float ballDuration = 4f;

    Coroutine mainLogic;

    void Start()
    {
        mainLogic = StartCoroutine(GameMainLogic());
    }

    IEnumerator GameMainLogic()
    {
        isGameActive = true;

        red = false;
        blue = false;
        green = false;

        List<bool> randList;


        yield return null;
        mainCircle.ChangeColor(Color.black);

        while (isGameActive)
        {
            if (getBallSpawnChance())
            {
                randList = new List<bool>(3)
                {
                    true,
                    (bool)ChooseOne(true, false),
                    (bool)ChooseOne(true, false),
                };

                var combined = GetCombinedColor(randBool(randList), randBool(randList), randBool(randList));
                SpawnBall(combined, (bool)ChooseOne(true, false));
            }
            yield return null;
        }

        yield break;

        bool getBallSpawnChance()
        {
            return Random.Range(0f, 100f) <= 0.5f;
        }
        bool randBool(List<bool> list)
        {
            int rand = Random.Range(0, list.Count);
            bool result = list[rand];
            list.RemoveAt(rand);

            return result;
        }
    }

    public void ChangeColor(bool _red, bool _blue, bool _green)
    {
        if (_red) red = !red;
        if (_blue) blue = !blue;
        if (_green) green = !green;

        mainCircle.ChangeColor(GetCombinedColor(red, blue, green));
    }
    Ball SpawnBall(Color color, bool isHorizontal)
    {
        if (color == Color.black) return null;

        float yHalf = Camera.main.orthographicSize;
        float xHalf = yHalf * Camera.main.aspect;
        Vector2 spawnPos;

        if (isHorizontal)
        {
            spawnPos = new Vector2(Random.Range(-xHalf, xHalf), (float)ChooseOne(-yHalf, yHalf));
        }
        else
        {
            spawnPos = new Vector2((float)ChooseOne(-xHalf, xHalf), Random.Range(-yHalf, yHalf));
        }

        Ball ballTmp = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        ballTmp.Init(mainCircle.transform, color, ballDuration);
        return ballTmp;

    }
    object ChooseOne(params object[] contents)
    {
        int rand = Random.Range(0, contents.Length);
        return contents[rand];
    }
    Color GetCombinedColor(bool _red, bool _blue, bool _green)
    {
        Color result = Color.black;

        result += _red ? Color.red : Color.black;
        result += _blue ? Color.blue : Color.black;
        result += _green ? Color.green : Color.black;
        result.a = 1f;

        return result;
    }
}
