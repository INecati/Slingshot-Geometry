using Assets.Entities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] public float scoreValue;
    [SerializeField] public float shapeDifficulty;

    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] protected BallType ballDropType;
    [SerializeField] protected int ballDropAmount;

    [SerializeField] private TMP_Text txtHealth;
    private Vector3 moveDirection;

    private Rigidbody2D rb;
    void Start()
    {
        txtHealth?.SetText(health.ToString());
        rb = GetComponent<Rigidbody2D>();
        moveDirection = new Vector3(0, -speed, 0);
        foreach(Transform tran in transform)
        {
            var x1 = tran.name;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        txtHealth?.SetText(health.ToString());
        if (health <= 0)
            OnShapeDestroy();
    }
    public void OnShapeDestroy()
    {
        FindObjectOfType<ScoreSystem>().AddScore(scoreValue);
        BallInventory.instance.AddBall(ballDropType, ballDropAmount);
        Destroy(gameObject);
    }
}
