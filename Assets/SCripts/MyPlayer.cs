using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{

    MapCreater M;
    Exit exit ;
    int boxCompleted = 0;
    bool gameOver = false;  // 游戏是否结束

    void Awake()
    {
        M = FindObjectOfType<MapCreater>();
        exit = FindObjectOfType<Exit>();
        exit.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int dx = 0;
        int dy = 0;
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            dx++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dx--;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dy++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dy--;
        }

        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        // 下一步走到的坐标(nx, ny)
        int nx = dx + x;   
        int ny = dy + y;

        if(isWall(nx, ny))
        {
            Debug.Log("撞墙..." + (nx * 100 + y));
            return;
        }

        if(isBox(nx, ny))
        {
            Debug.Log("Box...");
            // 下一步可否移动？
            int nnx = nx + dx;
            int nny = ny + dy;
            if (isWall(nnx, nny) || isBox(nnx, nny)) return;

            // 可以移动
            GameObject G = getBox(nx, ny);
            G.transform.position = new Vector3(nnx, nny);

            // 如果盒子走出目标, 改变盒子状态
            if (isTarget(nx, ny))
            {
                // 1.获取对应的盒子
                GameObject tBox = getTargetBox(nx, ny);
                // 2.改变盒子的位置
                tBox.transform.position = new Vector3(30, nny);
                // 3.更新boxCompleted
                boxCompleted--;
            }

            // 如果盒子位于目标, 改变盒子状态
            if (isTarget(nnx, nny))
            {
                // 1.获取对应的盒子
                GameObject tBox = getTargetBox(nnx, nny);
                // 2.改变盒子的位置
                tBox.transform.position = new Vector3(nnx, nny);
                // 3.更新boxCompleted
                boxCompleted++;
            }


            Debug.Log("移动Box:x=" + nnx + ",y=" + nny);
            M.pos_box.Remove(nx * 100 + ny);
            M.pos_box.Add(nnx * 100 + nny, G);
        }

        transform.position = new Vector3(nx,ny);
        if(gameOver)
        {
            return;
        }
        if(isWin())
        {
            Debug.Log("游戏胜利");
            // 显示胜利画面
            Win win = FindObjectOfType<Win>();
            win.transform.position = new Vector3(8, -5);
            // 更新胜利变量
            gameOver = true;
            // 显示退出程序按钮
            //exit.transform.position = new Vector3(0, 0);
            exit.gameObject.SetActive(true);
        }

    }

    bool isWall(int x, int y)
    {
        return M.walls.Contains(x * 100 + y);
    }

    bool isBox(int x, int y)
    {
        return M.pos_box.ContainsKey(x * 100 + y);
    }

    bool isTarget(int x, int y)
    {
        return M.pos_target.ContainsKey(x * 100 + y);
    }

    GameObject getBox(int x, int y)
    {
        return M.pos_box[x * 100 + y];
    }

    GameObject getTargetBox(int x, int y)
    {
        return M.pos_targetBox[x * 100 + y];
    }

    bool isWin()
    {
        if (boxCompleted >= M.boxNums) return true;
        return false;
    }
}
