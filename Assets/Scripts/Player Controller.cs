using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.81f;

    public CinemachineSwitcher Switcher;
    public CinemachineVirtualCamera virtualCamera;
    public float rotationSpeed = 10f;
    private CinemachinePOV pov;
    private CharacterController controller;
    private Vector3 velocity;
    public bool isGrounded;

    public int maxHP = 100;
    private int currentHP;

    public Slider hpSlider;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        // Virtual Camera�� POV ������Ʈ ��������

        currentHP = maxHP;
        hpSlider.value = 1f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pov.m_HorizontalAxis.Value = transform.eulerAngles.y;
            pov.m_VerticalAxis.Value = 0f;
        }

        //���� ��� �ִ��� Ȯ��
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;    //���鿡 ���̱�
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //ī�޶� ���� ���� ���
        Vector3 camForward = virtualCamera.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = virtualCamera.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized; // �̵����� = ī�޶� forward/right ���
        if(!Switcher.usingFreeLook)
        controller.Move(move * speed * Time.deltaTime);

        float cameraYaw = pov.m_HorizontalAxis.Value;   //���콺 �¿� ȸ����
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);


        //����
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpPower;
        }

        // �߷� ����
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
            virtualCamera.m_Lens.FieldOfView = 80f;
        }
        else
        {
            speed = 5f;
            virtualCamera.m_Lens.FieldOfView = 60f;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpSlider.value = (float)currentHP / maxHP;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
