using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeManager : MonoBehaviour
{
    //This Script spawns nodes at the given locations based on selected Difficulty, and calculates the time it takes to travel from there to the selected node Trigger

    //Debugging
    [Header("Debug")]
    [SerializeField]
    private bool debugPrint = false;
    //Variables
    [Header("Node Prefab")]
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private float laneDistance = 1.5f;
    //Difficulty
    [Header("Difficulty Settings")]
    [SerializeField]
    private DIFFICULTY difficulty;
    [SerializeField]
    private float easySpeed = 2.0f;
    [SerializeField]
    private float mediumSpeed = 2.5f;
    [SerializeField]
    private float hardSpeed = 3.0f;
    [SerializeField]
    private GameObject middleLane;
    [SerializeField]
    private GameObject nodeTrigger;
    [SerializeField]
    private float warmUpTime;
    //Points
    [Header("Points")]
    [SerializeField]
    GameObject ScoreField;
    [SerializeField]
    GameObject ComboField;
    [SerializeField]
    GameObject MultiplierField;

    [SerializeField]
    private int points = 0;
    [SerializeField]
    private int combo = 0;
    [SerializeField]
    private int multiplier = 0;
    [SerializeField]
    private int multiplierMax = 10;
    [SerializeField]
    private int multiplierIncreaseThreshold = 5;
    [SerializeField]
    private int pointIncrease = 1;
    //HP
    [Header("Hit Points")]
    [SerializeField]
    private GameObject healthImage;
    private HealthBar healthBar;

    //Audio
    [Header("Audio Node Creation Settings")]
    [SerializeField]
    private GameObject MusicManager;
    [SerializeField]
    private int detail = 500;
    [SerializeField]
    private float beatMin = 60;
    [SerializeField]
    private float maxNodesPerSec = 2;
    private float lastNoteTime = 0.0f;
    private bool recentSpawn = false;
    [SerializeField]
    private bool useSeed = true;
    private float curTime = 0.0f;
    private float speed;
    private float travelTime;
    private AudioSource PreAudio;
    private AudioSource Track;
    private Vector3[] spawnLocations;
    private float lastSpawn;
    private float stopHealthTime = 3.0f;
    public bool gameStart = false;
    private bool isPlaying = false;
    private bool gameOver = false;
    private bool trackIsPlaying;

    //ResetValues
    [SerializeField]
    private int resetPoints;
    [SerializeField]
    private int resetCombo;
    [SerializeField]
    private int resetmultiplier;
    //Difficulty Enum
    enum DIFFICULTY
    {
        EASY, MEDIUM, HARD
    };

    // Use this for initialization
    void Start()
    {
        healthBar = healthImage.GetComponent<HealthBar>();
        resetPoints = points;
        resetCombo = combo;
        resetmultiplier = multiplier;

}
    
    public void StartGame() {
        AdjustDifficulty();
        CalculateTravelTime();
        MoveNode movNode = nodePrefab.GetComponent<MoveNode>();
        movNode._manager = this;
        movNode.speed = -speed;
        PreAudio = GetComponent<AudioSource>();
        Track = MusicManager.GetComponent<AudioSource>();
        if (useSeed)
        {
            if (debugPrint)
            {
                Debug.Log("Name and HashCode : " + PreAudio.clip.name + ", " + PreAudio.clip.name.GetHashCode());
            }
            Random.InitState(PreAudio.clip.name.GetHashCode());
        }
        isPlaying = true;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            NodeHit();
        }
        if (Input.GetKeyDown(KeyCode.D)){
            SetGameOver();
        }
    }
    void FixedUpdate()
    {

        if (gameStart)
        {
            if (!isPlaying)
            {
                StartGame();
            }
            else
            {
                curTime += Time.deltaTime;
                if (curTime >= warmUpTime && !PreAudio.isPlaying)
                {
                    PreAudio.Play();
                }

                if (curTime >= (warmUpTime + travelTime) && !Track.isPlaying)
                {
                    Invoke("GameOver", Track.clip.length);
                    Track.Play();
                    healthBar.SetRunning(true);
                    trackIsPlaying = true;
                }
                NodePlacer(AudioBeat());
                if (healthBar.GetHealth() == 0.0f)
                {
                    SetGameOver();
                }
                if (lastSpawn > stopHealthTime && trackIsPlaying)
                {
                    healthBar.SetRunning(false);
                }
                else if (trackIsPlaying)
                {
                    healthBar.SetRunning(true);
                    lastSpawn += Time.deltaTime;
                }
            }
        }


    }
    // Update is called once per frame
    public void EndGame()
    {

        isPlaying = false;
        gameOver = false;
        trackIsPlaying = false;
        gameStart = false;
        Track.Stop();
        PreAudio.Stop();
        curTime = 0;
        SaveScore();
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (GameObject gameObject in nodes) {
            Destroy(gameObject);
        }
        healthBar.ResetHealth();
        healthBar.SetRunning(false);
        points = resetPoints;
        combo = resetCombo;
        multiplier = resetmultiplier;
        SetFields();
    }
    void SetFields() {
        if (ScoreField != null)
        {
            if (ScoreField.GetComponent<TextMeshPro>() != null)
            {
                ScoreField.GetComponent<TextMeshPro>().text = points + "";
            }
        }
        if (ComboField != null)
        {
            if (ComboField.GetComponent<TextMeshPro>() != null)
            {
                ComboField.GetComponent<TextMeshPro>().text = multiplier + "x";
            }
        }
        if (MultiplierField != null)
        {
            if (MultiplierField.GetComponent<TextMeshPro>() != null)
            {
                MultiplierField.GetComponent<TextMeshPro>().text = "Combo: " + combo;
            }
        }
    }

    void SaveScore() {
    }

    public void SetGameOver() {
        gameOver = true;
         
    }
    public bool GameOver() {
        return gameOver;
    }
    public void ResetGame() {
       

    }
    public void TogglePause() {
        if (gameStart)
        {
            gameStart = false;
            PreAudio.Pause();
            Track.Pause();
            healthBar.SetRunning(false);
        }
        else {
            gameStart = true;
            PreAudio.Play();
            if (trackIsPlaying) { 
            Track.Play();
            healthBar.SetRunning(true);
            }
        }
    }
    /// <summary>
    /// Detects the output volume to simulate beats
    /// </summary>
    private float AudioBeat()
    {

        float[] info = new float[detail];
        PreAudio.GetOutputData(info, 0);

        float packagedData = 0.0f;
        for (int i = 0; i < info.Length; i++)
        {
            packagedData += System.Math.Abs(info[i]);
        }
        if (debugPrint)
        {
        }
        return packagedData;

    }
    //Places a Node at a random lane when a high enough beat is played
    private void NodePlacer(float beat) {
        if (recentSpawn) {
            //lastNoteTime
            //maxNodesPerSec
            lastNoteTime += Time.deltaTime;
            if (lastNoteTime >= 1 / maxNodesPerSec) {
                recentSpawn = false;
                lastNoteTime = 0.0f;
            }
        }
        if (beat >= beatMin && !recentSpawn) {
            int i = Random.Range(0,spawnLocations.Length);
            float rand = getRandomRotation(Random.Range(1,5));

            GameObject node = Instantiate(nodePrefab, spawnLocations[i], Quaternion.identity);
            node.transform.rotation = Quaternion.AngleAxis(rand, node.transform.forward);
            lastSpawn = 0.0f;
            recentSpawn = true;

        }
        
    }
    private float getRandomRotation(int number) {

        switch (number) {
            case 1: return 0.0f;
            case 2: return 90.0f;
            case 3: return 180.0f;
            case 4: return 270.0f;
            default: return 0.0f;
                
        }
    }
    private void CalculateTravelTime()
    {
        //Calculate traveltime 
        //Time = Distance%Speed 		
        //Since both are measured in unity meters pr second our expected input will be pr sec
        //We are only interested in traveling along the Z Axis So we will be traveling in a straight line
       
        float distance = middleLane.transform.position.z - nodeTrigger.transform.position.z;
        if (distance < 0)
        {
            distance = distance * -1;
        }
        travelTime = distance / speed;
        if (debugPrint)
        {
            Debug.Log("Travel Time" + travelTime);
        }

    }
    private void AdjustDifficulty()
    {
        float z = middleLane.transform.position.z;
        float x = middleLane.transform.position.x;
        float y = middleLane.transform.position.y;
        switch (difficulty)
        {
            case DIFFICULTY.EASY:
                speed = easySpeed;
                spawnLocations = new[] {
                    middleLane.transform.position,
                    new Vector3((x + laneDistance),y,z),
                    new Vector3((x-laneDistance),y,z) };
                break;
            case DIFFICULTY.MEDIUM:
                speed = mediumSpeed;
                spawnLocations = new[] {
                    middleLane.transform.position,
                    new Vector3((x + laneDistance),y,z),
                    new Vector3((x-laneDistance),y,z),
                    new Vector3((x + laneDistance*2),y,z),
                    new Vector3((x-laneDistance*2),y,z) };

                break;
            case DIFFICULTY.HARD:
                speed = hardSpeed;
                spawnLocations = new[] {
                    middleLane.transform.position,
                    new Vector3((x + laneDistance),y,z),
                    new Vector3((x-laneDistance),y,z),
                    new Vector3((x + laneDistance*2),y,z),
                    new Vector3((x-laneDistance*2),y,z) };
                break;
        }
    }
    public void NodeHit()
    {
        combo++;
        //On hit increase hp and score
        if (combo % multiplierIncreaseThreshold == 0 && multiplier < multiplierMax) {
            multiplier = combo / multiplierIncreaseThreshold;
        }
        points += (pointIncrease * multiplier);
        healthBar.HealthIncrease();
        if (debugPrint)
        {
            Debug.Log("Object HIT!!. Points: " + points + " Multiplier: " + combo);
        }
        SetFields();
    }
    public void NodeMiss()
    {
        healthBar.HealthDecrease();
        combo = 0;
        multiplier = 1;
    }

}
