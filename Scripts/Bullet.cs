using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;


    void Start()
    {
        rb.velocity = transform.right * speed;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = (transform.up) * speed;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = (transform.up * -1) * speed;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.velocity = (transform.right) * speed;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.velocity = (transform.right * -1) * speed;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemyfollow enemy = collision.GetComponent<enemyfollow>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        PlayerController mainCharacter = collision.GetComponent<PlayerController>();
        if (mainCharacter == null)
        {
            Destroy(gameObject);
        }
    }

}
