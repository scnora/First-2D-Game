
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy_Respawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private float coolDown;
    [Space]
    [SerializeField] private float coolDownDecreaseRate = 0.02f;
    [SerializeField] private float coolDownCap = 0.7f;
    private float timer;

    private Transform player;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>().transform;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = coolDown;
            CreateNewEnemy();

            coolDown = Mathf.Max(coolDownCap, coolDown - coolDownDecreaseRate);
        }
    }

    private void CreateNewEnemy()
    {
        int respawnPointIndex = Random.Range(0, respawnPoints.Length);
        GameObject newEnemy = Instantiate(enemyPrefab, respawnPoints[respawnPointIndex].position, Quaternion.identity);

        bool createOnRight = newEnemy.transform.position.x > player.transform.position.x;

        if (createOnRight)
        {
            newEnemy.GetComponent<Enemy>().Flip();
        }
    }
}
