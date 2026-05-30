using UnityEngine;

public class Ground_Spawner : MonoBehaviour
{
    public static Ground_Spawner instance;
    [Header("Spawn Ground")]
    public GameObject[] ground_Prefabs;
    private Vector2 nextSpawnPoint = new Vector2(25f, -2.06f);

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    void Start()
    {
        for (int i = 0; i < 5; i++) {
            SpawnGround();
        }
    }

    public void SpawnGround() {
        int index = Random.Range(0, ground_Prefabs.Length);
        GameObject tempGround = Instantiate(ground_Prefabs[index], nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = tempGround.gameObject.transform.Find("NextSpawnPoint").transform.position;
    }
}
