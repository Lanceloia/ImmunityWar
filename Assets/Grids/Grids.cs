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
    // ���ݳ�Ա
    int type;   // ���ӵ�����
    int next;   // ��һ�����ӵķ���
    bool accessRoad;    // �Ƿ����֧·
    int accessRoadNext; // ֧·�ķ���
    bool canStay;       // �ܷ�����ͣ���ڴ˴�


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}