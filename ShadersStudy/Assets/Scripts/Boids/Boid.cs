using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{

    BoidsSettings settings;

    //state
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    Vector3 acceleration;
    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidDst;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numFlockmatesSeen;

    Material mat;
    Transform cachedtransform;
    Transform target;

    private void Awake()
    {
        mat = transform.GetComponentInChildren<MeshRenderer>().material;
        cachedtransform = transform; //cache initial transform
    }

    //initialise boid settings and speed
    public void Init(BoidsSettings set, Transform target)
    {
        this.target = target;
        this.settings = set;

        position = cachedtransform.position;
        forward = cachedtransform.forward;

        float startSpeed = (set.minSpeed + set.maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
    }

    public void SetColour(Color col)
    {
        if (mat != null)
            mat.color = col;
    }

    public void UpdateBoid()
    {
        Vector3 acceleration = Vector3.zero;

        if (target != null)
        {
            Vector3 offsetToTarget = (target.position - position);
            acceleration = SteerTowards(offsetToTarget) * settings.targetWeight;
        }
    }

    Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, settings.maxSteerForce);
    }
}
