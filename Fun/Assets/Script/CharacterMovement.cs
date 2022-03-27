using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController CharControl;

    [SerializeField]
    private Transform Camera;

    [SerializeField]
    private float Speed = 3.0f;
    private float Gravity = 0.0f;
    [SerializeField]
    private float JumpHeight = 2.0f;
    private Vector3 PlayerMovement;

    private float TargetAngle;

    public float SmoothRotation = 0.01f;
    private float TurnSmoothVel;

    void Start()
    {
    }

    void Update()
    {
        Gravity += Physics.gravity.y * Time.deltaTime;
       
        PlayerMovement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        Vector3 MoveDirection = new Vector3(0f, 0f, 0f);

        if (PlayerMovement.magnitude >= 0.1)
        {
            TargetAngle = Mathf.Atan2(PlayerMovement.x, PlayerMovement.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref TurnSmoothVel, SmoothRotation);

            transform.rotation = Quaternion.Euler(0.0f, Angle, 0.0f);

            MoveDirection = Quaternion.Euler(0.0f, TargetAngle, 0.0f) * Vector3.forward;
            //MoveDirection.y = MoveDirection.y + Gravity;

            //CharControl.Move(MoveDirection * Speed * Time.deltaTime);
        }

        if (CharControl.isGrounded)
        {
            Gravity = 0;
            if (Input.GetButtonDown("Jump"))
            {
                Gravity = JumpHeight;
                //MoveDirection.y = MoveDirection.y + Gravity;
            }
            //CharControl.Move(MoveDirection * Speed * Time.deltaTime);
        }
        else
        {
            //MoveDirection.y = MoveDirection.y - Gravity;
        }

        MoveDirection.y = MoveDirection.y + Gravity;

        CharControl.Move(MoveDirection * Speed * Time.deltaTime);


    }
}
