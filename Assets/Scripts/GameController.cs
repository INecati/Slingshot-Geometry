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
    private Spawner spawner;
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        Debug.Log("StartGame()");
        //FindObjectOfType<Spawner>().StartSpawning();
        //Invoke(nameof(StartSpawning), initialSpawnDelay);
        //Spawner spawner = FindObjectOfType<Spawner>();
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
}
