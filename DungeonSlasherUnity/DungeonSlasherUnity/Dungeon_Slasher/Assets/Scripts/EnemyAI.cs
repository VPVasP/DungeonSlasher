using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float Speed;
    public Transform target;
    public float minimumDistance;
    public float timeBetweenAttacks;
    private float nextAttackTime;
    [SerializeField]private Animator enemyAnim;
    public float EnemyHealth = 100;
    public bool isAttackingPlayer=false;
    public bool isMoving = false;
    private AudioSource aud;
  [SerializeField]  private AudioClip[] enemyAudios;
    private bool isHurt = false;
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        transform.LookAt(target);
        aud = GetComponent<AudioSource>();
        enemyAnim = GetComponent<Animator>();
    }


    void Update()
    {
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);
            float randomAttackValue = Random.Range(5, 10);

            if (distanceToPlayer > minimumDistance)
            {
                transform.LookAt(target);
                transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
                isMoving = true;
                enemyAnim.SetBool("isAttacking", false);
            }
            else
            {
                isMoving = false;

                if (Time.time >= nextAttackTime&&isHurt==false)
                {
                    aud.clip = enemyAudios[0];
                    aud.Play();
                    isAttackingPlayer = true;
                    enemyAnim.SetBool("isAttacking", true);
                    target.GetComponent<PlayerStats>().LoseHealth(randomAttackValue);
                    Debug.Log("AttackingPlayer");
                    nextAttackTime = Time.time + timeBetweenAttacks;

                }
            }
        }
    
    public void LoseHealth(float damage)
    {
      
        EnemyHealth -= damage;
        enemyAnim.SetTrigger("Hurt");
        Debug.Log("Enemy Attacked");
        isAttackingPlayer = false;
        isHurt = true;
        if (EnemyHealth <= 0)
        {
            aud.clip = enemyAudios[1];
            aud.Play();
            enemyAnim.SetTrigger("Dead");
            Speed = 0;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Destroy(this.gameObject, 2f);
            Debug.Log("EnemyDied" + this.gameObject.name);
            isMoving = false;
            isAttackingPlayer = false;
            GameManager.instance.UpdateEnemies();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, minimumDistance);
    }
}