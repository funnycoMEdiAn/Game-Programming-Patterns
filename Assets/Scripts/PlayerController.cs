using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;

    Vector3 start_pos;

    // Commands
    Command cmd_W = new MoveForwardCommand();
    Command cmd_A = new MoveLeftCommand();
    Command cmd_S = new MoveBackCommand();
    Command cmd_D = new MoveRightCommand();

    Command _last_command;

    bool bReplaying = false;


    // _undo_commands.push // C# stack // push työntää, pop ottaa ylimmän pois
    // Stacks to store the commands
    Stack<Command> _undo_commands = new Stack<Command>();
    Stack<Command> _redo_commands = new Stack<Command>();
    Stack<Command> _replay_commands = new Stack<Command>();


    void SwapCommands(ref Command A, ref Command B)
    {
        Command tmp = A;
        A = B;
        B = tmp;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        start_pos = transform.position;
    }

    IEnumerator Replay()
    {
        while ( _replay_commands.Count > 0 )
        {
            _replay_commands.Pop().Execute(_rigidbody);
            yield return new WaitForSeconds(.5f);
        }

        bReplaying = false;
    }

    void Update()
    {
        if (bReplaying)
        {

        }

        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                bReplaying = true;
                while( _undo_commands.Count > 0)
                {
                    _replay_commands.Push(_undo_commands.Pop());
                }
                transform.position = start_pos;

                StartCoroutine(Replay());

            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                cmd_W.Execute(_rigidbody);
                _undo_commands.Push(cmd_W);
                _redo_commands.Clear();

                Debug.Log("W pressed");
                //_last_command = cmd_W;
                //transform.position += Vector3.forward;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                cmd_S.Execute(_rigidbody);
                _undo_commands.Push(cmd_S);
                _redo_commands.Clear();

                Debug.Log("S pressed");
                //_last_command = cmd_S;
                //transform.position += Vector3.back;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                cmd_A.Execute(_rigidbody);
                _undo_commands.Push(cmd_A);
                _redo_commands.Clear();

                Debug.Log("A pressed");
                //_last_command = cmd_A;
                //transform.position += Vector3.left;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                cmd_D.Execute(_rigidbody);
                _undo_commands.Push(cmd_D);
                _redo_commands.Clear();

                Debug.Log("D pressed");
                //_last_command = cmd_D;
                //transform.position += Vector3.right;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody.AddForce(5.0f * transform.up, ForceMode.Impulse);
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (_undo_commands.Count > 0)
                {
                    Command cmd = _undo_commands.Pop();
                    _redo_commands.Push(cmd);
                    cmd.Undo(_rigidbody);
                    //_undo_commands.Pop().Undo(_rigidbody);
                }

                //_last_command = new DoNothingCommand();
                Debug.Log("Z pressed");
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (_redo_commands.Count > 0)
                {
                    Command cmd = _redo_commands.Pop();
                    _undo_commands.Push(cmd);
                    cmd.Execute(_rigidbody);
                }

                /*  //Omat vanhat koodit
                _last_command.Redo(_rigidbody);
                _last_command = new DoNothingCommand();
                Debug.Log("R pressed");
                */
            }

            /* //Jos haluaa vaihtaa jotkin napit toisin päin
            if (Input.GetKeyDown(KeyCode.Escape)){
                SwapCommands(ref cmd_A, ref cmd_D);
            }
            */
        }

    }
}
