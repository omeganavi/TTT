using Firebase;
using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Extensions;
using Firebase.Firestore;

public class Partida
{
    int arma;
    int monedas;
    int pantalla;
    String skin;
    float vida;
    String correoCargado;

    public Partida(int armaC, int monedasC, int pantallaC, String skinC, float vidaC, String correoCargadoC)
    {
        arma = armaC;
        monedas = monedasC;
        pantalla = pantallaC;
        skin = skinC;
        vida = vidaC;
        correoCargado = correoCargadoC;
    }

    public int getArma(){return arma;}
    public int getMonedas(){return monedas;}
    public int getPantalla(){return pantalla;}
    public String getSkin(){return skin;}
    public float getVida(){ return vida;}
    public String getCorreo(){return correoCargado;}


}





public class OAuthManager : MonoBehaviour
{
    static Partida partidaCargada;
    static Dictionary<String, object> dictPartidaCargada;

    // Start is called before the first frame update
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
   

    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;
    public Text warningLoginText;
    public Text confirmLoginText;

    static String correo;

    //Register variables
    [Header("Register")]
    public InputField usernameRegisterField;
    public InputField emailRegisterField;
    public InputField passwordRegisterField;
    public InputField passwordRegisterVerifyField;
    public Text warningRegisterText;

    public string getCorreo()
    {
        return correo;
    }


    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }
    public virtual void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Awake();
            }
            else
            {
                Debug.LogError(
                "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
        
    }
    //Function for the login button
    public void LoginButton()
    {

        correo = emailLoginField.text;
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";
            SceneManager.LoadScene("MainMenu");
        }
    }

    

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
                }


                try
                {
                    FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
                    DocumentReference docRef = db.Collection("usuarios").Document(_email);
                    Dictionary<string, object> user = new Dictionary<string, object>
                    {
                    { "Correo", _email },
                    { "Usuario", _username},
                    { "Password",  _password},
                    { "Puntos", 0 }



};
                    docRef.SetAsync(user).ContinueWithOnMainThread(task =>
                    {

                        Debug.Log("Added data to the alovelace document in the users collection.");
                    });
                }
                catch
                {
                    Debug.Log("Error");
                }

            }


        }
    }

    public Partida cargarPartida()
    {
        Debug.Log("Cargando Partida");

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        CollectionReference partidaRef = db.Collection("partida");
        Query query = partidaRef.WhereEqualTo("correo", correo);
        
        query.GetSnapshotAsync().ContinueWithOnMainThread((querySnapshotTask) =>
        {
            foreach (DocumentSnapshot documentSnapshot in querySnapshotTask.Result.Documents)
            {

                dictPartidaCargada = documentSnapshot.ToDictionary();

                partidaCargada = new Partida(Int32.Parse(dictPartidaCargada["Arma"].ToString()),
                    Int32.Parse(dictPartidaCargada["Monedas"].ToString()),
                    Int32.Parse(dictPartidaCargada["Pantalla"].ToString()), 
                    dictPartidaCargada["Skin"].ToString(),
                    float.Parse(dictPartidaCargada["Vida"].ToString()), 
                    dictPartidaCargada["correo"].ToString());

               
               
            }
        });

        return partidaCargada;


    }
}
