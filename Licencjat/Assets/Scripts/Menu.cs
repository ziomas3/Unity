using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject cm;
    // Start is called before the first frame update
    private void Start()
    {
        MyszkaOff();
    }
    public void MyszkaOff()
    {
        cm.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "";
        cm.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "";
    }

    public void MyszkaOn()
    {
        cm.GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "Mouse Y";
        cm.GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "Mouse X";
    }
    public void Play()
    {
        MyszkaOn();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Quit()
    {
        Application.Quit();
    }
}
