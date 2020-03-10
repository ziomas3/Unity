using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampa : MonoBehaviour
{
    GameObject[] woda;
    [SerializeField]
    private int maxParticles = 1000;
    [SerializeField]
    private Material blueeee;
    private int j = 0;
    [SerializeField]
    private bool gen = true;
    private GameObject v1;
    private GameObject v2;
    private GameObject v3;
    // Start is called before the first frame update
    void Awake()
    {
        woda = new GameObject[maxParticles];

        v1 = this.gameObject.transform.GetChild(0).gameObject;
        v2 = this.gameObject.transform.GetChild(1).gameObject;
        v3 = this.gameObject.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (gen)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = new Vector3(Random.Range(v1.transform.position.x, v2.transform.position.x), Random.Range(v1.transform.position.y, v2.transform.position.y), Random.Range(v1.transform.position.z, v2.transform.position.z));
            go.AddComponent<Rigidbody>();
            go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            go.GetComponent<Renderer>().material = blueeee;
            go.AddComponent<WodaColider>();
            go.transform.parent = this.gameObject.transform;

            if (j < maxParticles)
            {

                woda[j] = go;
                j++;
            }
            else
            {
                gen = false;
            }
        }
        
        for (int i = 0; i < j; i++)
        {
            
            
            if (woda[i].transform.localPosition.y < v3.transform.localPosition.y)
            {
                woda[i].transform.localPosition = new Vector3(Random.Range(v1.transform.localPosition.x, v2.transform.localPosition.x), Random.Range(v1.transform.localPosition.y, v2.transform.localPosition.y), Random.Range(v1.transform.localPosition.z, v2.transform.localPosition.z));
                woda[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                if (gen)
                        StartCoroutine(zabij());

            }
        }
    }
    
    IEnumerator zabij()
    {
        yield return new WaitForSeconds(2);
        gen = false;
    }
    public void Activate()
    {
        gen = true;
    }
}
