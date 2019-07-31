using System;
using UnityEngine;


[ExecuteInEditMode]
public class Clouds : MonoBehaviour
{
    [SerializeField] private Shader CloudsShader;
    [SerializeField] private float minHeight = 0.0f;
    [SerializeField] private float maxHeight = 5.0f;
    [SerializeField] private float fadeDist = 2;
    [SerializeField] private float Scale = 5;
    [SerializeField] private float Steps = 50;
    [SerializeField] private Texture ValueNoiseImage;
    [SerializeField] private Transform Sun;

    private Camera _cam;

    public Material Material
    {
        get
        {
            if (_Material == null && CloudsShader != null)
            {
                _Material = new Material(CloudsShader);
            }

            if (_Material != null && CloudsShader == null)
            {
                DestroyImmediate(_Material);
            }

            if (_Material != null && CloudsShader != null && CloudsShader != _Material.shader)
            {
                DestroyImmediate(_Material);
                _Material = new Material(CloudsShader);
            }

            return _Material;
        }
    }

    private Material _Material;

    // Start is called before the first frame update
    void Start()
    {
        if(_Material) DestroyImmediate(_Material);
    }

    Matrix4x4 GetFrustumCorners()
    {
        Matrix4x4 frustumCorners = Matrix4x4.identity;
        Vector3[] fCorners = new Vector3[4];
        
        _cam.CalculateFrustumCorners(new Rect(0,0,1,1), _cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, fCorners);
        
        frustumCorners.SetRow(0, fCorners[1]);
        frustumCorners.SetRow(0, fCorners[2]);
        frustumCorners.SetRow(0, fCorners[3]);
        frustumCorners.SetRow(0, fCorners[0]);

        return frustumCorners;
    }

    [ImageEffectOpaque]
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (_Material == null || ValueNoiseImage == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        if (_cam == null)
        {
            _cam = GetComponent<Camera>();
        }
        
        Material.SetTexture("ValueNoise", ValueNoiseImage);
        if (Sun != null)
        {
            Material.SetVector("_SunDir", -Sun.forward);
        }
        else
        {
            Material.SetVector("_SunDir", Vector3.up);
        }
        
        Material.SetFloat("_MinHeight", minHeight);
        Material.SetFloat("_MaxHeight", maxHeight);
        Material.SetFloat("_FadeDist", fadeDist);
        Material.SetFloat("_Scale", Scale);
        Material.SetFloat("_Step", Steps);
        
        Material.SetMatrix("_FrustumCornersWS", GetFrustumCorners());
        Material.SetMatrix("_CameraInvViewMatrix", _cam.cameraToWorldMatrix);
        Material.SetVector("_CameraPosWS", _cam.transform.position);

        CustomGraphicsBlit(source, destination, Material, 0);
    }
}
