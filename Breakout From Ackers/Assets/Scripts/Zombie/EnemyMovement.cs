using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    float speed = 100f;
    private void Start()
    {
        
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (Input.GetButtonDown("Fire1") && distance < 5)
        {
            rotateToPlayer();
        }
    }

    private void rotateToPlayer()
    {
        Debug.Log("ROTATE");
        //transform.LookAt(player);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(player.position.x, player.position.y, player.position.z), speed * Time.deltaTime);

    }
}
