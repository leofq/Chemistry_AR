using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


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
    bool isPlacing = false;

    // Screen bounds
    private float minX, minY, maxX, maxY, minZ, maxZ;
    private Vector2 pos;

    Vector3 hitPosePosition;
    Quaternion hitPoseRotation;

    private int ceCount;

    private float timer;


    void Start()
    {
        Debug.Log("Hello, Unity!");
        // Chemical elements and chemical groups
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

    public void spawnChemicalElements()
    {
        var dictionary = dict.ToList();
        ChemicalElements = new GameObject[num_chemicalelements];
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, dictionary.Count);
        } while (usedElements.Contains(randomIndex));
        usedElements.Add(randomIndex);
        var randomChemicalElement = dictionary[randomIndex];
        Debug.Log(hitPosePosition);
        ChemicalElements[ceCount] = Instantiate(ChemicalElement_Prefab, hitPosePosition, hitPoseRotation);
        ChemicalElements[ceCount].GetComponent<ChemicalElement>().ElementName = randomChemicalElement.Key;
        ChemicalElements[ceCount].GetComponent<ChemicalElement>().ElementGroup = randomChemicalElement.Value;
        StartCoroutine(SetIsPlacingToFalseWithDelay());

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!raycastManager) return;
        if (usedElements.Count < num_chemicalelements && timer > 0.5)
        {
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

    bool getPositionAndRotation(Vector2 screenPosition)
    {
        Vector2 screenPoint = new Vector2(Random.value * Screen.width, Random.value * Screen.height);
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);

        var rayHits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenPoint, rayHits, TrackableType.Planes);
        if (rayHits.Count > 0)
        {
            int randomElement = Random.Range(0, rayHits.Count());
            hitPosePosition = rayHits[randomElement].pose.position;
            hitPoseRotation = rayHits[randomElement].pose.rotation;

            // Save for debugging
            debugHasHit = true;
            debugHitPos = hitPosePosition;

            return true;
        }
        else
        {
            debugHasHit = false;
            return false;
        }
    }

    void OnDrawGizmos()
    {
       // if (!debugHasHit) return;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(debugHitPos, 0.2f);
    }

    private Vector2 getRandomPoint()
    {
        //float height = 2f * playerCamera.orthographicSize;
        //float width = height * playerCamera.aspect;

        //Vector2 pos = new Vector2(
        //    Random.Range(-width / 2f, width / 2f),
        //    Random.Range(-height / 2f, height / 2f));
        return new Vector2(Random.value, Random.value);
        //return pos;
    }

    IEnumerator SetIsPlacingToFalseWithDelay()
    {
        yield return new WaitForSeconds(1);
        isPlacing = false;
    }
}
