using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zorza : MonoBehaviour
{
    float x;
    private Vector3 inputVector;
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        x += Time.deltaTime*10;
        transform.rotation = Quaternion.Euler(0,x,0);
    }
}
