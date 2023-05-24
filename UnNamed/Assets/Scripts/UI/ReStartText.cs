using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReStartText : MonoBehaviour
{
    float _time = 0;
    Text text = null;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
            return;

        _time += Time.deltaTime;

        if (_time >= 1f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            if (_time >= 2f)
            {
                _time = 0;
                return;
            }
        }
        else
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 255);
        }

    }
}
