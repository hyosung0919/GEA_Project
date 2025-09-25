using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;     // Projectile ������
    public GameObject projectilePrefab2;
    public Transform firePoint;             // �߻� ��ġ (�ⱸ)

    public bool isShooting = false;

    Camera Cam;
    void Start()
    {
        Cam = Camera.main;  // ���� ī�޶� ��������
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // ��Ŭ�� �߻�
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ChangeWeapon();
        }
    }

    void Shoot()
    {
        // ȭ�鿡�� ���콺 + ����(Ray) ���
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction =  (targetPoint - firePoint.position).normalized; // ���� ����

        // Projectile ����

        if (isShooting)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        }
        else
        {
            GameObject proj = Instantiate(projectilePrefab2, firePoint.position, Quaternion.LookRotation(direction));

        }
    }
    void ChangeWeapon()
    {
        isShooting = !isShooting;
    }
}
