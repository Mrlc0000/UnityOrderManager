using UnityEditor;
using UnityEngine;


#region UIDepthEditor
[CustomEditor(typeof(UIDepth))]
[CanEditMultipleObjects]
public class UIDepthEditor : Editor
{
    UIDepth script;

    private void Awake()
    {
        script = (UIDepth)target;


    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        script.Order = EditorGUILayout.IntField("Order", script.Order);

        script.IsUI = EditorGUILayout.Toggle("isUI", script.IsUI);


    }
}

#endregion

#region UIDepthManagerEditor

[CustomEditor(typeof(UIDepthManager))]
[CanEditMultipleObjects]
public class UIDepthManagerEditor : Editor
{
    UIDepthManager script;

    private void Awake()
    {
        script = target as UIDepthManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUI.color = script.isShow ? Color.green : Color.red;
        string name = script.isShow ? "关闭" : "打开";
        GUILayout.BeginHorizontal(EditorStyles.helpBox);
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(name, GUILayout.Width(80)))
        {
            script.isShow = !script.isShow;
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (!script.isShow)
            return;
        GUILayout.BeginHorizontal(EditorStyles.helpBox);
        if (GUILayout.Button("增加层级组", GUILayout.Width(80)))
        {
            script.AddUIDepthItem();
        }
        GUILayout.EndHorizontal();

        if (script.UIDepthItemList == null)
            return;
        //步骤更新模块
        GUI.color = Color.white;

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        for (int i = 0; i < script.UIDepthItemList.Count; i++)
        {
            GUI.color = Color.white;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            //GUILayout.Label("模块" + i);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("父级");
            script.UIDepthItemList[i].ParentItem = (GameObject)EditorGUILayout.ObjectField(script.UIDepthItemList[i].ParentItem, typeof(UnityEngine.GameObject), true, GUILayout.Width(150));
            script.UIDepthItemList[i].parentOrder = EditorGUILayout.IntField("父级层级", script.UIDepthItemList[i].parentOrder);
            if (GUI.changed)
            {
                script.UIDepthItemList[i].ApplyParentItem();

            }
            EditorGUILayout.EndVertical();
            GUI.color = Color.green;
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("添加子层级", GUILayout.Width(80)))
            {
                script.UIDepthItemList[i].childItem.Add(null);

            }
            GUILayout.Space(10);
            if (GUILayout.Button("自动添加子层级", GUILayout.Width(120)))
            {
                script.UIDepthItemList[i].AutoAddAllChild();
            }
            EditorGUILayout.EndHorizontal();
            GUI.color = Color.white;
            for (int ia = 0; ia < script.UIDepthItemList[i].childItem.Count; ia++)
            {

                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                script.UIDepthItemList[i].childItem[ia] = (GameObject)EditorGUILayout.ObjectField(script.UIDepthItemList[i].childItem[ia], typeof(UnityEngine.GameObject), true, GUILayout.Width(150));


                #region depth层级处理
                if (script.UIDepthItemList[i].childItem[ia] != null)
                {
                    UIDepth tempDepth = script.UIDepthItemList[i].childItem[ia].GetComponent<UIDepth>();
                    if (tempDepth != null)
                    {
                        EditorGUILayout.LabelField("order", GUILayout.MaxWidth(50));
                        EditorGUILayout.IntField(tempDepth.Order, GUILayout.MaxWidth(50));
                        EditorGUILayout.LabelField("isUI", GUILayout.MaxWidth(50));
                        tempDepth.IsUI = EditorGUILayout.Toggle(tempDepth.IsUI, GUILayout.MaxWidth(50));
                    }
                }
                #endregion

                //  EditorGUILayout.IntField(script.UIDepthItemList[i].childItem[ia].)

                GUI.color = Color.green;
                if (ia > 0)
                {
                    if (GUILayout.Button("上", GUILayout.MaxWidth(80)))
                    {
                        int nextId = ia - 1;
                        script.UIDepthItemList[i].ChangeTwoItem(ia, nextId);
                    }
                }
                if (ia < script.UIDepthItemList[i].childItem.Count - 1)
                {
                    if (GUILayout.Button("下", GUILayout.MaxWidth(80)))
                    {

                        int nextId = ia + 1;
                        script.UIDepthItemList[i].ChangeTwoItem(ia, nextId);
                    }
                }
                if (GUI.changed)
                {
                    script.UIDepthItemList[i].ApplyChildItem(ia);
                }
                GUI.color = Color.red;
                if (GUILayout.Button("删除", GUILayout.MaxWidth(80)))
                {
                    script.UIDepthItemList[i].RemoveAtIndex(ia);
                }
                GUI.color = Color.white;
                EditorGUILayout.EndHorizontal();
            }
            GUI.color = Color.red;
            if (GUILayout.Button("删除层级组", GUILayout.Width(80)))
            {
                script.UIDepthItemList.RemoveAt(i);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

}

#endregion



