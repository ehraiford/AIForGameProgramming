using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandles : Interactable
{

    [SerializeField] private GameObject flame;
    private bool  canTurnOff = false, unlit; 
    AudioSource light;
    float time;
    [SerializeField] float timeToOff = 100; // seconds to turn off candle
    [SerializeField] int randMax = 100; // Max random number for random generator
    [SerializeField] int threshold = 50;// threshold to turn off candle
    [SerializeField] bool startingState = false;
    FirstPersonController FPC; //first person controller
    float DD; //Dynamic Difficulty value
    [SerializeField] int ChangeDiffScore;
    // Start is called before the first frame update
    void Start()
    {
        flame.SetActive(startingState);
        unlit = !startingState;
        light = gameObject.GetComponentInChildren<AudioSource>();
        FPC = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
        DD = FPC.diffcultyValue();
        
    }
    private void Update()
    {
        //If candle is on and can be turn off, after correct amount of time turn off
        if ((Time.time > time + timeToOff) && !unlit && canTurnOff)
        {
            //Debug.Log(unlit.ToString() + " "+ canTurnOff.ToString());
            unlit = true;
            flame.SetActive(false);
            canTurnOff = false;
        }
    }
    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        //Interact with items
        if (unlit)
        {
            //Standard Turn on candle
            light.Play();
            flame.SetActive(true);
            unlit = false;
            time = Time.time;

            //Probabilty to turn off
            float ran = Random.Range(0f, randMax);
            //Debug.Log(ran);
            ran *= DD;
            //Debug.Log(DD);
            if (ran >= threshold)
            {
                canTurnOff = true;
            }
            //Increase the difficulty for the player
            FPC.scoreAdjustment(ChangeDiffScore);
        }
        
    }

    public override void OnLoseFocus()
    {
    }

    public void forceLightingState(bool onOff)
    {
        flame.SetActive(onOff);
        unlit = !onOff;
        //canTurnOff = onOff;
    }

    public bool GetLightingState()
    {
        return !unlit;
    }
}
