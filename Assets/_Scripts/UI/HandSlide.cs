using UnityEngine;

public class HandSlide : MonoBehaviour
{
    [Header("Hidden in Inspector")]
    private float           frequency = 1f;
    private float           amplitude = 200f;
    private float           speed = 3f;

    void FixedUpdate()
    {
        float x = Mathf.Sin(Time.time * speed * frequency) * amplitude;
        transform.localPosition = new Vector3(x, transform.localPosition.y , transform.localPosition.z);
    }
}
