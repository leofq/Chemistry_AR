using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class CESpawner : MonoBehaviour
{
    // Dictionary for storing the chemical elements with corresponding chemical element group
    Dictionary<string, string> dict = new Dictionary<string, string>();

    // number of chemical elements to be in scene
    [SerializeField] private int num_chemicalelements;

    // Chemical element reference
    public GameObject ChemicalElement_Prefab;
    private GameObject[] ChemicalElements;

    // List for the elements already spawned 
    List<int> usedElements = new List<int>();

    // Position for spawning chemical element in scene
    private Vector3 position = new Vector3(0, 0, 5);

    // Not sure if needed right now
    [SerializeField] private GameObject target;
    [SerializeField] private Camera playerCamera;

    // Plane manager for detecting if plane is visible
    public ARPlaneManager planeManager;
    [SerializeField] private ARRaycastManager raycastManager;

    Vector3 hitPosePosition;
    Quaternion hitPoseRotation;
    private bool isPlacing;

    private int ceCount;

    private float timer;

    XRGrabInteractable grabbed;

    void Start()
    {
        Debug.Log("Hello, Unity!");

        // Add Chemical elements and chemical groups to dictionary
        dict.Add("Li", "Alkali");
        dict.Add("Na", "Alkali");
        dict.Add("K", "Alkali");
        dict.Add("Rb", "Alkali");
        dict.Add("Cs", "Alkali");
        dict.Add("Fr", "Alkali");

        dict.Add("Sc", "Transition");
        dict.Add("Ti", "Transition");
        dict.Add("Cr", "Transition");
        dict.Add("Mn", "Transition");
        dict.Add("Fe", "Transition");
        dict.Add("Co", "Transition");

        dict.Add("B", "Metalloids");
        dict.Add("Si", "Metalloids");
        dict.Add("Ge", "Metalloids");
        dict.Add("As", "Metalloids");
        dict.Add("Sb", "Metalloids");
        dict.Add("Te", "Metalloids");

      
    }

    
    //Funtion for randomly picking chemical elements from the dictionaty
    public void spawnChemicalElements()
    {
        var dictionary = dict.ToList();
        ChemicalElements = new GameObject[num_chemicalelements];
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, dictionary.Count); // finds a random index in the dictionary
        } while (usedElements.Contains(randomIndex));
        usedElements.Add(randomIndex); // store the used index in a used elements list
        var randomChemicalElement = dictionary[randomIndex];
        
        // create the chemical element
        ChemicalElements[ceCount] = Instantiate(ChemicalElement_Prefab, hitPosePosition, hitPoseRotation);
        ChemicalElements[ceCount].GetComponent<ChemicalElement>().ElementName = randomChemicalElement.Key;
        ChemicalElements[ceCount].GetComponent<ChemicalElement>().ElementGroup = randomChemicalElement.Value;
        StartCoroutine(SetIsPlacingToFalseWithDelay()); 
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!raycastManager) return;

        // Check if element needs to be spawned
        if (usedElements.Count < num_chemicalelements && timer > 0.5)
        {
            // get a radom point in the screen and spawn the chemical element
            Vector2 randPos = getRandomPoint();
            if(getPositionAndRotation(randPos))
            {
                spawnChemicalElements();
                ceCount++;
                timer = 0;
            }     
        }
    }

    private Vector3 debugHitPos;
    private bool debugHasHit;


    // Function for performing a ray cast 
    bool getPositionAndRotation(Vector2 screenPosition)
    {
        // Get a random screen point in the camera
        Vector2 screenPoint = new Vector2(Random.value * Screen.width, Random.value * Screen.height);
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);

        // Add to list and if the ray cast is successful and set hit post to the hit point
        var rayHits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPoint, rayHits, TrackableType.Planes);
        if (rayHits.Count > 0)
        {
            int randomElement = Random.Range(0, rayHits.Count());
            hitPosePosition = rayHits[randomElement].pose.position;
            hitPoseRotation = rayHits[randomElement].pose.rotation;

            return true;
        }
        else
        {
            return false;
        }
    }

    // Gets random point
    private Vector2 getRandomPoint()
    {
        return new Vector2(Random.value, Random.value);
    }

    IEnumerator SetIsPlacingToFalseWithDelay()
    {
        yield return new WaitForSeconds(1);
        isPlacing = false;
    }
}
