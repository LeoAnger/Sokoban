using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class MyUtil
{
    
    // 获取json
    public static string getJsonByLocalDir(string dir)
    {
        try
        {
            if (File.Exists(dir))
            {
                string jsonText = File.ReadAllText(dir);
                return jsonText;
            }
            else
            {
                // Console.WriteLine("文件不存在...或未找到...");
                Debug.Log("文件不存在...或未找到...");
            }
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.Message);
        }
        return null;
    }


    // Unity获取当前路径
    public static string getUnityCurrentPath()
    {
        //获取模块的完整路径。
        string path1 = Process.GetCurrentProcess().MainModule.FileName;
        //获取和设置当前目录(该进程从中启动的目录)的完全限定目录
        string path2 = Environment.CurrentDirectory;
        //获取应用程序的当前工作目录
        string path3 = Directory.GetCurrentDirectory();
        //获取程序的基目录
        string path4 = AppDomain.CurrentDomain.BaseDirectory;
        //获取和设置包括该应用程序的目录的名称
        //string path5 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        //获取启动了应用程序的可执行文件的路径
        //string path6 = System.Windows.Forms.Application.StartupPath;
        //获取启动了应用程序的可执行文件的路径及文件名
        //string path7 = System.Windows.Forms.Application.ExecutablePath;

        StringBuilder str=new StringBuilder();
        str.AppendLine("System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName:" + path1);
        str.AppendLine("System.Environment.CurrentDirectory:" + path2);
        str.AppendLine("System.IO.Directory.GetCurrentDirectory():" + path3);
        str.AppendLine("System.AppDomain.CurrentDomain.BaseDirectory:" + path4);
        // str.AppendLine("System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase:" + path5);
        // str.AppendLine("System.Windows.Forms.Application.StartupPath:" + path6);
        // str.AppendLine("System.Windows.Forms.Application.ExecutablePath:" + path7);
        string allPath = str.ToString();
        return path2;
    }

    // 创建GameObject
    /*
     * @author:Anger
     * @Date:    2020年7月20日07点21分
     */
    public static GameObject createGameObject(string name, GameObject parent, Vector2 anchoredPosition, Vector2 sizeDelta)
    {
        GameObject gameObject = new GameObject(name, typeof(RectTransform));
        // 1. 父类
        if (parent != null)
            gameObject.transform.parent = parent.transform;
        // 2位置
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        
        // 3. 宽高
        rectTransform.sizeDelta = sizeDelta;
        
        // 4. 组件
        //gameObject.AddComponent<RectTransform>();//     添加组件失败（报错...）
        
        return gameObject;
    }
    
    // 创建Text
    /*
     * @author:Anger
     * @Date:    2020年7月20日07点21分
     */
    public static GameObject creatText(string name, string content, GameObject parent, Vector2 anchoredPosition, Vector2 sizeDelta, Font font, Color color)
    {
        GameObject gameObject = new GameObject(name, typeof(Text));
        // 1. 父类
        if (parent != null)
            gameObject.transform.parent = parent.transform;
        // 2位置
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        // 3. 宽高
        rectTransform.sizeDelta = sizeDelta;
        // 4. 文本内容
        Text text = gameObject.GetComponent<Text>();
        text.text = content;
        // 5.字体
        text.font = font;
        // 6.字体颜色
        text.color = color;
        // 7.字体对齐方式

        return gameObject;
    }
}
