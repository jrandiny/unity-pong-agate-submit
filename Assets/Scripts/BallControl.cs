using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public float xInitialForce;
    public float yInitialForce;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        RestartGame();
    }

    void Update()
    {
    }

    void ResetBall()
    {
        transform.position = Vector2.zero;
        _rigidbody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        float randomDirection = Random.Range(0, 2);

        _rigidbody2D.AddForce(
            new Vector2(
                randomDirection < 1.0f ? -xInitialForce : xInitialForce,
                yRandomInitialForce)
        );
    }

    void RestartGame()
    {
        ResetBall();

        Invoke(nameof(PushBall), 2);
    }
}