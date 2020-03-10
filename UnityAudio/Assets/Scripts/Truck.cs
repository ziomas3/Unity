using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public float speed = 5f;
    public float positionLimit = 75;
  
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(speed, 0, 0);
        if (transform.position.x > positionLimit)
        {
            transform.position = new Vector3(-positionLimit, transform.position.y, transform.position.z);
        }
    }
}
