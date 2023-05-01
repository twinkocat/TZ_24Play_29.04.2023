using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private const float                         DESTROY_TIME = 2f;
    private Vector3                             offset = new Vector3(0, 2, 0);


    private void Start()
    {
        Destroy(gameObject, DESTROY_TIME);
        transform.localPosition += offset;
    }
}
