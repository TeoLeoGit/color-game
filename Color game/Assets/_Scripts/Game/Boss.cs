using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] Projectile _bossProjectile;
    [SerializeField] Transform _projectileTarget;
    private bool _isMoving = false;
    private bool _isAttacking = false;

    private Animator _anim;

    #region Cached Properties

    private int _currentState;
    private float _lockedTill;
    private float _attackAnimTime = 0.2f;

    private static readonly int Idle = Animator.StringToHash("IdleBoss");
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int Attack = Animator.StringToHash("AttackBoss");
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Fly = Animator.StringToHash("Fly");

    #endregion

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        _isAttacking = true;
        AnimateCharacter();
    }
    void AnimateCharacter()
    {
        var state = GetState();
        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    void AttackProjectile()
    {
        _isAttacking = true;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        // Priorities
        if (_isMoving) return LockState(Attack, _attackAnimTime);
        return Idle;

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }
}
