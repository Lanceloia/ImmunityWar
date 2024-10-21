using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//供参考
public class ImmuneCells : MonoBehaviour
{
    // 基础属性
    public int immuneCellIndex;           // 防御塔名称
    public int rank;                  // 当前等级
    public float attackPower;         // 攻击力

    // 经济属性
    public int initCost;              // 建造费用
    public int upgradeCost;           // 升级费用

    // 技能相关属性
    public float skillCooldown;       // 技能冷却周期
    public float curCooldown;         // 当前技能冷却状态
    public bool canUseSkill;          // 标识技能是否可用

    // 初始化方法，用于造塔时的初始设置
    public void Init(int index, int initCost, int upgradeCost, float power, float cooldown)
    {
        immuneCellIndex = index;
        rank = 1;  // 初始等级为1
        attackPower = power;

        this.initCost = initCost;      // 初始化建造费用
        this.upgradeCost = upgradeCost; // 初始化升级费用
        skillCooldown = cooldown;
        curCooldown = 0;
        canUseSkill = true;

        Debug.Log(index + " initialized. Build cost: " + initCost + ", Upgrade cost: " + upgradeCost);
    }

    // 升级方法，用于提升防御塔的属性
    public virtual void Upgrade()
    {
        rank++;
        attackPower *= 1.2f;  // 升级后攻击力增加20%
        upgradeCost += 100;   // 升级后升级费用增加100

        Debug.Log(immuneCellIndex + " upgraded to rank " + rank + ". New upgrade cost: " + upgradeCost);
    }

    // 普通攻击方法，定义攻击行为
    public virtual int Attack(Transform target)
    {
        if (target == null)
        {
            Debug.Log("No target to attack.");
            return 0;
        }

        Debug.Log(immuneCellIndex + " is attacking " + target.name + " with power " + attackPower);
        return 0;
        // 这里可以实现攻击目标的逻辑，例如对目标造成伤害
        // 例如：target.GetComponent<Enemy>().TakeDamage(attackPower);
    }

    // 技能释放方法，定义技能行为
    public virtual void Skill()
    {
        if (!canUseSkill)
        {
            Debug.Log("Skill is on cooldown, current cooldown: " + curCooldown + " seconds remaining.");
            return;
        }

        // 释放技能的逻辑
        Debug.Log(immuneCellIndex + " is using skill!");

        // 技能冷却处理
        canUseSkill = false;
        curCooldown = skillCooldown;
    }
    public void CoolDown()
    {
        // 技能冷却逻辑
        if (!canUseSkill)
        {
            curCooldown -= 1;
            if (curCooldown <= 0)
            {
                canUseSkill = true;
                curCooldown = 0;
                Debug.Log("Skill is ready to use again.");
            }
        }
    }
    // 处理技能冷却
    private void Update()
    {
        
    }
}
