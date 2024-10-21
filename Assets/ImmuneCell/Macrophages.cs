using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 巨噬细胞类，继承自免疫细胞类(供参考)
public class Macrophages : ImmuneCells
{
    // 特定属性
    public float stunChance;       // 击晕几率
    public float stunDuration;     // 击晕持续时间
    public float bossDamage;       // 对Boss的特殊技能伤害
    public bool autoDevourUnlocked = false; // 自动吞噬是否解锁

    public GameObject devourEffect; // 吞噬特效的预制体

    // 初始化方法，增加巨噬细胞的特定属性
    public void InitMacrophages(int index, int initCost, int upgradeCost, float power,  float cooldown, float stunChance, float stunDuration, float bossDamage)
    {
        base.Init(index, initCost, upgradeCost, power, cooldown);
        this.stunChance = stunChance;
        this.stunDuration = stunDuration;
        this.bossDamage = bossDamage;
    }

    // 重拳出击（普通技能）：有几率击晕敌人并造成伤害
    public void HeavyPunch(int target)
    {
        Debug.Log(immuneCellIndex + " hits the pathogen with a heavy punch!");

        // 计算是否击晕
        
        if (Random.value <= stunChance)
        {
            
            Debug.Log(target + " is stunned and movement reduced by half!");
            //TBD:击晕
        }

        // 造成伤害
        //TBD:计算伤害并返回
    }

    // 吞噬（特殊技能）：吞噬小型病原体或对Boss造成大量伤害
    public void Devour(int target)
    {
        /*if (target.IsBoss)
        {
            // 对Boss造成大量伤害
            target.TakeDamage(bossDamage);
            Debug.Log(cellName + " dealt massive damage to the boss pathogen!");
        }
        else
        {
            // 吞噬病原体
            Debug.Log(cellName + " devoured the pathogen!");
            Destroy(target.gameObject); // 移除病原体

            // 显示吞噬效果
            Instantiate(devourEffect, target.transform.position, Quaternion.identity);
        }*/
        //TBD
    }

    // 重写Upgrade方法，强化伤害和击晕几率
    public override void Upgrade()
    {
        base.Upgrade();
        if (rank == 1)
        {
            attackPower *= 1.2f;        
        }else if(rank == 2)
        {
            stunChance *= 1.2f;
        }else if(rank == 3) { 
            UnlockAutoDevour();// 如果达到满级，解锁自动吞噬
        }
       
        Debug.Log(immuneCellIndex + " upgraded: Heavy punch damage: " + attackPower + ", Stun chance: " + stunChance);

        

    }

    // 自动吞噬：解锁自动吞噬机制
    private void UnlockAutoDevour()
    {
        autoDevourUnlocked = true;
        Debug.Log("Auto-devour unlocked! Macrophages will automatically devour smaller pathogens.");
    }

    // 更新函数，自动吞噬
    private void Update()
    {
        if (autoDevourUnlocked)
        {
            AutoDevourNearbyPathogens(); // 自动吞噬附近病原体
        }
    }

    // 自动吞噬附近病原体
    private void AutoDevourNearbyPathogens()
    {
        //TBD
    }
}

