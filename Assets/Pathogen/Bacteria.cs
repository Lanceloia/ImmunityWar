using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public enum BacteriaType    //尚未使用，供参考
{
    Normal,
    Fast,
    Big,
    Small,
    Poison,
    Slow,
    Shield,
    Bomb,
}
*/

public class Bacteria : Pathogen
{
    void Start()
    {
        type = PathogenType.bacteria;
        health = 10;
        speed = 3f;
    }
    public override void onHurt(byte damage)
    {
        health -= damage;
        if (health <= 0)
        {
            
            Destroy(gameObject);
            Board.instance.pathogenList.Remove(this.gameObject);
        }
    }
}
