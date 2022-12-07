using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonNote : Interactable
{

    private GameObject OnScreenUI;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] int noteNumber;
    [SerializeField] int fontNumber;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject musicSource;
    private bool spawnedAcker = false;

    public override void OnFocus()
    {  
    }

    public override void OnInteract()
    {
        spawnAcker();
    }

    public override void OnLoseFocus()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        OnScreenUI = GameObject.Find("On Screen UI");
    }


    public bool spawnAcker()
    {
        if (!spawnedAcker)
        {//Set new music
            BGMScript bgm = musicSource.GetComponent<BGMScript>();
            bgm.setNewMusic(1);
            //Activate Boss
            boss.SetActive(true);
            Debug.Log("Item Has been Interacted with");
            OnScreenUI.GetComponent<OnScreenUIScript>().OpenNote(noteNumber, fontNumber);
            OnScreenUI.GetComponent<OnScreenUIScript>().SetCurrentObjective(4);
            audioSource.Play();
            spawnedAcker = true;
            return true;
        }
        else return false;
    }
}
