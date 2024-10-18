using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathogen : MonoBehaviour
{
    public Position p;
    public int index;


    public Vector3 target;
    public float speed;
    public bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("test for github again");
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (target != null)
        {
            if (transform.position == target)
            {
                isMove = false;
                Board.instance.isMove4Pathogen = false;
            }
        }
    }
}
