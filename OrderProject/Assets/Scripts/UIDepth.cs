
using UnityEngine;



public class UIDepth : MonoBehaviour
{
    public int Order
    {
        set { m_order = value; SetOrder(); }
        get { return m_order; }
    }
    private int m_order;
    public bool IsUI
    {
        set
        {  
            isUI = value;
            SetIsUI();
        }
        get { return isUI; }
    }
    private bool isUI = true;

    public static UIDepth Add(GameObject obj)
    {
        if (obj == null)
            return null;

        UIDepth temp = obj.GetComponent<UIDepth>();
        if (temp == null)
            temp = obj.AddComponent<UIDepth>();
        return temp;
    }

    public static void Remove(GameObject obj)
    {
        if (obj == null)
            return;
        UIDepth temp = obj.GetComponent<UIDepth>();
        if (temp != null)
        {
            if (temp.GetComponent<Canvas>() != null) {
                DestroyImmediate(temp.GetComponent<Canvas>());
            }
            DestroyImmediate(temp);
        }  
    }
    /// <summary>
    /// 设置层级界面
    /// </summary>
    public void SetOrder()
    {
        if (isUI)
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = gameObject.AddComponent<Canvas>();
            }
            canvas.overrideSorting = true;
            canvas.sortingOrder = Order;
        }
        else
        {
            Renderer[] renders = GetComponentsInChildren<Renderer>();

            foreach (Renderer render in renders)
            {
                render.sortingOrder = Order;
            }
        }
    }
    /// <summary>
    /// 设置IsUI更新自身组件
    /// </summary>
    public void SetIsUI()
    {
        if (isUI == false)
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null)
            {
                DestroyImmediate(canvas);
            }
        }
        SetOrder();//重新更新一下order

    }
}