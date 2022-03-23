using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text txtScore;
    public const string HighScoreKey = "HighScore";
    public float score { get; private set; }
    void Start()
    {
        txtScore.text = "0";
    }

    public void AddScore(float value)
    {
        score += value;
        txtScore.text = score.ToString();
    }
    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }
}
