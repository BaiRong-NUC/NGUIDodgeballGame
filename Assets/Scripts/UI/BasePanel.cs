using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T : class
{
    private static T _instance;
    public static T instance;

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        instance = _instance;
    }

    // 初始化面板的抽象方法，由子类实现
    public abstract void InitPanel();

    void Start()
    {
        this.InitPanel();
    }

    public virtual void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
}