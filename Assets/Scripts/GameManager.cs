using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControl player1;
    public PlayerControl player2;

    private Rigidbody2D _player1Rigidbody2D;
    private Rigidbody2D _player2Rigidbody2D;

    public BallControl ballControl;
    private Rigidbody2D _ballRigidBody2D;
    private CircleCollider2D _ballCollider2D;

    public int maxScore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
