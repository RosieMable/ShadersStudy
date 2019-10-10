using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidsSettings : ScriptableObject
{
    [Header("Boid Settings")]
    public float minSpeed = 2;
    public float maxSpeed = 5;
    public float sightRadius = 2.5f;
    public float avoidRadius = 1.5f;
    public float maxSteerForce = 3;

    [Header("Boid Weight")]
    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float seperateWeight = 1;

    public float targetWeight = 1;

    [Header("Collision")]
    public LayerMask obstacleLayer;
    public float boundsRadius = .35f;
    public float avoidCollisionWeight = 10;
    public float collisionAvoidDst = 5;
}
