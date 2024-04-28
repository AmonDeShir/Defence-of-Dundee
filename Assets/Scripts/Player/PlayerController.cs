using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController2D
{
    protected float jumpTimer = 0f;

    protected new void Update() {
        base.Update();
        
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        crouch = direction.y <= -1f && IsOnGround();

        if (Input.GetButtonDown("Jump")) {
            jumpTimer += Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump")) {
            crouch = false;
            jump = true;

            if (direction.y <= -1f) {
                ultraJump = true;
            }

            jumpTimer = 0f;
        }
    }
}
