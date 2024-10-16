using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Position
{
    public int x;
    public int y;
    public Position(int _x, int _y)
    {
        x = _x; y = _y;
    }

    public static Position operator +(Position p, Direction dir)
    {
        if (dir == Direction.TopLeft)
        {
            return new Position(p.x, p.y - 1);
        }
        else if (dir == Direction.TopRight)
        {
            return new Position(p.x + 1, p.y);
        }
        else if(dir == Direction.BottomRight)
        {
            return new Position(p.x, p.y + 1);
        }
        else
        {
            return new Position(p.x - 1, p.y);
        }
    }
}

abstract public class Maps : MonoBehaviour
{
    public List<Position> StemCellsOriginPosition;    // 图上所有起始点的位置
    public List<Position> PathogensOriginPosition;    // 图上所有病原体区的位置
    public List<GameObject> GridsList;

    protected Dictionary<Position, GameObject> GridsDict;

    abstract public void Init();    // 初始化数据成员
    abstract public Vector3 PositionChange(Position p); // 地图坐标转换为世界坐标

    public GameObject GetGridsFromPosition(Position p)
    {
        return GridsDict[p];
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
