using UnityEngine;
namespace PongGameSystem
{
    [CreateAssetMenu(menuName ="SO/ObstacleData")]
    public class ObstacleData : ScriptableObject
    {
        public Obstacle obstaclePrefab;
    }
}