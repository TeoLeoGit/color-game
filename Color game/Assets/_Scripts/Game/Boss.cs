using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] Projectile _bossProjectile;
    [SerializeField] Transform _projectileLaunchStart;
    private bool _isMoving = false;
    private bool _isAttacking = false;
    private int _projectileLaunchCount = 0;
    private List<Transform> _projectileTargets = new();

    private Animator _anim;

    #region Cached Properties

    private int _currentState;
    private float _lockedTill;
    private float _attackAnimTime = 0.3f;

    private static readonly int Idle = Animator.StringToHash("IdleBoss");
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    private static readonly int Attack = Animator.StringToHash("AttackBoss");
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int Fly = Animator.StringToHash("Fly");

    #endregion

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();

        GameController.OnBossAttack += CallAttackState;
        GameController.OnBossLaunchProjectile += AttackProjectile;
    }

    private void OnDestroy()
    {
        GameController.OnBossAttack -= CallAttackState;
        GameController.OnBossLaunchProjectile -= AttackProjectile;
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

    void CallAttackState(List<Transform> targets)
    {
        StartCoroutine(ILaunchAttackOnTargets(targets));
    }

    IEnumerator ILaunchAttackOnTargets(List<Transform> targets)
    {
        _projectileTargets = targets;
        _projectileLaunchCount = 0;
        _isAttacking = true;
        yield return new WaitUntil(() => _projectileLaunchCount == _projectileTargets.Count);
        _isAttacking = false;
    }
    void AttackProjectile()
    {
        var projectile = Instantiate(_bossProjectile, _projectileLaunchStart.position, Quaternion.identity);
        projectile.SetTarget(_projectileTargets[_projectileLaunchCount]);
        _projectileLaunchCount++;
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        // Priorities
        if (_isAttacking) return LockState(Attack, _attackAnimTime);
        return Idle;

        int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }
}
