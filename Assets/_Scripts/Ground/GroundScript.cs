using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    #region Params
    [Header("Set in Inspector")]
    public List<GameObject>                 barriersList;
    public List<GameObject>                 cubePickUpSetList;
    [SerializeField]private GameObject      playerWhichCollide;
    
    private GameObject                      barrierAnchor;
    private GameObject                      cubePickUpAnchor;

    public bool hasTriggered = false;
    #endregion

    #region Initialization
    private void Start()
    {
        barrierAnchor = new GameObject("BarrierAnchor");
        cubePickUpAnchor = new GameObject("CubePickUpAnchor");

        barrierAnchor.transform.parent = this.transform;
        cubePickUpAnchor.transform.parent = this.transform;

        barrierAnchor.transform.localPosition = new Vector3(0, 0, 1);
        cubePickUpAnchor.transform.localPosition = new Vector3(0, 0, 0.5f);


        if (barriersList.Count == 0  || cubePickUpSetList.Count == 0)
        {
            print("PrefabsLists empty. Put prefabs in Inspector"); 
            return; 
        }
            
        SpawnBarrier(barrierAnchor.transform);
        SpawnPickUpCubes(cubePickUpAnchor.transform);
    }
    #endregion

    #region Main Logic
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerWhichCollide.tag)
        {
            if (!hasTriggered) //check 
            {
                hasTriggered = true;
                TrackGroundLogic.S_TrackGrLogic.TrackGroundMoving();
            }
        }
    }

    void SpawnBarrier(Transform parent)
    {
        int rnd = Random.Range(0, barriersList.Count);
        Instantiate(barriersList[rnd], parent, false);
    }

    void SpawnPickUpCubes(Transform parent)
    {
        int rnd = Random.Range(0, cubePickUpSetList.Count);
        Instantiate(cubePickUpSetList[rnd], parent, false);
    }

    void ClearChildsInAnchors()
    {
        foreach (Transform child in barrierAnchor.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(Transform child2 in cubePickUpAnchor.transform)
        {
            Destroy(child2.gameObject);
        }
    }

    public void Reshuffle()
    {
        ClearChildsInAnchors();
        SpawnBarrier(barrierAnchor.transform);
        SpawnPickUpCubes(cubePickUpAnchor.transform);
    }
    #endregion
}