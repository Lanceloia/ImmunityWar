using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 存储测试用地图TestMap的地图数据
public class TestMap : Maps
{
    public override void Init()
    {
        StemCellsOriginPosition.Clear();
        StemCellsOriginPosition.Add(new Position(4, 1));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
