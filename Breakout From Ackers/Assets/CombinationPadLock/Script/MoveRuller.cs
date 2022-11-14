// Original Script by Marcelli Michele

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveRuller : MonoBehaviour
{

    public bool isActive;

    [HideInInspector]
    public List <GameObject> _rullers = new List<GameObject>();
    private int _scroolRuller = 0;
    private int _changeRuller = 0;

    [HideInInspector]
    public int[] _numberArray = {0,0,0,0};
    private int _numberRuller = 0;
    private bool _isActveEmission = false;
    private float time;
    private bool unlocked = false;

    [SerializeField] int[] _numberPassword = { 0, 0, 0, 0 };
    [SerializeField] GameObject firstPersonControllerCamera;
    [SerializeField] GameObject combinationLockCamera;
    [SerializeField] GameObject parentObject;
    [SerializeField] AudioSource audioSource;
    
    [SerializeField] AudioClip failedToUnlock;
    [SerializeField] AudioClip successfullyUnlock;
    [SerializeField] GameObject[] rullers;
    void Awake()
    {
        

        _rullers.Add(rullers[0]);
        _rullers.Add(rullers[1]);
        _rullers.Add(rullers[2]);
        _rullers.Add(rullers[3]);
        _numberRuller = 0;
        _isActveEmission = true;
        foreach (GameObject r in _rullers)
        {
            r.transform.Rotate(-144, 0, 0, Space.Self);
        }
    }
    void Update()
    {
        if (isActive)
        {
            MoveRulles();
            RotateRullers();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("KeyCode read");
                Password();
            }
        }
        if (unlocked)
        {
           
            if (Time.time - time > 1.5)
            { 
                parentObject.GetComponent<Animator>().SetBool("isOpen", true);
                parentObject.GetComponent<AudioSource>().Play();
                Destroy(gameObject);
            }
        }
    }

    void MoveRulles()
    {
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            _isActveEmission = true;
            _changeRuller ++;
            _numberRuller += 1;

            if (_numberRuller > 3)
            {
                _numberRuller = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            _isActveEmission = true;
            _changeRuller --;
            _numberRuller -= 1;

            if (_numberRuller < 0)
            {
                _numberRuller = 3;
            }
        }
        _changeRuller = (_changeRuller + _rullers.Count) % _rullers.Count;


        for (int i = 0; i < _rullers.Count; i++)
        {
            if (_isActveEmission)
            {
                if (_changeRuller == i)
                {

                    _rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = true;
                    _rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
                }
                else
                {
                    _rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
                    _rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
                }
            }
        }

    }

    public void  Password()
    {
        combinationLockCamera.SetActive(false);
        firstPersonControllerCamera.SetActive(true);
        isActive = false;

        for (int i = 0; i < _rullers.Count; i++)
        {
            _rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
            _rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
        }

        if (_numberArray.SequenceEqual(_numberPassword))
        {
            
            Debug.Log("Correct Password was entered.");
            time = Time.time;
            unlocked = true;
            audioSource.clip = successfullyUnlock;
            audioSource.Play();
            
        }
        else
        {
            Debug.Log("Incorrect Password was entered.");

            audioSource.clip = failedToUnlock;
            audioSource.Play();
        }
    }
    void RotateRullers()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _isActveEmission = true;
            _scroolRuller = 36;
            _rullers[_changeRuller].transform.Rotate(-_scroolRuller, 0, 0, Space.Self);

            _numberArray[_changeRuller] += 1;

            if (_numberArray[_changeRuller] > 9)
            {
                _numberArray[_changeRuller] = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _isActveEmission = true;
            _scroolRuller = 36;
            _rullers[_changeRuller].transform.Rotate(_scroolRuller, 0, 0, Space.Self);

            _numberArray[_changeRuller] -= 1;

            if (_numberArray[_changeRuller] < 0)
            {
                _numberArray[_changeRuller] = 9;
            }
        }
    }
}
