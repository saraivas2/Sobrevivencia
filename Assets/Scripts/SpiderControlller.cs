using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderControlller : MonoBehaviour
{
    private NavMeshAgent spider;
    private GameObject player;
    public float distance;
    private Animator animator;
    public int vida;
    public int danoPistol;
    public int danoRifle;
    public float tempWait;
    private WaitForSeconds temp;
    public Transform[] pointWait;
    private int index;
    public bool spidermove = false;

    
    // Start is called before the first frame update
    void Start()
    {
        temp = new WaitForSeconds(tempWait);
        spider = GetComponent<NavMeshAgent>();
        index = Random.Range(0, pointWait.Length);
        StartCoroutine(CallVisit());
        player = GameObject.Find("Breathing Idle");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (spider.velocity.magnitude>0.02f)
        {
            spidermove = true;
        }
        else
        {
            spidermove = false;
        }

        AnimeMoveSpider();

        Hunting();

        if (vida <= 0)
        {
            deathSpider();
        }
    }

    private void Hunting()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distance)
        {
            movimentSpider();
        }
    }
    IEnumerator CallVisit()
    {
        while (true)
        {
            yield return temp;
            patrol();
        }
    }

    private void patrol()
    {
        index = index == pointWait.Length - 1 ? 0 : index + 1;
        spider.destination = pointWait[index].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dante"))
        {
            int val = Random.Range(0, 2);
            if (val == 0)
            {
                animator.SetBool("attack1",true);
            }
            else
            {
                animator.SetBool("attack2",true);
            }
        }
        else
        {
            animator.SetBool("attack1", false);
            animator.SetBool("attack2", false);
        }

        if (other.CompareTag("damage1"))
        {
            VidaSpider(danoPistol);

        }

        if (other.CompareTag("damage2"))
        {
            VidaSpider(danoRifle);
            animator.SetBool("damage", true);
        }
        else
        {
            animator.SetBool("damage", false);
        }
    }


    private void movimentSpider()
    {
        spider.SetDestination(player.transform.position);
    }
    
    private void AnimeMoveSpider()
    {
        animator.SetBool("walk", spidermove);
    }
    
    public void VidaSpider(int damage)
    {
        animator.SetBool("damage",true);
        vida -= damage; 
    }

    private void deathSpider()
    {
        animator.SetBool("death", true);
        Destroy(this);
    }

    private void damageSpider()
    {
        animator.SetBool("damage", true);
    }
}
