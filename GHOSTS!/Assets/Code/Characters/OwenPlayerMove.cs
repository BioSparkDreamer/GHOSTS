// PLACEHOLDER TO TEST SCENE TRIGGERS, NOT A PLAYERCONTROLLER
// code borrowed from my DIG3480 class tutorials
// feels really floaty and not good but its good enough to test
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwenPlayerMove : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
}