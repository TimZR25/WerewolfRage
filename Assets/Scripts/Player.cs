using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _speed;

    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal= Input.GetAxis("Horizontal");
        FlipRotation(moveHorizontal);
        Vector3 movePos = new Vector3(moveHorizontal, moveVertical,0);
        _rb.MovePosition(transform.position + movePos * _speed * Time.deltaTime);
    }
    public void PushAway(Vector2 pushFrom, float pushPower)
    {
        pushFrom = (Vector2)transform.position - pushFrom;
        _rb.AddForce((Vector2)transform.position + pushFrom * pushPower);
    }

    private void FlipRotation(float moveHorizontal)
    {
        if (moveHorizontal < 0)
        {
            _sr.flipX = true;
        }
        if (moveHorizontal > 0)
        {
            _sr.flipX = false;
        }
    }
}
