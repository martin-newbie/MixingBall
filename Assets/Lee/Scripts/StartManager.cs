using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(1);
    }
}
