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
    void Awake()
    {
        base.Awake();
        type = PathogenType.bacteria;
        antigenType = AntigenType.staph;
        health = 10;
        speed = 3f;
        isIn = false;
    }
    public override void onHurt(int damage)
    {
        health -= damage;

        Debug.Log(string.Format("HP: {0}, DMG: {1}", health, damage));

        if (health <= 0)
        {
            
            Destroy(gameObject);
            Board.instance.pathogenList.Remove(this.gameObject);
        }
    }
    
}
