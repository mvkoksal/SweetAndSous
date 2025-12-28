using System.Collections;
using UnityEngine;

public class PlayerControllerWoman : PlayerController
{
    protected override void Start()
    {
        base.Start();
        facingRight = true;
    }

    public override void GetMovementInput()
    {
        // Get horizontal input and add horizontal movement
        horizontalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }
    }

    public override void GetJumpInput()
    {
        jumping = Input.GetKey(KeyCode.UpArrow);
    }

    public override void GetDownInput()
    {
        hasDownInput = Input.GetKeyDown(KeyCode.DownArrow);
    }
}

