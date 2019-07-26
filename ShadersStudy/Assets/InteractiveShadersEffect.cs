using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveShadersEffect : MonoBehaviour
{
    [SerializeField] private RenderTexture rt;

    [SerializeField] private Transform target;

    private void Awake()
    {
        Shader.SetGlobalTexture("_GlobalEffectRT", rt);
        Shader.SetGlobalFloat("_OrtographicCamSize", GetComponent<Camera>().orthographicSize);
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        Shader.SetGlobalVector("_Position", transform.position);
    }
}
