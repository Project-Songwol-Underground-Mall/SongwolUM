using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform CameraTransform;

    public float MoveSpeed; // 이동 속도
    public float RotationSpeed; // 회전 속도

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
            //RB.velocity = new Vector3(MoveValue.x * Time.deltaTime, 0, MoveValue.y * Time.deltaTime);
            //Vector3 LocalVelocity = transform.InverseTransformDirection(RB.velocity);
            //LocalVelocity.x = MoveValue.x * Time.deltaTime;
            //LocalVelocity.z = MoveValue.y * Time.deltaTime;
            Vector3 LocalVelocity = new Vector3(MoveValue.x * Time.deltaTime, 0, MoveValue.y * Time.deltaTime);
            RB.velocity = transform.TransformDirection(LocalVelocity);
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