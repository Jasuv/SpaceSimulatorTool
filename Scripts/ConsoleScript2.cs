using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ConsoleScript2 : MonoBehaviour
{
    public Transform camPos;
    public Transform bodies;
    public SolarSystemScript solarScript;
    public ConsoleScript otherConsole;
    public PathPrediction predictionScript;
    public GameObject body;
    public GameObject intInput;
    public Button maxIterations;
    public Button toggleLines;
    public Button createBody;
    public Button clear;
    public Button help;
    public RawImage helpImg;
    public Text maxIterationsTxt;
    private bool toggleHelp = false;

    void Start()
    {
        intInput.SetActive(false);
    }

    void Update()
    {
        maxIterationsTxt.text = "" + predictionScript.maxIterations;
        if (solarScript.start == true || bodies.childCount == 0)
            clear.interactable = false;
        else
            clear.interactable = true;
        if (toggleHelp)
            helpImg.enabled = true;
        else
            helpImg.enabled = false;
    }

    public void selectMaxIt() 
    {
        intInput.SetActive(true);
        intInput.transform.position = maxIterations.transform.position;
    }

    public void InputMaxIt(string inMax) 
    {
        if (int.TryParse(inMax, out int value)) 
        {
            predictionScript.maxIterations = int.Parse(inMax);
            intInput.GetComponent<InputField>().text = "";
            intInput.SetActive(false);
            otherConsole.reset = true;
        }
    }

    public void ToggleLines() 
    {
        predictionScript.showlines = !predictionScript.showlines;
        otherConsole.reset = true;
    }

    public void SpawnBody() 
    {
        Vector3 bodyPos = new Vector3(camPos.position.x, 0, camPos.position.z);
        GameObject newBody = Instantiate(body, bodyPos, transform.rotation);
        newBody.transform.parent = bodies;
        otherConsole.reset = true;
    }

    public void ClearBodies() 
    {
        foreach (Transform body in bodies) 
        {
            Destroy(body.gameObject);
            otherConsole.reset = true;
        }
    }

    public void ToggleHelp() 
    {
        toggleHelp = !toggleHelp;
    }
}
