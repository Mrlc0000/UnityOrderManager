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
public class UIDepthItem
{
    public int parentOrder;
    public GameObject ParentItem;//父级组
    [HideInInspector]
    public List<GameObject> childItem = new List<GameObject>();

    /// <summary>
    /// 自动添加全部子物体
    /// </summary>
    public void AutoAddAllChild() {

        if (ParentItem == null)
        {
            Debug.Log("父级物体未设置");
            return;
        }
        for (int i = 0; i < ParentItem.transform.childCount; i++)
        {
            //todo防止多次add
            if (childItem.Find(x => x.Equals(ParentItem.transform.GetChild(i).gameObject))==null)
            {
                childItem.Add(ParentItem.transform.GetChild(i).gameObject);
            }
        }
    }
    /// <summary>
    /// 只清空逻辑
    /// </summary>
    public void RemoveAtIndex(int index) {

        if (index < childItem.Count)
        {
            UIDepth.Remove(childItem[index]);
            childItem.RemoveAt(index);
        }
    }

    /// <summary>
    /// 添加父级
    /// </summary>
    /// <param name="order"></param>
    public void ApplyParentItem()
    {
        UIDepth temp = UIDepth.Add(ParentItem);
        if (temp != null)
        {
            temp.IsUI = true;
            temp.Order = parentOrder;
        }
    }
    /// <summary>
    /// 添加子元素
    /// </summary>
    /// <param name="obj"></param>
    public void ApplyChildItem(int index)
    {
        if (index >= childItem.Count)
            return;
        UIDepth temp = UIDepth.Add(childItem[index]);
        if (temp != null)
        {
            temp.SetIsUI();
            temp.Order = parentOrder + index * 1;
            temp = null;
        }
    }
    /// <summary>
    /// 设置子物体UI
    /// </summary>
    /// <param name="index"></param>
    /// <param name="isUI"></param>
    public void SetChildItemIsUI(int index,bool isUI) {
        if (index >= childItem.Count)
            return;
        UIDepth temp = UIDepth.Add(childItem[index]);
        temp.IsUI=isUI;
    }
    /// <summary>
    /// 交换数组
    /// </summary>
    /// <param name="index"></param>
    /// <param name="toIndex"></param>
    public void ChangeTwoItem(int index,int toIndex) {
  
        if (index < 0 || index >childItem.Count-1)
            return;

        if (toIndex < 0 || toIndex >childItem.Count-1)
            return;
  
        if (index == toIndex)
            return;
        GameObject temp = childItem[index];
        childItem[index] = childItem[toIndex];
        childItem[toIndex] = temp;

        ApplyChildItem(index);
        ApplyChildItem(toIndex);
    }
}

public class UIDepthManager : MonoBehaviour
{
    public List<UIDepthItem> UIDepthItemList = new List<UIDepthItem>();//界面深度表
    [HideInInspector]
    public bool isShow;

    public void AddUIDepthItem()
    {
        UIDepthItemList.Add(new UIDepthItem());
    }

    public void RemoveAtIndex(int index)
    {
        for (int i = 0; i < UIDepthItemList.Count; i++)
        {
            //清空所有uidepth
            UIDepthItemList[index].RemoveAtIndex(i);
        }        
    }
}