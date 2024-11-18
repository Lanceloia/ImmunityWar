using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    public int type = 1;

    public bool canBeUsed = true;


    private Vector3 initialOffset;

    

    
    private void OnMouseDown()
    {
        if (canBeUsed)
        {
            if(Board.instance.isSelectMove)RollX(Board.instance.selectNum);
            else RollDice(); // �������ʱ����RollDice
            int stepCount = type;
            if(Board.instance.isDoubleMove){//用于实现双倍移动卡牌
                stepCount*=2;
                Board.instance.isDoubleMove = false;
            }
            if (Board.instance.token != CurrentRound.AI)
                StartCoroutine(Board.instance.StemCellForward((int)Board.instance.token, stepCount));
            canBeUsed = false;
        }
        if(Board.instance.isSelectMove){
            Board.instance.isSelectMove = false;
        }
    }
    public void RollDice()
    {
        StartCoroutine(RollAnimation());// ����RollAnimation
    }

    void Start()
    {
        // ��ȡSprite Renderer���?
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialOffset = transform.position - Camera.main.transform.position;
    }
    // void Update(){
    //     if(Board.instance.isSelectMove){
    //         canBeUsed = false;
    //     }
    // }
    void LateUpdate()
    {
        // ÿ֡���¶���λ�ã�ʹ�������ڳ�ʼλ�ò���������ƶ�
        transform.position = Camera.main.transform.position + initialOffset;
    }
    private IEnumerator RollAnimation()
    {
        //Debug.Log("Rolling Dice");
        if (spriteRenderer == null||sprites.Length != 6)
        {
            Debug.Log("Dice RollAnimation error");
        }
        int randomNumber = Random.Range(1, 7);
        //
        //���������ӵĶ���
        //
        spriteRenderer.sprite = sprites[randomNumber - 1];
        type = randomNumber;

        
        yield return new WaitForSeconds(1);//����һ��ֵ���ѣ�û������
    }

    public int getDiceType()
    {
        return type;
    }
    public void RollX(int i)
    {
        spriteRenderer.sprite = sprites[i-1];
        type = i;
    }
}
