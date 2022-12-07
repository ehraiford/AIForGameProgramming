using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnZombiesServantsHallway : MonoBehaviour
{
    private float triggeredTimer = 0.0f;

    [SerializeField] GameObject[] zombies, candles;
    [SerializeField] AudioClip creepySound;
    [SerializeField] AudioSource audioSource;
    private bool creepySoundPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        triggeredTimer = Time.time;

       
    }
    private void Update()
    {
        if(triggeredTimer != 0.0f && Time.time - triggeredTimer > 2.00 && !creepySoundPlayed)
        {
            creepySoundPlayed = true;
            audioSource.PlayOneShot(creepySound);

            for (int i = 0; i < zombies.Length; i++)
            {
                zombies[i].SetActive(true);
            }

            for (int i = 0; i < candles.Length; i++)
            {
                candles[i].GetComponent<LightCandles>().forceLightingState(false);
            }

        } else if(triggeredTimer != 0.0f && Time.time - triggeredTimer > 10.00)
        {
            Destroy(gameObject);
        }
    }
}
