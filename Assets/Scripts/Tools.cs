using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public static class Tools
{
    static Transform transform;

    public static Transform Transform
    {
        get
        {
            if (transform==null)
            {
                transform = GameObject.Find("Canvas").transform;
            }
            return transform;
        }
        set
        {
            transform = value;
        }
    }
    public static GameObject CreatUIPanel(PanelType type)
    {
        GameObject gameObject = Resources.Load<GameObject>("Prefab\\"+type.ToString());
        if (gameObject==null)
        {
            Debug.Log(type.ToString() + "不存在");
            return null;
        }
        else
        {
            GameObject panel = GameObject.Instantiate(gameObject, Transform);
            panel.name = type.ToString();
            return gameObject;
        }
    }

    //public static void DoMoveM(Transform transform,Transform transform2,float time)
    //{
    //   transform.d
    //}
}

