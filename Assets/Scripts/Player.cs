using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _speed;

    private int _currentHealth;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;

    public Action<float> HealthChanged;

    void Start()
    {
        _currentHealth = _maxHealth;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void ApplyHeal(int health)
    {
        _currentHealth += health;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        HealthChanged?.Invoke((float)_currentHealth / _maxHealth);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke((float)_currentHealth/_maxHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("онлеп");
    }

    private void Move()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");
        FlipRotation(moveHorizontal);
        Vector3 movePos = new Vector3(moveHorizontal, moveVertical, 0);
        _rigidbody.MovePosition(transform.position + movePos * _speed * Time.deltaTime);
    }

    public void PushAway(Vector2 pushFrom, float pushPower)
    {
        pushFrom -= (Vector2)transform.position;
        _rigidbody.AddForce(pushFrom.normalized * pushPower);
    }

    private void FlipRotation(float moveHorizontal)
    {
        if (moveHorizontal < 0)
        {
            _spriteRenderer.flipX = true;
        }
        if (moveHorizontal > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
