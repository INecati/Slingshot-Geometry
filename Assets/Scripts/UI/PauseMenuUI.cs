using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Button btnPause;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnExit;
    [SerializeField] private TMP_Text txtScore;
    void Start()
    {
        btnPause.onClick.AddListener(delegate { PauseGame(); });
        btnContinue.onClick.AddListener(delegate { UnpauseGame(); });
        btnExit.onClick.AddListener(delegate { ExitToMainMenu(); });
    }

    private void PauseGame()
    {
        txtScore.SetText(ScoreSystem.Score.ToString());
        mainMenu.SetActive(true);
        gameController.PauseGame();
    }
    private void UnpauseGame()
    {
        mainMenu.SetActive(false);
        gameController.UnpauseGame();
    }
    private void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
