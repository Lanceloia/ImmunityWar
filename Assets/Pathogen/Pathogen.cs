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
    public Position p;      //����λ��
    public int index;       //ϸ�����

    public PathogenType type;      //��������
    public int health;     //����Ѫ��


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
            //����ƶ�
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

    abstract public void onHurt(byte damage);      //����ʱ����

}
