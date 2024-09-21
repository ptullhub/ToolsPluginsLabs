using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera mCam;
    private CinemachineBasicMultiChannelPerlin perlin;
    private float shakeTimer = 0;
    private void Awake()
    {
        mCam = GetComponent<CinemachineVirtualCamera>();
        perlin = mCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();   
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                perlin.m_AmplitudeGain = 0f;
            }
        }
    }
    public void ShakeCamera(float intensity, float time)
    {
        perlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }


}
