using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHolderController : MonoBehaviour
{
    #region Params
    public static CubeHolderController                      S_CubeHoldController;  //Singleton
    
    [HideInInspector]
    public List<GameObject>                                 cubesList;
    private const float                                     LERP_TIME = 0.5f;
    [SerializeField]private PickUpCubeFloatingText          pickUpFloatingTextScript;
    #endregion

    #region Initialization
    private void Awake()
    {
        if (S_CubeHoldController != null)
        { 
            print("CubeHolderController: Instance already set. It should be Singleton"); 
            return; 
        }

        S_CubeHoldController = this;

        if (pickUpFloatingTextScript == null)
        {
            print("Add script <PickUpCubeFloatingText> to CubeHolder if exist, or change this logic");
            return;
        }


        foreach (Transform child in transform)
        {
            cubesList.Add(child.gameObject);
        }
    }
    #endregion


    #region Main Logic
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CubeObjectController>()) // PickUpCube
        {
            CubesUp(other.gameObject);
        }
    }

    private void CubesUp(GameObject cube)
    {

        foreach (GameObject child in cubesList)
        {
            Vector3 childTmpPos = child.transform.localPosition;
            childTmpPos.y += 1f;
            StartMoveCubeCoro(child, childTmpPos);
        }
        CubeAdd(cube);
    }

    private void CubeAdd(GameObject cube)
    {
        pickUpFloatingTextScript.ShowFloatingText();

        cubesList.Add(cube);
        cube.transform.SetParent(transform, true);
        cube.transform.localPosition = Vector3.zero;
    }

    void StartMoveCubeCoro(GameObject cube, Vector3 targetPos)
    {
        StartCoroutine(MoveCube(cube, targetPos));
    }

    private IEnumerator MoveCube(GameObject cube, Vector3 targetPos)
    {
        float currentTime = 0f;

        while (currentTime < LERP_TIME)
        {
            currentTime += Time.deltaTime;
            cube.transform.localPosition = Vector3.Lerp(cube.transform.localPosition, targetPos, currentTime/LERP_TIME);
            yield return new WaitForFixedUpdate();
        }
    }

    #endregion
}
