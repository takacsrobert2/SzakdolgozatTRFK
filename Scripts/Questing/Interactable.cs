using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    private bool hasInteracted;
    bool isEnemy;
    private Transform playerTransform;

    public virtual void MoveToInteraction(Transform transform)
    {
        isEnemy = gameObject.tag == "Enemy";
        playerTransform = transform;
        Debug.Log(playerTransform.GetComponent<Transform>());
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (!hasInteracted && distance <= 3f && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Interactable.cs update");
            if (!isEnemy){
                hasInteracted = true;
                Interact();
            }
        }
        else if (distance > 3f)
        {
            hasInteracted = false;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with base class.");
    }

    private Vector3 GetTargetPosition()
    {
        return transform.position;
    }
}
