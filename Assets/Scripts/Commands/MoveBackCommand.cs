using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackCommand : Command
{
    public override void Execute(Rigidbody rb)
    {
        //rb.AddForce(_speed * -rb.transform.forward, ForceMode.Impulse);
        rb.transform.position -= rb.transform.forward;
    }

    public override void Undo(Rigidbody rb)
    {
        rb.transform.position += rb.transform.forward;
    }

    public override void Redo(Rigidbody rb)
    {
        rb.transform.position -= rb.transform.forward;
    }
}