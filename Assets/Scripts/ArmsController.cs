using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsController : MonoBehaviour
{
    public float ArmScale => armScale;
    
    [SerializeField] private float bodyRadius = .5f;
    [SerializeField] private float armRadius = .5f;
    [SerializeField] private float armsOffset = .15f;
    [SerializeField] private float armScale = .25f;
    [SerializeField] private Transform body = null;
    [SerializeField] private Transform leftArm = null;
    [SerializeField] private Transform rightArm = null;

    private void Update()
    {
        HandleArmsScale();

        float totalDistance = bodyRadius + armRadius + armsOffset;

        Vector3 localPos = leftArm.localPosition;
        localPos.y = totalDistance;

        leftArm.localPosition = localPos;
        rightArm.localPosition = localPos;
    }

    public void SetArmsScale(float value)
    {
        armScale = value;
    }

    private void HandleArmsScale()
    {
        var bodyLocalScale = body.localScale;
        Vector3 newArmScale = bodyLocalScale;

        armRadius = leftArm.localScale.x;
        
        newArmScale.x = 1 / bodyLocalScale.x * armScale;
        newArmScale.y = 1 / bodyLocalScale.y * armScale;

        leftArm.localScale = newArmScale;
        rightArm.localScale = newArmScale;
    }
}
