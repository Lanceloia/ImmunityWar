using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PathogenType
{
   bacteria,
   virus,
   strep

}
abstract public class Pathogen : MonoBehaviour
{
    public Position p;      //����λ��
    public int index;       //ϸ�����

    public PathogenType type;      //��������
    public AntigenType antigenType;     //�����Ŀ�ԭ����
    public int health;     //����Ѫ��
    public bool isIn;      //�����Ƿ���������

    public GameObject bCell;     //�������������Bϸ��

    public int antiTime = 0; //�����������ʱ��

    public int targetIndex;

    public Vector3 target;
    public float speed;
    public bool isMove = false;

    private SpriteRenderer sr;
    public List<Sprite> sprites;
    private int curFrame;
    public int FrameCount
    {
        get
        {
            return sprites.Count;
        }
    }
    private float timeDelta;
    private int FPS = 4;


    protected void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (FrameCount > 0)
        {
            sr.sprite = sprites[0];
        }
    }

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

        if (FrameCount > 0)
        {
            timeDelta += Time.deltaTime;
            if (timeDelta > 1.0f / FPS)
            {
                timeDelta -= 1.0f / FPS;
                curFrame = (curFrame + 1) % FrameCount;
                sr.sprite = sprites[curFrame];
            }
        }
    }

    abstract public void onHurt(int damage);      //����ʱ����

    virtual public void beAntibodyed(AntiBody antibody)      //���ܿ���ʱ����
    {
        //��antibody���Ҷ�������Ϊ�������Ӷ���
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
