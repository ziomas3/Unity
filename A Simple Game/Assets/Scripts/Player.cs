using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody playerBody;
    private Game game;
    private bool jump;
    [SerializeField]
    private TMPro.TextMeshProUGUI CoinText;
    [SerializeField]
    private int coins;
    private GameObject sword;
    private Vector3 inputVector;
    void Start()
    {
        sword = transform.GetChild(0).gameObject;
        game = FindObjectOfType<Game>();
        playerBody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        
        inputVector = new Vector3(Input.GetAxis("Horizontal")*10f,playerBody.velocity.y, Input.GetAxis("Vertical") * 10f);
        transform.LookAt(transform.position + new Vector3(inputVector.x,0,inputVector.z));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Attack"))
        {
            PerformAttack();
        }
    }
    private void FixedUpdate()
    {
        playerBody.velocity = inputVector;
        if (jump&&isGrounded())
        {
            playerBody.AddForce(Vector3.up*400f, ForceMode.Impulse);
            jump=false;
        }
    }

    private void PerformAttack()
    {
        if (!sword.activeSelf)
        {
            sword.SetActive(true);
        }
    }

    bool isGrounded()
    {
        float distance = GetComponent<Collider>().bounds.extents.y + 0.01f;
        Ray ray = new Ray(transform.position, Vector3.down);

        return Physics.Raycast(ray, distance);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            game.ReloadCurrentLevel();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Coin":
                coins++;
                Destroy(other.gameObject);
                CoinText.text = string.Format("Coins\n{0}", coins); //"Coins\n" + coins;
                break;
            case "Goal":
                other.GetComponent<Goal>().CheckForCompletion(coins);
                break;
            default:
                break;
        }
    }
}

