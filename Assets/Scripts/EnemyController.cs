using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private Transform target;
    [SerializeField] private float chaseDistance = 1.0f;

    private bool alive = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        animator = transform.Find("Body").GetComponent<Animator>();
    }

    private void Update()
    {
        if (!alive) return;
        
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= chaseDistance)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
        else
        {
            agent.ResetPath();
        }
        
        animator.SetFloat("Speed", agent.velocity.magnitude);
        Debug.Log(agent.velocity.magnitude);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void PlayDeath()
    {
        animator.SetTrigger("Die");
        GetComponent<Collider>().enabled = false;
        alive = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
