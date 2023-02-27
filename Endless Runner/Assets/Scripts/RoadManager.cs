using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public GameObject[] cornerPrefabs;

    public float roadLength = 10f;
    public int numRoadsOnScreen = 0;
    public int cornerNow = 0;
    public int cornerMax = 3;

    public List<GameObject> activeRoads = new List<GameObject>();

    public Transform playerTransform;
    public Transform halfOfRoad;

    private float spawnZ = 0f;

    private GameObject lastPref;
    private float rotate = 0;

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();
        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                SpawnRoad(0);
            }
            else
            {
                SpawnRoad();
            }
        }
    }

    private void Update()
    {
        halfOfRoad = activeRoads[activeRoads.Count / 2].transform;

        if (playerTransform != null)
        {
            if (numRoadsOnScreen < 20)
            {

                if (playerTransform.transform.position.z > 0 || playerTransform.transform.position.x > 0)
                {
                    if (playerTransform.position.z + 6 > halfOfRoad.position.z && playerTransform.position.z - 6 < halfOfRoad.position.z || playerTransform.position.x + 6 > halfOfRoad.position.x && playerTransform.position.x - 6 < halfOfRoad.position.x)
                    {
                        SpawnRoad();
                        DeleteRoad();
                    }
                }
                else
                {
                    if (playerTransform.position.z - 6 < halfOfRoad.position.z && playerTransform.position.z + 6 > halfOfRoad.position.z || playerTransform.position.x - 6 < halfOfRoad.position.x && playerTransform.position.x + 6 > halfOfRoad.position.x)
                    {
                        SpawnRoad();
                        DeleteRoad();
                    }
                }
            }
        }
        
    }

    private void SpawnRoad(int prefabIndex = -1)
    {
        GameObject go;

        if (prefabIndex == -1 && cornerNow != cornerMax)
            prefabIndex = Random.Range(0, roadPrefabs.Length);
        else
            prefabIndex = Random.Range(0, cornerPrefabs.Length);


        if (cornerNow != cornerMax)
        {
            cornerNow++;
            go = Instantiate(roadPrefabs[prefabIndex]) as GameObject;
        }
        else
        {
            cornerNow = 0;
            cornerMax = Random.Range(3, 5);
            go = Instantiate(cornerPrefabs[prefabIndex]) as GameObject;
        }


        go.transform.SetParent(transform);



        if (lastPref != null)
        {
            if (lastPref.tag == "TurnLeft")
                rotate += -90;
            if (lastPref.tag == "TurnRight")
                rotate += +90;

            go.transform.position = lastPref.transform.GetChild(0).position;
            go.transform.Rotate(0f, rotate, 0f);
        }
        else
        {
            go.transform.position = Vector3.forward * spawnZ;
        }

        numRoadsOnScreen++;
        activeRoads.Add(go);
        spawnZ += roadLength;
        lastPref = go;
    }

    private void DeleteRoad()
    {
        if(activeRoads.Count >= 19)
        {
            numRoadsOnScreen--;
            GameObject roadToDestroy = activeRoads[0];
            activeRoads.RemoveAt(0);
            Destroy(roadToDestroy);
        }
    }
}