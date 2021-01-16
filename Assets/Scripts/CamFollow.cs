using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State
    {
        Idle, Ready, Tracking
    }

    // 프로퍼티
    // 처리를 간결하게 보이게 함
    private State state
    {
        set
        {
            switch (value)
            {
                case State.Idle:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    targetZoomSize = ReadyShotZoomSize;
                    break;
                case State.Tracking:
                    targetZoomSize = trackingZoomsize;
                    break;
            }
        }
    }

    private Transform target;
    // 원하는 값에 도달하기 전 지연시간
    public float smoothTime = 0.2f;
    // 원하는 위치까지 어느 속도로 이동했는지
    private Vector3 lastmovingVelocity;
    private Vector3 targetPosition;

    private Camera cam;
    private float targetZoomSize = 5f;

    private const float roundReadyZoomSize = 14.5f;
    private const float ReadyShotZoomSize = 5f;
    private const float trackingZoomsize = 10f;

    private float lastZoomSpeed;
    
    private void Awake()
    {
        // 자식에 있는 컴포넌트 중 타입에 맞는 것을 찾아줌
        cam = GetComponentInChildren<Camera>();
        // state를 변수로 전달
        // 내부에서는 equal을 통해 전달한 값이 value로 들어감
        state = State.Idle;
    }

    private void Move()
    {
        targetPosition = target.transform.position;
        // transform.position = targetPosition; -> 카메라가 딱 붙어다님
        //ref -> 값이 변경되면 lastmovingVelocity에 다시 넣음
        //smoothDamp(현재위치, 가고싶은 위치, 마지막 순간의 변화랑,지연시간)
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition,ref lastmovingVelocity,smoothTime);
        transform.position = smoothPosition;
    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, smoothTime);
        cam.orthographicSize = smoothZoomSize;
    }

    // 렉과 상관없이 정해진 간격에 따라 업데이트
    private void FixedUpdate()
    {
        if(target != null)
        {
            Zoom();
            Move();
        }
    }

    public void Reset()
    {
        state = State.Idle;
    }

    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
