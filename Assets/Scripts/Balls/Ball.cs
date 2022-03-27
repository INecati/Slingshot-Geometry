using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    public float speed;
    public bool isDurable;
    public float durability;
    

    [SerializeField] protected int damage;
    [SerializeField] protected float lifeSpan;
    

    protected void Start()
    {
    }
    public void OnFired() {
        if (lifeSpan > 0)
            Destroy(gameObject, lifeSpan);
    }
    protected void OnBallDestroy()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Shape"))
        {
            collision.gameObject.GetComponent<Shape>().TakeDamage(damage);
            durability--;
            if (!isDurable && durability <= 0)
                OnBallDestroy();
        }
    }
}
