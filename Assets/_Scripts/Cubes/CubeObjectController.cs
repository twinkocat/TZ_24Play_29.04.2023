using System.Collections;
using UnityEngine;

public class CubeObjectController : MonoBehaviour
{
    #region Params
    [Header("Set in Inspector")]
    [SerializeField]private GameObject          GOWallWhichCollide;
    private bool                                isDestroyed = false;
    private const float                         DELAY_TO_DESTROY = 2f;
    #endregion


    #region Main logic
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag  == GOWallWhichCollide.tag && !isDestroyed)  // Go CubeWall
        {
            isDestroyed = true;
            transform.SetParent(null);

            CubeHolderController.S_CubeHoldController.cubesList.Remove(this.gameObject);
            Destroy(this.gameObject, DELAY_TO_DESTROY);

            if (CubeHolderController.S_CubeHoldController.cubesList.Count == 0)
            {
                Main.S_Main.EndGame();
            }
        }
    }
    #endregion
}
