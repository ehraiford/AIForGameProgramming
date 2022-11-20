using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDoorCollision : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Door"))
        {
            Debug.Log("hitDoor");
            anim.SetTrigger("hitDoor");
        }
    }
}
