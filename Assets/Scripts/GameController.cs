using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private float initialSpawnDelay;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject ballInventoryUI;
    [SerializeField] private GameObject scoreUI;
    public static bool isPaused { get; private set; }
    private Spawner spawner;
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        Time.timeScale = 1f;
        StartGame();
    }
    public void StartGame()
    {
        Debug.Log("StartGame()");
        spawner.Invoke(nameof(spawner.StartSpawning), initialSpawnDelay);
    }
    public void EndGame()
    {
        Debug.Log("EndGame()");
        spawner.StopSpawning();
        gameOverUI.SetActive(true);
        ballInventoryUI.SetActive(false);
        scoreUI.SetActive(false);
        SceneManager.LoadScene(0);
    }
    public void PauseGame()
    {
        Debug.Log("Pause");
        isPaused = true;
        Time.timeScale = 0f;
    }
    public void UnpauseGame()
    {
        Debug.Log("Pause");
        isPaused = false;
        Time.timeScale = 1f;
    }

}
