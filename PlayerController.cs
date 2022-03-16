using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Image barravida;
    public float vidamaxima;
    public float health;
    [SerializeField] private float speed = 3f;
    private Rigidbody2D playerrb;
    private Vector2 moveinput;
    
    // Start is called before the first frame update
    void Start()
    {
        playerrb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movex = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveinput = new Vector2(movex, moveY).normalized;
        barravida.fillAmount = health / vidamaxima;
    }
    private void FixedUpdate()
    {
        playerrb.MovePosition(playerrb.position + moveinput * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
  
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 20;

            if (health <= 0)
            {
                Die();
            }
        }


    }

    private void Die()
    {
        barravida.fillAmount = 0;
        Destroy(gameObject);
    }
}

