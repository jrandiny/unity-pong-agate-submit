using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpController : MonoBehaviour
{
    [HideInInspector] public UnityEvent activated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            activated.Invoke();
        }
    }
}
