using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridsType
{
    None = 0,
    StartGrid = 1,
    MainWayGrid = 2,
    ImmuneCellGrid = 3,
    PathogenGrid = 4,
}

public enum Direction
{
    TopLeft = 0,
    TopRight = 1,
    BottomRight = 2,
    BottomLeft = 3
}

abstract public class Grids : MonoBehaviour
{
    // 数据成员
    public GridsType type;   // 格子的类型
    public Position p;             // 格子的地图坐标（内部计算用）
    public int x, y;               // 格子的地图坐标（外部绑定用）

    public Direction next;         // 下一个格子的方向
    public bool accessRoad;        // 是否存在支路
    public Direction accessRoadNext; // 支路的下一个格子的方向
    bool canActiveStay;              // 能否主动停留在此处

    // List of ImmuneCells    // 一个列表，表示这个格子在列表中的细胞的攻击范围内

    // 干细胞经过时的处理
    public abstract void onStemCellPassBy();

    // 干细胞停留时处理
    public abstract void onStemCellStay();

    private void Awake()
    {
        p.x = x;
        p.y = y;
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