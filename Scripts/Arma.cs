using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arma : MonoBehaviour
{
    public Transform firePoint;
    public List<GameObject> balaPrefab = new List<GameObject>();
    public float bulletForce = 20f;
    public float x = 0;
    public float xNegativa = 0;
    public float y = 0;
    public List<Sprite> skinsArmas = new List<Sprite>();
    static int armaSeleccionada = 0;
 

    public int getArmaSeleccionada()
    {
        return armaSeleccionada;
    }

    public void setArmaSeleccionada(int numArma)
    {
        armaSeleccionada = numArma;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)  || Input.GetKeyDown(KeyCode.A)) transform.localScale = new Vector3(xNegativa, y, 1);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) transform.localScale = new Vector3(x, y, 1);


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Shoot();
        }

        firePoint.GetComponent<SpriteRenderer>().sprite = skinsArmas[armaSeleccionada];

    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(balaPrefab[armaSeleccionada], firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
