using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PathPrediction : MonoBehaviour
{
    public ConsoleScript console;
    public CameraController camScript;
    public Transform bodies;
    public GameObject lineObject;
    public int maxIterations;
    public bool showlines = true;

    private SolarSystemScript mainSolarScript;
    private Scene simScene;
    private List<CelestialBodyScript> simBodies;
    private List<Path> paths;
    private bool refreshtime = true;

    public void Awake()
    {
        simBodies = new List<CelestialBodyScript>();
        paths = new List<Path>();
        mainSolarScript = GetComponent<SolarSystemScript>();
        FindBodies();
        simScene = SceneManager.CreateScene("path trajectory");
    }

    public void FindBodies() 
    {
        mainSolarScript.bodies.Clear();
        foreach (Transform body in bodies)
        {
            mainSolarScript.bodies.Add(body.GetComponent<CelestialBodyScript>());
        }
    }
     
    private void FixedUpdate()
    {
        if (refreshtime && (camScript.isMoving == true || console.reset == true))
            ResetSimScene();
    }

    public void ResetSimScene() 
    {
        FindBodies();
        foreach (CelestialBodyScript simBody in simBodies) 
        {
            Destroy(simBody.gameObject);
        }
        simBodies.Clear();
        paths.Clear();
        GameObject[] destroying = GameObject.FindGameObjectsWithTag("simObj");
        foreach (GameObject obj in destroying) 
        {
            Destroy(obj);
        }
        CreateSimScene();
    }

    public void CreateSimScene()
    {
        foreach (Transform body in bodies)
        {
            GameObject bodyClone = Instantiate(body.gameObject, body.transform.position, body.rotation);
            bodyClone.GetComponent<Renderer>().enabled = false;
            Destroy(bodyClone.GetComponent<SphereCollider>());
            bodyClone.name = "sim " + body.name;
            SceneManager.MoveGameObjectToScene(bodyClone, simScene);
            simBodies.Add(bodyClone.GetComponent<CelestialBodyScript>());
            Path newPath = new Path();
            paths.Add(newPath);
        }
        GameObject newSolarSusScript = new GameObject("sim solar script");
        newSolarSusScript.AddComponent<SolarSystemScript>();
        newSolarSusScript.GetComponent<SolarSystemScript>().bodies = simBodies;
        SceneManager.MoveGameObjectToScene(newSolarSusScript, simScene);
        newSolarSusScript.tag = "simObj";
        SimulateTrajectory(newSolarSusScript.GetComponent<SolarSystemScript>());
    }

    public void SimulateTrajectory(SolarSystemScript sussyBaka)
    {
        int pointsCount = 0;
        for (int i = 0; i < maxIterations; i++)
        {
            sussyBaka.SimulateGravity();
            foreach (CelestialBodyScript simBody in sussyBaka.GetComponent<SolarSystemScript>().bodies)
            {
                paths[pointsCount].points.Add(simBody.GetComponent<CelestialBodyScript>().transform.position);
                pointsCount++;
            }
            pointsCount = 0;
        }
        foreach (Path path in paths)
        {
            GameObject newLine = Instantiate(lineObject, transform.position, transform.rotation);
            newLine.tag = "simObj";
            SceneManager.MoveGameObjectToScene(newLine, simScene);
            newLine.GetComponent<LineRenderer>().positionCount = maxIterations;
            for (int i = 0; i < path.points.Count; i++)
            {
                newLine.GetComponent<LineRenderer>().SetPosition(i, path.points[i]);
            }
            if (showlines)
                newLine.GetComponent<LineRenderer>().widthMultiplier = 10;
            else
                newLine.GetComponent<LineRenderer>().widthMultiplier = 0;
        }
        refreshtime = true;
        console.reset = false;
    }
}

public class Path
{
    public List<Vector3> points = new List<Vector3>();
    public Color color;
}




















