using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithNote : Interactable
{
    private GameObject OnScreenUI;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] GameObject bossAudio;

    [SerializeField] string noteText;
    [SerializeField] int fontNumber;
   
    public override void OnFocus()
    {
       
    }

    public override void OnInteract()
    {
        Debug.Log("Item Has been Interacted with");
        OnScreenUI.GetComponent<OnScreenUIScript>().OpenNote(noteText, fontNumber);
        audioSource.Play();
        bossAudio.GetComponent<BossStat>().pauseFootStep();
    }

    public override void OnLoseFocus()
    {
        bossAudio.GetComponent<BossStat>().resumeFootStep();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnScreenUI = GameObject.Find("On Screen UI");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
