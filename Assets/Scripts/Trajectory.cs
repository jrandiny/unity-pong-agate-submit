using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public BallControl ball;
    private CircleCollider2D _ballCircleCollider2D;
    private Rigidbody2D _ballRigidbody2D;

    public GameObject ballAtCollision;
    void Start()
    {
        _ballRigidbody2D = ball.GetComponent<Rigidbody2D>();
        _ballCircleCollider2D = ball.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        bool drawBallAtCollision = false;

        var offsetHitPoint = new Vector2();
        
        RaycastHit2D[] circleCastHit2DArray =
            Physics2D.CircleCastAll(_ballRigidbody2D.position, _ballCircleCollider2D.radius,
                _ballRigidbody2D.velocity.normalized);

        foreach (RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            if (circleCastHit2D.collider != null &&
                circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {
                Vector2 hitPoint = circleCastHit2D.point;
                Vector2 hitNormal = circleCastHit2D.normal;

                offsetHitPoint = hitPoint + hitNormal * _ballCircleCollider2D.radius;

                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                if (circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if (outDot > -1.0f && outDot < 1.0)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(
                            offsetHitPoint,
                            offsetHitPoint + outVector * 10.0f);

                        drawBallAtCollision = true;
                    }
                }

                break;
            }
        }

        if (drawBallAtCollision)
        {
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.SetActive(true);
        }
        else
        {
            ballAtCollision.SetActive(false);
        }
    }

    private void OnDisable()
    {
        ballAtCollision.SetActive(false);
    }
}
