using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music4 : MonoBehaviour
{
    static music4 instance4 = null;

    void Start()
    {
        if (instance4 != null)
        {
            Destroy(gameObject);
            return;
        }

        
        instance4 = this;
    }
}
