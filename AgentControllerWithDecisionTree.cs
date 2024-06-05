using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentControllerWithDecisionTree : MonoBehaviour
{
    private List<Transform> waypoints;
    private List<Transform> visitedWaypoints;
    private Transform goalWaypoint;
    public float speed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        visitedWaypoints = new List<Transform>();
    }

    public void SetWaypoints(List<Transform> newWaypoints, Transform goal)
    {
        waypoints = newWaypoints ?? new List<Transform>(); // Null check
        goalWaypoint = goal;
        if (waypoints.Count > 0)
        {
            MoveToNextWaypoint();
        }
    }

    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Count == 0)
            return;

        if (visitedWaypoints.Count < waypoints.Count)
        {
            Vector3 direction = waypoints[0].position - transform.position;
            rb.velocity = direction.normalized * speed;

            if (Vector3.Distance(transform.position, waypoints[0].position) < 1.0f)
            {
                Debug.Log($"Visited waypoint: {waypoints[0].name}");
                visitedWaypoints.Add(waypoints[0]);
                waypoints.RemoveAt(0);
                MoveToNextWaypoint();
            }
        }
        else
        {
            MoveToGoal();
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Count > 0)
        {
            Transform nextWaypoint = GetNearestWaypoint();
            if (nextWaypoint != null)
            {
                Vector3 direction = nextWaypoint.position - transform.position;
                rb.velocity = direction.normalized * speed;
            }
        }
    }

    void MoveToGoal()
    {
        if (goalWaypoint != null)
        {
            Vector3 direction = goalWaypoint.position - transform.position;
            rb.velocity = direction.normalized * speed;
            if (Vector3.Distance(transform.position, goalWaypoint.position) < 1.0f)
            {
                rb.velocity = Vector3.zero; // Stop the ball at the goal waypoint
                OnReachExit();
            }
        }
    }

    Transform GetNearestWaypoint()
    {
        Transform nearestWaypoint = null;
        float minDistance = float.MaxValue;

        foreach (Transform waypoint in waypoints)
        {
            if (!visitedWaypoints.Contains(waypoint))
            {
                float distance = Vector3.Distance(transform.position, waypoint.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestWaypoint = waypoint;
                }
            }
        }

        return nearestWaypoint;
    }

    void OnReachExit()
    {
        Debug.Log("Ball has reached the exit point!");
        // Add any additional logic here, like triggering a win condition.
    }
}
