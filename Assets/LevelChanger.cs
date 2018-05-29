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
    [SerializeField]
    private int NextLevel;

    private void Start()
    {
        audioSource = audioObject.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update () {
       
        if(isFading){
                audioSource.volume = Mathf.MoveTowards(
                    audioSource.volume, 0, fadePerSec * Time.deltaTime);
            
        }
	}
    public void ChangeLevel() {

        FadeToNextLevel();
        isFading = true;
        fadePerSec = audioSource.volume / fadeOutTime;
    }

    private void FadeToNextLevel ()
    {
        FadeToLevel(NextLevel);
    }

    private void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    private void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);

    }

   
}
