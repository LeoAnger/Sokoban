using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box1 : MonoBehaviour
{
    Vector2 moveDir;
    Vector3 vector3 = new Vector3(0.6f, 0.6f, 0f);
    public LayerMask boxLayerMask;
    public bool CanMoveToDir(Vector2 vector)
    {
        /*if(vector == Vector2.right)
        {
            origin =  transform.position + new Vector3(0.6f, -0.1f, 0f);
        } else if (vector == Vector2.left)
        {
            origin = transform.position + new Vector3(-0.6f, -0.1f, 0f);
        } else if (vector == Vector2.up)
        {
            origin = transform.position + new Vector3(0.1f, 0.6f, 0f);
        } else if (vector == Vector2.down)
        {
            origin = transform.position + new Vector3(0.1f, -0.6f, 0f);
        }*/


        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, vector, 1.5f, boxLayerMask);
        Debug.Log("射线原点：" + (transform.position));
        Debug.Log("射线方向：" + vector);
        if (hit2D.collider.GetComponent<Box>() != null)
        {
            Debug.Log("撞到了Box");
        }

        if (hit2D.collider.GetComponent<Wall>() != null)
        {
            Debug.Log("撞到了Wall");
        }

        if (!hit2D)
        {
            transform.Translate(vector);
            return true;
        }
            


        return false;

    }


    void Update()
    {

        // 方向
        DirectFun();
        if (moveDir != Vector2.zero)
        {
            if (CanMoveToDir(moveDir))
            {   // 可以移动
                Debug.Log("箱子可以移动：" + moveDir);
                //Move(moveDir);
            }
        }


        moveDir = Vector2.zero;
        //RaycastHit2D hit2D = Physics2D.Raycast(transform.position, moveDir, 1.5f, boxLayerMask);
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
