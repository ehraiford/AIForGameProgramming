using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject respawnPoint;

    public void RespawnCharacter()
    {
        gameOver.SetActive(false);
        Debug.Log("Respawn");
        GetComponent<FirstPersonController>().AddHealth(50);
        GetComponent<FirstPersonController>().enabled = false;
        transform.position = respawnPoint.transform.position;
        Physics.SyncTransforms();
        GetComponent<FirstPersonController>().enabled = true;
        Time.timeScale = 1.0f;
    }
}
