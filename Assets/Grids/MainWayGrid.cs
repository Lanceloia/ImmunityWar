using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWayGrid : Grids
{
    public bool canbuild2x2;
    public GameObject build2x2;
    private void _init()
    {
        type = GridsType.MainWayGrid;
    }
    void Start()
    {
        _init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void onStemCellPassBy()
    {
        throw new System.NotImplementedException();
    }

    public override void onStemCellStay()
    {
        if (canbuild2x2)
        {
            build2x2.SetActive(true);
        }
        build2x2.GetComponent<Macrophages>().Upgrade();
        /*É¾³ý½¨Öþ
        if (canbuild2x2 && Input.GetKeyDown(KeyCode.B))
        {
            build2x2.SetActive(false);
        }
        */
    }
}
