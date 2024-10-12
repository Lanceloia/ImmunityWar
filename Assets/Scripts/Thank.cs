using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Thank : MonoBehaviour
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
        // 播放音效

        // 弹出菜单

        SceneManager.LoadScene(2);
    }
}
