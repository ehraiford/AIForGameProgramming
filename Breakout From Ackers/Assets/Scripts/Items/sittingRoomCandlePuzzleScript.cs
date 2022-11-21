using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sittingRoomCandlePuzzleScript : MonoBehaviour
{

    private float startTime;
    private bool wrongCode = false, playedAudio = false, complete = false;
    [SerializeField] GameObject[] candles;
    [SerializeField] GameObject diningRoomDoor;
    [SerializeField] AudioClip[] audioClips;
    void Start()
    {
        
    }

    void Update()
    {
        //preps to blow out candles if they were in the wrong order.
        if (!wrongCode && !CheckProperOrder())
        {
            wrongCode = true;
            startTime = Time.time;
        }
        else if (AllLit() && !complete)
        {
            gameObject.GetComponent<AudioSource>().clip = audioClips[1];
            gameObject.GetComponent<AudioSource>().Play();
            diningRoomDoor.GetComponent<Door>().isLocked = false; //unlock door to foyer
            diningRoomDoor.GetComponent<Door>().door.Play("Door2_Open"); //open door to foyer
            diningRoomDoor.GetComponent<Door>().isOpen = true;
            startTime = Time.time;
            complete = true;
        }

        //this chunk of code handles blowing out the candles.
        if (wrongCode && Time.time - startTime > 4)
        {
            for (int i = 0; i < candles.Length; i++)
            {
                candles[i].GetComponent<LightCandles>().forceLightingState(false);
            }
            wrongCode = false;
            playedAudio = false;
            //calls the blow sound .2 seconds before they blow out.
        }
        else if (wrongCode && !playedAudio && Time.time - startTime > 3.5)
        {
            playedAudio = true;
            gameObject.GetComponent<AudioSource>().clip = audioClips[0];
            gameObject.GetComponent<AudioSource>().Play();
        }
        if (!diningRoomDoor.GetComponent<Door>().isLocked && Time.time - startTime > 3) Destroy(gameObject);

    }

    private bool AllLit()
    {
        for(int i = 0; i < candles.Length; i++)
        {   //returns false if any candle is not lit
            if (!candles[i].GetComponent<LightCandles>().GetLightingState()) return false;
        }
        //returns true if all candles are lit.
        return true;
    }

    bool CheckProperOrder()
    {

        for (int i = candles.Length - 1; i > 0; i--)
        {   //if a candle is lit but the candle that that was supposed to be lit before it is not, returns false.
            if (candles[i].GetComponent<LightCandles>().GetLightingState() && !candles[i - 1].GetComponent<LightCandles>().GetLightingState())
            {
                Debug.Log("Wrong order candles");
                return false;
            }
        }
        return true;
    }
}
