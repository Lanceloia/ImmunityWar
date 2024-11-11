using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AntigenType
{
    staph = 0,  //���ɫ�������
    flu = 1,    //����
}
public class StemCell : MonoBehaviour
{
    public Position p;
    public Vector3 target;
    public float speed;     //�������ƶ��ٶ�
    public bool isMove = false;
    public int forward_step = 0;


    public Dictionary<AntigenType, byte> antigens = new Dictionary<AntigenType, byte>(); //��������ͺ�����
    public byte ATP;         //����
    public byte ATPMax;      //��������
    public byte ATPspeed;   //�����ظ��ٶ�


    protected virtual void Awake()
    {
        speed = 3f;

        //Ϊ��������ͺ���������ֵ
        antigens.Add(AntigenType.staph, 2);//2������Ϊ�˲��ԣ�ʵ��Ӧ��Ϊ0
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