/*{
    public Transform bodies;

    private SolarSystemScript mainSolarSusScript;
    private Scene simScene;
    private List<CelestialBodyScript> simBodies;
   
    public List<Path> paths;
    public GameObject lineObject;
    public int maxIterations;

    public void Awake()
    {
        simBodies = new List<CelestialBodyScript>();
        paths = new List<Path>();
        mainSolarSusScript = GetComponent<SolarSystemScript>();
        foreach (Transform body in bodies)
        {
            mainSolarSusScript.bodies.Add(body.GetComponent<CelestialBodyScript>());
        }
        CreatePhysicsScene();
    }

    private void CreatePhysicsScene() 
    {
        simScene = SceneManager.CreateScene("trajectory");
        foreach (Transform body in bodies) 
        {
            GameObject bodyClone = Instantiate(body.gameObject, body.position, body.rotation);
            //bodyClone.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(bodyClone, simScene);
            bodyClone.name = "sussy";
            simBodies.Add(bodyClone.GetComponent<CelestialBodyScript>());
            Path newPath = new Path();
            paths.Add(newPath);
        }
        GameObject newSolarSusScript = new GameObject("sussy baka");
        newSolarSusScript.AddComponent<SolarSystemScript>();
        newSolarSusScript.GetComponent<SolarSystemScript>().bodies = simBodies;
        SceneManager.MoveGameObjectToScene(newSolarSusScript, simScene);
        SimulateTrajectory(newSolarSusScript.GetComponent<SolarSystemScript>());
    }

    public void SimulateTrajectory(SolarSystemScript sussyBaka) 
    {
        int pointsCount = 0;
        for (int i = 0; i < maxIterations; i++) 
        {
            sussyBaka.SimulateGravity();
            foreach (CelestialBodyScript simBody in sussyBaka.GetComponent<SolarSystemScript>().bodies) 
            {
                print(simBody.GetComponent<CelestialBodyScript>().rig.position + " body is " + simBody.name);
                paths[pointsCount].points.Add(simBody.GetComponent<CelestialBodyScript>().rig.position);
                pointsCount++;
            }
            pointsCount = 0;
        }
        foreach (Path path in paths)
        {
            GameObject newLine = Instantiate(lineObject, transform.position, transform.rotation);
            SceneManager.MoveGameObjectToScene(newLine, simScene);
            newLine.GetComponent<LineRenderer>().positionCount = maxIterations;
            for (int i = 0; i < path.points.Count; i++)
            {
                newLine.GetComponent<LineRenderer>().SetPosition(i, path.points[i]);
            }
        }
    }
}

public class Path
{
    public List<Vector3> points = new List<Vector3>();
    public Color color;
}*/

























/*{
    public SolarSystemScript solarScript;
    public Transform bodiesList;
    public List<SimCelestialBody> simBodies;
    public List<Path> paths;
    public GameObject lineObject;
    public int maxIterations;

    private void Start()
    {
        solarScript = GetComponent<SolarSystemScript>();
        paths = new List<Path>();
        simBodies = new List<SimCelestialBody>();
        CreateSimulation();
    }

    public void FixedUpdate()
    {
        if (!solarScript.start)
            CreateSimulation();
    }

    public void CreateSimulation()
    {
        ClearSimulation();
        for (int i = 0; i < bodiesList.childCount; i++)
        {
            SimCelestialBody bodyClone = new SimCelestialBody(bodiesList.GetChild(i).GetComponent<CelestialBodyScript>().position,
                                                              bodiesList.GetChild(i).GetComponent<CelestialBodyScript>().velocity,
                                                              bodiesList.GetChild(i).GetComponent<CelestialBodyScript>().surfaceGravity,
                                                              bodiesList.GetChild(i).GetComponent<CelestialBodyScript>().radius,
                                                              bodiesList.GetChild(i).GetComponent<CelestialBodyScript>().mass);
            simBodies.Add(bodyClone);
            Path newPath = new Path();
            paths.Add(newPath);
        }
        SimulateTrajectory();
    }

    public void ClearSimulation() 
    {
        simBodies.Clear();
        paths.Clear();
        if (this.gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < this.gameObject.transform.childCount; i++)
            {
                Destroy(this.gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    private void SimulateTrajectory()
    {
        for (int k = 0; k < maxIterations; k++)
        {
            for(int i = 0; i < simBodies.Count; i++)
            {
                for (int j = 0; j < simBodies.Count; j++)
                {
                    if (simBodies[j] != simBodies[i])
                    {
                        Vector3 oldPos = simBodies[i].position;
                        Vector3 posPrime = oldPos + StaticVariables.physicsTimeScale * simBodies[i].velocity;
                        simBodies[i].velocity += StaticVariables.physicsTimeScale * CalculateAcceleration(simBodies[i], simBodies[j]);
                        simBodies[i].position = posPrime;
                    }
                }
                paths[i].points.Add(simBodies[i].position);
            }
        }
        foreach (Path path in paths) 
        {
            GameObject newLine = Instantiate(lineObject, transform.position, transform.rotation);
            newLine.transform.parent = this.gameObject.transform;
            newLine.GetComponent<LineRenderer>().positionCount = path.points.Count;
            for (int i = 0; i < path.points.Count; i++)
            {
                newLine.GetComponent<LineRenderer>().SetPosition(i, path.points[i]);
            }
        }
    }
    
    private Vector3 CalculateAcceleration(SimCelestialBody body, SimCelestialBody otherBody)
    {
        float totalMass = (body.mass * otherBody.mass);
        float distance = ((body.position) - otherBody.position).magnitude;
        Vector3 direction = ((body.position) - otherBody.position) / distance;
        Vector3 gravityForce = -StaticVariables.gravityConstant * (totalMass / distance) * direction;
        return (gravityForce / body.mass) * StaticVariables.physicsTimeScale;
    }
}

public class SimCelestialBody
{
    public Vector3 position;
    public Vector3 velocity;
    public float surfaceGravity;
    public float radius;
    public float mass;

    public SimCelestialBody(Vector3 inPos, Vector3 inVeloc, float inSurfGravity, float inRad, float inMass)
    {
        position = inPos;
        velocity = inVeloc;
        surfaceGravity = inSurfGravity;
        radius = inRad;
        mass = inMass;
    }
}

public class Path
{
    public List<Vector3> points = new List<Vector3>();
    public Color color;
}*/