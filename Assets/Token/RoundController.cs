using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundControler : MonoBehaviour
{
    public Dice dice;
    public bool tokenLock;

    private void OnMouseDown()
    {
        if (!tokenLock)
        {
            Board.instance.NextRound();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tokenLock = false;
        //dice = GetComponent<Dice>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
