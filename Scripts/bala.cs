using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public Transform player_pos;
    public float velx;
    float vely = 0;
  
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    private object collision;
    public int damage = 40;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject && collision.gameObject.tag != "Soldado")
        {
            Destroy(gameObject);
        }

    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController enemy = collision.GetComponent<PlayerController>();
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }

    }
    */
}

