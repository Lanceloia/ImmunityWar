using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int type = 1;
    
    private void OnMouseDown()
    {
        RollDice(); // 当鼠标点击时调用RollDice
    }
    public void RollDice()
    {
        StartCoroutine(RollAnimation());// 调用RollAnimation
    }
     void Start()
    {
        // 获取Sprite Renderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator RollAnimation()
    {
        Debug.Log("Rolling Dice");
        if (spriteRenderer == null||sprites.Length != 6)
        {
            Debug.Log("Dice RollAnimation error");
        }
        int randomNumber = Random.Range(1, 7);
        //
        //播放扔骰子的动画
        //
        spriteRenderer.sprite = sprites[randomNumber - 1];
        type = randomNumber;

        /*
         * 直接使1号玩家移动
         */
        Board.instance.StemCellForward(0, randomNumber);
        
        
        yield return new WaitForSeconds(1);//返回一个值而已，没有意义
    }

    public int getDiceType()
    {
        return type;
    }

}
