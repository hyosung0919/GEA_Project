using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public enum Enemystate { Idle, Trace, Attack, RunAway }
    public Enemystate state = Enemystate.Idle;
    public int maxHealth = 5;       //�� ü��
    public int cureentHealth;       //�� ���� ü��
    public float moveSpeed = 2f;    // �̵� �ӵ�
    public float traceRange = 15f;  //���� ���� �Ÿ�
    public float attackRange = 6f;  //���� ���� �Ÿ�
    public float attackCooldown = 1.5f;
    public GameObject projectilePrefab;         //����ü ������
    public Transform firepoint;                 //�߻� ��ġ
    public Transform player;       // �÷��̾� ������
    private float lastAttackTime;
    bool t = false;


    public Slider hpSlider;

    void Start()
    {
        cureentHealth = maxHealth;
        lastAttackTime = -attackCooldown;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        hpSlider.value = 1f;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 playerTargetPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        float dist = Vector3.Distance(player.position, transform.position);

        if(cureentHealth <= maxHealth * 0.2f && !t)
        {
            state = Enemystate.RunAway;
        }
        

        switch (state)
        {
            case Enemystate.Idle:
                if (dist < traceRange)
                    state = Enemystate.Trace;
                break;
            case Enemystate.Trace:
                if (dist < attackRange)
                    state = Enemystate.Attack;
                else if (dist > traceRange)
                    state = Enemystate.Idle;
                else
                    TracePlayer();
                break;
            case Enemystate.Attack:
                if (dist > attackRange)
                    state = Enemystate.Trace;
                else
                    AttackPlayer();
                break;
            case Enemystate.RunAway:
                    RunAwayPlayer();
                break;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet1"))
        {
            cureentHealth--;
            if (cureentHealth < 0)
            {
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Bullet2"))
        {
            cureentHealth -= 3;
            if (cureentHealth < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        cureentHealth -= damage;
        hpSlider.value = (float)cureentHealth / maxHealth;

        if (cureentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void TracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void AttackPlayer()
    {
        // ���� ��ٿ�� �߻�
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firepoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefab, firepoint.position, firepoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if (ep != null)
            {
                Vector3 dir = (player.position - firepoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }
    void RunAwayPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position -= dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
        Invoke("popop", 1.5f);

    }

    private void popop()
    {
        t = true;
        state = Enemystate.Idle;

    }
}
