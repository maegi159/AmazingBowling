﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    // 한정된 상태에 대해 한 가지 값만 가지게 함
    private enum RotateState
    {
        Idle,Vertical,Horizontal,Ready
    }
    // 현재 상태를 구분하여 저장
    private RotateState state = RotateState.Idle;
    // 수평방향 회전 속도
    public float verticalRotateSpeed = 360f;
    // 수직방향 회전 속도
    public float horizontalRotateSpeed = 360f;

    public void Update()
    {
        switch (state)
        {
            case RotateState.Idle:
                if (Input.GetButtonDown("Fire1"))
                {
                    state = RotateState.Horizontal;
                }
                break;
            case RotateState.Horizontal:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(0, horizontalRotateSpeed * Time.deltaTime, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Vertical;
                }
                break;
            case RotateState.Vertical:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Ready;
                }
                break;
            case RotateState.Ready:
                break;
            default:
                break;
        }
        
    }
}
