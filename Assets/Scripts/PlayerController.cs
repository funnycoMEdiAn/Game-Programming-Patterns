using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        JUMPING,
        STANDING,
        CROUCHING
    }

    public enum AbilityState
    {
        NONE,
        SUPERSIZE
    }

    public PlayerState my_state = PlayerState.JUMPING;

    Rigidbody _rigidbody;
    public GameObject Coin;
    public GameObject Enemy;


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
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // Destroy the coin!
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

    public void OnCollisionEnter(Collision collision)
    {
        if (my_state == PlayerState.JUMPING)
        {
            my_state = PlayerState.STANDING;
        }
       // Lisää jotain schaisee jolla tunnistetaa et pelaaja osuu groundii ja sen state vaihtuu
    }


    void Update()
    {
        if (bReplaying)
        {

        }

        else
        {
            switch (my_state)
            {
                case PlayerState.STANDING:

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _rigidbody.AddForce(5.0f * transform.up, ForceMode.Impulse);
                        my_state = PlayerState.JUMPING;
                    }

                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        cmd_W.Execute(_rigidbody);
                        _undo_commands.Push(cmd_W);
                        _redo_commands.Clear();

                        my_state = PlayerState.STANDING;
                    }

                    if (Input.GetKeyDown(KeyCode.S))
                    {
                        cmd_S.Execute(_rigidbody);
                        _undo_commands.Push(cmd_S);
                        _redo_commands.Clear();

                        my_state = PlayerState.STANDING;
                    }

                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        cmd_A.Execute(_rigidbody);
                        _undo_commands.Push(cmd_A);
                        _redo_commands.Clear();

                        my_state = PlayerState.STANDING;
                    }

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        cmd_D.Execute(_rigidbody);
                        _undo_commands.Push(cmd_D);
                        _redo_commands.Clear();

                        my_state = PlayerState.STANDING;
                    }

                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        transform.localScale = new Vector3(1f, 0.5f, 1f);
                        my_state = PlayerState.CROUCHING;
                    }
                    

                    break;

                case PlayerState.JUMPING:
                    break;

                case PlayerState.CROUCHING:
                    if (Input.GetKeyDown(KeyCode.C))
                    {
                        transform.localScale = new Vector3(1f, 1f, 1f);
                        my_state = PlayerState.STANDING;
                    }
                    break;

            }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    bReplaying = true;
                    while (_undo_commands.Count > 0)
                    {
                        _replay_commands.Push(_undo_commands.Pop());
                        
                    }
                    transform.position = start_pos;

                    StartCoroutine(Replay());
                    //my_state = PlayerState.REPLAYING;
                    my_state = PlayerState.STANDING;

                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (_undo_commands.Count > 0)
                    {
                        Command cmd = _undo_commands.Pop();
                        _redo_commands.Push(cmd);
                        cmd.Undo(_rigidbody);

                        my_state = PlayerState.STANDING;
                    }
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    if (_redo_commands.Count > 0)
                    {
                        Command cmd = _redo_commands.Pop();
                        _undo_commands.Push(cmd);
                        cmd.Execute(_rigidbody);

                        my_state = PlayerState.STANDING;

                    }

                }

        }

    }
}
