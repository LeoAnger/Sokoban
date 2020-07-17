using System;
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
    int[,]mapsArr;
    public int boxNums = 0;
    public GameObject wall;
    public GameObject target;
    public GameObject targetInBox;
    public GameObject ground;
    public GameObject player;
    public GameObject box;


    public Dictionary<int, GameObject> pos_targetBox = new Dictionary<int, GameObject>();   // 目标盒子
    public Dictionary<int, GameObject> pos_box = new Dictionary<int, GameObject>();         // 盒子
    public Dictionary<int, GameObject> pos_target = new Dictionary<int, GameObject>();
    public Dictionary<string, int> layerMaps = new Dictionary<string, int>();
    public HashSet<int> walls = new HashSet<int>();
    public List<int> targets = new List<int>();

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
        /// 0: 空    1：墙 2：目标    3.盒子    4.位于目标的盒子   9.玩家
        mapsArr = new int[20, 20] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 0, 3, 0, 2, 0, 0, 3, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };

        //"逐层"生成地图 先创建的在下面原则，即由低到高原则。
        //Ground --> Wall --> target --> box --> box&target --> ... --> player
        // 生成Ground
        initGround();
        initWall(1);
        initTarget(2);
        initBox(3);
        initTargetInBox(2);
        
        // 初始化玩家
        MyPlayer MP = FindObjectOfType<MyPlayer>();
        MyPlayer[] g = FindObjectsOfType<MyPlayer>();
        Debug.Log("玩家总数是:" + g.Length);
        
        Box[] boxes = FindObjectsOfType<Box>();
        Debug.Log("盒子总数是:" + boxes.Length);
        MP.transform.position = new Vector3(2, -3);

        /// 初始化盒子总数
        boxNums = FindObjectsOfType<Box>().Length - 1;
    }

    private void initTarget(int num)
    {
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                int i = mapsArr[r, c];

                if (i == num)
                {
                    GameObject target1 = Instantiate(target, new Vector3(c, -r), Quaternion.identity);
                    target1.layer = layerMaps["Target"];
                    pos_target.Add(c * 100 + (-r), target1);
                }
            }
        }
    }

    private void initTargetInBox(int num)
    {
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                //Console.Write(intArr[r, c] + ",");
                int i = mapsArr[r, c];

                if (i == num)
                {
                    GameObject g = Instantiate(targetInBox, new Vector3(c * 30, -r), Quaternion.identity);
                    g.layer = layerMaps["TargetInBox"];
                    pos_targetBox.Add(c * 100 + (-r), g);
                }
            }
        }

        //打印walls
        Debug.Log("targetBox集合length：");
        Debug.Log(pos_targetBox.Count);
    }

    private void initBox(int num)
    {
        for (int r = 0; r < rows; r++)
        {   // 行：r
            for (int c = 0; c < column; c++)
            {
                int i = mapsArr[r, c];

                if (i == num)
                {
                    GameObject box1 = Instantiate(box, new Vector3(c, -r), Quaternion.identity);
                    box1.layer = layerMaps["Box"];
                    pos_box.Add(c * 100 + (-r), box1);
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
                    Instantiate(wall, new Vector3(c, -r), Quaternion.identity);
                    walls.Add(c * 100 + (-r)); // 墙不会移动，只存位置
                }
            }
            //打印walls
            string str = "";
            foreach (int i in walls)
            {
                str = str + i + ",";
            }
            Debug.Log("walls集合：");
            Debug.Log(str);
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
