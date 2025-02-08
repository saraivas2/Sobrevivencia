using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    
    private NavMeshAgent agent;
    private Transform target;
    private float speed;
    public Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speed = agent.speed;
        target = pointA; // Começa indo para o ponto A
        MoveToTarget();
    }

    void Update()
    {
        // Se o inimigo chegou ao destino, troca para o outro ponto
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            target = (target == pointA) ? pointB : pointA;
            MoveToTarget();
        }

        if (speed > 0) {
            anim.SetBool("walk", true);
        }
    }

    void MoveToTarget()
    {
        agent.SetDestination(target.position);
    }
}
