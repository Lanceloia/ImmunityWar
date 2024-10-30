using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // ����Ϊ��ͷָ��һ����������������ƶ���ת��
    public Direction dire;  // �����ͷ���ƶ���Ŀ��λ�ã���Ϊʾ����

    // �����ͷʱ���õ��¼�
    public delegate void ArrowClickAction();
    public event ArrowClickAction OnArrowClicked;

    void Start()
    {
        // ������ Start ��������ӳ�ʼ������
        if (OnArrowClicked == null)
        {
            // ���û�а��¼����ṩһ��Ĭ�ϵĵ����Ϊ
            OnArrowClicked += DefaultClickAction;
        }
    }

    void OnMouseDown()
    {
        // ��������ö���ʱ����
        if (OnArrowClicked != null)
        {
            OnArrowClicked.Invoke();
        }
    }

    // �������
    private void DefaultClickAction()
    {
        Debug.Log(dire+"��ͷ�������");
        // �������ִ��һЩĬ�ϲ����������ƶ���Ŀ��λ��
        Board.instance.nextAccessDirection = dire;
        Board.instance.isSelectingAccess = false;
        
    }


}

