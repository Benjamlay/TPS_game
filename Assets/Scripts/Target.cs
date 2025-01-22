using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private float _currentHealth;
// OnEnable
    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log("took damage");
        if(_currentHealth <= 0)
            Destroy(gameObject);
    }
}
