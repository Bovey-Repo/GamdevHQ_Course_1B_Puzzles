using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    private Transform _thisFloor;
    [SerializeField]
    private MeshRenderer _callButton;
    [SerializeField]
    private int _coinsReqd = 8;

    private int _coins;
    private Elevator _elevator;
    private bool _calledToThisFloor = false;

    private void Start()
    {
        _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();
        if (_elevator == null)
        {
            Debug.LogError("ElevatorPanel : Elevator is NULL");
        }
    }

    private void Update()
    {
        if (_elevator.transform.position == _thisFloor.position)
        {
            if (_callButton.material.color != Color.green)
            {
                _callButton.material.color = Color.green;
                _calledToThisFloor = false;
            }
        }
        else if (_elevator.transform.position != _thisFloor.position && _calledToThisFloor)
        {
            if (_callButton.material.color != Color.blue)
            {
                _callButton.material.color = Color.blue;
            }
        }
        else if (_elevator.transform.position != _thisFloor.position)
        {
            if (_callButton.material.color != Color.red)
            {
                _callButton.material.color = Color.red;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            _coins = player.GetCoinCount();

            if (Input.GetKeyDown(KeyCode.E))
            {                
                if (_coins == _coinsReqd)
                {
                    _elevator.CallElevator(_thisFloor);
                    _calledToThisFloor = true;

                }
                else
                {
                    //play error sound
                }
            }
        }
    }

}
