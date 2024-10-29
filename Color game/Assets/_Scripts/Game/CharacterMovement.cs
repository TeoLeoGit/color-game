using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 25f; // Speed of movement between cells
    public Vector2 gridPosition = new Vector2(0, 0); // Initial grid position
    public float cellSize = 1f; // Size of each cell

    private Vector3 targetPosition;
    private Rigidbody2D _rigidbody;
    private Animator _anim;
    private bool _isMoving = false;

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
        gridPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        HandleInput();
        MoveCharacter();
        AnimateCharacter();
    }

    void HandleInput()
    {
        if (_isMoving) return;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            gridPosition.y += 1;
            _isMoving = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            gridPosition.y -= 1;
            _isMoving = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.localScale = Vector3.one + Vector3.left * 2;
            gridPosition.x -= 1;
            _isMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.localScale = Vector3.one;
            gridPosition.x += 1;
            _isMoving = true;
        }

        // Clamp the grid position to stay within the grid boundaries
        //gridPosition.x = Mathf.Clamp(gridPosition.x, 0, 8);
        //gridPosition.y = Mathf.Clamp(gridPosition.y, 0, 8);

        if (_isMoving)
        {
            SetTargetPosition();
        }
    }

    void MoveCharacter()
    {
        if (!_isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            _isMoving = false;
        }
    }

    void SetTargetPosition()
    {
        targetPosition = new Vector3(gridPosition.x * cellSize, gridPosition.y * cellSize, 0);
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
