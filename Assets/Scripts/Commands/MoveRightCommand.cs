using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightCommand : Command
{
    public override void Execute(Rigidbody rb)
    {
        //rb.AddForce(_speed * rb.transform.right, ForceMode.Impulse);
        rb.transform.position += rb.transform.right;
    }

    public override void Undo(Rigidbody rb)
    {
        rb.transform.position -= rb.transform.right;
    }

    public override void Redo(Rigidbody rb)
    {
        rb.transform.position += rb.transform.right;
    }
}