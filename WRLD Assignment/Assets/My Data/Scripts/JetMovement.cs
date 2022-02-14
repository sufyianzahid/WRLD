using UnityEngine;
using System.Collections;

public class JetMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float liftSpeed = 10f;
    public float turnSpeed = 10f;
    public float boostSpeed = 20f;
    public float bankSpeed = 10f;

    private void InputHandling()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * boostSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0f, -turnSpeed * Time.deltaTime, 0f, Space.World);
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f, Space.World);

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Rotate(liftSpeed * Time.deltaTime, 0f, 0f);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Rotate(-liftSpeed * Time.deltaTime, 0f, 0f);

        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(0f, 0f, bankSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(0f, 0f, -bankSpeed * Time.deltaTime);
    }
    private void Update()
    {
        InputHandling();
    }
}