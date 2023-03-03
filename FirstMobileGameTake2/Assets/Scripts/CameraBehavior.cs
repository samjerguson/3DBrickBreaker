using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject ballPrefab; // The ball prefab to instantiate
    public float ballSpeed = 10f; // The speed at which the ball will be shot
     public float defaultDistance = 10f; // The default distance at which the ball will be shot if the ray does not hit anything
    public Transform target; // the point to orbit around
    public float distance; // distance from the point
    public float height; // height above the point
    public float speed; // speed of rotation around the point

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + height, target.position.z);
        transform.position = targetPosition - transform.forward * distance;
        transform.RotateAround(targetPosition, Vector3.up, speed * Time.deltaTime);
        transform.LookAt(targetPosition);
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Check if the left mouse button is clicked
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera towards the mouse position
            Vector3 target = ray.GetPoint(defaultDistance); // Get the default target point along the ray
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // Check if the ray hits something in the scene
            {
                target = hit.point; // Get the point where the ray hits the scene
            }
            Vector3 direction = (target - transform.position).normalized; // Get the direction from the camera to the target point
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity); // Instantiate the ball prefab at the camera position
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.velocity = direction * ballSpeed; // Shoot the ball in the direction of the target point with the given speed
        }
    }
}
