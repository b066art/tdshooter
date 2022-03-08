using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameMng;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    private Vector2 playerPos;
    private Vector2 enemyPos;
    private int[] a = { -1, 1 };
    public int rnd;

    private void Awake()
    {
        gameMng = this;
        rnd = 1;
    }

    void Start()
    {
        GameObject Player = Instantiate(playerPrefab);
        GameObject Enemy = Instantiate(enemyPrefab);
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        enemyPos = GameObject.FindGameObjectWithTag("Enemy").transform.position;
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }


    public void RespawnPlayers()
    {
        rnd = a[Random.Range(0, 2)];
        Debug.Log(rnd);
        GameObject.FindGameObjectWithTag("Player").transform.position = playerPos;
        GameObject.FindGameObjectWithTag("Enemy").transform.position = enemyPos;
    }
}
