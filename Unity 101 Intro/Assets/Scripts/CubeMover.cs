using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left* moveSpeed;
        if (transform.position.x>10)
        {
            moveSpeed *= -1;
        }
        if (transform.position.x < 0)
        {
            moveSpeed *= -1;
        }
    }
}
