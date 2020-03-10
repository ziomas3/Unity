using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Character
{
   
    [SerializeField]
    private GameObject menu;
    
    private void Update()

    {
        if (Input.GetButtonDown("Gravity"))
        {
            if (Physics.gravity.y == 0)
                Physics.gravity = new Vector3(0, -9.81f, 0);
            else
                Physics.gravity = new Vector3(0, 0, 0);
        }
       
        
        if (Input.GetButtonDown("Cancel"))
        {
            menu.GetComponent<Menu>().MyszkaOff();
            menu.SetActive(true);
        }
        
    }
  
    private void OnTriggerStay(Collider collision)
    {
        if(Input.GetButtonDown("Use") && collision.GetComponent<NPC>() != null)
        {
            collision.GetComponent<NPC>().StartDialogue();
        }
        if (Input.GetButtonDown("Use") && collision.tag=="leaver")
        {
            collision.GetComponent<Leaver>().LeaverAction();
        }
    }
}
