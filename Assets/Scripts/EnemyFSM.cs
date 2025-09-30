using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public Transform Player;      
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackRange = 1.5f;
    public float chaseRange = 5f;

    private enum State { Patrol, Chase, Attack }
    private State currentState = State.Patrol;

    private Vector3 patrolPoint;

    void Start()
    {
        patrolPoint = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                if (distance < chaseRange)
                    currentState = State.Chase;
                break;

            case State.Chase:
                Chase();
                if (distance < attackRange)
                    currentState = State.Attack;
                else if (distance >= chaseRange)
                    currentState = State.Patrol;
                break;

            case State.Attack:
                Attack();
                if (distance >= attackRange)
                    currentState = State.Chase;
                break;
        }
    }

    void Patrol()
    {
        transform.position = patrolPoint + new Vector3(Mathf.PingPong(Time.time * patrolSpeed, 3), 0, 0);
    }

    void Chase()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            Player.position,
            chaseSpeed * Time.deltaTime
        );
    }

    void Attack()
    {
        Debug.Log("Атака игрока!");
    }
}