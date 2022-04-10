using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public PlayerController escena;

    public void abrirMenuSkin()
    {
        SceneManager.LoadScene("ElegirSkin");
    }
    public void empezarPartida()
    {
        SceneManager.LoadScene("tutorial");
    }

    public void salirJuego()
    {
        Application.Quit();
    }

    public void atras()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void menuJugar()
    {
        SceneManager.LoadScene("JugarMenu");
    }

    public void guardarpartida(){
        escena.guardar();
    }

    public void cargarPartida(PlayerController player){
        player.cargarPartida(); 
    }
}
