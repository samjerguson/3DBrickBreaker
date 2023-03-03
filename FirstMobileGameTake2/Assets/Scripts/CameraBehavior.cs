using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject ballPrefab;
    public float ballSpeed = 10f;
    public float defaultDistance = 10f; // The default distance at which the ball will be shot if the ray does not hit anything
    public Transform target; // the point to orbit around
    public float distance; // distance from the point
    public float height; // height above the point
    public float speed; // speed of rotation around the point
    public float swipeSensitivity = 1f; // The sensitivity of the swipe
    public float swipeMagnitudeThreshold = 20f; // The minimum magnitude of swipe required to move the camera
    public float ballSpawnDistance;

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private Vector3 ballInstPos;
    Vector2 swipeDelta;

    void LateUpdate()
    {
        ballInstPos = transform.position + (transform.forward * ballSpawnDistance);
        if (Input.GetMouseButton(0)) // Check if the left mouse button is clicked
        {
            if (fingerDownPosition == Vector2.zero) // Store the finger down position only once
            {
                fingerDownPosition = Input.mousePosition;
            }
            else // Rotate the camera based on the swipe distance
            {
                fingerUpPosition = Input.mousePosition;
                Vector2 swipeDelta = (fingerUpPosition - fingerDownPosition) * swipeSensitivity;

                if (swipeDelta.magnitude < swipeMagnitudeThreshold) // Check if the swipe is below the magnitude threshold
                {
                    // Shoot the ball
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera towards the mouse position
                    Vector3 target = ray.GetPoint(defaultDistance); // Get the default target point along the ray
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit)) // Check if the ray hits something in the scene
                    {
                        target = hit.point; // Get the point where the ray hits the scene
                    }
                    Vector3 direction = (target - transform.position).normalized; // Get the direction from the camera to the target point

                    GameObject ball = Instantiate(ballPrefab, ballInstPos, Quaternion.identity); // Instantiate the ball prefab at the camera position
                    Rigidbody rb = ball.GetComponent<Rigidbody>();
                    rb.velocity = direction * ballSpeed; // Shoot the ball in the direction of the target point with the given speed
                }
                else
                {
                    // Move the camera
                    transform.RotateAround(target.position, Vector3.up, swipeDelta.x * Time.deltaTime);
                    height -= swipeDelta.y * Time.deltaTime;
                    height = Mathf.Clamp(height, 0f, 10f);

                    fingerDownPosition = fingerUpPosition;
                }
            }
        }
        else // Apply the default camera rotation
        {
            //transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
        }

        // Update the camera position and look at the target position
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + height, target.position.z);
        transform.position = targetPosition - transform.forward * distance;
        transform.LookAt(targetPosition);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) // Reset the finger positions when the user releases the finger
        {
            fingerDownPosition = Vector2.zero;
            fingerUpPosition = Vector2.zero;
        }
    }
}