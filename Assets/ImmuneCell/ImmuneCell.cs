using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImmuneCellType
{
    MacrophageCell = 0
}

abstract public class ImmuneCell : MonoBehaviour
{
    public Position p;          // ������λ��
    public GameObject tower;    // �������Ķ�����

    // ��������
    public int index;            // ���������
    public ImmuneCellType type;  // ����������
    public int rank;             // �������ȼ�
    public int maxRank;          // ���ȼ�

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    abstract public void Upgrade();

    abstract public void SpriteChange();

}
