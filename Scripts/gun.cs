using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour

{
    
    public GameObject bullet;
    public float firerate = 0.5f;
    float nextfire = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > nextfire)
        {
            nextfire = Time.time + firerate;
            fire();
        }
    }

    void fire()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
