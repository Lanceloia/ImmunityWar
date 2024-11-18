using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCard : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 originalPosition; // 保存卡牌的初始位置
    private bool isDraggedOut = false; // 标记卡牌是否被拖出容器

    public float useThreshold = 150f; // 拖出多远视为“使用”卡牌（根据需求调整）
    public Card card;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.position; // 记录初始位置
    }
    private void Awake(){
        // rectTransform = GetComponent<RectTransform>();
        // originalPosition = rectTransform.position; // 记录初始位置

    }
    public void OnDrag(PointerEventData eventData)
    {
        // 让卡牌跟随拖拽位置
        Vector2 mp = Input.mousePosition;
        mp.x+=200;
        mp.y+=50;
        rectTransform.position = mp;

        // 判断卡牌是否超过使用阈值距离
        if (Mathf.Abs(rectTransform.position.y-originalPosition.y) > useThreshold)
        {
            isDraggedOut = true;
        }
        else
        {
            isDraggedOut = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggedOut)
        {
            UseCard(); // 触发卡牌使用效果
        }
        else
        {
            ResetPosition(); // 若未拖出阈值，则回归初始位置
        }
    }

    private void UseCard()
    {
        // 在此处实现卡牌使用效果的逻辑
        Debug.Log("Card used!");
        card.CardEffect();
        // 销毁卡牌，或根据需要更新状态
        Board.instance.cardUsedinThisRound.Add(card.index);
        Destroy(gameObject);
    }

    private void ResetPosition()
    {
        // 将卡牌重置回初始位置
        rectTransform.position = originalPosition;
    }
}
