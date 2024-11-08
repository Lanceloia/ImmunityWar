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



    public Dictionary<AntigenType, byte> antigens = new Dictionary<AntigenType, byte>(); //��������ͺ�����
    public byte ATP;         //����
    public byte ATPMax;      //��������
    public byte ATPspeed;   //�����ظ��ٶ�


    protected virtual void Awake()
    {
        speed = 3f;

        //Ϊ��������ͺ���������ֵ
        antigens.Add(AntigenType.staph, 0);
        antigens.Add(AntigenType.flu, 0);
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
