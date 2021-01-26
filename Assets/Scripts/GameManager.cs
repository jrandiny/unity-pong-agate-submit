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

    private bool _showDebugWindow = false;

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
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), player1.Score.ToString());
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), player2.Score.ToString());

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
        }
        else if (player2.Score == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");
            ballControl.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }

        if (_showDebugWindow)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            var ballMass = _ballRigidBody2D.mass;
            var ballVelocity = _ballRigidBody2D.velocity;
            var ballSpeed = ballVelocity.magnitude;
            var ballMomentum = ballMass * ballVelocity;
            var ballFriction = _ballCollider2D.friction;

            var impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            var impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            var impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            var impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            var debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea) {alignment = TextAnchor.UpperCenter};
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            GUI.backgroundColor = oldColor;
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            _showDebugWindow = !_showDebugWindow;
        }
    }
}