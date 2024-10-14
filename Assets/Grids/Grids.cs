using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GridsType 
{
    None = 0,
    StartGrid = 1,
    MainWayGrid = 2
}

enum Direction
{
    TopLeft = 0,
    TopRight = 1,
    BottomRight = 2,
    BottomLeft = 3
}

public class Grids : MonoBehaviour
{
    // 数据成员
    int type;   // 格子的类型
    int next;   // 下一个格子的方向
    bool accessRoad;    // 是否存在支路
    int accessRoadNext; // 支路的方向
    bool canStay;       // 能否主动停留在此处


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}