using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateObjectOnTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToSpawn;

    private void OnTriggerEnter(Collider other)
    {
       for(int i = 0; i < objectsToSpawn.Length; i++)
        {
            objectsToSpawn[i].SetActive(true);
        }
        Destroy(gameObject);
    }
}
