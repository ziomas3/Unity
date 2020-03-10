using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    static music instance = null;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        
        instance = this;
    }
}
