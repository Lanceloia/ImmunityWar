using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class virus : Pathogen
{
    void Awake()
    {
        type = PathogenType.virus;
        antigenType = AntigenType.Virus;
        health = 10;
        speed = 3f;
        isIn = true;
    }
    public override void onHurt(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            
            Destroy(gameObject);
            Board.instance.pathogenList.Remove(this.gameObject);
        }
    }


}