using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

//creates a 3Dimentional grid that produces different directions
public class NoiseFlowfield : MonoBehaviour
{
    
    private FastNoise _fastNoise;
    public Vector3Int _gridSize;
    public float _cellSize;
    public float _increment;

    public Vector3 _offset, _offsetSpeed;

    public Vector3[,,] _flowFieldDirections;
    
    
    //particles
    public GameObject particlePrefab;
    public int amountParticles;
    public List<flowFieldParticle> _particles;
    public float _particleScale;
    public float _spawnRadius;

    private bool _particleSpawnValidation(Vector3 position)
    {
        bool valid = true;
        
        foreach (var particle in _particles)
        {
            if (Vector3.Distance(position, particle.transform.position) < _spawnRadius)
            {
                valid = false;
                break;
            }
        }

        if (valid)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        _flowFieldDirections = new Vector3[_gridSize.x, _gridSize.y, _gridSize.z];
        _fastNoise = new FastNoise();
        
        _particles = new List<flowFieldParticle>();

        for (int i = 0; i < amountParticles; i++)
        {
            Vector3 randomPos = new Vector3(UnityEngine.Random.Range(this.transform.position.x, this.transform.position.x + _gridSize.x * _cellSize), 
                UnityEngine.Random.Range(this.transform.position.y, this.transform.position.y + _gridSize.y * _cellSize),
                UnityEngine.Random.Range(this.transform.position.z, this.transform.position.z + _gridSize.z * _cellSize));

            bool isValid = _particleSpawnValidation(randomPos);

            if (isValid)
            {
                GameObject particleInstance = Instantiate(particlePrefab);
                particleInstance.transform.position = randomPos;
                particleInstance.transform.parent = this.transform;
                particleInstance.transform.localScale = new Vector3(_particleScale, _particleScale, _particleScale);
            
                _particles.Add(particleInstance.GetComponent<flowFieldParticle>());
            }
            
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateFlowFieldDirection();
    }

    void CalculateFlowFieldDirection()
    {
        _fastNoise = new FastNoise();
        
        float xOff = 0;

        for (int x = 0; x < _gridSize.x; x++)
        {
            float yOff = 0;
            
            for (int y = 0; y < _gridSize.y; y++)
            {
                float zOff = 0;
                for (int z = 0; z < _gridSize.z; z++)
                {
                    float noise = _fastNoise.GetSimplex(xOff + _offset.x, yOff + _offset.y, zOff + _offset.z) + 1;
                    Vector3 noiseDirection = new Vector3(Mathf.Cos(noise * Mathf.PI), Mathf.Sin(noise * Mathf.PI), Mathf.Cos(noise * Mathf.PI));
                    _flowFieldDirections[x, y, z] = Vector3.Normalize(noiseDirection);
                    zOff += _increment;
                }

                yOff += _increment;
            }

            xOff += _increment;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(this.transform.position + new Vector3((_gridSize.x * _cellSize) *0.5f,(_gridSize.y * _cellSize) *0.5f,(_gridSize.z * _cellSize) *0.5f),
            new Vector3 (_gridSize.x * _cellSize, _gridSize.y * _cellSize, _gridSize.z *_cellSize));
    }
}
