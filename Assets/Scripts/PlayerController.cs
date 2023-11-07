using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;

    // Commands
    Command cmd_W = new MoveForwardCommand();
    Command cmd_A = new MoveLeftCommand();
    Command cmd_S = new MoveBackCommand();
    Command cmd_D = new MoveRightCommand();

    Command _last_command;

    Stack<Command> _undo_commands = new Stack<Command>();
    Stack<Command> _redo_commands = new Stack<Command>();

    void SwapCommands(ref Command A, ref Command B)
    {
        Command tmp = A;
        A = B;
        B = tmp;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        // _undo_commands.push // C# stack // push työntää, pop ottaa ylimmän pois
        //_undo_commands.Push(cmd_W);
        //_undo_commands.Pop(cmd_W);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            cmd_W.Execute(_rigidbody);
            _last_command = cmd_W;
            Debug.Log("W pressed");
            //transform.position += Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            cmd_S.Execute(_rigidbody);
            _last_command = cmd_S;
            Debug.Log("S pressed");
            //transform.position += Vector3.back;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            cmd_A.Execute(_rigidbody);
            _last_command = cmd_A;
            Debug.Log("A pressed");
            //transform.position += Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            cmd_D.Execute(_rigidbody);
            _last_command = cmd_D;
            Debug.Log("D pressed");
            //transform.position += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(5.0f * transform.up, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            _last_command.Undo(_rigidbody);
            _last_command = new DoNothingCommand();
            Debug.Log("Z pressed");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _last_command.Redo(_rigidbody);
            _last_command = new DoNothingCommand();
            Debug.Log("R pressed");
        }

        /*
        if (Input.GetKeyDown(KeyCode.Escape)){
            SwapCommands(ref cmd_A, ref cmd_D);
        }
        */

    }
}
