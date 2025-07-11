﻿using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class PatrolBehaviour : StateMachineBehaviour
{
    float timer;
    List<Transform> points = new List<Transform>();
    NavMeshAgent agent;

    Transform player;
    float chaseRange = 10f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;
        foreach (Transform t in pointsObject)
            points.Add(t);

        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 0.5f; // Додаємо цей рядок для фіксації швидкості патруля
        agent.SetDestination(points[0].position);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(points[Random.Range(0, points.Count)].position);

        timer += Time.deltaTime;
        if (timer > 10)
            animator.SetBool("isPatrolling", false);

        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < chaseRange)
            animator.SetBool("isChasing", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

}
