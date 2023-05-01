using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Params
    [Header("Set in Inspector")]
    public GameObject           Player;
    public GameObject           wrapEffectPrefab;

    private float               smoothTime = 0.35f;
    private Vector3             offset = new Vector3 (3f, 6f, -10f);
    private Vector3             velocity = Vector3.zero;
    private Vector3             wrapEffectOffset = new Vector3(0, 0, 10f);
    #endregion

    #region Initialization
    private void Awake()
    {
        if (Player == null)
        {
            print("Set player in Inspector");
        }
        if(wrapEffectPrefab == null)
        {
            print("Set wrapEffect in Inspector");
        }
        wrapEffectPrefab = Instantiate(wrapEffectPrefab);
    }
    #endregion

    #region Following logic
    private void FixedUpdate()
    {
        Vector3 desiredPosition = Player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothedPosition;
        wrapEffectPrefab.transform.position = transform.position + wrapEffectOffset;
        transform.LookAt(Player.transform.position);
    }
    #endregion
}

