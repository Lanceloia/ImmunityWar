using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AntigenType
{
    staph = 0,  //����?�������?
    flu = 1,    //����
}
public class StemCell : MonoBehaviour
{
    public Position p;
    public Vector3 target;
    public float speed;     //�������ƶ��ٶ�
    public bool isMove = false;
    public int forward_step = 0;


    public Dictionary<AntigenType, byte> antigens = new Dictionary<AntigenType, byte>(); //��ԭ�����ͺ�����
    public byte ATP;         //����
    public byte ATPMax;      //��������
    public byte ATPspeed;   //�����ظ��ٶ�
    public int ATPbuffRound;//用于atpget卡牌
    public byte extraReward;
    public int rewardRound;

    protected virtual void Awake()
    {
        speed = 3f;
        ATPbuffRound = 0;

        //Ϊ��������ͺ����������?
        antigens.Add(AntigenType.staph, 2);//2������Ϊ�˲��ԣ�ʵ��Ӧ��Ϊ0
        antigens.Add(AntigenType.flu, 2);
    }

    public virtual void TurnStart ()
    {
        ATPrecover();
        
    }

    protected virtual void ATPrecover()
    {
        byte addUp=ATPspeed;
        if(ATPbuffRound>0){
            addUp  += 1;
            ATPbuffRound-=1; 
        }
        if (ATP < ATPMax)
        {
            ATP += addUp;
        }
        if(rewardRound>0){
            rewardRound--;
            if(rewardRound == 0){
                ATP+=extraReward;
            }
        }
        if(Board.instance.immuneBuffRound>0){
            ATP+=1;
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
