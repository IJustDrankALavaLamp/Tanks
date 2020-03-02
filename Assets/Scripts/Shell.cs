using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

    public float m_MaxLifeTime = 2f;

    public float m_MaxDamage = 34f;

    public float m_ExplosionForce = 100f;

    public ParticleSystem m_ExplosionParticles;


    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, m_MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Rigidbody targetRigidBody = other.gameObject.GetComponent<Rigidbody>();
        //add damgae code

        m_ExplosionParticles.transform.parent = null;

        m_ExplosionParticles.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

        Destroy(gameObject);


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
