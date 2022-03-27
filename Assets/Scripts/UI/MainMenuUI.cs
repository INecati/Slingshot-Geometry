using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnExit;
    [SerializeField] private TMP_Text highScoreText;
    void Start()
    {
        btnStart.onClick.AddListener(delegate { StartGame(); });
        btnExit.onClick.AddListener(delegate { ExitGame(); });

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
        highScoreText.text = highScore.ToString();
    }

    private void StartGame()
    {
        Debug.Log(nameof(StartGame));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void ExitGame()
    {
        Debug.Log(nameof(ExitGame));
        Application.Quit();
    }
}
