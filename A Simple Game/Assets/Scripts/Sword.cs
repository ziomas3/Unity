using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float attackLenght = .5f;


    // Update is called once per frame
    void Update()
    {
        attackLenght -= Time.deltaTime;
        if (attackLenght <= 0)
        {
            gameObject.SetActive(false);
            attackLenght = .5f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
