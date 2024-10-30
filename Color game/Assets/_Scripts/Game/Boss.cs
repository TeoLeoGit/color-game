using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private bool _isMoving = false;
    private Animator _anim;

    #region Cached Properties

    private int _currentState;
    private float _lockedTill;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Fly = Animator.StringToHash("Fly");

    #endregion

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        AnimateCharacter();
    }
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
        //if (_isMoving) return LockState(Walk, _walkAnimTime);
        return Idle;

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }
}
