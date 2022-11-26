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
        BGM.PlayOneShot(BGMArray[i]);
    }
    //0 is normal background music
    //1 is the boss music
}
