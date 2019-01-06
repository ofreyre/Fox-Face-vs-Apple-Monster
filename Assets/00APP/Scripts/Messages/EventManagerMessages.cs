using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerMessages : MonoBehaviour
{
    public static EventManagerMessages instance;
    public delegate void DelegateMessage(string message, bool ok, bool yes, bool no, bool remove, bool watch);
    public DelegateMessage Message;
    public delegate void DelegateInt(int value);
    public DelegateInt Int;

    public delegate void DelegateOK();
    public DelegateOK OK;
    public delegate void DelegatYes();
    public DelegateOK Yes;
    public delegate void DelegatNo();
    public DelegateOK No;
    public delegate void DelegatRemove();
    public DelegatRemove Remove;
    public delegate void DelegatWatch();
    public DelegatWatch Watch;

    private void Awake()
    {
        instance = this;
    }

    public void DispatchMessage(string message, bool ok = true, bool yes = false, bool no = false, bool remove = false, bool watch = false)
    {
        if (Message != null)
        {
            Message(message, ok, yes, no, remove, watch);
        }
    }

    public void DispatchInt(int value)
    {
        if (Int != null)
        {
            Int(value);
        }
    }

    public void DispatchOK()
    {
        if (OK != null)
        {
            OK();
        }
    }

    public void DispatchYes()
    {
        if (Yes != null)
        {
            Yes();
        }
    }

    public void DispatchNo()
    {
        if (No != null)
        {
            No();
        }
    }

    public void DispatchRemove()
    {
        if (Remove != null)
        {
            Remove();
        }
    }

    public void DispatchWatch()
    {
        if (Watch != null)
        {
            Watch();
        }
    }
}
