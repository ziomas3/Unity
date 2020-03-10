using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    

    public IEnumerator MoveTo(Vector3 targetPosition, System.Action callback, float delay = 0f)
    {
        while (targetPosition != new Vector3(transform.position.x,transform.position.y,transform.position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition,10f*Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(delay);
        callback();
    }

    public void TeleportTo(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }

}
