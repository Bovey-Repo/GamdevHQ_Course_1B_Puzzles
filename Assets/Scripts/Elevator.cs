using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private Transform _level1, _level2;

    [SerializeField]
    private MeshRenderer _button1, _button2;

    private Transform _target;
    private bool _buttonsBlack = false;

    private void Start()
    {
        _target = this.transform;
         _button1.material.color = Color.black;
         _button2.material.color = Color.black;
    }

    private void FixedUpdate()
    {
        if (transform.position != _target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }
        else if (transform.position == _target.position)
        {
            if (_buttonsBlack == false)
            {
                _button1.material.color = Color.black;
                _button2.material.color = Color.black;
                _buttonsBlack = true;
            }
        }

    }

    public void CallElevator(Transform t)
    {
        _target = t;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CallElevator(_level1);
                _button1.material.color = Color.white;
                _buttonsBlack = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CallElevator(_level2);
                _button2.material.color = Color.white;
                _buttonsBlack = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
