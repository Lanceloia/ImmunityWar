using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AntigenType
{
    staph = 0,  //金黄色葡萄球菌
    flu = 1,    //流感
}
public class StemCell : MonoBehaviour
{
    public Position p;
    public Vector3 target;
    public float speed;     //动画的移动速度
    public bool isMove = false;
    public int forward_step = 0;


    public Dictionary<AntigenType, byte> antigens = new Dictionary<AntigenType, byte>(); //抗体的类型和数量
    public byte ATP;         //能量
    public byte ATPMax;      //能量上限
    public byte ATPspeed;   //能量回复速度


    protected virtual void Awake()
    {
        speed = 3f;

        //为抗体的类型和数量赋初值
        antigens.Add(AntigenType.staph, 2);//2仅仅是为了测试，实际应该为0
        antigens.Add(AntigenType.flu, 2);
    }

    public virtual void TurnStart ()
    {
        ATPrecover();
    }

    protected virtual void ATPrecover()
    {
        if (ATP < ATPMax)
        {
            ATP += ATPspeed;
        }
        if(ATP > ATPMax)
        {
            ATP = ATPMax;
        }
    }

    protected void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (target!=null)
        {
            if (transform.position == target)
            {
                isMove = false;
                
            }
        }            
    }
}
