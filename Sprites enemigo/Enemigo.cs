using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    private Animator animator;
    public Transform player_pos;
    public float speed;
    public float distancia_frenado;
    public float distancia_retraso;
    [SerializeField] private float tiempoEntreDano;
    private float tiempoSiguienteDano;

    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.Find("Personatge").transform;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isWalk", false);
        if (player_pos != null){
        if (Vector2.Distance(transform.position, player_pos.position) > distancia_frenado){
            transform.position = Vector2.MoveTowards(transform.position, player_pos.position, speed * Time.deltaTime);
            animator.SetBool("isWalk", true);
        }
        if (Vector2.Distance(transform.position, player_pos.position) < distancia_retraso){
            transform.position = Vector2.MoveTowards(transform.position, player_pos.position, -speed* Time.deltaTime);
            animator.SetBool("isWalk", true);
        }
        if (Vector2.Distance(transform.position, player_pos.position) < distancia_frenado && Vector2.Distance(transform.position, player_pos.position) > distancia_retraso){
            transform.position = transform.position;
            animator.SetBool("isWalk", true);
        }
        if (player_pos.position.x > this.transform.position.x){
            this.transform.localScale = new Vector2(-0.9f,0.9f);
        }else{
            this.transform.localScale = new Vector2(0.9f,0.9f);
        }
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        
        if(player_pos != null){
        if(other.collider.CompareTag("Player")){
            tiempoSiguienteDano -= Time.deltaTime;
            if (tiempoSiguienteDano <= 0){ 
            other.collider.GetComponent<Movimiento_Momia>().TomarDano(5);   
            tiempoSiguienteDano = tiempoEntreDano;
            }
        }
        }
    }
}
