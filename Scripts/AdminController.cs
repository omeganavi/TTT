using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminController : MonoBehaviour
{

    public GameObject inputBan;
    public GameObject inputBan2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void banear()
    {
        string correo = inputBan.GetComponent<InputField>().text;
        inputBan.GetComponent<InputField>().text = "";
        try
        {
           
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = db.Collection("baneados").Document(correo);
            Dictionary<string, object> user = new Dictionary<string, object>
        {
        { "Email", correo }
        };

            docRef.SetAsync(user).ContinueWithOnMainThread(task =>
            {
                Debug.Log("Usuario Baneado");
            });
        }
        catch
        {
            Debug.Log("Error");
        }

        

    }


    public void desbanear()
    {
        string correo2 = inputBan2.GetComponent<InputField>().text;
        inputBan2.GetComponent<InputField>().text = "";
        try
        {

            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = db.Collection("baneados").Document(correo2);
            Dictionary<string, object> user = new Dictionary<string, object>
        {
        { "Email", correo2 }
        };

            docRef.DeleteAsync().ContinueWithOnMainThread(task =>
            {
                Debug.Log("Usuario Desbaneado");
            });

          
        }
        catch
        {
            Debug.Log("Error");
        }



    }
}

