using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class roomInfoScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomInfoText;
    [SerializeField] float fadeInTime, stayTime, fadeOutTime;
    public static bool fadeRoomText = false;
    private float fadeTimer = 0;
    private GameObject onScreenUI;

    // Start is called before the first frame update
    void Start()
    {
        roomInfoText.color = new Color(1, 1, 1, 0);
        onScreenUI = GameObject.Find("On Screen UI");

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeRoomText)
        {
            if (Time.time - fadeTimer <= fadeInTime)
            {
                float alpha = (Time.time - fadeTimer) / fadeInTime;
                roomInfoText.color = new Color(roomInfoText.color.r, roomInfoText.color.g, roomInfoText.color.b, alpha);
            }
            else if (Time.time - fadeTimer <= fadeInTime + stayTime)
            {
                roomInfoText.color = new Color(roomInfoText.color.r, roomInfoText.color.g, roomInfoText.color.b, 1);
            }
            else if (Time.time - fadeTimer <= fadeInTime + stayTime + fadeOutTime)
            {
                float alpha = 1 - (Time.time - fadeTimer - fadeInTime - stayTime) / fadeOutTime;

                roomInfoText.color = new Color(roomInfoText.color.r, roomInfoText.color.g, roomInfoText.color.b, alpha);
            }
            else
            {
                fadeRoomText = false;
            }
        }
    }
    public void setRoomInfoText(string newRoomText)
    {
        roomInfoText.text = newRoomText;
        fadeRoomText = true;
        fadeTimer = Time.time;
        onScreenUI.GetComponent<OnScreenUIScript>().currentRoom.text = newRoomText;
    }
}
