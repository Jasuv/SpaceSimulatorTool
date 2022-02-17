using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityObject : MonoBehaviour
{
    /*public SolarSystemScript solarSystemManager;
    public float gravityMultiplier = 1;

    [HideInInspector]
    public CelestialBodyScript closestBody;
    [HideInInspector]
    public Vector3 closestBodyVelocity;

    private Vector3 strongestGravitationalPull;
    private Vector3 targetVelocity;

    private Rigidbody rig;

    private void Start()
    {
        RigidbodySetup();
    }

    private void RigidbodySetup() 
    {
        rig = GetComponent<Rigidbody>();
        rig.mass = 1;
        rig.drag = 0;
        rig.angularDrag = 0;
        rig.useGravity = false;
        rig.isKinematic = false;
        rig.interpolation = RigidbodyInterpolation.Interpolate;
        rig.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        rig.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        Vector3 oldPos = rig.position;
        Vector3 PrimePos = oldPos + (targetVelocity * Time.deltaTime) + closestBodyVelocity * StaticVariables.physicsTimeScale * 4f;
        rig.position = PrimePos;
        float nearestSurfaceDst = Mathf.Infinity;
        foreach (CelestialBody body in solarSystemManager.bodies)
        {
            float sqrdDist = (body.transform.position - rig.position).sqrMagnitude;
            Vector3 Dirc = (body.transform.position - rig.position).normalized;
            Vector3 force = Dirc * StaticVariables.gravityConstant * (body.mass / sqrdDist);
            rig.AddForce(force * gravityIntensity * StaticVariables.physicsTimeScale, ForceMode.Acceleration);

            float distToSurface = Mathf.Sqrt(sqrdDist) - body.radius;
            if (distToSurface < nearestSurfaceDst && body.gameObject.tag != "Sun")
            {
                nearestSurfaceDst = distToSurface;
                strongestGravitationalPull = force;
                closestBody = body;
            }
        }
    }

    public void UpdateClosestBodyVelocity(Vector3 inVeloc)
    {
        closestBodyVelocity = inVeloc;
    }

    public void UpDateTargetVelocity(Vector3 inVeloc) 
    {
        targetVelocity = inVeloc;
    }*/
}
