using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentScipt_Av : Agent
{
    public float moveSpeed = 10.0f;
    public float turnSpeed = 10.0f;
    private float minDistanceForReward = 5.0f; // Minimum distance to other agents to receive reward
                                               // Maximum expected distance for normalization

    private Transform[] otherAgents;
    private Rigidbody2D rb;

    private Vector2 lastPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found.");
            return;
        }
    }

    public override void OnEpisodeBegin()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found.");
            return;
        }
        // Find all other agents in the scene
        var agentObjects = GameObject.FindGameObjectsWithTag("Avci");
        if (agentObjects.Length == 0)
        {
            Debug.LogError("No agents found with the 'Avci' tag. Make sure agents are correctly tagged.");
        }

        // Exclude this agent from the list of other agents
        otherAgents = new Transform[agentObjects.Length];
        int index = 0;
        foreach (var agentObj in agentObjects)
        {
            if (agentObj.transform != transform)
            {
                otherAgents[index++] = agentObj.transform;
            }
        }

        // Reset last position and total distance moved
        lastPosition = rb.position;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Collect observations about the agent's position and rotation
        sensor.AddObservation(rb.transform.localPosition);
        sensor.AddObservation(rb.transform.localRotation);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Process actions from the neural network
        float moveForward = actionBuffers.ContinuousActions[0];
        float turn = actionBuffers.ContinuousActions[1];

        // Debug: Check the received actions


        // Move the agent forward or backward
        rb.velocity = transform.up * moveSpeed * moveForward;

        // Debug: Check the velocity


        rb.rotation += turnSpeed * turn * Time.deltaTime;

        // Debug: Check the rotation


        // Reward based on distance from other agents
        if (otherAgents != null && otherAgents.Length > 0)
        {
            float minDistance = float.MaxValue;
            foreach (var otherAgent in otherAgents)
            {
                if (otherAgent != null)
                {
                    float distance = Vector2.Distance(transform.position, otherAgent.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                    }
                }
            }

            // Normalize the reward based on the distance
            float normalizedDistance = Mathf.Clamp(minDistance / minDistanceForReward, 0, 1.0f);
            float reward = normalizedDistance;

            // Give more reward if the agent is farther from others
            AddReward(reward * 0.1f); // Scaled reward

            // Debug: Check the reward

        }

        // Update last position
        lastPosition = rb.position;

        // Debug: Check the total distance moved

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            continuousActionsOut[0] = 1.0f;
            continuousActionsOut[1] = 0.0f;
        }
        else
        {
            continuousActionsOut[0] = 0.0f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            continuousActionsOut[1] = 1.0f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            continuousActionsOut[1] = -1.0f;
        }
        else
        {
            continuousActionsOut[1] = 0.0f;
        }
    }
}
