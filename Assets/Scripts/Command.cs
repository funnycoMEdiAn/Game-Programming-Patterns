using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    // All movement commands use this speed
    protected float _speed = 8.0f;

    public abstract void Execute(Rigidbody rb);

    public abstract void Undo(Rigidbody rb);

}
