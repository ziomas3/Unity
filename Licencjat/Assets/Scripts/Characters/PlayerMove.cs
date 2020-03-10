using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    static Animator anim;
    public float speed = 8.0f;
    public float rotationSpeed = 75.0f;
    private GameObject sword;
    private float translation;
    private float rotation;
    private bool walk = true;
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        sword = GameObject.FindGameObjectWithTag("Sword");
        sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (walk)
        {
            translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);
        }
        if (translation != 0)
        {
            anim.SetBool("IsWalking", true);
            if (Input.GetButton("Run"))
            {
                speed = 8.0f;
                anim.SetBool("IsRunning", true);

            }
            else
            {
                speed = 4.0f;
                anim.SetBool("IsRunning", false);
            }
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if (Input.GetButtonDown("Sword"))
        {
            if (sword.activeSelf)
            {
                
                StartCoroutine(czekaj());
                anim.SetTrigger("Schowaj");
                StartCoroutine(SchowajMiecz());
            }
            else
            {
                
                StartCoroutine(czekaj());
                anim.SetTrigger("Wyjmij");
                StartCoroutine(WyciagnijMiecz());
            }
        }
        if (Input.GetButton("Fire1") && sword.activeSelf)
        {
            
            StartCoroutine(czekaj());
            anim.SetTrigger("Atak");
        }

        
    }
    IEnumerator SchowajMiecz()
    {
        yield return new WaitForSeconds(1);

        sword.SetActive(false);
    }
    IEnumerator WyciagnijMiecz()
    {
        yield return new WaitForSeconds(0.5f);

        sword.SetActive(true);
    }
    IEnumerator czekaj()
    {
        translation = 0;
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRunning", false);
        walk = false;
        yield return new WaitForSeconds(1.75f);

        walk = true;
    }
}
