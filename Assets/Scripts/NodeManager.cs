using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int points = 0;
    [SerializeField]
    private int multiplier = 1;
    [SerializeField]
    private int multiplierMinimum = 5;
    [SerializeField]
    private int pointIncrease = 1;
    //HP
    [Header("Hit Points")]
    [SerializeField]
    private int hitPoints;
    [SerializeField]
    private int hpIncrease = 1;
    [SerializeField]
    private int hpDecrease = 5;
    [SerializeField]
    private int hpMultiplier = 1;
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
    private bool useSeed = false;
    private float curTime = 0.0f;
    private float speed;
    private float travelTime;
    private AudioSource PreAudio;
    private AudioSource Track;
    private Vector3[] spawnLocations;
    
    //Difficulty Enum
    enum DIFFICULTY
    {
        EASY, MEDIUM, HARD
    };

    // Use this for initialization
    void Start()
    {
       

        AdjustDifficulty();
        CalculateTravelTime();
        MoveNode movNode = nodePrefab.GetComponent<MoveNode>();
        movNode._manager = this;
        movNode.speed = -speed;
        PreAudio = GetComponent<AudioSource>();
       Track =  MusicManager.GetComponent<AudioSource>();
        if (useSeed)
        {
            if (debugPrint)
            {
                Debug.Log("Name and HashCode : " + PreAudio.clip.name + ", " + PreAudio.clip.name.GetHashCode());
            }
            Random.InitState(PreAudio.clip.name.GetHashCode());
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        curTime += Time.deltaTime;
        if (curTime >= warmUpTime && !PreAudio.isPlaying)
        {
            PreAudio.Play();
        }

        if (curTime>= (warmUpTime+travelTime) && !Track.isPlaying)
        {
            Track.Play();
        } 
        NodePlacer(AudioBeat());

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
            Debug.Log("Sound Value: " + packagedData);
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
            Instantiate(nodePrefab, spawnLocations[i], Quaternion.identity);
            recentSpawn = true;

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
        //On hit increase hp and score
        if (multiplier > multiplierMinimum)
        {
            points += (pointIncrease * multiplier);
        }
        else
        {
            hitPoints += hpIncrease;
            points += pointIncrease;

        }
        multiplier++;
        if (debugPrint)
        {
            Debug.Log("Object HIT!!. Points: " + points + " Multiplier: " + multiplier);
        }
    }
    public void NodeMiss()
    {
        if (hpMultiplier > 0)
        {
            hitPoints -= (hpDecrease * hpMultiplier);

        }
        multiplier = 1;
        hpMultiplier++;
    }

}
