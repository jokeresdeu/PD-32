using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D _playerRb;

    [Header("Movement")]
    [SerializeField] private float _speed;

    [Header("Jumping")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private bool _airControll;
    [SerializeField] private Transform _groundCheck;

    [Header("Crawling")]
    [SerializeField] private Transform _cellCheck;
    [SerializeField] private Collider2D _headCollider;
   
    private float _move;
    private bool _canJump;
    private bool _jump;
    private int _jumpCount;
    private bool _crawling;
    private bool _canStand;
    private bool _faceRight;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(_groundCheck.position, _radius, _whatIsGround))
        {
            _canJump = true;
            _jumpCount = 0;
        }
        else
            _canJump = false;

        _canStand = !Physics2D.OverlapCircle(_cellCheck.position, _radius, _whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_cellCheck.position, _radius);
    }

    public void Move(float move, bool jump, bool crawling)
    {
        if (_move != 0 && (_canJump || _airControll))
        {
            _playerRb.velocity = new Vector2(_speed * _move, _playerRb.velocity.y);
        }
        else if (_move == 0 && _canJump)
            _playerRb.velocity = new Vector2(0, _playerRb.velocity.y);

        if(move>0 && !_faceRight)
        {
            Flip();
        }
        else if(move <0 && _faceRight)
        {
            Flip();
        }

        if (_jump && (_canJump || _jumpCount < 2))
        {
            _jumpCount++;
            if (_jumpCount == 2)
            {
                _playerRb.velocity = new Vector2(_playerRb.velocity.x, 0);
            }
            _playerRb.AddForce(Vector2.up * _jumpForce);
            _jump = false;
        }

        if (_crawling)
        {
            _headCollider.enabled = false;
        }
        else if (!_crawling && _canStand)
        {
            _headCollider.enabled = true;
        }
    }
    private void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
    }
}
