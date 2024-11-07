using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    public GameObject cardPrefab;   // 卡牌预制件
    public Transform cardContainer; // 你的 cardContainer UI 元素
    public GameObject cardHolder; // 用于卡牌放置的 Transform，位于 cardContainer 中
    public GameObject keepButton;
    public float cardSpacing = 20f;        // 卡牌之间的间距
    public Vector2 startPosition = new Vector2(-300f, -50f);  // 卡牌排列的起始位置

    void Start()
    {
        // 假设卡牌数据已经赋值给 cardDataList
    }

    public void DisplayCardsForTurn(int turnIndex)
    {
        // 清空旧的卡牌
        ClearPreviousCards();
        Board.instance.cardUsedinThisRound = new List<int>();
        cardHolder.SetActive(true);
        // 检查回合索引是否在列表范围内
        if (turnIndex >= 0 && turnIndex < Board.instance.handcardList.Count)
        {
            Vector2 position = cardContainer.GetComponent<RectTransform>().anchoredPosition;
            position.x+=20;
            position.y+=50;
            // 遍历当前回合的卡牌数据
            foreach (int cardIndex in Board.instance.handcardList[turnIndex])
            {
                // 实例化卡牌UI
                GameObject newCard = Instantiate(cardPrefab, cardContainer);
                
                // 设置卡牌内容
                SetCardUI(newCard, cardIndex);
                RectTransform cardRect = newCard.GetComponent<RectTransform>();
                cardRect.anchoredPosition = position; // 使用锚点位置进行设置

            // 更新下一个卡牌的位置
                position.x += cardRect.sizeDelta.x + cardSpacing;
            }
        }
        else
        {
            Debug.LogWarning("回合索引超出范围");
        }
        
    }

    private void ClearPreviousCards()
    {
        // 删除容器中的所有子对象（即旧的卡牌UI）
        foreach (Transform child in cardContainer)
        {
            if (child.gameObject == keepButton) 
                continue;
            Destroy(child.gameObject);
        }
    }

    private void SetCardUI(GameObject card, int cardIndex)
    {
        card.GetComponent<CardDisplay>().card = Board.instance.cardsList[cardIndex].GetComponent<Card>();
        card.GetComponent<DraggableCard>().card = Board.instance.cardsList[cardIndex].GetComponent<Card>();
        card.GetComponent<CardDisplay>().DisplayCard();
        

        // 设置卡牌图片和名称
        
    }
}
