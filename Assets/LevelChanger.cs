using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public Animator animator;

    private int levelToLoad;

    [SerializeField]
    private float fadeOutTime = 3.0f;

    [SerializeField]
    private GameObject audioObject;
    AudioSource audioSource;
    bool isFading;
    float fadePerSec;

    private void Start()
    {
        audioSource = audioObject.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            FadeToNextLevel();
            isFading = true;
            fadePerSec = audioSource.volume / fadeOutTime;

        }
        if(isFading){
                audioSource.volume = Mathf.MoveTowards(
                    audioSource.volume, 0, fadePerSec * Time.deltaTime);
            
        }
	}

    public void FadeToNextLevel ()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);

    }

   
}
