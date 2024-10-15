using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 存储测试用地图TestMap的地图数据
public class TestMap : Maps
{
    public override void Init()
    {
        //有bug，暂时注释掉
        //StemCellsOriginPosition.Clear();
        //StemCellsOriginPosition.Add(new Position(4, 1));//地图坐标（4，1）
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
