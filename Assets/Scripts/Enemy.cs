using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 5;       //적 체력
    public int cureentHealth;       //적 현재 체력
    public float moveSpeed = 2f;    // 이동 속도
    private Transform player;       // 플레이어 추적용
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

        // 플레이어까지의 방향 구하기
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
