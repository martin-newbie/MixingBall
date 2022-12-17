using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] Animator canvasAnim;
    

    public void OnClickStartButton()
    {
        StartCoroutine(LoadingScene());
    }

    IEnumerator LoadingScene()
    {
        canvasAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }
}
