using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCTR : MonoBehaviour
{
    Vector2 moveDir;
    public LayerMask detectLayer;

    // Update is called once per frame
    void Update()
    {
        // 方向
        DirectFun();
        if (moveDir != Vector2.zero)
        {
            if (CanMoveToDir(moveDir))
            {   // 可以移动
                Debug.Log("可以移动：" + moveDir);
                Move(moveDir);
            }
        }


        moveDir = Vector2.zero;
    }

    bool CanMoveToDir(Vector2 vector)   // moveDir:(1.0, 0.0)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, vector, 1.0f, detectLayer);
        if (!hit2D)
            return true;
        else
        {
            if (hit2D.collider.GetComponent<Box1>() != null)
            {   // hit到的物体有Box属性
                Debug.Log("玩家撞到箱子了...");
                return hit2D.collider.GetComponent<Box1>().CanMoveToDir(vector);    // 箱子是否可以移动fun:CanMoveToDir
            }

            if (hit2D.collider.GetComponent<Wall>() != null)
            {   // hit到的物体有Wall属性
                Debug.Log("玩家撞到墙了...");
            }
        }

        return false;

    }

    void Move(Vector2 vector)
    {
        transform.Translate(vector);
    }

    private void DirectFun()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDir = Vector2.right;
            Debug.Log("moveDir:" + moveDir);    //  moveDir:(1.0, 0.0)
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDir = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDir = Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDir = Vector2.down;
        }
    }
}
