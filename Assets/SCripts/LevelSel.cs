using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSel : MonoBehaviour
{
  


    public void TestDug()
    {
        Debug.Log("触发事件...");
    }
    
    // 开始游戏
    public void StartGame()
    {
        //    1. 获取地图
        //    2.跳转场景
        Invoke("ChangeScene", 1);  // 1s后切换场景
    }
    
    void ChangeScene()
    {
        SceneManager.LoadScene("推箱子代码版");  // 登陆成功则切换到游戏界面
    }
}
