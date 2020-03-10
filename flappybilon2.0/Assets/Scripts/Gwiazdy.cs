using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gwiazdy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody gwiazda;

    private Vector3 inputVector;
    void Start()
    {
        gwiazda = GetComponent<Rigidbody>();
        gwiazda.velocity = new Vector3(Random.Range(-15.0f, -3.0f), 0.0f, 0.0f);
        transform.position = new Vector3(Random.Range(-45.0f, 75.0f), Random.Range(-20.5f, 20.6f), Random.Range(15.0f, 35.0f));
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gwiazda.position.x < -50)
        {
            gwiazda.velocity = new Vector3(Random.Range(-15.0f, -3.0f), 0.0f, 0.0f);
            transform.position = new Vector3(75f, Random.Range(-20.5f, 20.6f), Random.Range(15.0f, 35.0f));

        }

    }
}
