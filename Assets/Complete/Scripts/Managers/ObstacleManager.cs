using UnityEngine;
using System.Collections;

public class ObstacleManager : MonoBehaviour {
    public GameObject[] m_ObstacleTypes;
    public int MinObstacles = 0;
    public int MaxObstacles = 20;
    public GravityAttractor Ground;
    private GameObject[] Obstacles;

    // Use this for initialization
    void Start () {
        Obstacles = new GameObject[(int)Random.Range(0f + MinObstacles, 0.1f + MaxObstacles)];
        SpawnAllObstacles();
    }

    private void SpawnAllObstacles()
    {
        // For all the tanks...
        for (int i = 0; i < Obstacles.Length; i++)
        {
            int NextObstacle = GetRandomObstacleType();
            Vector3 NextSpawnPosition = GetRandomSpawnPosition(m_ObstacleTypes[NextObstacle]);
            Quaternion NextSpawnRotation = GetRandomSpawnRotation(NextSpawnPosition);

            Obstacles[i] = Instantiate(m_ObstacleTypes[NextObstacle], NextSpawnPosition, NextSpawnRotation) as GameObject;

        }
    }

    private int GetRandomObstacleType()
    {
        return (int)Random.Range(0f, (float)(m_ObstacleTypes.Length) - 0.1f);
    }

    private Vector3 GetRandomSpawnPosition(GameObject ObstacleType)
    {
        Vector3 SpawnDirection = Random.onUnitSphere;
        Vector3 SpawnPosition = Ground.transform.position + SpawnDirection * ObstacleType.transform.position.y;

        return SpawnPosition;
    }

    private Quaternion GetRandomSpawnRotation(Vector3 SpawnPosition)
    {
        Vector3 GravityDirection = (SpawnPosition - Ground.transform.position).normalized;
        Quaternion TargetRotation = Quaternion.FromToRotation(Vector3.up, GravityDirection);

        return TargetRotation;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
