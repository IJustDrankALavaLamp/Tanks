using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankMovement : MonoBehaviour
{
    public float m_CloseDistance = 8f; //will stop following the player at this distance

    public Transform m_Turret; //the tanks turret object

    private GameObject m_Player; //reference to the player

    private NavMeshAgent m_NavAgent; // reference to navmesh

    private Rigidbody m_Rigidbody; //a reference to the rigidbody

    private bool m_Follow; //set to true will follow player

    GameObject[] patrols;
    Transform PatrolTarget;
    // Start is called before the first frame update
    void Start()
    {
        patrols = GameObject.FindGameObjectsWithTag("Patrol");
    }

    private void Awake()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_NavAgent = GetComponent<NavMeshAgent>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Follow = false;
    }
    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_Follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            m_Follow = false;
            Update();
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (m_Follow == false)
        { 
            Patrol();
            return;
        }

        float distance = (m_Player.transform.position - transform.position).magnitude;

        if (distance > m_CloseDistance)
        {
            m_NavAgent.SetDestination(m_Player.transform.position);
            m_NavAgent.isStopped = false;
        }
        else
        {
            Patrol();
        }
        if (m_Turret != null)
        {
            m_Turret.LookAt(m_Player.transform);
        }

    }
    private void Patrol()
    {
        if (PatrolTarget == null || Vector3.Distance(transform.position, PatrolTarget.position) < 2)
        {
            int index = Random.Range(0, patrols.Length);
            PatrolTarget = patrols[index].transform;
            m_NavAgent.SetDestination(PatrolTarget.position);
            m_NavAgent.isStopped = false;
        }
    }
}
