using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WolfController : MonoBehaviour
{
    private NavMeshAgent wolf;
    private GameObject player;
    public float distance;
    private Animator animator;
    public float vida;
    public float tempWait;
    private WaitForSeconds temp;
    public Transform[] pointWait;
    private int index;
    public bool wolfmove = false;
    public GameObject light;
    public bool attackBool = false, attackTemp = true;
    private MovimentPlayer scriptPlayer;
    private float timerAttack = 1.5f;
    public float timerStandard;
    public LayerMask Dante;
    public Transform posAttack;
    public float vidaFull;
    bool death = false;
    float timer = 15;
    public float velocity;
    int count=0;


    // Start is called before the first frame update
    void Start()
    {
        temp = new WaitForSeconds(tempWait);
        wolf = GetComponent<NavMeshAgent>();
        index = Random.Range(0, pointWait.Length);
        StartCoroutine(CallVisit());
        player = GameObject.Find("Breathing Idle");
        animator = GetComponent<Animator>();
        scriptPlayer = player.GetComponent<MovimentPlayer>();
        vidaFull = vida;
        StopCoroutine(ResetarAtaque());
    }

    void Update()
    {
        if (!death)
        {
            if (wolf.remainingDistance > wolf.stoppingDistance + 0.3f)
            {
                wolfmove = true;
            }
            else
            {
                wolfmove = false;
            }

            AttackDante();

            Hunting();
            velocity = wolf.velocity.magnitude;
            if (velocity > 0.2f)
            {
                AnimeMovewolf();
            }
            else
            {
                if (!attackBool)
                {
                    idleWolf();
                }
            }
            


            if (vida <= 0)
            {
                deathWolf();
            }
        }
        else
        {
            deathWolf();
            TimerDestroy();

        }

        if (scriptPlayer.GetVida() <= 0)
        {
            attackBool = false;
            eatingWolf();

        }
    }


    IEnumerator CallVisit()
    {
        if (!death)
        {
            while (true)
            {
                yield return temp;
                patrol();
            }
        }
    }


    public float GetVida()
    {
        return vida;
    }

    public float GetVidaFull()
    {
        return vidaFull;
    }

    private void patrol()
    {
        index = index == pointWait.Length - 1 ? 0 : index + 1;
        wolf.destination = pointWait[index].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dante"))
        {
            attackBool = true;
        }

        if (other.gameObject.CompareTag("damagePistol"))
        {
            Vidawolf(other.GetComponent<balas>().Damage());
            
        }

        if (other.gameObject.CompareTag("damageRifle1"))
        {
            Vidawolf(other.GetComponent<balas>().Damage());
            
        }

        if (other.gameObject.CompareTag("damageRifle2"))
        {
            Vidawolf(other.GetComponent<balas>().Damage());
            
        }
    }


    private void Hunting()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            if (count < 1)
            {
                howlWolf();
                count++;
            }
            if (!attackBool)
            {
                movimentwolf();
            }
            light.SetActive(true);
        }
        else
        {
            light.SetActive(false);
            count = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Dante"))
        {
            attackBool = false;
            StopCoroutine(ResetarAtaque());
        }
    }


    private void TimerDestroy()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //Destroy(this.wolfGameobject);
        }
    }
    private void AttackDante()
    {
        if (!attackBool) return;

        wolfmove = false;

        int val = Random.Range(0, 2);
        animator.SetBool("attack1", val == 0);
        animator.SetBool("attack2", val == 1);


        DamageDante();

    }


    private void DamageDante()
    {

        if (!attackTemp) return;

        float radius = 1.0f;
        Collider[] isAttack = Physics.OverlapSphere(posAttack.position, radius);

        foreach (Collider col in isAttack)
        {
            if (col.gameObject.CompareTag("Dante"))
            {
                col.GetComponent<MovimentPlayer>().VidaDante(10);
                StartCoroutine(ResetarAtaque());
                break;
            }
        }
    }

    private IEnumerator ResetarAtaque()
    {
        attackTemp = false;
        yield return new WaitForSeconds(timerAttack);
        attackTemp = true;
    }

    private void movimentwolf()
    {
        wolf.SetDestination(player.transform.position);
        wolf.speed = 5f;
    }

    private void AnimeMovewolf()
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 1);
    }

    public void Vidawolf(int damage)
    {
        vida -= damage;
    }

    private void deathWolf()
    {
        animator.SetFloat("X", 1);
        animator.SetFloat("Y", -1);
        death = true;
        light.SetActive(false);
    }

    private void runWolf()
    {
        animator.SetFloat("X", -1);
        animator.SetFloat("Y", 1);
    }

    private void howlWolf()
    {
        animator.SetBool("howl", true);
        animator.SetBool("howl", false);
    }

    private void eatingWolf()
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
    }

    private void idleWolf()
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 0);
    }
}