using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music2 : MonoBehaviour
{
    static music2 instance2 = null;

    void Start()
    {
        if (instance2 != null)
        {
            Destroy(gameObject);
            return;
        }

        
        instance2 = this;
    }
}
