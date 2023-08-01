using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("바퀴를 회전 시키기 위한 힘")] 
    public float wheelPower;
    [Header("바퀴의 회전 각도")] 
    public float wheelRot = 45;
    //rigidbody
    private Rigidbody rigid;
    [Header("휠 콜라이더 배열")] public List<WheelCollider> wheels = new List<WheelCollider>();
    [Header("휠 콜라이더 배열")] public List<WheelCollider> fWheels = new List<WheelCollider>();
    [Header("휠 콜라이더 배열")] public List<WheelCollider> bWheels = new List<WheelCollider>();
    [Header("휠 콜라이더 뒷 바퀴 나팧")]
    float slipRate = 1.0f;
    float handBreakeSlipRate = 0.4f;
    
    
    void Start()
    {   
        rigid = GetComponent<Rigidbody>();
        // 무게 중심을 아래 방향으로 나주기 가능
        
        //휠 콜라이더를 받아옴
        for (int i = 0; i < this.transform.GetChild(0).Find("ColWheel").childCount; i++)
        {
            wheels.Add(this.transform.GetChild(0).Find("ColWheel").GetChild(i).GetComponent<WheelCollider>());
            if (i < 2)
            {
                fWheels.Add(this.transform.GetChild(0).Find("ColWheel").GetChild(i).GetComponent<WheelCollider>());
            }
            else
            {
                bWheels.Add((this.transform.GetChild(0).Find("ColWheel").GetChild(i).GetComponent<WheelCollider>()));
            }
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            for (int i = 0; i < bWheels.Count; i++)
            {
                WheelFrictionCurve fFriction = bWheels[i].forwardFriction;
                fFriction.stiffness = handBreakeSlipRate;
                bWheels[i].forwardFriction = fFriction;

                WheelFrictionCurve sFriction = bWheels[i].sidewaysFriction;
                sFriction.stiffness = handBreakeSlipRate;
                bWheels[i].sidewaysFriction = sFriction;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            for (int i = 0; i < bWheels.Count; i++)
            {
                WheelFrictionCurve fFriction = bWheels[i].forwardFriction;
                fFriction.stiffness = slipRate;
                bWheels[i].forwardFriction = fFriction;

                WheelFrictionCurve sFriction = bWheels[i].sidewaysFriction;
                sFriction.stiffness = slipRate;
                bWheels[i].sidewaysFriction = sFriction;
            }
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < wheels.Count; i++)
        {
            wheels[i].motorTorque = Input.GetAxis("Vertical") * wheelPower;
        }

        for (int i = 0; i < fWheels.Count; i++)
        {
            fWheels[i].steerAngle = Input.GetAxis("Horizontal") * wheelRot;
        }
    }
}
