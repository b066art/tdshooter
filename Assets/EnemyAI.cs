using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float aiSpeed;
    public float range;
    public Transform firePoint;
    public Transform rayPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;
    public float cooldown = 1.5f;
    private bool wallCheck = false;
    float lastShot;
    Vector2 lookDir;

    private Transform playerPos;
    private Rigidbody2D aiRb2D;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        aiRb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Aiming(); 
        Chasing();
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, lookDir); //проверка отсутствия препятствий
        if (hit.collider.tag == "Player")
        {
            Shooting();
        }
    }

    private void Aiming() //наведение на цель
    {
        lookDir = new Vector2(playerPos.position.x, playerPos.position.y) - aiRb2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        aiRb2D.rotation = angle;
    }

    private void Chasing() //движение к цели
    {
        if (Vector2.Distance(transform.position, playerPos.position) > range)
            if (wallCheck)
            {
                transform.Translate(((GameManager.gameMng.rnd) * Vector2.up) * aiSpeed * Time.deltaTime);
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, aiSpeed * Time.deltaTime / 2);
            }

            else
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, aiSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, playerPos.position) <= range)
        {
            RaycastHit2D look = Physics2D.Raycast(rayPoint.position, lookDir);
            if (look.collider.tag == "Player")
                transform.Translate(-Vector2.right * aiSpeed * Time.deltaTime);

            if (look.collider.tag != "Player")
            {
                transform.Translate(-Vector2.up * aiSpeed * Time.deltaTime);
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, aiSpeed * Time.deltaTime / 2);
            }
                
        }
    }

    private void Shooting() //кулдаун между выстрелами
    {
        if (Time.time - lastShot < cooldown)
            return;
        lastShot = Time.time;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb2D = bullet.GetComponent<Rigidbody2D>();
        rb2D.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerStay2D(Collider2D coll) //движение к цели
    {
        if (coll.gameObject.CompareTag("Wall"))
            wallCheck = true;
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        wallCheck = false;
    }
}
