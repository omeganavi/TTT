using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letrero : MonoBehaviour
{

    public GameObject informacion;
    public GameObject mostrarInformacion;
    public bool informacionHabilitada;
    public bool mostrarInformacionHabilitada;
    public LayerMask personaje;
    public int coinbuy = 0;
    public int id;


    // Start is called before the first frame update
    void Start()
    {
        informacion.gameObject.SetActive(false);
        mostrarInformacion.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        informacionHabilitada = Physics2D.OverlapCircle(this.transform.position, 3f, personaje);
        if (informacionHabilitada == true)
        {
            informacion.gameObject.SetActive(true);
        }
        if (informacionHabilitada == false)
        {
            informacion.gameObject.SetActive(false);
        }
        if (informacionHabilitada == true && Input.GetKeyDown(KeyCode.E) && CoinManager.instance.score >= coinbuy && id == 1)
        {
            informacion.gameObject.SetActive(true);
            CoinManager.instance.buyweapon(coinbuy);

        }
        if (informacionHabilitada == true && Input.GetKeyDown(KeyCode.E) && CoinManager.instance.score >= coinbuy && id == 2 && PlayerController.instance.health != 100)
        {
            informacion.gameObject.SetActive(true);
            CoinManager.instance.buyhealth(coinbuy);

        }


        /*
        mostrarInformacionHabilitada = Physics2D.OverlapCircle(this.transform.position, 1f, personaje);

        if (mostrarInformacionHabilitada == true && Input.GetKeyDown(KeyCode.P))
        {
            mostrarInformacion.gameObject.SetActive(true);
            coinbuy += 1;
        }
        if(mostrarInformacionHabilitada == false)
        {
            mostrarInformacion.gameObject.SetActive(false);
        }
        */
    }
}
