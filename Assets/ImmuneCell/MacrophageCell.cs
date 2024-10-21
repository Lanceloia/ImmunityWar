using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcophageCell : ImmuneCell
{
    // Start is called before the first frame update
    void Start()
    {
        rank = 1;
        attackPower = 3;
        attackRange = 1;
        attackSpeed = 1;
        attackLeft = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Upgrade()
    {
        // 断言此处等级必定小于3（等于3时调用者不该调用此函数）
        Debug.Assert(rank < 3);
        rank++;
        SpriteChange();
        AttackChange();

        if (rank == 3)
        {
            GameObject immune_cell_grid = Board.instance.map.GetComponent<Maps>().GetGridsFromPosition(p);
            immune_cell_grid.GetComponent<ImmuneCellGrid>().state = ImmuneCellGridState.MaxRank;
        }
    }

    public override void SpriteChange()
    {
        //贴图更新
        if (rank == 2)
            tower.GetComponent<SpriteRenderer>().color = Color.green;
        else if (rank == 3)
            tower.GetComponent<SpriteRenderer>().color = Color.blue;
    }


    public override void AttackChange()
    {
        //攻击力更新
        if (rank == 2)
        {
            attackPower = 4;
            attackSpeed = 2;
        }
        else if (rank == 3)
        {
            attackPower = 5;
            attackRange = 2;
            attackSpeed = 3;
        }
    }
    public override void attack(GameObject pathogen)
    {
        while(attackLeft != 0 && pathogen != null)
        {
            attackLeft--;
            pathogen.GetComponent<Pathogen>().onHurt(attackPower);
        }
    }
}
