using UnityEngine;

public class MeshDeformerInput : MonoBehaviour
{
    
    //It makes most sense to attach this component to the camera, as that represents the user's point of view. We shouldn't attach it to the deforming mesh object, because there could be multiple of those in the scene.


    public float force = 10f;

    public float forceOffset = 0.1f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        //Now we have to figure out where the user is pointing. We do this by casting a ray from the camera into the scene.
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
        {
            //We use the physics engine to cast the ray and store information about what it hit. If the ray collided with something, we can retrieve the MeshDeformer component from the object that was hit.
            MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer>();

            if (deformer)
            {
                Vector3 point = hit.point;
                point += hit.normal * forceOffset;
                deformer.AddDeformingForce(point, force);
            }
        }
    }


}