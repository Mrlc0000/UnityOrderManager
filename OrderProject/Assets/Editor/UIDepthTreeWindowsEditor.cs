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
using UnityEditor;
using System.Collections.Generic;

#region UIDepthTreeDataWindowEitor
public class UIDepthTreeDataWindowEitor : EditorWindow
{
    public List<UIDepthPoint> dataList;

    private Vector2 scrollVec;
    GUIStyle buttonGUIStyle;
    private void Awake()
    {
        buttonGUIStyle = new GUIStyle();
        buttonGUIStyle.fontSize = 40;
    }
    private void OnGUI()
    {
        GUILayout.BeginHorizontal(EditorStyles.helpBox);
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("层级数据", buttonGUIStyle, GUILayout.MaxWidth(200), GUILayout.MaxHeight(50));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();


        scrollVec = EditorGUILayout.BeginScrollView(scrollVec);
        foreach (UIDepthPoint item in dataList)
        {
            UpdateUIDepthPointGUI(item);
        }

        EditorGUILayout.EndScrollView();


    }

    private void UpdateUIDepthPointGUI(UIDepthPoint point)
    {
        GUILayout.BeginHorizontal(EditorStyles.helpBox);
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("层级", GUILayout.MaxWidth(40));
        GUI.color = Color.red;
        EditorGUILayout.IntField(point.mainOrder, GUILayout.MaxWidth(30));
        GUI.color = Color.white;
        EditorGUILayout.LabelField("point:", GUILayout.MaxWidth(40));
        EditorGUILayout.ObjectField(point.depth, typeof(UIDepthPoint), GUILayout.MaxWidth(100));
        GUILayout.EndHorizontal();
        GUILayout.BeginVertical(EditorStyles.helpBox);
        for (int i = 0; i < point.childItem.Count; i++)
        {
            UpdateUIDepthPointGUI(point.childItem[i]);
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
}

#endregion



#region UIDepthTreeManagerEditor

[CustomEditor(typeof(UIDepthTreeManager))]
[CanEditMultipleObjects]
public class UIDepthTreeManagerEditor : Editor
{
    UIDepthTreeManager script;
    static UIDepthTreeDataWindowEitor myWindow;

    private void Awake()
    {
        script = target as UIDepthTreeManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.BeginVertical(EditorStyles.helpBox);

        GUI.color = Color.red;
        GUILayout.Label("务必请设置第一个物体的MainOrder字段，并将UIDepth拖入，且不能为0");
        GUI.color = Color.green;
        GUILayout.Label("子物体若包含UIDepth,点击更新会自动根据第一层级成倍迭增层级");
        GUILayout.Label("层级不是迭代递增的需求 请不要使用本功能！！！");
        GUILayout.BeginHorizontal(EditorStyles.helpBox);
        if (GUILayout.Button("更新", GUILayout.Width(80)))
        {
            script.UpdateDepth();
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("数据窗口", GUILayout.Width(80)))
        {

            myWindow = EditorWindow.GetWindow<UIDepthTreeDataWindowEitor>(false, "OrderData", false);
            myWindow.dataList = script.UIDepthItemList;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }
}

#endregion