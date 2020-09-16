using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private Animator _playerAnimator;
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
 
    private bool _grounded;
    private int _jumpCount;
    private bool _canStand;
    private bool _faceRight = true;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_cellCheck.position, _radius);
    }

    public void Move(float move, bool jump, bool crouch)
    {
        #region Animation
        _playerAnimator.SetFloat("Speed", Mathf.Abs(move));
        _playerAnimator.SetBool("Jump", !_grounded);
        _playerAnimator.SetBool("Crouch", !_headCollider.enabled);
        #endregion
        #region Horizontalmovement
        if (move != 0 && (_grounded || _airControll))
        {
            _playerRb.velocity = new Vector2(_speed * move, _playerRb.velocity.y);
        }
        else if (move == 0 && _grounded)
            _playerRb.velocity = new Vector2(0, _playerRb.velocity.y);

        if (move > 0 && !_faceRight)
        {
            Flip();
        }
        else if (move < 0 && _faceRight)
        {
            Flip();
        }
        #endregion
        #region Jumping
        if (Physics2D.OverlapCircle(_groundCheck.position, _radius, _whatIsGround))
        {
            _grounded = true;
            _jumpCount = 0;
        }
        else
            _grounded = false;

        if (jump && (_grounded || _jumpCount < 2))
        {
            _jumpCount++;
            _playerRb.velocity = new Vector2(_playerRb.velocity.x, _jumpForce);
        }

        #endregion
        #region Crouching
        _canStand = !Physics2D.OverlapCircle(_cellCheck.position, _radius, _whatIsGround);
        if (crouch)
        {
            _headCollider.enabled = false;
        }
        else if (!crouch && _canStand)
        {
            _headCollider.enabled = true;
        }
        #endregion 
    }
    private void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
    }
}
