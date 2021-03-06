﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 日期：2020年6月25日
 */
public class MapCreater : MonoBehaviour
{
    int rows = 10;      // 行
    int column = 20;    // 列
    public static int[,]mapsArr;
    public int boxNums = 0;
    public GameObject wall;
    public GameObject target;
    public GameObject targetInBox;
    public GameObject ground;
    public GameObject box;
    public GameObject player;


    public Dictionary<int, GameObject> pos_targetBox = new Dictionary<int, GameObject>();   // 目标盒子
    public Dictionary<int, GameObject> pos_box = new Dictionary<int, GameObject>();         // 盒子
    public Dictionary<int, GameObject> pos_target = new Dictionary<int, GameObject>();      // creat()关卡时，需要初始化此变量：2020年7月28日
    public Dictionary<string, int> layerMaps = new Dictionary<string, int>();    // 不可被清空：初始化
    public HashSet<int> walls = new HashSet<int>();

    public static List<int[,]> MapList = new List<int[,]>();                    // 地图集合（不可被清空：初始化）
    public static List<GameObject> GameObjectList = new List<GameObject>();    // 地图物体集合

    void Awake()
    {
        // 初始化layer层（参考unity视图）
        layerMaps.Add( "Player", 8);
        layerMaps.Add("Box", 9);
        layerMaps.Add("Wall", 10);
        layerMaps.Add("Ground", 20);
        layerMaps.Add("Target", 11);
        layerMaps.Add("TargetInBox", 12);
        // 初始化地图
        Maps.initMaps();
    }

    public void creat()
    {
        // 下一关
        NextLevel.nowLevel++;
        // 1.获取地图
        mapsArr = MyUtil.getMapByNum(MapList, NextLevel.nowLevel);
        // 2.创建地图
        //"逐层"生成地图 先创建的在下面原则，即由低到高原则。
        //Ground --> Wall --> target --> box --> box&target --> ... --> player
        // 生成Ground
        initGround();
        initWall(1);
        initTarget(2, 4);
        initBox(3, 4);
        initTargetBox(2, 4);
        initPlayer(9);
        
        Box[] boxes = FindObjectsOfType<Box>();
        Debug.Log("盒子总数是:" + boxes.Length);

        // 更新胜利变量
        NextLevel.nowLevelBool = false;
        print("当前地图：" + NextLevel.nowLevel);
    }

    private void initPlayer(int num)
    {
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                int i = mapsArr[r, c];

                if (i == num)
                {
                    player.transform.position = new Vector3(c, -r);
                    Debug.Log("玩家位置：" + player.transform.position);
                }
            }
        }
    }

    private void initTarget(int num, int targetAndBox)
    {
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                int i = mapsArr[r, c];

                if (i == num || i == targetAndBox)
                {
                    GameObject target1 = Instantiate(target, new Vector3(c, -r), Quaternion.identity);
                    target1.layer = layerMaps["Target"];
                    //target1.GetComponent<Renderer>().sortingOrder = 1;
                    pos_target.Add(c * 100 + (-r), target1);
                    GameObjectList.Add(target1);
                    boxNums++;
                }
            }
        }
    }

    private void initTargetBox(int num, int targetAndBox)
    {
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                int i = mapsArr[r, c];
                if (i == num)
                {
                    // GameObject g = Instantiate(targetInBox, new Vector3(c * 30, -r), Quaternion.identity);
                    GameObject g = Instantiate(targetInBox, new Vector3(c, -r), Quaternion.identity);
                    g.layer = layerMaps["TargetInBox"];
                    // 隐藏目标盒子
                    g.SetActive(false);
                    pos_targetBox.Add(c * 100 + (-r), g);
                    GameObjectList.Add(g);
                }
            }
        }
        
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                int i = mapsArr[r, c];
                if (i == targetAndBox)
                {
                    GameObject g = Instantiate(targetInBox, new Vector3(c, -r), Quaternion.identity);
                    g.layer = layerMaps["TargetInBox"];
                    pos_targetBox.Add(c * 100 + (-r), g);
                    GameObjectList.Add(g);
                    MyPlayer.boxCompleted++;
                }
            }
        }
    }

    private void initBox(int num, int targetAndBox)
    {
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                int i = mapsArr[r, c];

                if (i == num || i == targetAndBox)
                {
                    GameObject box1 = Instantiate(box, new Vector3(c, -r), Quaternion.identity);
                    box1.layer = layerMaps["Box"];
                    pos_box.Add(c * 100 + (-r), box1);
                    GameObjectList.Add(box1);
                }
            }
        }
    }

    // 墙
    private void initWall(int num)
    {
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                int i = mapsArr[r, c];
                if (i == num)
                {
                    GameObject w = Instantiate(wall, new Vector3(c, -r), Quaternion.identity);
                    walls.Add(c * 100 + (-r)); // 墙不会移动，只存位置
                    GameObjectList.Add(w);
                }
            }
            //打印walls
            string str = "";
            foreach (int i in walls)
            {
                str = str + i + ",";
            }
            // Debug.Log("walls集合：");
            // Debug.Log(str);
        }
    }

    // 生成地板
    private void initGround()
    {
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                GameObject ground1 = Instantiate(ground, new Vector3(c, -r), Quaternion.identity);
                ground1.layer = layerMaps["Ground"];
                GameObjectList.Add(ground1);
            }
        }
    }

    // 测试...
    public void newGameObject()
    {
        // 动态生成一个GameObject
        Debug.Log("动态生成一个GameObject...");
        Instantiate(box, new Vector3(-5, 1), Quaternion.identity);
    }

    public void btnFunc()
    {
        // 改变箱子状态为进入目标状态

        

    }
}
