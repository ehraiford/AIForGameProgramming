using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie/Head"))
            EnemyStat.OnTakeDamage(100);
        else if (other.CompareTag("Zombie/Body"))
            EnemyStat.OnTakeDamage(30);
    }
}
