using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float shakeTime = 2.0f;
    public float fps = 20.0f;
    public float frameTime = 0.3f;
    public float shakeDelta = 0.015f;
    public Camera cam;
    public  bool isshakeCamera = false;
    private void Awake()
    {
    }

    void Update()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    isshakeCamera = false;
                    shakeTime = 2.0f;
                    fps = 20.0f;
                    frameTime = 0.03f;
                    shakeDelta = 0.015f;
                }
                else
                {
                    frameTime += Time.deltaTime;

                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        cam.rect = new Rect(shakeDelta * (-1.0f + 2.0f * Random.value), shakeDelta * (-1.0f + 2.0f * Random.value), 1.0f, 1.0f);
                    }
                }
            }
        }
    }

    public  void shakeCamera()
    {
            isshakeCamera = true;

    }
}