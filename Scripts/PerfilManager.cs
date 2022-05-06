using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerfilManager : MonoBehaviour
{

    public Text usuario;
    public Text correo;
    public GameObject botonUsuario;
    public GameObject botonCorreo;
    public GameObject botonPass;
    public GameObject inputUsuario;
    public GameObject inputCorreo;
    public GameObject inputPass;

    public GameObject aceptar1;
    public GameObject aceptar2;
    public GameObject aceptar3;

    public GameObject cancelar1;
    public GameObject cancelar2;
    public GameObject cancelar3;

    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser user;



    public FirebaseUser User;
    // Start is called before the first frame update
    void Awake()
    {
       

        

    }

    // Update is called once per frame
    void Update()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;

        correo.text = user.Email;
        usuario.text = user.DisplayName;
    }

    public void modificarUsuario()
    {
        usuario.enabled = false;
        inputUsuario.SetActive(true);
        botonUsuario.SetActive(false);
        aceptar1.SetActive(true);
        cancelar1.SetActive(true);
    }

    public void modificarCorreo()
    {
        correo.enabled = false;
        inputCorreo.SetActive(true);
        botonCorreo.SetActive(false);
        aceptar2.SetActive(true);
        cancelar2.SetActive(true);
    }

    public void modificarPass()
    {
        inputPass.SetActive(true);
        botonPass.SetActive(false);
        aceptar3.SetActive(true);
        cancelar3.SetActive(true);
    }

    public void updateCorreo()
    {
        user.UpdateEmailAsync(inputCorreo.GetComponent<InputField>().text);
        correo.enabled = true;
        inputCorreo.SetActive(false);
        botonCorreo.SetActive(true);
        aceptar2.SetActive(false);
        cancelar2.SetActive(false);
    }

    public void updateUsuario()
    {

        Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
        {
            DisplayName = inputUsuario.GetComponent<InputField>().text

        };
        user.UpdateUserProfileAsync(profile);

   
        usuario.enabled = true;
        inputUsuario.SetActive(false);
        botonUsuario.SetActive(true);
        aceptar1.SetActive(false);
        cancelar1.SetActive(false);
    }

    public void updatePassword()
    {

        user.UpdatePasswordAsync(inputPass.GetComponent<InputField>().text);


        usuario.enabled = true;
        inputPass.SetActive(false);
        botonPass.SetActive(true);
        aceptar3.SetActive(false);
        cancelar3.SetActive(false);
    }

    public void cancelarCorreo()
    {
        correo.enabled = true;
        inputCorreo.SetActive(false);
        botonCorreo.SetActive(true);
        aceptar2.SetActive(false);
        cancelar2.SetActive(false);
    }

    public void cancelarUsuario()
    {
        usuario.enabled = true;
        inputUsuario.SetActive(false);
        botonUsuario.SetActive(true);
        aceptar1.SetActive(false);
        cancelar1.SetActive(false);
    }

    public void cancelarPassword()
    {
        usuario.enabled = true;
        inputPass.SetActive(false);
        botonPass.SetActive(true);
        aceptar3.SetActive(false);
        cancelar3.SetActive(false);
    }




}
