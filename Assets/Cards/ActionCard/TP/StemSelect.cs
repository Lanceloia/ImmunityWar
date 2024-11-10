using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemSelect : MonoBehaviour
{

    public GameObject cardContainer;
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
        //StartCoroutine(Board.instance.StemCellForward((int)Board.instance.token, step));
        //dice.GetComponent<Dice>().RollX(step);
        // Board.instance.selectTPnum = type;
        // Board.instance.isTP = true;
        Board.instance.StemCellSmoothMove(type,Board.instance.stemCellList[(int)Board.instance.token].GetComponent<StemCell>().p);
        cardContainer.SetActive(false);    
    }
}
