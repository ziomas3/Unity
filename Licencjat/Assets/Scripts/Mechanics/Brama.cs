using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brama : MonoBehaviour
{
    private IEnumerator bramka;
    private bool brama = false;
    private int wysokosc = -6;
    private bool once;
    private bool doit = true;
    // Start is called before the first frame update
    void Start()
    {

        //OtworzBrame();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position != new Vector3(transform.position.x, wysokosc, transform.position.z) && brama)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, wysokosc, transform.position.z), 10f * Time.deltaTime);
        }
        else
        if(doit)
        {
            if (wysokosc == -6)
                wysokosc = 5;
            else
                wysokosc = -6;
            brama = false;
            if (once)
                doit = false;
        }
    }

    public void Activate()
    {
        brama = true;
        once = false;
        doit = true;
    }
    public void ActivateOnce()
    {
        brama = true;
        wysokosc = -6;
    }
    public void Close()
    {
        wysokosc = 5;
        brama = true;
    }

}

