using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text txtScore;
    public const string HighScoreKey = "HighScore";
    public static float Score { get; private set; }
    void Start()
    {
        txtScore.text = "0";
    }

    public void AddScore(float value)
    {
        Score += value;
        txtScore.text = Score.ToString();
    }
    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (Score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(Score));
        }
    }
}
