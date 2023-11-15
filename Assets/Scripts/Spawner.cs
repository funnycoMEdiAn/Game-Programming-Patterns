using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Monster monster;

    public float X = 1f;
    public float Y = 1f;
    public float Z = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, new Vector3(X, Y, Z));
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
