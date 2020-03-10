using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerBody;
    private bool jump;
    public int score=0;
    static int record = 0;
    private Vector3 inputVector;
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {

        //inputVector = new Vector3(Input.GetAxis("Horizontal")*10f,playerBody.velocity.y, Input.GetAxis("Vertical") * 10f);
        inputVector = new Vector3(playerBody.velocity.x, 11f, playerBody.velocity.z);
        //transform.LookAt(transform.position + new Vector3(inputVector.x,0,inputVector.z));
        if (Input.GetButtonDown("Jump")||Input.GetMouseButtonDown(0))
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        GameObject.Find("wynik").GetComponent<TMPro.TextMeshProUGUI>().text = "Wynik: " + score.ToString();
        if (score > record) record = score;
        GameObject.Find("rekord").GetComponent<TMPro.TextMeshProUGUI>().text = "Rekord: " + record.ToString();
        
        if (jump)
        {
            playerBody.velocity = inputVector;
            //playerBody.AddForce(Vector3.up*400f, ForceMode.Impulse);
            GameObject.Find("skok").GetComponent<AudioSource>().Play(0);
            jump =false;
        }
    }
    bool isGrounded()
    {
        float distance = GetComponent<Collider>().bounds.extents.y + 0.01f;
        Ray ray = new Ray(transform.position, Vector3.down);

        return Physics.Raycast(ray, distance);
    }
    
}

