using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    [SerializeField]
    GameObject gate;
    [SerializeField]
    GameObject [] delete;
   
    
    private void OnTriggerStay(Collider collision)
    {
        
        if (collision.tag == "Player")
        {
            Usun();
        }
    }
    public void Usun()
    {
        if (gate)
            gate.GetComponent<Brama>().Close();
        for (int i = 0; i < delete.Length; i++)
        {
            Destroy(delete[i].gameObject);
        }
    }
}
