using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationListener : MonoBehaviour
{
    // this is brittle and sucks
    private void AnimEnableAnObject(string o)
    {
        Transform t;
        t = gameObject.transform.Find(o);
        t.gameObject.SetActive(true);
    }

    // this is brittle and sucks
    private void AnimDisableAnObject(string o)
    {
        Transform t;
        t = gameObject.transform.Find(o);
        t.gameObject.SetActive(false);
    }

    private void AnimReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
