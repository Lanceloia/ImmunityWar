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
    public List<Position> StemCellsOriginPosition;    // ͼ��������ʼ���λ��
    public List<Position> PathogensOriginPosition;    // ͼ�����в�ԭ������λ��
    public List<GameObject> GridsList;           // ͼ�����и��ӵĶ����

    protected Dictionary<Position, GameObject> GridsDict; // ͼ�����и��ӵ�λ�úͶ���

    abstract public void Init();    // ��ʼ�����ݳ�Ա
    abstract public Vector3 PositionChange(Position p); // ��ͼ����ת��Ϊ��������

    public GameObject GetGridsFromPosition(Position p) // ���������ȡ���Ӷ���
    {
        try
        {
            return GridsDict[p];
        }
        catch (KeyNotFoundException ex){
            Debug.Log(string.Format("Key not found position ({0},{1})", p.x, p.y));
            return new GameObject();
        };
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
