using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform CameraTransform;
    public Transform CEA;

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

    //public void OnLook(InputValue value)
    //{
    //    Vector3 localRotation = CEA.localRotation.eulerAngles;
    //    RotationValue = new Vector2(-localRotation.x, localRotation.y);
    //}

    void Update()
    {
        //Vector3 localRotation = CEA.localRotation.eulerAngles;
        //Vector3 newRotation = new Vector3(localRotation.x, localRotation.y, 0);
        //transform.rotation = Quaternion.Euler(newRotation);
        //CEA.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        // if (35f < localRotation.x && localRotation.x < 180f) localRotation.x = 35f;
        // if (180f < localRotation.x && localRotation.x < 330f) localRotation.x = 330f;

        CEA.position = transform.position;


        if (RB != null) 
        {
            Vector3 LocalVelocity = new Vector3(MoveValue.x * Time.deltaTime, 0, MoveValue.y * Time.deltaTime);
            RB.velocity = transform.TransformDirection(LocalVelocity);
            // RB.AddRelativeTorque(-RotationValue.y * Time.deltaTime, RotationValue.x * Time.deltaTime, 0);
        }
    }
}