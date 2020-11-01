using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerScript : MonoBehaviour {

    public Animator animator;
    private int levelToLoad;
    [HideInInspector]
    public static LevelChangerScript instance;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
    }

    public void FadeToBattleField()
    {
        FadeToLevel(StoryManager.instance.GetIndexOfBattleGround());
    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    public void FadeToPrevLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void FadeToLevel(int levelIndex, bool resetMusic = true)
    {
        if (resetMusic)
        {
            Debug.Log("reset music");
            FindObjectOfType<AudioManager>().FadeOutTheme();
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().moveSpeed = 0f;
        }
        
        //player.GetComponent<PlayerMovement>().moveSpeed = 0f;
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().moveSpeed = 0f;
        }
        SceneManager.LoadScene(levelToLoad);
        animator.SetTrigger("FadeIn");
    }
}
