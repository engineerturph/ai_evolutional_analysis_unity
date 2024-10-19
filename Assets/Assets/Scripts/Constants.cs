using UnityEngine;

public class Constants : MonoBehaviour
{
    public const float hunter_delay = 5f;
    public const float hunter_damage = 5f;
    public const float hunter_health = 100f;
    public const float hunter_health_decay = 20f;
    public const float prey_health = 100f;
    public const float prey_healing_to_hunter = 5f;
    public const float agent_speed = 10f;
    public const float agent_wander_radius = 5f;
    public const float agent_rotation_speed = 3f;
    public const float agent_stuck_movement_threshold = 0.1f; // How little movement is considered "not much"
    public const float agent_stuck_check_interval = 2.0f; // How often to check (in seconds)
    public const float agent_stuck_time_since_last_check = 0.0f;
    public const float prey_survival_time_interval = 5f;
    public const float prey_chance_to_duplicate = 0.5f;
    public const int spawn_count = 10;
    public const int spawnXRadius = 10;
    public const int spawnYRadius = 7;
    public const float reset_interval = 60f;

}
