using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerBody;
    private bool jump;

    private Vector3 inputVector;
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {

        inputVector = new Vector3(Input.GetAxis("Horizontal") * 1f, 0, Input.GetAxis("Vertical") * 1f);
        transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        playerBody.velocity += inputVector;
        if (jump && isGrounded())
        {
            playerBody.AddForce(Vector3.up * 500f, ForceMode.Impulse);
            jump = false;
        }
    }
    bool isGrounded()
    {
        float distance = GetComponent<Collider>().bounds.extents.y + 0.01f;
        Ray ray = new Ray(transform.position, Vector3.down);

        return Physics.Raycast(ray, distance);
    }
    private void OnTriggerStay(Collider collision)
    {
        if (Input.GetButtonDown("Fire1") && collision.GetComponent<NPC>() != null)
        {
            collision.GetComponent<NPC>().StartDialogue();
        }
    }
}

