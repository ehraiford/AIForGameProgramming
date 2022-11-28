using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorCollision : MonoBehaviour
{
    Collider[] hitCollider;
    [SerializeField] float sphereRadius;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Get every object its radius
        hitCollider = Physics.OverlapSphere(transform.position, sphereRadius);
        //Check if there is something in array
        if(hitCollider.Length > 0)
        {
            //Loop through array
            foreach(var obj in hitCollider)
            {
                //Debug.Log("hello");
                //Check if name contains door
                if (obj.transform.name.Contains("Door2") && obj.GetComponent<Door>() != null)
                {
                    obj.GetComponent<Door>().bossOpenDoor();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
}
