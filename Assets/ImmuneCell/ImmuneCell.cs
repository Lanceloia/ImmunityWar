using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImmuneCellType
{
    MacrophageCell = 0,
    BCell = 1,
    TCell = 2
}

abstract public class ImmuneCell : MonoBehaviour
{
    public Position p;          // ������λ��
    public GameObject tower;    // �������Ķ�����

    public GameObject grid;   // ���������ڵĸ���

    // ��������
    public int index;            // ���������
    public ImmuneCellType type;  // ����������
    public int rank;             // �������ȼ�
    public int maxRank;          // ���ȼ�


    public byte attackPower;         // ������
    public byte attackRange;         // ������Χ
    public byte attackSpeed;        // �����ٶ�(ÿ�غϹ�������)
    public byte attackLeft;         // ʣ�๥������


    public byte ATPcost;     // ���������ʱATP����
    public byte antigenCost;     // ���������ʱ��ԭ����
    public GameObject grid;   // ���������ڵĸ���

    public int CytokineLeft;     // Cytokine����ģʽ��ʣ�༸�غ�,1����ֱ�����
    public int tempCytokine;     // Cytokine����ģʽ����ʱ����¼Cytokine�ĵȼ�

    public virtual void NextRound()
    {
        attackLeftReset();
        if (CytokineLeft > 0)
        {
            CytokineLeft--;
            if (CytokineLeft == 0)
            {
                CytokineAccepted(0);
            }
        }
    }
    public void attackLeftReset()
    {
        attackLeft = attackSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    abstract public void Upgrade(ShapeType shapeType);

    abstract public void SpriteChange();

    abstract public void AttackChange();
    abstract protected void GridsImmuneChange(ShapeType shapeType);

    abstract public void attack(GameObject pathogen);

    abstract public void CytokineAccepted(int cyRank);
}
