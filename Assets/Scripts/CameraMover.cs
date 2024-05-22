using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 1.0f;
    public float leftLimit = -10.0f;
    public float rightLimit = 10.0f;

    private bool movingLeft = true;

    void Update()
    {
        if (movingLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= leftLimit)
            {
                movingLeft = false;
            }
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= rightLimit)
            {
                movingLeft = true;
            }
        }
    }
}
