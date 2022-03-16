using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Momia : MonoBehaviour
{
    public float velocidad;
    private Rigidbody2D rigidbody;
    private Animator animator;

    [SerializeField] int vida;
    [SerializeField] int vidaMax;
    
    private void Start()
    {
        vida = vidaMax;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalk", false);
        if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey("a")){
            transform.position += Vector3.left * velocidad * Time.deltaTime;
            transform.localScale = new Vector3(-0.9f, 0.9f, 1);
            animator.SetBool("isWalk", true);
        }

        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey("d")){
            transform.position += Vector3.right * velocidad * Time.deltaTime;
            transform.localScale = new Vector3(0.9f, 0.9f, 1);
            animator.SetBool("isWalk", true);
        }

        if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey("w")){
            transform.position += Vector3.up * velocidad * Time.deltaTime;
            animator.SetBool("isWalk", true);
        }

        if (Input.GetKey(KeyCode.DownArrow)||Input.GetKey("s")){
            transform.position += Vector3.down * velocidad * Time.deltaTime;
            animator.SetBool("isWalk", true);
        }
    }

    public void TomarDano(int daño){
        vida -= daño;
        if(gameObject != null){
            if (vida<=0)
            {
                Destroy (gameObject);
            }
        }
    }
}