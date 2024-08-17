using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private int LevelToLoad;

    // Update is called once per frame
    void Update()
    {
    }

    public void FadeToLevel(int LevelIndex)
    {
        LevelToLoad = LevelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
