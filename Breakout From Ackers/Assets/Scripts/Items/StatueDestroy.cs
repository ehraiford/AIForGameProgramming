using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueDestroy : MonoBehaviour
{
    [SerializeField] AudioSource shatterSound;
    [SerializeField] GameObject bookcase;
    private void OnDestroy()
    {
        bookcase.GetComponent<MoveBookcase>().decreaseNumStatue(1);
        shatterSound.Play();
    }
}
