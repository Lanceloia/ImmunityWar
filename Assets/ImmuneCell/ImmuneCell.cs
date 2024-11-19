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
    public Position p;          // 防御塔位置
    public GameObject tower;    // 防御塔的对象本身

    public GameObject grid;   // 防御塔所在的格子

    // 基础属性
    public int index;            // 防御塔编号
    public ImmuneCellType type;  // 防御塔类型
    public int rank;             // 防御塔等级
    public int maxRank;          // 最大等级


    public byte attackPower;         // 攻击力
    public byte attackRange;         // 攻击范围
    public byte attackSpeed;        // 攻击速度(每回合攻击次数)
    public byte attackLeft;         // 剩余攻击次数


    public byte ATPcost;     // 建造或升级时ATP消耗
    public byte antigenCost;     // 建造或升级时抗原消耗
    public GameObject grid;   // 防御塔所在的格子

    public int CytokineLeft;     // Cytokine激活模式还剩余几回合,1代表直接清除
    public int tempCytokine;     // Cytokine激活模式激活时，记录Cytokine的等级

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
