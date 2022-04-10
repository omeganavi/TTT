using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyfollow : MonoBehaviour
{
    private Animator animator;
    public Transform player_pos;
    public float speed;
    public float distancia_frenado;
    public float distancia_retraso;
    public float lineOfsite;
    public Transform PuntaArma;
    public GameObject bala;
    private float tiempo;
    public float shotingRange;
    public int health = 100;
    public GameObject deathEffect;
    public GameObject coinDrop;

    void Start()
    {
        player_pos = GameObject.Find("MainCharacter_animado_1").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //rango

        float distanciaplayer = Vector2.Distance(player_pos.position, transform.position);
        animator.SetBool("iswalk", false);
        if (distanciaplayer < lineOfsite && distanciaplayer > shotingRange) 
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player_pos.position, speed * Time.deltaTime);
            animator.SetBool("iswalk", true);
        }
        else if (distanciaplayer <= shotingRange)
        {
            tiempo += Time.deltaTime;
            if (tiempo >= 0.5)
            {
                Instantiate(bala, PuntaArma.position, Quaternion.identity);
                tiempo = 0;
            }
        }

        //flip
        #region
        if(player_pos.position.x>this.transform.position.x)

        {
            this.transform.localScale = new Vector2(2, 2);
        }else 
        {
            this.transform.localScale = new Vector2(-2, 2);
        }
        #endregion

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfsite);
        Gizmos.DrawWireSphere(transform.position, shotingRange);
    }

    private void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Die1()
    {
        Instantiate(coinDrop, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
            Die1();


        }

    }

}
