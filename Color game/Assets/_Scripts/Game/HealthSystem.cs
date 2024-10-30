using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float _maxHealth;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;

        GameController.OnPlayerHealthUpdate += UpdateHealth;
    }

    private void OnDestroy()
    {
        GameController.OnPlayerHealthUpdate -= UpdateHealth;
    }

    void UpdateHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0f, _currentHealth + amount);
    }
}
