using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridsType
{
    None = 0,
    StartGrid = 1,
    MainWayGrid = 2,
    ImmuneCellGrid = 3,
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
    // ���ݳ�Ա
    GridsType type;   // ���ӵ�����
    public Position p;             // ���ӵĵ�ͼ���꣨�ڲ������ã�
    public int x, y;               // ���ӵĵ�ͼ���꣨�ⲿ���ã�

    public Direction next;         // ��һ�����ӵķ���
    public bool accessRoad;        // �Ƿ����֧·
    public Direction accessRoadNext; // ֧·����һ�����ӵķ���
    bool canActiveStay;              // �ܷ�����ͣ���ڴ˴�

    // List of ImmuneCells    // һ���б�����ʾ����������б��е�ϸ���Ĺ�����Χ��

    // ��ϸ������ʱ�Ĵ���
    public abstract void onStemCellPassBy();

    // ��ϸ��ͣ��ʱ����
    public abstract void onStemCellStay();


    // Start is called before the first frame update
    void Start()
    {
        p.x = x;
        p.y = y;
    }

    // Update is called once per frame
    void Update()
    {

    }
}