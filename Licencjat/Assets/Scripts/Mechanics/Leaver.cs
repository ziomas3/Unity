using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaver : MonoBehaviour
{
    [SerializeField]
    GameObject gate;
    [SerializeField]
    public bool is_switchable;
    [SerializeField]
    public enum which_Object { Woda, Drzwi, Flipper};
    public which_Object typ_Obiektu;
    private bool doit = true;
 
    private void przestaw()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y * -1, transform.localPosition.z);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x * -1, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
    public void LeaverAction()
    {

        if (is_switchable)
        {
            
            switch (typ_Obiektu)
            {
                case which_Object.Woda:
                    gate.GetComponent<Rampa>().Activate();
                    przestaw();
                    break;
                case which_Object.Drzwi:
                    gate.GetComponent<Brama>().Activate();
                    przestaw();
                    break;
                case which_Object.Flipper:
                    gate.GetComponent<Flipper>().Activate();
                    przestaw();
                    break;
            }
        }
        
        else if (doit)
        {
            switch (typ_Obiektu)
            {
                case which_Object.Woda:
                    gate.GetComponent<Rampa>().Activate();
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y * -1, transform.localPosition.z);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x * -1, transform.localEulerAngles.y, transform.localEulerAngles.z);
                    break;
                case which_Object.Drzwi:
                    gate.GetComponent<Brama>().ActivateOnce();
                    break;
                case which_Object.Flipper:
                    gate.GetComponent<Flipper>().Activate();
                    break;
            }
        }
    }
}
