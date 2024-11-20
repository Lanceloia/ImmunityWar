using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AntigenType
{
    staph = 0,  //锟斤拷锟�??锟斤拷锟斤拷锟斤拷锟?
    Strep = 1,    //链球菌病原体
    Virus = 2
}
public class StemCell : MonoBehaviour
{
    public Position p;
    public Vector3 target;
    public float speed;     //锟斤拷锟斤拷锟斤拷锟狡讹拷锟劫讹�?
    public bool isMove = false;
    public int forward_step = 0;



    public Dictionary<AntigenType, byte> antigens = new Dictionary<AntigenType, byte>(); //锟斤拷原锟斤拷锟斤拷锟酵猴拷锟斤拷锟斤拷
    public byte ATP;         //锟斤拷锟斤拷
    public byte ATPMax;      //锟斤拷锟斤拷锟斤拷锟斤拷
    public byte ATPspeed;   //锟斤拷锟斤拷锟截革拷锟劫讹拷
    public int ATPbuffRound;//鐀��簬atpget鍗＄�?
    public byte extraReward;
    public int rewardRound;


    protected virtual void Awake()
    {
        speed = 3f;
        ATPbuffRound = 0;

        //为锟斤拷锟斤拷锟斤拷锟斤拷秃锟斤拷锟斤拷锟斤拷锟斤拷锟街?
        antigens.Add(AntigenType.staph, 2);//2锟斤拷锟斤拷锟斤拷为锟剿诧拷锟皆ｏ拷实锟斤拷应锟斤拷�?0
        antigens.Add(AntigenType.Strep, 2);
        antigens.Add(AntigenType.Virus,2);
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
