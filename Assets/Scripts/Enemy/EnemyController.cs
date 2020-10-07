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
    private Vector3 origin;
    [SerializeField] private float chaseDistance = 1.0f;
    [SerializeField] private float rotationSlerp = 5f;

    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float normalSpeed = 5f;
    private float stoppingDistance;
    
    [SerializeField] private float walkPointDistance =  5f;
    private Vector3 walkPoint;
    private bool walkPointSet;

    [SerializeField] private LayerMask groundMask;

    public bool alive { get; private set; } = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        animator = GetComponent<Animator>();
        characterCombat = GetComponent<CharacterCombat>();
        origin = transform.position;
        stoppingDistance = agent.stoppingDistance;
    }

    private void Update()
    {
        if (!alive) return;
        
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= chaseDistance)
        {
            //Set to normal Values
            agent.speed = normalSpeed;
            agent.stoppingDistance = stoppingDistance;
            
            if (!characterCombat.IsAttacking())
            { 
                agent.SetDestination(target.position);
                FaceTarget();
            }
            
            if (distance <= agent.stoppingDistance && !characterCombat.IsAttacking())
            {
                characterCombat.Attack();
            }

        } else
        {
            Patroling();
        }
        
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void Patroling()
    {
        //Set to patrol speed
        agent.speed = patrolSpeed;
        agent.stoppingDistance = 0;
        
        if(!walkPointSet) SearchWalkpoint();
        
        if (walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector3 distance = transform.position - walkPoint;
        
        if (distance.magnitude < 1f)
            walkPointSet = false;
    }

    public void SearchWalkpoint()
    {
        float randomX = Random.Range(-walkPointDistance, walkPointDistance);
        float randomZ = Random.Range(-walkPointDistance, walkPointDistance);
        walkPoint = new Vector3(origin.x + randomX, transform.position.y, origin.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, Mathf.Infinity, groundMask))
        {
            walkPointSet = true;
        }
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
