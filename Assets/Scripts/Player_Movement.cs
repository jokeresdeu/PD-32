using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D _playerRb;
    private SpriteRenderer _playerSprite;
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce;
    float _move;

    // Start is called before the first frame update

    void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _playerRb.AddForce(Vector2.up * _jumpForce);
        }

        if(_move > 0 && _playerSprite.flipX)
        {
            _playerSprite.flipX = false;
        }
        else if(_move < 0 && !_playerSprite.flipX)
        {
            _playerSprite.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        if (_move != 0)
        {
            _playerRb.velocity = new Vector2(1, _playerRb.velocity.y) * _speed * _move;
        }
        else
            _playerRb.velocity = new Vector2(0, _playerRb.velocity.y);
    }
}
