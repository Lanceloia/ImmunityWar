using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ο�
public class ImmuneCells : MonoBehaviour
{
    // ��������
    public int immuneCellIndex;           // ����������
    public int rank;                  // ��ǰ�ȼ�
    public float attackPower;         // ������

    // ��������
    public int initCost;              // �������
    public int upgradeCost;           // ��������

    // �����������
    public float skillCooldown;       // ������ȴ����
    public float curCooldown;         // ��ǰ������ȴ״̬
    public bool canUseSkill;          // ��ʶ�����Ƿ����

    // ��ʼ����������������ʱ�ĳ�ʼ����
    public void Init(int index, int initCost, int upgradeCost, float power, float cooldown)
    {
        immuneCellIndex = index;
        rank = 1;  // ��ʼ�ȼ�Ϊ1
        attackPower = power;

        this.initCost = initCost;      // ��ʼ���������
        this.upgradeCost = upgradeCost; // ��ʼ����������
        skillCooldown = cooldown;
        curCooldown = 0;
        canUseSkill = true;

        Debug.Log(index + " initialized. Build cost: " + initCost + ", Upgrade cost: " + upgradeCost);
    }

    // ������������������������������
    public virtual void Upgrade()
    {
        rank++;
        attackPower *= 1.2f;  // �����󹥻�������20%
        upgradeCost += 100;   // ������������������100

        Debug.Log(immuneCellIndex + " upgraded to rank " + rank + ". New upgrade cost: " + upgradeCost);
    }

    // ��ͨ�������������幥����Ϊ
    public virtual int Attack(Transform target)
    {
        if (target == null)
        {
            Debug.Log("No target to attack.");
            return 0;
        }

        Debug.Log(immuneCellIndex + " is attacking " + target.name + " with power " + attackPower);
        return 0;
        // �������ʵ�ֹ���Ŀ����߼��������Ŀ������˺�
        // ���磺target.GetComponent<Enemy>().TakeDamage(attackPower);
    }

    // �����ͷŷ��������弼����Ϊ
    public virtual void Skill()
    {
        if (!canUseSkill)
        {
            Debug.Log("Skill is on cooldown, current cooldown: " + curCooldown + " seconds remaining.");
            return;
        }

        // �ͷż��ܵ��߼�
        Debug.Log(immuneCellIndex + " is using skill!");

        // ������ȴ����
        canUseSkill = false;
        curCooldown = skillCooldown;
    }
    public void CoolDown()
    {
        // ������ȴ�߼�
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
    // ��������ȴ
    private void Update()
    {
        
    }
}
