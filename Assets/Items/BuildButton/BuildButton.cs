using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
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
        Debug.Log("yes");

        Board.instance.isBuilding = true;
        Board.instance.isSelectingBuild = false;

    }
}