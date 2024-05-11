using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterController characterController;

    public float MoveSpeed = 10f; // 이동 속도
    public float JumpSpeed = 10f; // 점프 속도
    public float Gravity = -20f; // 중력
    public float YVelocity = 0; // Y축 움직임

    public float Sensitivity = 1f;
    public float RotationX;
    public float RotationY;

    private Vector2 movementValue;
    private float lookValue;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputValue value)
    {
        movementValue = value.Get<Vector2>() * MoveSpeed;
    }

    public void OnLook(InputValue value)
    {
        lookValue = value.Get<Vector2>().x * MoveSpeed;
    }

    void Update()
    {
        //float h = Input.GetAxis("Horizontal");
        //// h 변수에 키보드의 가로값 (좌, 우) 을 읽어온 결과를 넘긴다.
        //// ◀, ▶, A, D 키

        //float v = Input.GetAxis("Vertical");
        //// v 변수에 키보드의 세로값 (상, 하) 을 읽어온 결과를 넘긴다.
        //// ▲, ▼, W, S 키

        //Vector3 MoveDirection = new Vector3(h, 0, v);

        //MoveDirection = cameraTransform.TransformDirection(MoveDirection);

        //MoveDirection *= MoveSpeed;

        //if (characterController.isGrounded)
        //{
        //    YVelocity = 0;
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        YVelocity = JumpSpeed;
        //    }
        //}

        //YVelocity += (Gravity * Time.deltaTime);
        //MoveDirection.y = YVelocity;
        //characterController.Move(MoveDirection * Time.deltaTime);


        //float MouseX = Input.GetAxis("Mouse X");
        //float MouseY = Input.GetAxis("Mouse Y");

        //RotationX += MouseY * Sensitivity * 5f * Time.deltaTime;
        //RotationY += MouseX * Sensitivity * 5f * Time.deltaTime;

        //if (RotationX > 35f) RotationX = 35f;
        //if (RotationX < -30f) RotationX = -30f;
        //cameraTransform.eulerAngles = new Vector3(-RotationX, RotationY, 0);
    }
}