using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWayGrid : Grids
{
    public bool canbuild2x2;
    public ImmuneCellGrid build2x2;//2x2的ImmuneCellGrid
    public TestMap testMap;
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
        if(build2x2.towertype[0] == null)
        {
            Debug.Log("没有2x2建筑");
        }
        
        if (canbuild2x2&&build2x2.hasTower==false)
        {
           ImmuneCells.immuneCells.Add(Instantiate(build2x2.towertype[0] ,testMap.PositionChange(build2x2.p), Quaternion.identity));
        }
        
        //获得其编号，并对这个对象的Macrophages初始化（init()）

        //对该编号的塔upgrade



    }
}
