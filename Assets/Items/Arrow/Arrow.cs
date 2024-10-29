using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // 可以为箭头指定一个点击动作，比如移动或转向
    public Direction dire;  // 点击箭头后移动的目标位置（仅为示例）

    // 点击箭头时调用的事件
    public delegate void ArrowClickAction();
    public event ArrowClickAction OnArrowClicked;

    void Start()
    {
        // 可以在 Start 方法中添加初始化操作
        if (OnArrowClicked == null)
        {
            // 如果没有绑定事件，提供一个默认的点击行为
            OnArrowClicked += DefaultClickAction;
        }
    }

    void OnMouseDown()
    {
        // 当鼠标点击该对象时触发
        if (OnArrowClicked != null)
        {
            OnArrowClicked.Invoke();
        }
    }

    // 点击动作
    private void DefaultClickAction()
    {
        Debug.Log(dire+"箭头被点击！");
        // 这里可以执行一些默认操作，比如移动到目标位置
        Board.instance.nextAccessDirection = dire;
        Board.instance.isSelectingAccess = false;
        
    }


}

