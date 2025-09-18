using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;      //기본 TPS 카메라
    public CinemachineFreeLook freeLookcam;             //자유 회전 TPS 카메라
    public bool usingFreeLook = false;
    void Start()
    {
        // 시작은 Virtual Camera 활성화
        VirtualCamera.Priority = 10;
        freeLookcam.Priority = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))    //우클릭
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook)
            {
                freeLookcam.Priority = 20;      // FreeLook 활성화
                VirtualCamera.Priority = 0;
            }
            else
            {
                VirtualCamera.Priority = 20;        // Virtual Camera 활성화
                freeLookcam.Priority = 0;
            }
        }
    }
}
