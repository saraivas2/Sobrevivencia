using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpiderControlller : MonoBehaviour
{
    private NavMeshAgent spider;
    private GameObject player;
    public float distance;
    private Animator animator;
    public float vida;
    public float tempWait;
    private WaitForSeconds temp;
    public Transform[] pointWait;
    private int index;
    public bool spidermove = false;
    public GameObject light;
    public bool attackBool=false, attackTemp = true;
    private MovimentPlayer scriptPlayer;
    private float timerAttack = 1.5f;
    public float timerStandard;
    public LayerMask Dante;
    public Transform posAttack;
    public float vidaFull;
    bool death = false;
    float timer = 15;
    public GameObject spiderGameobject;


    // Start is called before the first frame update
    void Start()
    {
        temp = new WaitForSeconds(tempWait);
        spider = GetComponent<NavMeshAgent>();
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
            if (spider.remainingDistance > spider.stoppingDistance + 0.3f)
            {
                spidermove = true;
            }
            else
            {
                spidermove = false;
            }


            AttackDante();

            Hunting();

            if (spider.velocity.magnitude > 0.2f)
            {
                AnimeMoveSpider();
            }
            else
            {
                if (!attackBool)
                {
                    idleSpider();
                }
                
            }

            if (vida <= 0)
            {
                deathSpider();
            }
        }
        else
        {
            deathSpider();
            TimerDestroy();

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
        spider.destination = pointWait[index].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dante"))
        {
            attackBool = true;
        }

        if (other.gameObject.CompareTag("damagePistol"))
        {
            VidaSpider(other.GetComponent<balas>().Damage());
            damageSpider();
        }

        if (other.gameObject.CompareTag("damageRifle1"))
        {
            VidaSpider(other.GetComponent<balas>().Damage());           
            damageSpider();
        }

        if (other.gameObject.CompareTag("damageRifle2"))
        {
            VidaSpider(other.GetComponent<balas>().Damage());
            damageSpider();
        }
    }


    private void Hunting()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            if (!attackBool)
            {
                movimentSpider();
            }
            
            light.SetActive(true);
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
            Destroy(spiderGameobject);
        }
    }
    private void AttackDante()
    {
        if (!attackBool) return;
        
           spidermove = false;

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

    private void movimentSpider()
    {
        spider.SetDestination(player.transform.position);
    }
    
    private void AnimeMoveSpider()
    {
        animator.SetFloat("X", 1);
        animator.SetFloat("Y", 0);
    }
    
    public void VidaSpider(int damage)
    {
        vida -= damage; 
    }

    private void deathSpider()
    {
        animator.SetFloat("X", 1);
        animator.SetFloat("Y", 1);
        spider.enabled = false;
        death = true;
    }

    private void damageSpider()
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", -1);
    }

    private void idleSpider()
    {
        animator.SetFloat("X", 0);
        animator.SetFloat("Y", 0);
    }
}
