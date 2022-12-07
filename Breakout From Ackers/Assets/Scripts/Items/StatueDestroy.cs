using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueDestroy : MonoBehaviour
{
    private AudioSource statueAudio;
    [SerializeField] private AudioClip shatter;
    [SerializeField] private GameObject bookcase;
    [SerializeField] private GameObject bossSummonNote;

    private void Start()
    {
        statueAudio = GetComponentInParent<AudioSource>();
    }
    private void OnDestroy()
    {
        bookcase.GetComponent<MoveBookcase>().decreaseNumStatue(1);
        statueAudio.PlayOneShot(shatter);
        bossSummonNote.GetComponent<BossSummonNote>().spawnAcker();
    }
}
