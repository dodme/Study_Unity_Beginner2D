using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IManager
{
    public Action KeyAction = null;


    public void Init()
    {

    }

    public void OnUpdate()
    {
        if (KeyAction != null && Input.anyKey)
        {
            KeyAction.Invoke();
            // Invoke : �������� ����ϰ� ����
        }
    }

    public void Clear()
    {
        KeyAction = null;
    }

}
