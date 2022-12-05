using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeControlScript : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float timeStarted, inTime, stayTime, outTime;
    private bool fading;
    private Color textColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fading)
        {
            if(Time.time - timeStarted < inTime)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 255 * ((Time.realtimeSinceStartup - timeStarted)/inTime));
            } else if(Time.realtimeSinceStartup - timeStarted < inTime + stayTime)
            {
                
            } else if(Time.time - timeStarted < inTime + stayTime + outTime)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 255 * ((Time.realtimeSinceStartup - timeStarted) / (inTime + stayTime)));
            }
            
        }
        else text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);

    }

    public void FadeInAndOut(TextMeshProUGUI fadeText, float newInTime, float newStayTime, float newOutTime ) 
    {
        fading = true;
        text = fadeText;
        textColor = fadeText.color;
        inTime = newInTime;
        stayTime = newStayTime;
        outTime = newOutTime;
        timeStarted = Time.time;

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

    }
}
