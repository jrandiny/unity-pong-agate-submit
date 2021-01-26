using System;
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
        _player1Rigidbody2D = player1.GetComponent<Rigidbody2D>();
        _player2Rigidbody2D = player2.GetComponent<Rigidbody2D>();
        _ballRigidBody2D = ballControl.GetComponent<Rigidbody2D>();
        _ballCollider2D = ballControl.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width /2 -150 - 12, 20,100,100), player1.Score.ToString());
        GUI.Label(new Rect(Screen.width /2 +150 + 12, 20,100,100), player2.Score.ToString());

        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            player1.ResetScore();
            player2.ResetScore();

            ballControl.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
        }

        if (player1.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");
            ballControl.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }else if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
            ballControl.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
    }
}
