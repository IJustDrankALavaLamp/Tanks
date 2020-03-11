using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.2f;

    public Transform m_target;

    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;

    private void Awake()
    {
        m_target = GameObject.FindGameObjectWithTag("TankCam").transform; //gets the camera on the tank
    }

    public float rotateSpeed = 10;

    private void Update()
    {

        Vector3 angles = transform.localEulerAngles + (Vector3.right * Input.GetAxis("Mouse Y") + Vector3.up * Input.GetAxis("Mouse X")) * rotateSpeed;
        angles.x = Mathf.Clamp(angles.x, 0, 0);

        transform.localEulerAngles = angles;
    }
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        m_DesiredPosition = m_target.position; //sets the target position to the location of the selected thing

        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime); //sets the speed the camera follows




    }


}
