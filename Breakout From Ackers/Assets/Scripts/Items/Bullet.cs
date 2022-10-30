using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //For normal zombie
        if (other.CompareTag("Zombie/Head"))
            EnemyStat.OnTakeDamage(100);
        else if (other.CompareTag("Zombie/Body"))
            EnemyStat.OnTakeDamage(35);
        else if (other.CompareTag("Zombie/Legs"))
            EnemyStat.OnTakeDamage(25);
    }
}
