using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnAreaWidth;

    [SerializeField] private float initialSpawnInterval;
    [SerializeField] private float spawnIntervalDecreaseRate;
    public float spawnInterval;
    [SerializeField] private GameObject[] shapes;

    public bool isSpawning = false;


    public List<Func<GameObject, float>> spawnFunctionList = new List<Func<GameObject, float>>();
    private void Awake()
    {
        spawnFunctionList.Add(SpawnOne);
        spawnFunctionList.Add(SpawnTwo);
        spawnFunctionList.Add(SpawnThree);
    }
    private void Start()
    {
        //Invoke(nameof(StartSpawning), initialSpawnDelay);
        spawnInterval = initialSpawnInterval;
        
    }

    private IEnumerator IncreaseDifficulty()
    {
        yield return new WaitForSeconds(20f);
        while (true)
        {
            spawnInterval *= spawnIntervalDecreaseRate;
            yield return new WaitForSeconds(20f);
        }
    }
    public void StartSpawning() {
        if (!isSpawning)
        {
            StartCoroutine(nameof(Spawn));
            StartCoroutine(nameof(IncreaseDifficulty));
            isSpawning = true;
        }
    }
    public void StopSpawning() {
        StopCoroutine(nameof(Spawn));
        StopCoroutine(nameof(IncreaseDifficulty));
        isSpawning = false;
    }
    
    private IEnumerator Spawn()
    {
        while (true)
        {
            float waitTime = spawnFunctionList[Random.Range(0, spawnFunctionList.Count)](shapes[Random.Range(0, shapes.Length)]);
            Debug.Log("Shape Spawned");
            yield return new WaitForSeconds(waitTime);
        }
    }
    private float SpawnOne(GameObject shape)
    {
        float offsetX = Random.Range(-spawnAreaWidth, spawnAreaWidth);
        Instantiate(shape, new Vector3(spawnPoint.position.x+ offsetX, spawnPoint.position.y, 0), shape.transform.rotation);
        return shape.GetComponent<Shape>().shapeDifficulty * spawnInterval * 1f;
    }
    private float SpawnTwo(GameObject shape) {
        float offsetX = Random.Range(0f, spawnAreaWidth);
        Instantiate(shape, new Vector3(spawnPoint.position.x + -offsetX, spawnPoint.position.y, 0), shape.transform.rotation);
        Instantiate(shape, new Vector3(spawnPoint.position.x + offsetX, spawnPoint.position.y, 0), shape.transform.rotation);
        return shape.GetComponent<Shape>().shapeDifficulty * spawnInterval* 2f;
    }
    private float SpawnThree(GameObject shape)
    {
        float offsetX = Random.Range(0f, spawnAreaWidth);
        offsetX=Mathf.Max(offsetX, 1f);
        Instantiate(shape, new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), shape.transform.rotation);
        Instantiate(shape, new Vector3(spawnPoint.position.x + -offsetX, spawnPoint.position.y, 0), shape.transform.rotation);
        Instantiate(shape, new Vector3(spawnPoint.position.x + offsetX, spawnPoint.position.y, 0), shape.transform.rotation);
        return shape.GetComponent<Shape>().shapeDifficulty * spawnInterval* 3f;
    }
}
