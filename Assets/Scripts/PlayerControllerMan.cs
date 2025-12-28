using System.Collections;
using UnityEngine;

public class PlayerControllerMan : PlayerController
{
    protected override void Start()
    {
        base.Start();
        facingRight = false;
    }

    public override void GetMovementInput()
    {
        // Get horizontal input and add horizontal movement
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
        }
    }

    public override void GetJumpInput()
    {
        jumping = Input.GetKey(KeyCode.W);
    }

    public override void GetDownInput()
    {
        hasDownInput = Input.GetKeyDown(KeyCode.S);
    }
}

