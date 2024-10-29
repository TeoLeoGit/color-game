using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // Grid unit size for movement
    public float moveTime = 0.3f; // Grid unit size for movement

    private Vector3 _targetPosition;
    private bool _isMoving = false;
    private Animator _anim;

    #region Cached Properties

    private int _currentState;
    private float _lockedTill;

    private float _walkAnimTime = 0.25f;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Attack = Animator.StringToHash("Attack");

    #endregion

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveCharacter(Vector3.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveCharacter(Vector3.down);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveCharacter(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveCharacter(Vector3.right);
        }
        AnimateCharacter();
    }

    void MoveCharacter(Vector3 direction)
    {
        if (direction == Vector3.left) transform.localScale = Vector3.one + Vector3.left * 2;
        if (direction == Vector3.right) transform.localScale = Vector3.one;

        if (!_isMoving)
        {
            _targetPosition = transform.position + direction * moveSpeed;
            MoveToPosition(_targetPosition);
        }
    }

    void MoveToPosition(Vector3 target)
    {
        _isMoving = true;
        transform.DOMove(_targetPosition, moveTime).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _isMoving = false;
        });
    }

    #region Animations

    void AnimateCharacter()
    {
        var state = GetState();
        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        // Priorities
        if (_isMoving) return LockState(Walk, _walkAnimTime);
        return Idle;

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    #endregion
}
