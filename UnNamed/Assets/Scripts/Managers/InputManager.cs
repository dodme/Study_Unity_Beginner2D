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
            // Invoke : 한프레임 대기하고 실행
        }
    }

    public void Clear()
    {
        KeyAction = null;
    }

}
