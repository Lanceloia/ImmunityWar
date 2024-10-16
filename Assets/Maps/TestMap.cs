using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 存储测试用地图TestMap的地图数据
public class TestMap : Maps
{

    public override void Init()
    {
        Debug.Log("Init Map");
        //获得四位玩家的初始位置
        StemCellsOriginPosition = new List<Position>();
        StemCellsOriginPosition.Add(new Position(4, 1));
        StemCellsOriginPosition.Add(new Position(13, 6));
        StemCellsOriginPosition.Add(new Position(13, 16));
        StemCellsOriginPosition.Add(new Position(4, 11));

        //获得四位玩家的初始位置
        PathogensOriginPosition = new List<Position>();
        PathogensOriginPosition.Add(new Position(11, 4));
        PathogensOriginPosition.Add(new Position(16, 12));
        PathogensOriginPosition.Add(new Position(6, 13));
        PathogensOriginPosition.Add(new Position(1, 5));

        //以坐标为key将所有格子存入字典
        GridsDict = new Dictionary<Position, GameObject>();
        for (int i = 0; i < GridsList.Count; i++) {
            // 此时P全是0，无语
            // Position p = GridsList[i].GetComponent<Grids>().p;
            // Debug.Log(string.Format("i={0}: p.x={1}, p.y={2}", i, p.x, p.y));
            int x = GridsList[i].GetComponent<Grids>().x;
            int y = GridsList[i].GetComponent<Grids>().y;
            // Debug.Log(string.Format("i={0}: x={1}, y={2}", i, x, y));
            GridsDict[new Position(x, y)] = GridsList[i];
        }
    }

    public override Vector3 PositionChange(Position p)  // 坐标转换计算
    {
        int x = p.x;
        int y = p.y;

        // z轴坐标设置为-1，显示在地图前面
        Vector3 pos = new Vector3(0, 0, -1);
        pos.x = (float)(-8.0 + (float)0.5 * x + (float)0.5 * y);
        pos.y = (float)(0.0 + (float)0.25 * x - (float)0.25 * y);
        return pos;
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
