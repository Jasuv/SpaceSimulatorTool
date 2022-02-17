using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBodyScript : MonoBehaviour
{
    public Vector3 _position;
    public Vector3 _velocity;
    public float surfaceGravity;
    public float radius;
    public float mass;

    void Update()
    {
        _position = transform.position;
        mass = surfaceGravity * radius * radius / StaticVariables.gravityConstant;
        transform.localScale = new Vector3(radius, radius, radius);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
