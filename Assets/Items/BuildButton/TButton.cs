using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TButton : MonoBehaviour
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
        Debug.Log("T");

        Board.instance.isBuilding = true;
        Board.instance.buildingType = 1;
        Board.instance.isSelectingBuild = false;
        

    }
}