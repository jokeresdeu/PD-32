using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_InputController : MonoBehaviour
{
    [SerializeField] private Player_Movement _playerMovement;

    private float _move;
    private bool _jump;
    private bool _crawling;
    private void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _jump = true;
        }
        _crawling = Input.GetKey(KeyCode.C);
    }

    private void FixedUpdate()
    {
        _playerMovement.Move(_move, _jump, _crawling);
        _jump = false;
    }
}
