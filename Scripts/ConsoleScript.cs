using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ConsoleScript : MonoBehaviour
{
    public SolarSystemScript solarScript;
    public CameraController cameraScript;
    public PathPrediction predictionScript;
    public GameObject strInput;
    public GameObject intInput;
    public Button bodyName;
    public Button velocityX;
    public Button velocityY;
    public Button surfaceGravity;
    public Button radius;
    public Button destroyer;
    public Text bodyNameTxt;
    public Text positionTxt;
    public Text velocityXTxt;
    public Text velocityYTxt;
    public Text surfaceGravityTxt;
    public Text radiusTxt;
    public Text massTxt;
    public bool reset = false;

    private string selected;

    private void Awake()
    {
        strInput.SetActive(false);
        intInput.SetActive(false);
    }

    private void Update()
    {
        if (cameraScript.selectedUI != null)
        {
            bodyNameTxt.text = "" + cameraScript.selectedUI.name;
            Vector2 newBodyPos = new Vector2(cameraScript.selectedUI.GetComponent<CelestialBodyScript>()._position.x, cameraScript.selectedUI.GetComponent<CelestialBodyScript>()._position.z);
            positionTxt.text = "" + newBodyPos;
            velocityXTxt.text = "" + cameraScript.selectedUI.GetComponent<CelestialBodyScript>()._velocity.x;
            velocityYTxt.text = "" + cameraScript.selectedUI.GetComponent<CelestialBodyScript>()._velocity.z;
            surfaceGravityTxt.text = "" + cameraScript.selectedUI.GetComponent<CelestialBodyScript>().surfaceGravity;
            radiusTxt.text = "" + cameraScript.selectedUI.GetComponent<CelestialBodyScript>().radius;
            massTxt.text = "" + cameraScript.selectedUI.GetComponent<CelestialBodyScript>().mass;
        }

        if (solarScript.start)
            destroyer.interactable = false;
        else
            destroyer.interactable = true;
    }

    public void SelectName() 
    {
        strInput.SetActive(true);
        strInput.transform.position = bodyName.transform.position;
    }

    public void Select(int choice) 
    {
        if (choice == 0)
        {
            selected = "VelocX";
            intInput.SetActive(true);
            intInput.transform.position = velocityX.transform.position;
        }
        if (choice == 1)
        {
            selected = "VelocY";
            intInput.SetActive(true);
            intInput.transform.position = velocityY.transform.position;
        }
        if (choice == 2)
        {
            selected = "SurfGrav";
            intInput.SetActive(true);
            intInput.transform.position = surfaceGravity.transform.position;
        }
        if (choice == 3)
        {
            selected = "Radius";
            intInput.SetActive(true);
            intInput.transform.position = radius.transform.position;
        }
    }

    public void GetString(string newName) 
    {
        cameraScript.selectedUI.name = newName;
        strInput.GetComponent<InputField>().text = "";
        strInput.SetActive(false);
        reset = true;
}

    public void GetInt(string inNum)
    {
        if (int.TryParse(inNum, out int value))
        {
            if (selected == "VelocX")
            {
                cameraScript.selectedUI.GetComponent<CelestialBodyScript>()._velocity.x = int.Parse(inNum);
                intInput.GetComponent<InputField>().text = "";
                intInput.SetActive(false);
                reset = true;
            }
            else if (selected == "VelocY")
            {
                cameraScript.selectedUI.GetComponent<CelestialBodyScript>()._velocity.z = int.Parse(inNum);
                intInput.GetComponent<InputField>().text = "";
                intInput.SetActive(false);
                reset = true;
            }
            else if (selected == "SurfGrav")
            {
                cameraScript.selectedUI.GetComponent<CelestialBodyScript>().surfaceGravity = int.Parse(inNum);
                intInput.GetComponent<InputField>().text = "";
                intInput.SetActive(false);
                reset = true;
            }
            else if (selected == "Radius")
            {
                cameraScript.selectedUI.GetComponent<CelestialBodyScript>().radius = int.Parse(inNum);
                intInput.GetComponent<InputField>().text = "";
                intInput.SetActive(false);
                reset = true;
            }
            else
            {
                Debug.Log("you fucked up somewhere dumbass");
            }
        }
        else 
        {
            Debug.Log("you tried inputting a string buddy");
        }
    }

    public void DestroyBody() 
    {
        Destroy(cameraScript.selectedUI);
        cameraScript.selectedUI = null;
        reset = true;
    }
}
