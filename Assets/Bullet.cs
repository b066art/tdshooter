using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    Vector3 lastVelocity;
    Rigidbody2D bulletRb2D;
    private bool _notOnScreen = true;

    void Start()
    {
        bulletRb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        lastVelocity = bulletRb2D.velocity;
        if (_notOnScreen) //      
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) //    
        {
            var bulletSpeed = lastVelocity.magnitude;
            var bulletDirection = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            bulletRb2D.velocity = bulletDirection * Mathf.Max (bulletSpeed, 0f);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
            Score.scoreTxt.AddEnemyPoint();
            GameManager.gameMng.RespawnPlayers();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
            Score.scoreTxt.AddPlayerPoint();
            GameManager.gameMng.RespawnPlayers();
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }

    private void OnBecameVisible()
    {
        _notOnScreen = false;
    }
    
    private void OnBecameInvisible()
    {
        _notOnScreen = true;
    }
}
