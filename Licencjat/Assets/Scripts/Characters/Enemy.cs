using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody[] bodyParts;
    GameObject reka;
    private int atak = 0;
    // Start is called before the first frame update
    void Start()
    {
        reka = new List<GameObject>(GameObject.FindGameObjectsWithTag("Reka")).Find(g => g.transform.IsChildOf(this.transform));
        bodyParts =GetComponentsInChildren<Rigidbody>();
        foreach (var x in bodyParts)
        {
            if (x.tag != "Reka")
            {
                x.isKinematic = true;
                x.detectCollisions = false;
            }
        }

        bodyParts[0].detectCollisions = true;
        bodyParts[0].isKinematic = false;
        Atakuj();

    }

    private void FixedUpdate()
    {

        if (atak==1)
        {
            
            reka.GetComponent<Rigidbody>().velocity += transform.forward * 11.5f;
        }
    }

    // Update is called once per frame
    public void ded()
    {
        foreach (var x in bodyParts)
        {
            x.isKinematic = false;
            x.detectCollisions = true;
        }
    }
    public void Atakuj()
    {
        
                StartCoroutine(this.Atakowanie(Atakuj));
         
    }

    public IEnumerator Atakowanie(System.Action callback)
    {
        atak = 1 - atak;
        yield return new WaitForSeconds(2f);
        callback();
    }

}
