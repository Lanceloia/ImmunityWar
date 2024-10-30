using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    public int type = 1;

    public bool canBeUsed = true;
    
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
