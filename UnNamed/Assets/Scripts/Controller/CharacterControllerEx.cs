using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerEx : MonoBehaviour
{
    Define.State _state = Define.State.Idle;

    [SerializeField, Range(0, 20)]
    float _moveSpeed = 0;
    [SerializeField, Range(0, 20)]
    float _jumpSpeed = 0;

    Rigidbody2D _rigid = null;
    Animator _anim = null;
    SpriteRenderer _spriteRenderer = null;

    bool isGround = false;

    public bool IsGround { get => isGround; set => isGround = value; }

    GameObject GameOverCanvas = null;
    CameraController _cameraController = null;

    private void Start()
    {
        GameManager.Input.KeyAction -= Movement;
        GameManager.Input.KeyAction += Movement;
        GameManager.Input.KeyAction -= Jump;
        GameManager.Input.KeyAction += Jump;
        GameManager.Input.KeyAction -= Crouch;
        GameManager.Input.KeyAction += Crouch;

        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GameOverCanvas = Resources.Load<GameObject>("Prefabs/UI/GameOverCanvas");

        _cameraController = Camera.main.GetComponent<CameraController>();
        _cameraController.Player = this.gameObject;

    }

    private void Update()
    {
        if (_state == Define.State.Hit)
        {
            GameManager.Input.Clear();
            return;
        }

        if (!Input.anyKey)
        {
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        }

        if (_rigid.velocity.y <= -2)
        {
            _anim.Play("JumpDown");
            return;
        }

        switch (_state)
        {
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Run:
                UpdateRun();
                break;
            case Define.State.Jump:
                UpdateJump();
                break;
            case Define.State.Crouch:
                UpdateCrouch();
                break;
        }




    }


    private void UpdateIdle()
    {
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        _anim.SetBool("IsMoving", false);
        _anim.SetBool("IsJumping", false);
        _anim.SetBool("IsCrouching", false);
    }

    private void UpdateRun()
    {
        if (IsGround && (_rigid.velocity.magnitude <= 1f))
        {
            _state = Define.State.Idle;
            return;
        }

        _anim.SetBool("IsMoving", true);
        _anim.SetBool("IsJumping", false);
        _anim.SetBool("IsCrouching", false);
    }

    private void UpdateJump()
    {
        if (IsGround && (_rigid.velocity.magnitude <= 1f))
        {
            _state = Define.State.Idle;
            return;
        }

        _anim.SetBool("IsMoving", false);
        _anim.SetBool("IsJumping", true);
        _anim.SetBool("IsCrouching", false);

        if (_rigid.velocity.y <= 0.1f)
        {
            _anim.SetBool("IsJumpingDown", true);
            return;
        }
        _anim.SetBool("IsJumpingDown", false);
    }

    private void UpdateCrouch()
    {
        _anim.SetBool("IsMoving", false);
        _anim.SetBool("IsJumping", false);
        _anim.SetBool("IsCrouching", true);

        if (!Input.GetKey(KeyCode.DownArrow))
        {
            _state = Define.State.Idle;
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rigid.AddForce(Vector2.right * _moveSpeed, ForceMode2D.Impulse);
            _rigid.velocity = new Vector2(_moveSpeed, _rigid.velocity.y);
            _spriteRenderer.flipX = false;

            if (!_anim.GetBool("IsJumping"))
                _state = Define.State.Run;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rigid.AddForce(Vector2.left * _moveSpeed, ForceMode2D.Impulse);
            _rigid.velocity = new Vector2(-_moveSpeed, _rigid.velocity.y);
            _spriteRenderer.flipX = true;

            if (!_anim.GetBool("IsJumping"))
                _state = Define.State.Run;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround)
        {
            _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            _state = Define.State.Jump;
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.DownArrow) && isGround)
        {
            _state = Define.State.Crouch;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            _anim.Play("Hit");
            GameManager.Input.Clear();
            _state = Define.State.Hit;
        }

        if (collision.transform.CompareTag("SceneCheck"))
        {
            GameManager.Scene.SceneIndex++;
            GameManager.Input.Clear();
            GameManager.Scene.SceneLoad();
        }
    }

    // AnimEvent
    public void AnimEvent_Hit()
    {
        _rigid.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        Collider2D collider = GetComponent<CapsuleCollider2D>();
        collider.enabled = false;

        Instantiate(GameOverCanvas);

        Destroy(gameObject, 2f);
    }


    /*  private void OnDestroy()
      {
          GameManager.Input.Clear();
      }*/
}
