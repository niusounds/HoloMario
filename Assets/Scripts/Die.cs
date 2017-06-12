using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [Tooltip("Mario dies when Position.Y is under this value.")]
    public float deadZone = -5.0f;
    private bool dead = false;

    // Update is called once per frame
    void Update()
    {
        if (!dead && transform.position.y < deadZone)
        {
            SendMessage("OnDie");
            dead = true;

            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
