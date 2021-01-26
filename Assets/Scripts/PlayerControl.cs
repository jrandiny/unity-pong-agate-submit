using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Control
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;

    // Param
    public float speed = 10.0f;
    public float yBoundary = 9.0f;

    private Rigidbody2D _rigidbody2D;
    private int _score;

    private ContactPoint2D _lastContactPoint;
    private Vector2 _trajectoryOrigin;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trajectoryOrigin = transform.position;
    }

    void Update()
    {
        var velocity = _rigidbody2D.velocity;

        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0f;
        }

        _rigidbody2D.velocity = velocity;

        var position = transform.position;

        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        transform.position = position;
    }

    public void IncrementScore()
    {
        _score++;
    }

    public void ResetScore()
    {
        _score = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Ball")
        {
            _lastContactPoint = other.GetContact(0);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _trajectoryOrigin = transform.position;
    }

    public int Score => _score;
    public ContactPoint2D LastContactPoint => _lastContactPoint;
    public Vector2 TrajectoryOrigin => _trajectoryOrigin;
}
