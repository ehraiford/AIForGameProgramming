using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueDestroy : MonoBehaviour
{
    private AudioSource statueAudio;
    [SerializeField] private AudioClip shatter;
    [SerializeField] private GameObject bookcase;

    private void Start()
    {
        statueAudio = GetComponentInParent<AudioSource>();
    }
    private void OnDestroy()
    {
        bookcase.GetComponent<MoveBookcase>().decreaseNumStatue(1);
        statueAudio.PlayOneShot(shatter);
    }
}
