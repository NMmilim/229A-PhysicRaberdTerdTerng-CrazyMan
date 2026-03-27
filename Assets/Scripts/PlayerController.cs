using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 lastMoveDir;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);

        if (move.magnitude > 0.1f)
        {
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

            lastMoveDir = move.normalized;

            // rotate to movement
            transform.forward = lastMoveDir;
        }
    }
}
