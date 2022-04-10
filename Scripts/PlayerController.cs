using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Arma Arma;
    private CoinManager Coin;
    public MenuManager menu;
    public Scene miescena;
    public static PlayerController instance;
    public Image barravida;
    public float vidamaxima;
    public float health;
    [SerializeField] private float speed = 3f;
    private Rigidbody2D playerrb;
    private Vector2 moveinput;
    
    
    void Start()
    {
        miescena = SceneManager.GetActiveScene();
        if (instance == null)
        {
            instance = this;
        }
        playerrb = GetComponent<Rigidbody2D>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float movex = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveinput = new Vector2(movex, moveY).normalized;
        barravida.fillAmount = health / vidamaxima;

        if (Input.GetKeyDown(KeyCode.G)){
            guardar();
        }

        if (Input.GetKeyDown(KeyCode.C)){
           cargarPartida();
        }

        if (health > vidamaxima)
        {
            health = vidamaxima;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
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
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
    }

    public void healthup()
    {
        health += 40;


    }
    public void guardar(){
        SaveManager.SavePlayerData(this);
    }
    public void cargarPartida(){
        PlayerData playerData = SaveManager.LoadPlayerData();
        health = playerData.health;
        vidamaxima = playerData.maxhealth;
        transform.position = new Vector2(playerData.positionMain[0], playerData.positionMain[1]);
        CoinManager.instance.score = playerData.score;
        SceneManager.SetActiveScene(miescena);
        
    }
}

