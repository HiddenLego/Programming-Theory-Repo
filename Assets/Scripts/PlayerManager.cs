using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public int hitsLeft;
    public bool gameOver = false;

    private int score;
    private int seconds;
    private int minutes;

    private float spawnInterval;
    private float spawnDistance = 55;
    private float maxRespawnDelay = 2.5f;

    [SerializeField] private List<GameObject> projectiles;
    [SerializeField] public List<Material> materials;

    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        Instance = this;
        UpdateDifficulty();
        SpawnProjectile();
        StartCoroutine(SpawnCycle());
        StartCoroutine(Clock());
    }

    private void UpdateDifficulty() // Abstraction
    {
        if (SystemManager.Instance.difficulty == "Easy")
        {
            spawnInterval = 12.5f;
            hitsLeft = 5;
        }
        else if (SystemManager.Instance.difficulty == "Normal")
        {
            spawnInterval = 10;
            hitsLeft = 3;
        }
        else if (SystemManager.Instance.difficulty == "Hard")
        {
            spawnInterval = 7.5f;
            hitsLeft = 1;
        }
        else
        {
            SystemManager.Instance.difficulty = "Easy";
            UpdateDifficulty();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        Destroy(collider.gameObject);
        hitsLeft -= 1;
        if (hitsLeft <= 0)
        {
            EndGame();
        }
    }

    private void SpawnProjectile()
    {
        if (!gameOver)
        {
            int choice = Random.Range(0, projectiles.Capacity);
            int input = Random.Range(0, 4);
            Vector3 location = LocationPicker(input);
            Instantiate(projectiles[choice], location, projectiles[choice].transform.rotation);
        }
    }

    public void ScorePoint(int value, GameObject source)
    {
        DelayedSpawn();
        score += value;
        Destroy(source);
    }

    private Vector3 LocationPicker(int input) // Abstraction
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
        if (!gameOver)
        {
            StartCoroutine(SpawnCycle());
        }
    }

    IEnumerator Clock()
    {
        yield return new WaitForSeconds(1);
        seconds += 1;
        if (seconds >= 60)
        {
            seconds = 0;
            minutes += 1;
        }
        if (!gameOver)
        {
            StartCoroutine(Clock());
        }
    }

    private void EndGame()
    {
        gameOver = true;

        displayText.gameObject.SetActive(true);
        pointsText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);

        pointsText.text = "" + score;
        timeText.text = minutes + "m " + seconds + "s";
    }

    public void GoToMenu()
    {
        SystemManager.Instance.GoToMenu();
    }
}
