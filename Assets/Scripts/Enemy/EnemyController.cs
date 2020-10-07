using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(CharacterCombat))]
public class EnemyController : MonoBehaviour, ICharacter
{
    private NavMeshAgent agent;
    private Animator animator;
    private CharacterCombat characterCombat;

    private Transform target;
    [SerializeField] private float chaseDistance = 1.0f;
    [SerializeField] private float rotationSlerp = 5f;

    public bool alive { get; private set; } = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        characterCombat = GetComponent<CharacterCombat>();
    }

    private void Update()
    {
        if (!alive) return;
        
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= chaseDistance)
        {
            if(!characterCombat.IsAttacking()) agent.SetDestination(target.position);
            
            if (distance <= agent.stoppingDistance && !characterCombat.IsAttacking())
            {
                characterCombat.Attack();
            }

            FaceTarget();
        }
        else
        {
            agent.ResetPath();
        }
        
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSlerp);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    public void Die()
    {
        alive = false; //Set dead
        animator.SetTrigger("Die"); //Play Die Animation
        GetComponent<Collider>().enabled = false; //Disable Collider to move through
        GameObject nameplate = transform.Find("Nameplate").gameObject; //Find Nameplate to deactivate it
        if(nameplate != null) nameplate.SetActive(false);
    }
}
