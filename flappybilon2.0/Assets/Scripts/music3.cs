using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music3 : MonoBehaviour
{
    static music3 instance3 = null;

    void Start()
    {
        if (instance3 != null)
        {
            Destroy(gameObject);
            return;
        }

        
        instance3 = this;
    }
}
