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
            RollDice(); // �������ʱ����RollDice
            if (Board.instance.token != CurrentRound.AI)
                StartCoroutine(Board.instance.StemCellForward((int)Board.instance.token, type));
            canBeUsed = false;
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

}
