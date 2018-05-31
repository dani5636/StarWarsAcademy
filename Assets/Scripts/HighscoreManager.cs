using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreManager : MonoBehaviour {

    TextMeshPro textMeshPro;
    [SerializeField]
    private int highscoreLength;
    [SerializeField]
    private GameObject rankingTextField;
    public void SaveHighScore(string name, int points)
    {
        bool scoreSet = false;
        for (int i = 1; i < (highscoreLength + 1); i++) {
            if (points > PlayerPrefs.GetInt(name + i, 0) && !scoreSet) {
                if (PlayerPrefs.GetInt(name + i, 0) == 0) {

                    scoreSet = true;
                }
                int lastHighScore = PlayerPrefs.GetInt(name + i, 0);
                PlayerPrefs.SetInt(name + i, points);
                points = lastHighScore;
            }
        }
        PlayerPrefs.Save();

    }
    public void SetTextOfHighScoreBoard(string songName){
        rankingTextField.GetComponent<TextMeshPro>().text = GetHighScore(songName);
    }
    private string GetHighScore(string name) {
        string highScore = "";
        for (int i = 1; i < (highscoreLength + 1); i++)
        {
            highScore += i + ". "+PlayerPrefs.GetInt(name + i, 0) + "\n";

        }
        return highScore;
    }
}
