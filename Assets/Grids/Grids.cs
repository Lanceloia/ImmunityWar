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
    SmallSquare = 0,    //1x1�ĸ���
    UpTriangle = 1,   // 2x1�ĸ���,�������������ϵ�
    
    DownTriangle = 2,   // 1x2�ĸ���,�������������µ�

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
    // ���ݳ�Ա
    public GridsType type;   // ���ӵ�����
    public ShapeType shape;      // ���ӵ���״//��Ҫ��
    public Position p;             // ���ӵĵ�ͼ���꣨�ڲ������ã�
    public int x, y;               // ���ӵĵ�ͼ���꣨�ⲿ���ã�

    public HashSet<GameObject> immuneCells = new HashSet<GameObject>();     // ���Թ�����������ӵķ�����

    public Direction next;         // ��һ�����ӵķ���
    public Direction pre;
    public bool accessRoad;        // �Ƿ����֧·
    public Direction accessRoadNext; // ֧·����һ�����ӵķ���
    bool canActiveStay;              // �ܷ�����ͣ���ڴ˴�

    public bool nearHistiocyte;//�Ƿ��ڽ���֯ϸ��
    public Direction HistiocyteNext;//��֯ϸ������

    // List of ImmuneCells    // һ���б�����ʾ����������б��е�ϸ���Ĺ�����Χ��

    // ��ϸ������ʱ�Ĵ���
    public abstract void onStemCellPassBy(GameObject stemCell);


    // ��ϸ��ͣ��ʱ����
    public abstract IEnumerator onStemCellStay();

    // ��������ʱ�Ĵ���
    public abstract void onPathogenCellPassBy(GameObject pathogen);

    // ����ͣ��ʱ����
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