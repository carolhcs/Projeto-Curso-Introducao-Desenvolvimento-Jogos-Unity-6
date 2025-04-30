using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    [Header("Tempo de Spawn Aleatório")]
    public float minSpawnDelay = 1f;   // Mínimo de segundos para spawnar
    public float maxSpawnDelay = 3f;   // Máximo de segundos para spawnar

    [Header("Configurações de Posição")]
    public Transform spawnPoint;       // Ponto de spawn (canto direito)
                                       // Vamos manter o Y fixo. Caso queira trocar, basta alterar aqui.

    private float currentSpawnDelay;   // Tempo definido para o próximo spawn
    private float timer;               // Contador de tempo

    void Start()
    {
        // Sorteia um tempo inicial para o primeiro spawn
        currentSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    void Update()
    {
        if (!GameManager.Instance.isGameOver)
        {
            timer += Time.deltaTime;

            // Se o contador passar do "currentSpawnDelay", spawnamos um obstáculo
            if (timer >= currentSpawnDelay)
            {
                SpawnObstacle();

                // Reseta o timer
                timer = 0f;
                // Sorteia o próximo tempo de spawn
                currentSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            }
        }
    }

    void SpawnObstacle()
    {
        // Instancia o obstáculo na mesma posição Y do spawnPoint
        Instantiate(
            obstaclePrefab,
            spawnPoint.position,
            Quaternion.identity
        );
    }
}
