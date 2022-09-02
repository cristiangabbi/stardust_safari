using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float waitTime = 1f;


    public void LoadScene(string level)
    {
        StartCoroutine(Load(level));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //coroutine
    IEnumerator Load(string level)
    {
        transition.SetTrigger("Fade In");

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(level);
    }


    private void OnDestroy()
    {
        transition.ResetTrigger("Fade In");
    }

}
