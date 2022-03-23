using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnAreaWidth;
    //[SerializeField] private float initialSpawnDelay;
    

    //[SerializeField] private GameObject basicShape;
    [SerializeField] private float spawnInterval;
    [SerializeField] private GameObject[] shapes;

    public bool isSpawning = false;


    public List<Func<GameObject, float>> spawnFunctionList = new List<Func<GameObject, float>>();
    //public Func<GameObject, float>[] spawnFunctions = new Func<GameObject, float>[3];
    public delegate float Test1(GameObject gameObject);
    private void Awake()
    {
        spawnFunctionList.Add(SpawnOne);
        spawnFunctionList.Add(SpawnTwo);
        spawnFunctionList.Add(SpawnThree);
    }
    private void Start()
    {
        //spawnFunctions = new Func<GameObject, float>() { new Test1(SpawnOne(shape: gameObject)) };
        //Invoke(nameof(StartSpawning), initialSpawnDelay);
    }
    public void StartSpawning() {
        if (!isSpawning)
        {
            StartCoroutine(nameof(Spawn));
            isSpawning = true;
        }
    }
    public void StopSpawning() {
        StopCoroutine(nameof(Spawn));
        isSpawning = false;
    }
    
    private IEnumerator Spawn()
    {
        while (true)
        {
            //Instantiate(basicShape, new Vector3(spawnPoint.position.x + Random.Range(-spawnAreaWidth, spawnAreaWidth), spawnPoint.position.y, 0), basicShape.transform.rotation);
            //Debug.Log(Random.Range(0, spawnFunctionList.Count));
            //Debug.Log(Random.Range(0, shapes.Length));

            float waitTime = spawnFunctionList[Random.Range(0, spawnFunctionList.Count)](shapes[Random.Range(0, shapes.Length)]);
            //float waitTime = spawnFunctionList[0](shapes[0]);

            //yield return new WaitForSeconds(spawnInterval);
            yield return new WaitForSeconds(waitTime);
        }
        yield return null;
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
