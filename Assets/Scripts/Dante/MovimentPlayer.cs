using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class MovimentPlayer : MonoBehaviour
{
    public float velocity;
    public float vida = 100;
    private Animator animator;
    private Rigidbody rb;
    public Camera mycam;
    public float tempo;
    bool boolpistol = true;
    public int trocaArma = 1;
    bool boolrifle = false, SemTiro = false;
    NavMeshAgent dante;
    public GameObject pistolArm;
    public GameObject riflelArm;
    private GameObject pointFirePistol;
    private GameObject pointFireRifle1;
    private GameObject pointFirerifle2;
    bool deathDante = false;
    
   
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        dante = GetComponent<NavMeshAgent>();

        dante.speed = 5f;
        dante.acceleration = 8f;
        dante.angularSpeed = 120f;
        dante.stoppingDistance = 0.5f;

        pistolArm.SetActive(false);
        riflelArm.SetActive(false);

        pointFirePistol = GameObject.Find("pointFirePistol");
        pointFireRifle1 = GameObject.Find("pointFireRifle1");
        pointFirerifle2 = GameObject.Find("pointFireRifle2");
    }

    private void Update()
    {
        if (!deathDante)
        {
            trocaArmas();

            marcaPontoNaTela();

            move();

            tempoTiro();
        }
        else
        {
            GameOverOn();
        }
    }


    private void marcaPontoNaTela()
    {
        if (Input.GetMouseButton(0) & !Input.GetKey(KeyCode.LeftShift))
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
    }
    private void tempoTiro()
    {
        tempo -= Time.deltaTime;
        if (tempo <= 0)
        {
            SemTiro = false;
        }

        if (vida <= 0)
        {
            DeathPlayer();
        }

    }

    private void trocaArmas()
    {
        if (Input.GetMouseButton(1))
            {
                trocaArma *= -1;
            }
        
            if (trocaArma > 0)
            {
                boolpistol = true;
                boolrifle = false;
                pistolArm.SetActive(true);
                riflelArm.SetActive(false);
            }
            else
            {
                boolpistol = false;
                boolrifle = true;
                pistolArm.SetActive(false);
                riflelArm.SetActive(true);
            }
    }

    public void VidaDante(float damage)
    {
        vida -= damage;
    }

    public bool GetboolPistol()
    {
        return boolpistol;
    }

    public bool GetboolRifle()
    {
        return boolrifle;
    }

    public float GetVida()
    {
        return vida;
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
                RotateTowardsMouse();
                dante.speed = 10f;
                if (Input.GetMouseButton(0) & Input.GetKey(KeyCode.LeftShift))
                {
                    if (boolpistol)
                    {
                        RunningPistolPlayer();
                        ChamaTiroPistol();
                    }
                    else if (boolrifle)
                    {
                        RunningRiflePlayer();
                        ChamaTiroRifle01();
                    }
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (boolpistol)
                    {
                        RunningPistolPlayer();
                        
                    }
                    else if (boolrifle)
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
            
            if (Input.GetMouseButton(0) & Input.GetKey(KeyCode.LeftShift))
            {
                RotateTowardsMouse();
                dante.speed = 5f;
                if (boolpistol)
                {
                    WalkPistolPlayer();
                    ChamaTiroPistol();
                }
                else if (boolrifle)
                {
                    WalkRiflePlayer();
                    ChamaTiroRifle01();
                }
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                if (boolpistol)
                {
                    WalkPistolPlayer();
                    
                }
                else if (boolrifle)
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
            if (Input.GetMouseButton(0) & Input.GetKey(KeyCode.LeftShift))
            {
                RotateTowardsMouse();
                if (boolpistol)
                {
                    AttackPlayerPistol();
                    ChamaTiroPistol();
                }
                else if (boolrifle)
                {
                    AttackPlayerRifle();
                    ChamaTiroRifle01();
                }
            }
            else if (Input.GetKey(KeyCode.LeftShift))
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
    
    private void ChamaTiroPistol()
    {


        if (!SemTiro)
        {
            AtirarInstantiate PistolInstantiateScript = pistolArm.gameObject.GetComponentInChildren<AtirarInstantiate>();
            PistolInstantiateScript.InstantiateBala();
            tempo = 0.375f;
            SemTiro = true;
        }
    }

    private void ChamaTiroRifle01()
    {
        if (!SemTiro)
        {
            AtirarInstantiate RifleInstantiateScript = riflelArm.gameObject.GetComponentInChildren<AtirarInstantiate>();
            {
                RifleInstantiateScript.InstantiateBala();
                tempo = 0.1f;
                SemTiro = true;
            }
        }
    }

    private void ChamaTiroRifle02()
    {
        if (!SemTiro)
        {
            AtirarInstantiate RifleInstantiateScript = riflelArm.gameObject.GetComponentInChildren<AtirarInstantiate>();
            {
                RifleInstantiateScript.InstantiateBala();
                tempo = 0.375f;
                SemTiro = true;
            }
        }

    }

   
    private void movePlayer()
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);

    }

    private void WalkPistolPlayer()
    {
        animator.SetFloat("X", 1);
        animator.SetFloat("Y", 1);
    }

    private void WalkRiflePlayer()
    {
        animator.SetFloat("X", -1);
        animator.SetFloat("Y", -1);
    }

    private void AttackPlayerPistol()
    {
        animator.SetFloat("X", 1);
        animator.SetFloat("Y", 0);
    }

    private void AttackPlayerRifle()
    {
        animator.SetFloat("X", -1);
        animator.SetFloat("Y", 0);
    }

    private void GameOverOn()
    {
        GameOverScript gameover = GameObject.Find("GameOver").GetComponent<GameOverScript>();

        gameover.ShowTelaGameOver(true);

        Invoke("ReloadScene", 3f);
    }

    private void DeathPlayer()
    {
        /*barraVida.SetActive(false);*/
        animator.SetBool("die", true);
        deathDante = true;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void IdlePlayer()
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 0);
    }
    private void RunningPlayer()
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 1);
    }

    private void RunningPistolPlayer()
    {
        animator.SetFloat("X", -0.5f);
        animator.SetFloat("Y", 0);
    }

    private void RunningRiflePlayer()
    {
        animator.SetFloat("X", -1);
        animator.SetFloat("Y", 1);
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

    private void RotateTowardsMouse()
    {
        Ray ray = mycam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0; 
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
