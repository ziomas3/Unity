using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class NPC : Character
{
    [SerializeField]
    private float npcMoveDistance;
    private Vector3[] movementDirections = new Vector3[] { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
    [SerializeField]
    private DialogueData dialogueData;
    [SerializeField]
    private Dialogue dialogue;
    private Vector3 spawnPosition;
    [SerializeField]
    public bool wander;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        anim = this.GetComponent<Animator>();
        if (wander)
        {
            Wander();
        }
    }
    public void StartDialogue()
    {
        anim.SetBool("Idle", true);
        dialogue.StartDialogue(dialogueData.dialogue);
    }

    public void Wander()
    {
        if (wander)
        {
            //Vector3 currentPosition = transform.position;
            if (AlmostEqual(transform.position, spawnPosition, 0.1f))
            {
                int roll = Random.Range(0, 4);
                Vector3 destination = transform.position + movementDirections[roll] * npcMoveDistance;
                StartCoroutine(this.MoveTo(destination, Wander, Random.Range(0.1f, 3)));
                Debug.Log("idzie");
            }
            else
            {
                StartCoroutine(this.MoveTo(spawnPosition, Wander, Random.Range(0.1f, 3)));
                Debug.Log("wraca");
            }
        }
    }
    public static bool AlmostEqual(Vector3 v1, Vector3 v2, float precision)
    {
        bool equal = true;

        if (Mathf.Abs(v1.x - v2.x) > precision) equal = false;
        if (Mathf.Abs(v1.y - v2.y) > precision) equal = false;
        if (Mathf.Abs(v1.z - v2.z) > precision) equal = false;

        return equal;
    }
}
