using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Assets.SCripts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class ScrollContent : MonoBehaviour
{
    public GameObject content;    //滚动条内容对象
    private Transform contentTransform;
    public List<Text> texts;
    public Text text;
    public Font font;

    public static List<MapBean> MapBeans_list = new List<MapBean>();
    

    void Awake()
    {
        contentTransform = content.transform;
        // 1.获取Unity当前路径
        string unityCurrentPath = MyUtil.getUnityCurrentPath();
        log("unityCurrentPath:" + unityCurrentPath);    // unityCurrentPath:F:\unity_project\推箱子 - 副本1
        string[] files = Directory.GetFiles(unityCurrentPath + "/Assets/Maps" , "*_maps.json");
        foreach (string file in files)
        {
            log(file);
        }
        log(files.Length);
        
        // 2.初始化json为MapBean
        foreach (string file in files)
        {
            // Coding...
            string jsonByLocalDir = MyUtil.getJsonByLocalDir(file);
            MapBean mapBean = JsonConvert.DeserializeObject<MapBean>(jsonByLocalDir);
            MapBeans_list.Add(mapBean);
        }
        
        /*log( "MapBeans_list.Count:"+ MapBeans_list.Count);
        log(MapBeans_list[0].isWin);
        log(MapBeans_list[0].author);
        log(MapBeans_list[0].authorID);
        
        log(MapBeans_list[1].isWin);
        log(MapBeans_list[1].author);
        log(MapBeans_list[1].authorID);*/
        
        // 3.实例化到Unity
        //    Unity已经初始化好地图Content中的子物体，一开始都是隐藏的
        foreach (MapBean mapBean in MapBeans_list)
        {
            /*GameObject gameObject = MyUtil.createGameObject(mapBean.authorID, content, new Vector2(100, 100), new Vector2(160, 150));
            
            GameObject tt = MyUtil.creatText("Text", ("作者：" + mapBean.author + "/nID:" + mapBean.authorID), gameObject,
                new Vector2(0, -110),
                new Vector2(100, 50), font, Color.green);
            Text text = tt.GetComponent<Text>();*/
        }
        // 3.1 获取Content中的所有子物体
        int childCount = contentTransform.childCount;
        int count = MapBeans_list.Count;
        int contentChildCountTemp = 0;
        /*for (int i = 0; i < childCount ; i++)
        {
            // 测试：打印所有预置的name
            Transform child = contentTransform.GetChild(i);
            print(child.gameObject.name);
            if (child.gameObject.name == "Level002")
            {
                child.gameObject.name = "你好";
                print(child.gameObject.activeSelf);
                print(child.gameObject.activeInHierarchy);
            }
        }*/
        for (int i = 0; i < count; i++)
        {
            // 1.检测是否超出预置子物体的数量
            if (contentChildCountTemp >= childCount)
            {
                break;
            }
            // 2. 获取Content对应的子物体
            GameObject child = contentTransform.GetChild(i).gameObject;
            // 2.1 获取MapBean
            MapBean mapBean = MapBeans_list[i];
            // 3.子物体是否隐藏
            if (!child.activeSelf)
            {
                child.SetActive(true);
                // 4.改变图片
                // 5.改变文本
                // 5.1 获取文本实例
                GameObject text = child.transform.Find("Text").gameObject;
                print("物体名称name: " + text.name);
                // 5.2 改变文本
                text.GetComponent<Text>().text = mapBean.author + ":" + mapBean.authorID + "\n" + mapBean.isWin;
            }
            
            // 增量
            contentChildCountTemp++;
        }
    }

    void Start()
    {
        
    }

    public void fun()
    {
        
    }
    
    private static GameObject Text()
    {
        GameObject go = new GameObject("x_Text", typeof(Text));
        var text = go.GetComponent<Text>();
        //go.AddComponent<Outline>();   // 默认加入 附加组件
        return go;
    }
    
    private bool IsLog = true;
    void log(object log)
    {
        if (IsLog)
        {
            Debug.Log(log);
        }
    }
}
