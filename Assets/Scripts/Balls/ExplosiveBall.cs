using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBall : Ball
{
    [SerializeField] protected int explosionDamage;
    [SerializeField] protected float areaOfEffect;
    [SerializeField] private GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected new void OnBallDestroy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, areaOfEffect);
        foreach (var col in colliders)
        {
            if (col.CompareTag("Shape"))
            {
                col.GetComponent<Shape>().TakeDamage(explosionDamage);
            }

        }
        Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity), 1f);
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
