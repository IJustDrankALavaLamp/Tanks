using UnityEngine;

public class Shell : MonoBehaviour
{

    public float m_MaxLifeTime = 2f;

    public float m_MaxDamage = 34f;

    public float m_ExplosionRadius = 5;

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


        if (targetRigidBody != null)
        {
            targetRigidBody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            TankHealth targetHealth = targetRigidBody.GetComponent<TankHealth>();
            if (targetHealth != null)
            {
                float damage = CalculateDamage(targetRigidBody.position);

                targetHealth.TakeDamage(damage);
            }
        }



        m_ExplosionParticles.transform.parent = null;

        m_ExplosionParticles.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;

        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;


        float damage = relativeDistance * m_MaxDamage;

        damage = Mathf.Max(0f, damage);

        return damage;

    }
    // Update is called once per frame
    void Update()
    {

    }
}
