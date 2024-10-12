using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingList : MonoBehaviour
{
    AudioSource audioS;
    public AudioClip clicking;
    public GameObject settingList;
    // Start is called before the first frame update
    void Start()
    {
        if (settingList != null)
        {
            settingList.SetActive(false);
        }
        
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
        if (settingList != null)
        {
            settingList.SetActive(true); // 显示菜单
        }
        SceneManager.LoadScene(1);
    }
}
