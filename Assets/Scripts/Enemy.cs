using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 5;       //�� ü��
    public int cureentHealth;       //�� ���� ü��
    public float moveSpeed = 2f;    // �̵� �ӵ�
    private Transform player;       // �÷��̾� ������
    void Start()
    {
        cureentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) 
            return;

        Vector3 playerTargetPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        // �÷��̾������ ���� ���ϱ�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
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
            cureentHealth -=3;
            if (cureentHealth < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
