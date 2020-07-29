using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDestroyImmediate : MonoBehaviour
{
    public void DesGameObject()
    {
        List<GameObject> gameObjectList = MapCreater.GameObjectList;
        print("销毁方法..." + gameObjectList.Count);
        int count = gameObjectList.Count;
        for (int i = 0; i < count; i++)
        {
            DestroyImmediate(gameObjectList[i]);
        }
    }
}