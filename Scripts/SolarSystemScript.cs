using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemScript : MonoBehaviour
{
    public PathPrediction trajectory;
    public List<CelestialBodyScript> bodies;
    public bool start = false;

    public void StartStopSimulation()
    {
        if (!start)
            start = true;
        else
            start = false;
    }

    public void FixedUpdate()
    {
        if (start)
        {
            SimulateGravity();
        }
    }

    public void SimulateGravity()
    {
        foreach (CelestialBodyScript body in bodies)
        {
            foreach (CelestialBodyScript otherbody in bodies)
            {
                if (otherbody != body)
                {
                    Vector3 oldPos = body.transform.position;
                    Vector3 posPrime = oldPos + StaticVariables.physicsTimeScale * body._velocity;
                    body._velocity += StaticVariables.physicsTimeScale * CalculateAcceleration(body, otherbody);
                    body.transform.position = posPrime;
                }
            }
        }
    }

    private Vector3 CalculateAcceleration(CelestialBodyScript body, CelestialBodyScript otherBody)
    {
        float totalMass = (body.mass * otherBody.mass);
        float distance = ((body.transform.position) - otherBody.transform.position).magnitude;
        if (distance == 0)
            distance = 1;
        Vector3 direction = ((body.transform.position) - otherBody.transform.position) / distance;
        Vector3 gravityForce = -StaticVariables.gravityConstant * (totalMass / distance) * direction;
        return (gravityForce / body.mass) * StaticVariables.physicsTimeScale;
    }
}












/*{
    public List<CelestialBodyScript> bodies;
    public bool start = false;

    public void StartSimulation()
    {
        start = true;
    }

    public void StopSimulation()
    {
        start = false;
    }

    public void FixedUpdate()
    {
        if (start)
        {
            SimulateGravity();
        }
    }

    public void SimulateGravity() 
    {
        foreach (CelestialBodyScript body in bodies)
        {
            foreach (CelestialBodyScript otherbody in bodies)
            {
                if (otherbody != body)
                {
                    Vector3 oldPos = body.position;
                    Vector3 posPrime = oldPos + StaticVariables.physicsTimeScale * body.velocity;
                    body.velocity += StaticVariables.physicsTimeScale * CalculateAcceleration(body, otherbody);
                    body.rig.position = posPrime;
                }
            }
        }
    }

    private Vector3 CalculateAcceleration(CelestialBodyScript body, CelestialBodyScript otherBody) 
    {
        float totalMass = (body.mass * otherBody.mass);
        float distance = ((body.position) - otherBody.position).magnitude;
        Vector3 direction = ((body.position) - otherBody.position)/distance;
        Vector3 gravityForce = -StaticVariables.gravityConstant * (totalMass / distance) * direction;
        return (gravityForce / body.mass) * StaticVariables.physicsTimeScale;
    }
}*/
