using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float attackRange = 2f;
    float chaseRange = 10f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 4f; 

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position);

        // Плавний розворот у напрямку руху
        Vector3 direction = agent.velocity.normalized;
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * 8f);
        }

        if (distance < attackRange)
            animator.SetBool("isAttacking", true);

        if (distance > chaseRange)
        {
            animator.SetBool("isChasing", false);
        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 2f; // Reset speed when exiting chase state
    }


}
