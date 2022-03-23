using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("GameOverLine: "+collision.name);
        if (collision.CompareTag("Shape"))
        {
            FindObjectOfType<GameController>().EndGame();
        }
       
    }
}
