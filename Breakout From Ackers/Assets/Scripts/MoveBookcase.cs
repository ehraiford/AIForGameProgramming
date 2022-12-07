using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBookcase : MonoBehaviour
{
    [SerializeField] public int numOfStatues;
    private AudioSource bookcaseAudio;
    private bool doOnce = true;
    private GameObject onScreenUI;
    [SerializeField] AudioClip rumble;
    [SerializeField] GameObject servantsDoor, servantZombie;


    // Update is called once per frame
    void Update()
    {
        if(numOfStatues <= 0 && doOnce)
        {
            this.transform.position = new Vector3(-30.747f, 7.1769f, 10.073f);
            doOnce = false;
            bookcaseAudio.PlayOneShot(rumble);
            onScreenUI.GetComponent<OnScreenUIScript>().SetCurrentObjective(3);


            servantsDoor.GetComponent<Door>().isLocked = false; //unlock door to foyer
            servantsDoor.GetComponent<Door>().door.Play("Door2_Open"); //open door to foyer
            servantsDoor.GetComponent<Door>().isOpen = true;
            servantZombie.SetActive(true);
        }
        
    }

    public void decreaseNumStatue(int x)
    {
        numOfStatues -= x;
    }

    private void Start()
    {
        bookcaseAudio = GetComponent<AudioSource>();
        onScreenUI = GameObject.FindGameObjectWithTag("Menu");
    }
}
