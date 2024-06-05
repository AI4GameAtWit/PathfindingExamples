using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentControllerWithRandomizedWP : MonoBehaviour
{
    private Transform[] waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        if (waypoints.Length > 0)
        {
            MoveToNextWaypoint();
        }
    }

    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0 || currentWaypointIndex >= waypoints.Length)
            return;

        Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
        rb.velocity = direction.normalized * speed;

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1.0f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                rb.velocity = Vector3.zero; // Stop the ball at the final waypoint
                OnReachExit();
            }
        }
    }

    void MoveToNextWaypoint()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 direction = waypoints[currentWaypointIndex].position - transform.position;
            rb.velocity = direction.normalized * speed;
        }
    }

    void OnReachExit()
    {
        Debug.Log("Ball has reached the exit point!");
        // Add any additional logic here, like triggering a win condition.
    }
}
