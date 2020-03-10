using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("miecz trafil");
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().ded();
            collision.gameObject.GetComponent<NPC>().wander = false;
            Destroy(collision.gameObject,3);
        }
    }

}
