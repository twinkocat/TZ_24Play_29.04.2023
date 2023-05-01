using UnityEngine;

public class PlayerMovementLogic : MonoBehaviour
{
    #region Params
    public float            speed = 10f;
    private float           boundedMove = 2f;
    private float           x;
    #endregion

    #region Main Logic
    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float touchDeltaX = touch.deltaPosition.x;
            x = (Mathf.Abs(touchDeltaX) > 0) ? touchDeltaX / Mathf.Abs(touchDeltaX) : 0.0f;
        }

        if (Main.S_Main.gameState == GameState.GAME)
        {
            Move(x);
        }
    }

    private void Move(float direction)
    {
        Vector3 moveVec = new Vector3(direction, 0, 1) * speed * Time.deltaTime;
        Vector3 newPos = transform.position + moveVec;
        newPos.x = Mathf.Clamp(newPos.x, -boundedMove, boundedMove);
        transform.position = newPos;
    }
    #endregion
}
