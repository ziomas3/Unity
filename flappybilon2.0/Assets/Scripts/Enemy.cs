using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody enemyBody;

    private Vector3 inputVector;
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
        enemyBody.velocity = new Vector3(Random.Range(-8.0f, -3.0f), 0.0f, 0.0f);
        transform.position = new Vector3(25f, Random.Range(-3.5f, 5.6f), 0.0f);
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyBody.position.x < -6.5)
        {
            enemyBody.velocity = new Vector3(Random.Range(-8.0f, -3.0f), 0.0f, 0.0f);
            transform.position = new Vector3(25f, Random.Range(-3.5f, 5.6f), 0.0f);

            GameObject.Find("Player").GetComponent<Player>().score+=1;
        }

    }
}
