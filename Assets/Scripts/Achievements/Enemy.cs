using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    // The event / action "list" that has all "observers" registered
    public static event Action EnemyDestroyed;

    private void OnDisable()
    {
        EnemyDestroyed?.Invoke();
        Debug.Log("Enemy killed");
    }
}