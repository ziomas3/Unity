using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "sufit")
        {
            GameObject.Find("smierc").GetComponent<AudioSource>().Play(0);
            SceneManager.LoadScene("mapa", LoadSceneMode.Single);
        }
    }
}
