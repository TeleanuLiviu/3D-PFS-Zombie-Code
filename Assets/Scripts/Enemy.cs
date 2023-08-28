using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }
    public NavMeshAgent  _controller;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _gravity;
    private float horizontal, vertical;
    private Vector3 direction;
    public Player player;
    private Vector3 directionToPlayer;
    [SerializeField]
    private EnemyState currentState = EnemyState.Chase;
    Health health, enemyHealth;
    private float _nextAttack = 3.0f;
    private Animator _anim;



    private void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        health = player.GetComponent<Health>();
        enemyHealth = GetComponentInParent<Health>();
        _controller = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        _gravity = 10f;
        _speed = 5f;

       
    }

    private void Update()
    {
        if(player != null)
        {
            if (!enemyHealth.died && !player._health.died)
            {
                switch (currentState)
                {
                    case EnemyState.Attack:
                        Attack();
                        _anim.SetBool("Run", false);
                        break;

                    case EnemyState.Chase:
                        _anim.SetBool("Attack1", false);
                        _anim.SetBool("Attack2", false);
                        _anim.SetBool("Attack3", false);
                        Movement();
                        break;
                }
            }
        }
       

    }

    public void Attack()
    {
        if (Time.time > _nextAttack)
        {
            _anim.SetBool("Attack1", false);
            _anim.SetBool("Attack2", false);
            _anim.SetBool("Attack3", false);
            if (health != null)
            {

                _anim.SetBool("Attack" + Random.Range(1, 3).ToString(),true);
                health.Damage(Random.Range(5, 10));
            }
                
            _nextAttack = Time.time + Random.Range(1.5f, 3f);

           
        }
    }

   
    private void Movement()
    {
        _anim.SetBool("Run", true);
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Run"))
        {
        
                
                direction = player.transform.position - this.gameObject.transform.position;
                directionToPlayer = player.GetDirection() + direction;
                directionToPlayer.Normalize();
                directionToPlayer.y = 0;
                transform.localRotation = Quaternion.LookRotation(directionToPlayer);
                directionToPlayer = directionToPlayer * _speed;


                directionToPlayer.y -= _gravity * Time.deltaTime;

                _controller.Move(directionToPlayer * Time.deltaTime);
         
        }

    }

   public void StartAttack()
    {
        currentState = EnemyState.Attack;
    }
    public void StopAttack()
    {
        currentState = EnemyState.Chase;
    }


}
