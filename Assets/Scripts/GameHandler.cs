using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public GameObject startScreen;
    public int numOfEnemies = 24;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public Transform enemyRoot;
    public GameObject shield;
    public GameObject player;
    public static int lives = 3;
    public static bool GameOver = false;
    public GameObject over;
    private bool debounce = true;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startUp());
        Enemy.WallHit += OnWallHit;
        Enemy.WallLeft += OnWallLeave;
        Player.OnPlayerDestroyed += playerDestroyed;
    }

    public void startGame()
    {
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy1, new Vector3(-7f + (0.5f * i), 8f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy2, new Vector3(-7f + (0.5f * i), 7.5f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy2, new Vector3(-7f + (0.5f * i), 7f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy3, new Vector3(-7f + (0.5f * i), 6.5f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy3, new Vector3(-7f + (0.5f * i), 6f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }

        for (int i = 0; i < 4; i++)
        {
            Instantiate(shield, new Vector3(0 + (5 * i), 6f, 5f), new Quaternion(0f,0f,0f,0f));
        }
        
        
        Instantiate(player, new Vector3(0f, 0.5f, 5f), new Quaternion(0f, 90f, 0f, 0f));
    }

    private void reload()
    {
        lives = 3;
        Enemy.speed = 1.9f;
        Enemy.down = 0f;
        foreach (Transform child in enemyRoot)
        {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy1, new Vector3(-7f + (0.5f * i), 8f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy2, new Vector3(-7f + (0.5f * i), 7.5f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy2, new Vector3(-7f + (0.5f * i), 7f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy3, new Vector3(-7f + (0.5f * i), 6.5f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }
        for(int i = 0; i < numOfEnemies; i++)
        { 
            GameObject newEnemy = Instantiate(enemy3, new Vector3(-7f + (0.5f * i), 6f, 5f), new Quaternion(0f, 90f, 0f,0f));
            newEnemy.transform.parent = enemyRoot;
        }

        GameOver = false;
    }
    
    private IEnumerator startUp()
    {
        startScreen.SetActive(true);
        yield return new WaitForSeconds(6);
        startScreen.SetActive(false);
        yield return new WaitForSeconds(1);
        startGame();
    }

    public void OnWallHit()
    {
        if (debounce == true)
        {
            Debug.Log("Enemy Hit A Wall");
            dropDown();
            Enemy.direction = !(Enemy.direction);
            debounce = false;
        }
    }

    public void OnWallLeave()
    {
        Debug.Log("Enemy Left The Wall");
        debounce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives <= 0)
        {
            GameOver = true;
            over.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.R))
            {
                over.gameObject.SetActive(false);
                reload();
                Instantiate(player, new Vector3(0f, 0.5f, 5f), new Quaternion(0f, 90f, 0f, 0f));
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            lives += -1;
        }
    }
    
    private void dropDown()
    {
        Enemy.heightUpdate = true;
        Enemy.down = Enemy.down - 1f;
        Enemy.speed += -0.1f;
    }

    private void playerDestroyed()
    {
        Enemy.down = 0f;
        Enemy.speed = 1.9f;
        Instantiate(player, new Vector3(0f, 0.5f, 5f), new Quaternion(0f, 90f, 0f, 0f));
        lives += -1;
        Debug.Log(lives);
    }
}
