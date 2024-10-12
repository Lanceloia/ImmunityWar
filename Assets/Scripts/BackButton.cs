using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
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
    public void OnButtonClick()
    {
        audioS = GetComponent<AudioSource>();
        audioS.clip = clicking;
        audioS.PlayOneShot(clicking);
        SceneManager.LoadScene(0);
    }
}
