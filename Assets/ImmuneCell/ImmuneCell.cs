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

    // 基础属性
    public int index;            // 防御塔编号
    public ImmuneCellType type;  // 防御塔类型
    public int rank;             // 防御塔等级
    public int maxRank;          // 最大等级


    public byte attackPower;         // 攻击力
    public byte attackRange;         // 攻击范围
    public byte attackSpeed;        // 攻击速度(每回合攻击次数)
    public byte attackLeft;         // 剩余攻击次数

    public void NextRound()
    {
        attackLeftReset();
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
    abstract protected void GridsImmuneChange();

    abstract public void attack(GameObject pathogen);

}
