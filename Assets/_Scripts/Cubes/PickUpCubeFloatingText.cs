using UnityEngine;

public class PickUpCubeFloatingText : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject floatingTextPrefab;

    public void ShowFloatingText()
    {
        if (floatingTextPrefab == null)
        {
            print("Prefab not set in inspector");
        }
        Vector3 pos = transform.position;
        Instantiate(floatingTextPrefab, pos, Quaternion.identity);
    }


}
