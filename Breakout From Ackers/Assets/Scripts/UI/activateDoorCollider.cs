using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateDoorCollider : MonoBehaviour
{
    [SerializeField] GameObject passThroughParent;
    [SerializeField] bool isFirst;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            passThroughParent.GetComponent<passThroughDoorScript>().colliderEntered(isFirst);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            passThroughParent.GetComponent<passThroughDoorScript>().colliderExited(isFirst);
        }
    }
}
