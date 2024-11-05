using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
  
    public delegate void BuildClickAction();
    public event BuildClickAction OnBuildClicked;

    void Start()
    {
       
        if (OnBuildClicked == null)
        {
            
            OnBuildClicked += DefaultClickAction;
        }
    }

    void OnMouseDown()
    {
        
        if (OnBuildClicked != null)
        {
            OnBuildClicked.Invoke();
        }
    }

    
    private void DefaultClickAction()
    {
        Debug.Log("no");
        
        Board.instance.isBuilding = false;
        Board.instance.isSelectingBuild = false;

    }
}