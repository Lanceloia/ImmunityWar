using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public Dice dice;
    public bool tokenLock;
    private Vector3 initialOffset;
    private void OnMouseDown()
    {
        if (!tokenLock)
        {
            Board.instance.NextRound();
            dice.canBeUsed = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tokenLock = false;
        initialOffset = transform.position - Camera.main.transform.position;
    }
    void LateUpdate()
    {
        // ÿ֡���¶���λ�ã�ʹ�������ڳ�ʼλ�ò���������ƶ�
        transform.position = Camera.main.transform.position + initialOffset;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
