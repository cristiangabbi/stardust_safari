using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float GForce = 10f;
    public float gravitationalRadius;
    public float mass;
    public Vector3 gravitationalOffset;
    public LayerMask gravityLayer;


    // Update is called once per frame
    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere((transform.position + gravitationalOffset), gravitationalRadius, gravityLayer);

        foreach (Collider element in colliders)
        {
            if (element.enabled)
            {
                if (element.gameObject.GetComponent<Rigidbody>())
                {
                    AttractRigidBody(element.gameObject);
                }
            }
        }
    }

    public void AttractRigidBody(GameObject obj)
    {
        Rigidbody bodyToAttract = obj.GetComponent<Rigidbody>();

        Vector3 planetPos = (transform.position + gravitationalOffset);

        Vector3 direction = planetPos - bodyToAttract.position;
        float distance = direction.magnitude;

        //newton's law
        float forceMagnityde = GForce * (mass * bodyToAttract.mass) / (distance * distance);
        Vector3 force = direction.normalized * forceMagnityde;

        bodyToAttract.AddForce(force);

        //attract first person player if possible
        var FPController = obj.GetComponent<FirstPersonController>();
        if (FPController != null)
        {
            bodyToAttract.gameObject.transform.rotation = Quaternion.Slerp(bodyToAttract.gameObject.transform.rotation, 
                                                                Quaternion.FromToRotation(bodyToAttract.gameObject.transform.up, -direction) * bodyToAttract.gameObject.transform.rotation,
                                                                50f * Time.deltaTime);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + gravitationalOffset, gravitationalRadius);
    }

}
