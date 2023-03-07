using System;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;
public class Player : MonoBehaviour
{
    public delegate void PlayerDestroyed();
    public static event PlayerDestroyed OnPlayerDestroyed;
    public GameObject bulletPrefab;
    private float playerSpeed = 4.3f;
    public Rigidbody player;
    public bool fired = false;
    private Animator playerAnimator;

    //-----------------------------------------------------------------------------
    void Start()
    {
        player = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        Bullet.BulletDestroyed += bulletDestroyed;
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.velocity = new Vector3(playerSpeed, 0f, 0f);
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.velocity = new Vector3(-playerSpeed, 0f, 0f);
        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            player.velocity = new Vector3(0f, 0f, 0f);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && fired == false)
        {
            fired = true;
            // todo - trigger a "shoot" on the animator
            GameObject shot = Instantiate(bulletPrefab, new Vector3(player.position.x, player.position.y + 0.5f, 5f), Quaternion.identity);
            Debug.Log("Bang!");
            Destroy(shot, 3f);
        }

        if (GameHandler.GameOver == true)
        {
            Destroy(this.gameObject);
        }
    }

    void bulletDestroyed()
    {
        fired = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.CompareTag("EnemyBullet"))
            {
                OnPlayerDestroyed.Invoke();
                Destroy(this.gameObject);
            }
        }
    }
}
