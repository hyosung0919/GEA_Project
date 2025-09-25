using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;      //�⺻ TPS ī�޶�
    public CinemachineFreeLook freeLookcam;             //���� ȸ�� TPS ī�޶�
    public bool usingFreeLook = false;
    void Start()
    {
        // ������ Virtual Camera Ȱ��ȭ
        VirtualCamera.Priority = 10;
        freeLookcam.Priority = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))    //��Ŭ��
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook)
            {
                freeLookcam.Priority = 20;      // FreeLook Ȱ��ȭ
                VirtualCamera.Priority = 0;
            }
            else
            {
                VirtualCamera.Priority = 20;        // Virtual Camera Ȱ��ȭ
                freeLookcam.Priority = 0;
            }
        }
    }
}
