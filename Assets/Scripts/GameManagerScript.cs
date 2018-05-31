using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodeManager))]
public class GameManagerScript : MonoBehaviour {
    private bool gameStart;
    [SerializeField]
    private GameObject DangerSphere;
    [SerializeField]
    private GameObject StartGameObject;
    [SerializeField]
    private GameObject ResetSongObject;
    [SerializeField]
    private GameObject QuitToMenuObject;
    [SerializeField]
    private GameObject StartGamePosition;
    [SerializeField]
    private GameObject ResetSongPosition;
    [SerializeField]
    private GameObject QuitToMenuPosition;
    [SerializeField]
    private GameObject levelChanger;
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private GameObject LeaderBoard;
    private SphereDissolve dissolve;
    private bool menuReady = false;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<HighscoreManager>()
            .SetTextOfHighScoreBoard(gameObject.GetComponent<AudioSource>().clip.name);

    }

    // Update is called once per frame
    void Update () {
        if (gameStart) {
            if (!dissolve.running) {
                GetComponent<NodeManager>().gameStart = true;
            }
        }
        if (GetComponent<NodeManager>().GameOver() && gameStart == true) {
            
            dissolve.EndTriggerEffect();
            gameStart = false;
        }
        else if (GetComponent<NodeManager>().GameOver() && !dissolve.running) {
            EndGame();
            Debug.Log("gameover");
        }

       
	}

    public void ReadyMenu()
    {
        if (!menuReady) {
            menuReady = true;
        dissolve = DangerSphere.GetComponent<SphereDissolve>();
        GameObject child = Instantiate(StartGameObject, new Vector3(0, 0, 0), Quaternion.identity);
        child.transform.parent = StartGamePosition.transform;
        child.GetComponent<StartCut>().SetScript(this);
        }
    }
    public void StartGame()
    {
        dissolve.StartTriggerEffect();
        gameStart = true;
        LeaderBoard.SetActive(false);

        GameObject[] menuItems = GameObject.FindGameObjectsWithTag("StartGame");
        foreach (GameObject item in menuItems)
        {
            Destroy(item, 1.5f);
        }
    }
    public void EndGame()
    {
        GetComponent<NodeManager>().EndGame();
        gameObject.GetComponent<HighscoreManager>()
            .SetTextOfHighScoreBoard(gameObject.GetComponent<AudioSource>().clip.name);

        LeaderBoard.SetActive(true);
        GameObject child = Instantiate(ResetSongObject, new Vector3(0, 0, 0), Quaternion.identity);
        child.transform.parent = ResetSongPosition.transform;
        child.GetComponent<StartCut>().SetScript(this);
       

        GameObject childQuit = Instantiate(QuitToMenuObject, new Vector3(0, 0, 0), Quaternion.identity);
        childQuit.transform.parent = QuitToMenuPosition.transform;
        childQuit.GetComponent<QuitToMenuScript>().SetLevelChanger(levelChanger);

    }
}
