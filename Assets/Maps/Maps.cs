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
}

abstract public class Maps : MonoBehaviour
{
    // ͼ��������ʼ���λ��
    public List<Position> StemCellsOriginPosition;

    // ��ʼ�����ݳ�Ա
    abstract public void Init();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
