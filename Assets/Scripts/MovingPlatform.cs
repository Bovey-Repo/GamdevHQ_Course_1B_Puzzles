using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _pointA, _pointB;
    [SerializeField]
    private float _speed = 3.0f;

    private Transform _target;

    void Start()
    {
        _target = _pointB;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (transform.position == _pointA.position)
        {
            _target = _pointB;
        }
        else if (transform.position == _pointB.position)
        {
            _target = _pointA;
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
