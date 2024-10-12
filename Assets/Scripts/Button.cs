using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    AudioSource audioS;
    public AudioClip clicking;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickSound()
    {
        audioS.clip = clicking;
        audioS.Play();
    }
}
