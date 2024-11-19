using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathogenType
{
   bacteria,
   virus,
   fungi

}
abstract public class Pathogen : MonoBehaviour
{
    public Position p;      //病菌位置
    public int index;       //细菌编号

    public PathogenType type;      //病菌类型
    public AntigenType antigenType;     //病菌的抗原类型
    public int health;     //病菌血量
    public bool isIn;      //病菌是否在宿主内

    public GameObject bCell;     //攻击这个病菌的B细胞

    public int antiTime = 0; //被抗体包裹的时间

    public int targetIndex;

    public Vector3 target;
    public float speed;
    public bool isMove = false;

    void Start()
    {
       
    }

    void Update()
    {
        
        if (isMove)
        {
            //逐格移动
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (target != null)
        {
            if (transform.position == target)
            {
                isMove = false;
               
            }
        }
    }

    abstract public void onHurt(int damage);      //受伤时调用

    virtual public void beAntibodyed(AntiBody antibody)      //接受抗体时调用
    {
        //将antibody所挂对象设置为病菌的子对象
        antibody.transform.position = this.transform.position;
        antibody.transform.parent = this.transform;
        bCell = antibody.bCell;
    }

    public void antibodyDamage()
    {
        if(bCell != null)
        {
            if(antiTime == 0)
            {
                this.gameObject.GetComponent<Pathogen>().onHurt(1);
                antiTime ++;
            }
            else
            {
                int damage = bCell.GetComponent<BCell>().attackPower;
                damage += antiTime;
                this.gameObject.GetComponent<Pathogen>().onHurt(damage);
                antiTime ++;
            }

        }
    }
}
