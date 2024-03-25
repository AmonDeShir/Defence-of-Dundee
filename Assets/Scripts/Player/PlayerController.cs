using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController2D
{
    protected float jumpTimer = 0f;

    void Update() {
        base.Update();
        
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpTimer += Time.deltaTime;
            Debug.Log(jumpTimer);
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            jump = true;

            if (direction.y <= -1f) {
                ultraJump = true;
            }

            jumpTimer = 0f;
        }
    }
}
