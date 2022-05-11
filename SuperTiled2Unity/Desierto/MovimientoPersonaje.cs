using System.Collections;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{

    [Range(1, 10)]
    public float velocidad;
    Rigidbody2D rb2d;
    SpriteRenderer spRd;

    void Start()
    {

        //Capturo los componentes Rigidbody2D y Sprite Renderer del Jugador
        rb2d = GetComponent<Rigidbody2D>();
        spRd = GetComponent<SpriteRenderer>();

    }

    void FixedUpdate()
    {

        //Movimiento horizontal
        float movimientoH = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(movimientoH * velocidad, rb2d.velocity.y);

        //Sentido horizontal
        if (movimientoH > 0)
        {
            spRd.flipX = false;
        }
        else if (movimientoH < 0)
        {
            spRd.flipX = true;
        }

    }
}
