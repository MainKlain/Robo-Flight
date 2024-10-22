using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsScript : MonoBehaviour
{
    public Rigidbody2D RobotsRigidBody;
    public float FlyPower = 10;
    public GameLogicScript GameLogic;
    public bool isAlive = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            RobotsRigidBody.velocity = Vector2.up * FlyPower;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameLogic.GameOver();
        isAlive = false;
    }
}
