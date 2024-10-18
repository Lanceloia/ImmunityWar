using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImmuneCellType
{
    MacrophageCell = 0
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
