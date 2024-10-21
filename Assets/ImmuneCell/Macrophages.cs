using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ϸ���࣬�̳�������ϸ����(���ο�)
public class Macrophages : ImmuneCells
{
    // �ض�����
    public float stunChance;       // ���μ���
    public float stunDuration;     // ���γ���ʱ��
    public float bossDamage;       // ��Boss�����⼼���˺�
    public bool autoDevourUnlocked = false; // �Զ������Ƿ����

    public GameObject devourEffect; // ������Ч��Ԥ����

    // ��ʼ�����������Ӿ���ϸ�����ض�����
    public void InitMacrophages(int index, int initCost, int upgradeCost, float power,  float cooldown, float stunChance, float stunDuration, float bossDamage)
    {
        base.Init(index, initCost, upgradeCost, power, cooldown);
        this.stunChance = stunChance;
        this.stunDuration = stunDuration;
        this.bossDamage = bossDamage;
    }

    // ��ȭ��������ͨ���ܣ����м��ʻ��ε��˲�����˺�
    public void HeavyPunch(int target)
    {
        Debug.Log(immuneCellIndex + " hits the pathogen with a heavy punch!");

        // �����Ƿ����
        
        if (Random.value <= stunChance)
        {
            
            Debug.Log(target + " is stunned and movement reduced by half!");
            //TBD:����
        }

        // ����˺�
        //TBD:�����˺�������
    }

    // ���ɣ����⼼�ܣ�������С�Ͳ�ԭ����Boss��ɴ����˺�
    public void Devour(int target)
    {
        /*if (target.IsBoss)
        {
            // ��Boss��ɴ����˺�
            target.TakeDamage(bossDamage);
            Debug.Log(cellName + " dealt massive damage to the boss pathogen!");
        }
        else
        {
            // ���ɲ�ԭ��
            Debug.Log(cellName + " devoured the pathogen!");
            Destroy(target.gameObject); // �Ƴ���ԭ��

            // ��ʾ����Ч��
            Instantiate(devourEffect, target.transform.position, Quaternion.identity);
        }*/
        //TBD
    }

    // ��дUpgrade������ǿ���˺��ͻ��μ���
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
            UnlockAutoDevour();// ����ﵽ�����������Զ�����
        }
       
        Debug.Log(immuneCellIndex + " upgraded: Heavy punch damage: " + attackPower + ", Stun chance: " + stunChance);

        

    }

    // �Զ����ɣ������Զ����ɻ���
    private void UnlockAutoDevour()
    {
        autoDevourUnlocked = true;
        Debug.Log("Auto-devour unlocked! Macrophages will automatically devour smaller pathogens.");
    }

    // ���º������Զ�����
    private void Update()
    {
        if (autoDevourUnlocked)
        {
            AutoDevourNearbyPathogens(); // �Զ����ɸ�����ԭ��
        }
    }

    // �Զ����ɸ�����ԭ��
    private void AutoDevourNearbyPathogens()
    {
        //TBD
    }
}

