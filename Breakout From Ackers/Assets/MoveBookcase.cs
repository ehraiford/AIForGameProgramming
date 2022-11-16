using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBookcase : MonoBehaviour
{
    [SerializeField] public int numOfStatues;
    [SerializeField] private AudioSource rumbleSound;
    private bool doOnce = true;

    // Update is called once per frame
    void Update()
    {
        if(numOfStatues <= 0 && doOnce)
        {
            this.transform.position = new Vector3(-30.747f, 7.1769f, 10.073f);
            doOnce = false;
            rumbleSound.Play();
        }
        
    }

    public void decreaseNumStatue(int x)
    {
        numOfStatues -= x;
    }
}
