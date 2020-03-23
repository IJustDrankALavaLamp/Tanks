using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShot : MonoBehaviour
{

    public Rigidbody m_Shell;

    public Transform m_FireTransform;

    public float m_LauchForce = 30f;

    public int ammo = 6;

    public Text m_AmmoMess;

    private float reloadTimer;

    public float reloadTime = 10;

    public int maxammo = 6;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if ((Input.GetButtonUp("Fire1")) && ammo > 0)
        {
            Fire();
            ammo--;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {

            reloadTimer = reloadTime;
        }
        if(reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;

            if(reloadTimer<=0)
            {
                ammo = maxammo;
            }
        }

        m_AmmoMess.text = "Ammo: " + ammo.ToString();
    }
    private void Fire()
    {
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;


        shellInstance.velocity = m_LauchForce * m_FireTransform.forward;

    }
}
