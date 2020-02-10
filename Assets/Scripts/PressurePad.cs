using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _padDisplay;
    [SerializeField]
    private float _hitRange = 0.1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Moveable")
        {
            if ((other.transform.position.x >= this.transform.position.x - _hitRange) &&
                    (other.transform.position.x <= this.transform.position.x + _hitRange))
            {
                Rigidbody movingObject = other.attachedRigidbody;
                if (movingObject != null)
                {
                    movingObject.isKinematic = true;
                }

                _padDisplay.material.color = Color.green;
                Destroy(this);
            }
        }
    }
}
