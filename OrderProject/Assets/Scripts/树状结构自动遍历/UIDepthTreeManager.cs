/**
*Copyright(C) 2019 by #COMPANY#
*All rights reserved.
*FileName: #SCRIPTFULLNAME#
*Author: #AUTHOR#
*Version: #VERSION#
*UnityVersion：#UNITYVERSION#
*Date: #DATE#
*Description:
*History:
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



[Serializable]
public class UIDepthPoint
{
    public int mainOrder;
    public UIDepth depth;
    public List<UIDepthPoint> childItem = new List<UIDepthPoint>();


    /// <summary>
    /// 更新深度Point
    /// </summary>
    public void UpdateDepthPoint()
    {
        //更新父级层级
        if (depth != null)
        {
            depth.Order = mainOrder;
        }
        foreach (Transform item in depth.transform)
        {
            UIDepth depth = item.GetComponent<UIDepth>();
            if (depth)
            {
                if (childItem.Find(x => x.depth == depth) == null)
                {
                    UIDepthPoint uIDepth = new UIDepthPoint()
                    {
                        depth = depth
                    };
                    childItem.Add(uIDepth);
                }
            }
        }

        //用于清空 手动删除的NUll
        for (int i = 0; i < childItem.Count; i++)
        {
            if (childItem[i].depth == null)
            {
                childItem.RemoveAt(i);//清除这个空引用
            }
            else
            {
                childItem[i].mainOrder = mainOrder + mainOrder;//主节点索引
                childItem[i].depth.Order = mainOrder + mainOrder;
                childItem[i].UpdateDepthPoint();
            }
        }
    }

}

public class UIDepthTreeManager : MonoBehaviour
{
    public List<UIDepthPoint> UIDepthItemList = new List<UIDepthPoint>();//界面深度表;
    /// <summary>
    /// Editor更新方法
    /// </summary>
    public void UpdateDepth()
    {
        foreach (var item in UIDepthItemList)
        {
            item.UpdateDepthPoint();
        }
    }
}