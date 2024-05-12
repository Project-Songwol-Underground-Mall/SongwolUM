using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform CameraTransform;
    public CharacterController characterController;

    public float MoveSpeed; // 이동 속도
    public float RotationSpeed; // 회전 속도
    public float Gravity = -20f; // 중력
    public float YVelocity = 0; // Y축 움직임

    public float Sensitivity = 1f;
    public float RotationX;
    public float RotationY;

    private Vector2 RotationValue;
    private Vector2 MoveValue;
    private Rigidbody RB;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        RB = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        MoveValue = value.Get<Vector2>() * MoveSpeed;
    }

    public void OnLook(InputValue value)
    {
        RotationValue = value.Get<Vector2>() * RotationSpeed;
    }

    void Update()
    {
        if (RB != null) 
        {
            RB.AddRelativeForce(MoveValue.x * Time.deltaTime, 0, MoveValue.y * Time.deltaTime);
            RB.AddRelativeTorque(-RotationValue.y * Time.deltaTime, RotationValue.x * Time.deltaTime, 0);
        }

        // 플레이어(에 종속되어있는 카메라)의 z축 rotation 고정, x축 로테이션 범위 제한
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = 0f;
        if (35f < currentRotation.x && currentRotation.x < 180f) currentRotation.x = 35f;
        if (180f < currentRotation.x && currentRotation.x < 330f) currentRotation.x = 330f;
        transform.eulerAngles = currentRotation;
    }
}