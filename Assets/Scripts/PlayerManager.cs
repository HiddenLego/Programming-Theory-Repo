using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private int hitsLeft;
    public bool gameOver = false;

    public float spawnInterval;
    private float spawnDistance = 55;
    private float maxRespawnDelay = 2.5f;

    [SerializeField] private List<GameObject> projectiles;
    [SerializeField] public List<Material> materials;

    private void Start()
    {
        Instance = this;
        StartCoroutine(SpawnCycle());
    }

    private void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
        hitsLeft -= 1;
        if (hitsLeft <= 0)
        {
            gameOver = true;
        }
    }

    private void SpawnProjectile()
    {
        int choice = Random.Range(0, projectiles.Capacity);
        int input = Random.Range(0, 4);
        Vector3 location = LocationPicker(input);
        Instantiate(projectiles[choice], location, projectiles[choice].transform.rotation);
    }

    private Vector3 LocationPicker(int input)
    {
        Vector3[] directions = {Vector3.down, Vector3.left, Vector3.up, Vector3.right };
        Vector3 side = directions[input];
        side *= spawnDistance;
        return side;
    }

    public void DelayedSpawn()
    {
        float delay = Random.Range(0, maxRespawnDelay);
        StartCoroutine(SpawnDelay(delay));
    }
    private IEnumerator SpawnDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnProjectile();
    }

    IEnumerator SpawnCycle()
    {
        yield return new WaitForSeconds(spawnInterval);
        SpawnProjectile();
        StartCoroutine(SpawnCycle());
    }
}
