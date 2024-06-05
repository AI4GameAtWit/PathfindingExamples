using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSpawner : MonoBehaviour
{
    public GameObject waypointPrefab;
    public Transform goalWaypoint; // Assign the goal waypoint Transform
    public GameObject plane; // Assign the plane GameObject here
    public int numberOfWaypoints = 10;
    public float offsetY = 1f; // Offset to make sure waypoints are above the plane

    void Start()
    {
        if (plane == null || waypointPrefab == null || goalWaypoint == null)
        {
            Debug.LogError("Plane or waypoint prefabs are not assigned.");
            return;
        }

        List<Transform> waypoints = new List<Transform>();
        // Choose the method you want to use
        // SpawnWaypointsWithDT(waypoints);
        SpawnWaypointsWithBFS(waypoints);
    }

    void SpawnWaypointsWithDT(List<Transform> waypoints)
    {
        Vector3 planeSize = plane.GetComponent<MeshRenderer>().bounds.size;
        Vector3 planePosition = plane.transform.position;

        // Spawn regular waypoints
        for (int i = 0; i < numberOfWaypoints; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-planeSize.x / 2, planeSize.x / 2),
                offsetY,
                Random.Range(-planeSize.z / 2, planeSize.z / 2)
            ) + planePosition;
            GameObject waypoint = Instantiate(waypointPrefab, position, Quaternion.identity);
            waypoint.name = $"Waypoint {i + 1}"; // Label the waypoint
            waypoints.Add(waypoint.transform);
        }

        // Assign one of the waypoints as the start waypoint
        int startWaypointIndex = Random.Range(0, waypoints.Count);
        Transform startWaypoint = waypoints[startWaypointIndex];
        GameObject agent = GameObject.Find("Agent");
        if (agent == null)
        {
            Debug.LogError("Agent GameObject not found. Ensure it is named 'Agent' in the Hierarchy.");
            return;
        }
        agent.transform.position = startWaypoint.position;

        // Assign the waypoints and goal to the AgentController
        AgentControllerWithDecisionTree agentController = agent.GetComponent<AgentControllerWithDecisionTree>();
        if (agentController == null)
        {
            Debug.LogError("AgentController component not found on Agent GameObject.");
            return;
        }
        agentController.SetWaypoints(waypoints, goalWaypoint);
    }

    void SpawnWaypointsWithBFS(List<Transform> waypoints)
    {
        Vector3 planeSize = plane.GetComponent<MeshRenderer>().bounds.size;
        Vector3 planePosition = plane.transform.position;

        // Spawn regular waypoints
        for (int i = 0; i < numberOfWaypoints; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-planeSize.x / 2, planeSize.x / 2),
                offsetY,
                Random.Range(-planeSize.z / 2, planeSize.z / 2)
            ) + planePosition;
            GameObject waypoint = Instantiate(waypointPrefab, position, Quaternion.identity);
            waypoint.name = $"Waypoint {i + 1}"; // Label the waypoint
            waypoints.Add(waypoint.transform);
        }

        // Assign one of the waypoints as the start waypoint
        int startWaypointIndex = Random.Range(0, waypoints.Count);
        Transform startWaypoint = waypoints[startWaypointIndex];
        GameObject agent = GameObject.Find("Agent");
        if (agent == null)
        {
            Debug.LogError("Agent GameObject not found. Ensure it is named 'Agent' in the Hierarchy.");
            return;
        }
        agent.transform.position = startWaypoint.position;

        // Assign the waypoints and goal to the AgentController
        AgentControllerWithBFS agentController = agent.GetComponent<AgentControllerWithBFS>();
        if (agentController == null)
        {
            Debug.LogError("AgentController component not found on Agent GameObject.");
            return;
        }
        agentController.SetWaypoints(waypoints, goalWaypoint);
    }
}
