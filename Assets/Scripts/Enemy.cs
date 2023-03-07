using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDestroyed(string name);
    public static event EnemyDestroyed OnEnemyDestroyed;
    public delegate void Wall();
    public static event Wall WallHit;
    public static event Wall WallLeft;
    public static float speed = 1.9f;
    public static float down = 0;
    public static bool direction = true;
    private float y;
    private float height = 0;
    private bool debounce = false;
    private bool debounce2 = false;
    public GameObject enemyBullet;
    public static bool heightUpdate = false;
    public bool wallLeft = true;
    private GameObject enemy;

    void Start()
    {
        y = gameObject.transform.position.y;
        height = y + down;
        enemy = this.gameObject;
        Player.OnPlayerDestroyed += playerDestroyed;
    }

    private void Update()
    {
        if (direction == true && debounce == false)
        {
                debounce = true;
                height = y + down;
                StartCoroutine(movePositive());
        }
        if (direction == false && debounce == false) 
        {
                debounce = true;
                height = y + down;
                StartCoroutine(moveNegative());
        }
        if (debounce2 == false) 
        {
                debounce2 = true;
                StartCoroutine(enemyFire());
        }
    }

    //-----------------------------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            speed += -0.01f;
            OnEnemyDestroyed.Invoke(this.gameObject.name);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            WallHit.Invoke();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            WallLeft.Invoke();
        }
    }

    private IEnumerator movePositive()
    {
        yield return new WaitForSeconds(speed);
        float x = gameObject.transform.position.x;
        gameObject.transform.position = new Vector3(x + 0.5f, height, 5);
        debounce = false;
    }
    private IEnumerator moveNegative()
    {
        yield return new WaitForSeconds(speed);
        float x = gameObject.transform.position.x;
        gameObject.transform.position = new Vector3(x - 0.5f, height, 5);
        debounce = false;
    }

    private IEnumerator enemyFire()
    {
        int rand = Random.Range(1, 101);
        yield return new WaitForSeconds(2);
        if (rand == 25)
        {
            GameObject enemyShot = Instantiate(enemyBullet, new Vector3(enemy.transform.position.x, enemy.transform.position.y - 0.5f, 5f), Quaternion.identity);
        }
        debounce2 = false;
    }

    private void playerDestroyed()
    {
        height = y;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, 5);
    }
}
