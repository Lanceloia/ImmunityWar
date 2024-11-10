using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSelect : MonoBehaviour
{

    public GameObject cardContainer;
    public GameObject dice;
    public SpriteRenderer spriteRenderer;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown(){
        Debug.Log("dianle"+type);
        int step = type;
        //StartCoroutine(Board.instance.StemCellForward((int)Board.instance.token, step));
        //dice.GetComponent<Dice>().RollX(step);
        Board.instance.selectNum = type;
        Board.instance.isSelectMove = false;
        cardContainer.SetActive(false);    
    }
}
