using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    CharacterControllerEx _charactercontroller = null;

    private void Start()
    {
        _charactercontroller = transform.parent.gameObject.GetComponent<CharacterControllerEx>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _charactercontroller.IsGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _charactercontroller.IsGround = false;
    }
}
