using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WodaColider : MonoBehaviour
{
    private Material blueeee;
    private void Start()
    {
        blueeee = Resources.Load("Blue", typeof(Material)) as Material;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Zapadnia")
        {
            collision.gameObject.GetComponent<Leaver>().LeaverAction();
            collision.gameObject.GetComponent<Renderer>().material = blueeee;
            if (collision.gameObject.GetComponent<Cleaner>())
                collision.gameObject.GetComponent<Cleaner>().Usun();
        }

    }
    
}
