using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;


public class MovimentPlayer : MonoBehaviour
{
    public float velocity;
    private Animator animator;
    private Rigidbody rb;
    private int walkPlayer = Animator.StringToHash("walking");
    private int death = Animator.StringToHash("death");
    private int runplyer1 = Animator.StringToHash("running");
    private int pistol = Animator.StringToHash("pistol");
    private int rifle = Animator.StringToHash("rifle");
    private int walkpistol = Animator.StringToHash("walkpistol");
    private int walkrifle = Animator.StringToHash("walkrifle");
    private int runpistol = Animator.StringToHash("runpistol");
    private int runrifle = Animator.StringToHash("runrifle");
    public Camera mycam;
    bool boolpistol = true;
    int trocaArma = 1;
    bool boolrifle = false;
    NavMeshAgent dante;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        dante = GetComponent<NavMeshAgent>();

        dante.speed = 5f;
        dante.acceleration = 8f;
        dante.angularSpeed = 120f;
        dante.stoppingDistance = 0.5f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            trocaArma *= -1;
        }
        
        if (trocaArma > 0)
        {
            boolpistol = true;
            boolrifle = false;
        }
        else
        {
            boolpistol = false;
            boolrifle = true;
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = mycam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
              if (Physics.Raycast(ray, out hit))
                {
                    dante.SetDestination(hit.point);
                }
            } 
        }
      
        move();
       
    }

    private void AttackEnemy(GameObject enemy, int damage)
    {
             
        SpiderControlller enemyHealth = enemy.GetComponent<SpiderControlller>();

        if (enemyHealth != null)
        {
            enemyHealth.VidaSpider(damage); 
        }
    }

    private void move()
    {
        if (dante.remainingDistance > dante.stoppingDistance+0.2f)
        {
            if (Input.GetKey(KeyCode.W))
            {
                dante.speed = 10f;
                if (Input.GetMouseButton(1))
                {
                    if (boolpistol)
                    {
                        RunningPistolPlayer();
                    }
                    else
                    {
                        RunningRiflePlayer();
                    }
                }
                else
                {
                    RunningPlayer();
                }

            }
            else 
            
            if (Input.GetMouseButton(1))
            {
                dante.speed = 5f;
                if (boolpistol)
                {
                    WalkPistolPlayer();
                }
                else 
                {
                    WalkRiflePlayer();
                }
            }
            else
            {
                dante.speed = 5f;
                movePlayer();
            }
        }
        else
        {
            if (Input.GetMouseButton(1))
            {
                if (boolpistol)
                {
                    AttackPlayerPistol();
                }
                else if (boolrifle)
                {
                    AttackPlayerRifle();
                }
            }
            else
            {
                IdlePlayer();
            }
        }
    }

    private void movePlayer()
    {
        animator.SetBool(walkrifle, false);
        animator.SetBool(runpistol, false);
        animator.SetBool(runrifle, false);
        animator.SetBool(walkpistol, false);
        animator.SetBool(walkPlayer, true);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
        
    }

    private void WalkPistolPlayer()
    {
        animator.SetBool(runplyer1, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(walkrifle, false);
        animator.SetBool(runpistol, false);
        animator.SetBool(runrifle, false);
        animator.SetBool(walkpistol, true);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }

    private void WalkRiflePlayer()
    {
        animator.SetBool(runplyer1, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(walkrifle, true);
        animator.SetBool(runpistol, false);
        animator.SetBool(runrifle, false);
        animator.SetBool(walkpistol, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }

    private void AttackPlayerPistol()
    {
        animator.SetBool(walkrifle, false);
        animator.SetBool(runpistol, false);
        animator.SetBool(runrifle, false);
        animator.SetBool(walkpistol, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, true);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }

    private void AttackPlayerRifle()
    {
        animator.SetBool(walkrifle, false);
        animator.SetBool(runpistol, false);
        animator.SetBool(runrifle, false);
        animator.SetBool(walkpistol, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, true);
        animator.SetBool(death, false);
    }

    private void DeathPlayer()
    {
        /*barraVida.SetActive(false);*/
        animator.SetBool(walkrifle, false);
        animator.SetBool(runpistol, false);
        animator.SetBool(runrifle, false);
        animator.SetBool(walkpistol, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, true);/*
       
        bool resp = gameover.ShowTelaGameOver(true);
        
        Invoke("ReloadScene", 3f);*/

    }

    private void ReloadScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void IdlePlayer()
    {
        animator.SetBool(walkrifle, false);
        animator.SetBool(runpistol, false);
        animator.SetBool(runrifle, false);
        animator.SetBool(walkpistol, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(runplyer1, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }
    private void RunningPlayer()
    {
        animator.SetBool(runplyer1, true);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(walkrifle, false);
        animator.SetBool(runpistol, false);
        animator.SetBool(runrifle, false);
        animator.SetBool(walkpistol, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }

    private void RunningPistolPlayer()
    {
        animator.SetBool(runplyer1, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(walkrifle, false);
        animator.SetBool(runpistol, true);
        animator.SetBool(runrifle, false);
        animator.SetBool(walkpistol, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }

    private void RunningRiflePlayer()
    {
        animator.SetBool(runplyer1, false);
        animator.SetBool(walkPlayer, false);
        animator.SetBool(walkrifle, false);
        animator.SetBool(runpistol, false);
        animator.SetBool(runrifle, true);
        animator.SetBool(walkpistol, false);
        animator.SetBool(pistol, false);
        animator.SetBool(rifle, false);
        animator.SetBool(death, false);
    }
    private void OnDrawGizmos()
    {
        if (this)
        {
            Gizmos.color = Color.red;
            Vector3 Position = transform.position;
            Gizmos.DrawWireSphere(Position, 15);

        }
    }
}
