using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int currentHealth = 50;
    // Start is called before the first frame update
    void Start()
    {
        print(ReduceHealth(15));
        print(ReduceHealth(27));
    }
    public int ReduceHealth(int amountOfDamage)
    {
        currentHealth -= amountOfDamage;
        return currentHealth;
    }
}
