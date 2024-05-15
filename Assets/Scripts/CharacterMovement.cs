using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform CameraTransform;
    public Transform CEA;

    public float MoveSpeed; // �̵� �ӵ�
    public float RotationSpeed; // ȸ�� �ӵ�

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
        // Update RotationValue based on CEA's rotation
        Vector3 localRotation = CEA.localRotation.eulerAngles;

        // Ignore Z axis rotation from CEA
        Vector3 newRotation = new Vector3(localRotation.x, localRotation.y, 0);

        // Update GameObject's rotation to match CEA's rotation without Z axis rotation
        transform.rotation = Quaternion.Euler(newRotation);

        if (RB != null) 
        {
            Vector3 LocalVelocity = new Vector3(MoveValue.x * Time.deltaTime, 0, MoveValue.y * Time.deltaTime);
            RB.velocity = transform.TransformDirection(LocalVelocity);
            // RB.AddRelativeTorque(-RotationValue.y * Time.deltaTime, RotationValue.x * Time.deltaTime, 0);
        }

        // �÷��̾�(�� ���ӵǾ��ִ� ī�޶�)�� z�� rotation ����, x�� �����̼� ���� ����
        Vector3 currentRotation = transform.eulerAngles;
        currentRotation.z = 0f;
        if (35f < currentRotation.x && currentRotation.x < 180f) currentRotation.x = 35f;
        if (180f < currentRotation.x && currentRotation.x < 330f) currentRotation.x = 330f;
        transform.eulerAngles = currentRotation;
    }
}