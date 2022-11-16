using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passThroughDoorScript : MonoBehaviour
{

    public bool firstCollider, secondCollider;
    [SerializeField] string firstRoomName, secondRoomName;
    [SerializeField] GameObject roomInfo;
    public int cameFromFirstSecondNone;
    private void Update()
    {
        
    }

    internal void colliderEntered(bool isFirst)
    {
        if (isFirst)
        {
            firstCollider = true;
        }
        else
        {
            secondCollider = true;
        }

        if(cameFromFirstSecondNone == 0)
        {
            if (isFirst) cameFromFirstSecondNone = 1;
            else cameFromFirstSecondNone = 2;
        }
    }

    internal void colliderExited(bool isFirst)
    {
        if (isFirst)
        {
            firstCollider = false;
        }
        else
        {
            secondCollider = false;
        }

        if (firstCollider == false && secondCollider == false)
        {
            if (isFirst && cameFromFirstSecondNone == 2) roomInfo.GetComponent<roomInfoScript>().setRoomInfoText(firstRoomName);
            else if (!isFirst && cameFromFirstSecondNone == 1) roomInfo.GetComponent<roomInfoScript>().setRoomInfoText(secondRoomName);
            cameFromFirstSecondNone = 0;
        }
    }
}
