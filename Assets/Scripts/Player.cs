using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    [SerializeField]
    private int _coins;
    [SerializeField]
    private int _lives = 3;

    private UIManager _uiManager;
    private CharacterController _controller;
    private bool _canDoubleJump = false;
    private Vector3 _direction;
    private Vector3 _velocity;
    private Vector3 _wallJumpDirection;
    private float _yVelocity;
    private bool _canWalljump = false;
    private float _pushPower = 2.0f;


    void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Player: Controller is NULL.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("Player : UIManager is NULL."); 
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (_controller.isGrounded == true)
        {
            _wallJumpDirection = Vector3.zero;
            _canWalljump = false;
            float horizontalInput = Input.GetAxis("Horizontal");
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_canDoubleJump)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
                
                if (_canWalljump)
                {
                    _yVelocity += _jumpHeight;
                    _velocity = _wallJumpDirection * _speed;
                }
            }

            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Wall" && _controller.isGrounded == false)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.blue);
            _canWalljump = true;
            _wallJumpDirection = hit.normal;
        }

        if (hit.transform.tag == "Moveable")
        {
            // make sure the hit object has a rigidbody
            Rigidbody objBody = hit.collider.attachedRigidbody;
            if (objBody == null || objBody.isKinematic)
            {
                //no rigidbody or not able to affect it
                return;
            }

            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
            objBody.velocity = pushDir * _pushPower;
        }

    }

    
    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    public int GetCoinCount()
    {
        return _coins;
    }
}
