using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrackGroundLogic : MonoBehaviour
{
    #region Params
    [Header("Set in Inspector")]
    public GameObject                   trackGroundPrefab;

    [HideInInspector]
    public List<GameObject>             trackGroundList;

    public static TrackGroundLogic      S_TrackGrLogic;  //singleton
    private GameObject                  _TrackGround;

    [Header("Variables for moving")]
    private Vector3[]                   points;
    private Vector3                     nextPosForSpawn = Vector3.zero;
    private const float                 GROUND_LENGTH = 30f;
    private const int                   GROUNDS_START_COUNT = 4;
    private const float                 Y_POINT_TO = -30f;
    private const float                 GROUND_HEIGHT = 30f;
    private const float                 MOVE_TIME = 1f;
    #endregion

    #region Initialization
    private void Awake()
    {
        if (S_TrackGrLogic != null)
        { 
            print("S_TrackGrLogic: Instance already set. It should be Singleton"); 
            return; 
        }

        S_TrackGrLogic = this;
       
    }
    #endregion

    #region MainLogic

    public void InitializeLevel()
    {
        points = new Vector3[3];

        for (int i = 0; i < GROUNDS_START_COUNT; i++)
        {
            addNewTrackGround();
        }
        nextPosForSpawn = trackGroundList[trackGroundList.Count - 1].gameObject.transform.position + new Vector3(0, 0, GROUND_LENGTH);
    }

    private void addNewTrackGround()
    {
        _TrackGround = Instantiate(trackGroundPrefab, nextPosForSpawn, Quaternion.identity, this.transform);
        nextPosForSpawn.z += GROUND_LENGTH;
        trackGroundList.Add(_TrackGround);
    }

    public void TrackGroundMoving()
    {
        GameObject trackGround = trackGroundList[0];
        trackGroundList.Remove(trackGround);

        points[0] = trackGround.transform.position;
        points[1] = new Vector3(0, Y_POINT_TO, GROUND_HEIGHT * trackGroundList.Count);
        points[2] = nextPosForSpawn;

        float startTime = Time.time;
        float u = 0f;

        trackGround.GetComponent<GroundScript>().Reshuffle();

        TrackGroundMoveCoro(u, startTime, MOVE_TIME, trackGround);
        trackGroundList.Add(trackGround);
    }

    private void TrackGroundMoveCoro(float u, float startTime, float moveTime, GameObject groundGO)
    {
        StartCoroutine(Move(u, startTime, moveTime, groundGO));
    }

    private IEnumerator Move(float u, float startTime, float moveTime, GameObject groundGO) 
    {
        while (u < 1)
        {
            u = Mathf.Min(((Time.time - startTime) / moveTime), 1);

            Vector3 p01, p12;

            p01 = (1 - u) * points[0] + u * points[1];

            p12 = (1 - u) * points[1] + u * points[2];
            
            groundGO.transform.position = (1 - u) * p01 + u * p12;

            yield return new WaitForFixedUpdate();
        }
        nextPosForSpawn = trackGroundList[trackGroundList.Count - 1].gameObject.transform.position + new Vector3(0, 0, GROUND_LENGTH);
        groundGO.GetComponent<GroundScript>().hasTriggered = false;
    }
    #endregion

}
