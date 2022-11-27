using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    [SerializeField] public AudioClip[] BGMArray;
    AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {
        BGM = GetComponent<AudioSource>();
        BGM.PlayOneShot(BGMArray[0]);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setNewMusic(int i)
    {
        StartCoroutine(StartFade(BGM, 3, 0f));
        BGM.PlayOneShot(BGMArray[i]);
        StartCoroutine(StartFade(BGM, 10, 0.05f));

    }
    //0 is normal background music
    //1 is the boss music
    

    //Fade music in and out
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
