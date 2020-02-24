using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float m_Speed = 12f; //move speed
    public float m_Turn = 180f; //turning speed
    public float m_Jump = 20f; //jump force

    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;             //movement input value
    private float m_TurnInputValue;                //turn input value
    private float m_JumpInput;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();


    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;

        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }//makes sure tank isnt kinematic when turned on

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }//sets kinematic to stop tank when turned off

    private void Update()
    {
        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");

    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        

    }
    private void Move()
    {
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = m_TurnInputValue * m_Turn * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f); //makes rotation y axis

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        
    }





}
