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
    AccessRoadGrid = 5,
    HistiocyteGrid = 6,
}

public enum ShapeType
{
    SmallSquare = 0,    //1x1的格子
    UpTriangle = 1,   // 2x1的格子,从左向右是向上的
    
    DownTriangle = 2,   // 1x2的格子,从左向右是向下的

    BigSquare = 3,
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
    public ShapeType shape;      // 格子的形状//需要绑定
    public Position p;             // 格子的地图坐标（内部计算用）
    public int x, y;               // 格子的地图坐标（外部绑定用）

    public HashSet<GameObject> immuneCells = new HashSet<GameObject>();     // 可以攻击到这个格子的防御塔

    public Direction next;         // 下一个格子的方向
    public bool accessRoad;        // 是否存在支路
    public Direction accessRoadNext; // 支路的下一个格子的方向
    bool canActiveStay;              // 能否主动停留在此处

    public bool nearHistiocyte;//是否邻近组织细胞
    public Direction HistiocyteNext;//组织细胞方向

    // List of ImmuneCells    // 一个列表，表示这个格子在列表中的细胞的攻击范围内

    // 干细胞经过时的处理
    public abstract void onStemCellPassBy(GameObject stemCell);

    // 干细胞停留时处理
    public abstract IEnumerator onStemCellStay();

    // 病菌经过时的处理
    public abstract void onPathogenCellPassBy(GameObject pathogen);

    // 病菌停留时处理
    public abstract void onPathogenCellStay(GameObject pathogen);
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