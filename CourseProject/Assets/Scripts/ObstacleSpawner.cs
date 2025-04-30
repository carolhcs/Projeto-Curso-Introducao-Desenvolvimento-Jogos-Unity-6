using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    [Header("Tempo de Spawn Aleat�rio")]
    public float minSpawnDelay = 1f;   // M�nimo de segundos para spawnar
    public float maxSpawnDelay = 3f;   // M�ximo de segundos para spawnar

    [Header("Configura��es de Posi��o")]
    public Transform spawnPoint;       // Ponto de spawn (canto direito)
                                       // Vamos manter o Y fixo. Caso queira trocar, basta alterar aqui.

    private float currentSpawnDelay;   // Tempo definido para o pr�ximo spawn
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

            // Se o contador passar do "currentSpawnDelay", spawnamos um obst�culo
            if (timer >= currentSpawnDelay)
            {
                SpawnObstacle();

                // Reseta o timer
                timer = 0f;
                // Sorteia o pr�ximo tempo de spawn
                currentSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            }
        }
    }

    void SpawnObstacle()
    {
        // Instancia o obst�culo na mesma posi��o Y do spawnPoint
        Instantiate(
            obstaclePrefab,
            spawnPoint.position,
            Quaternion.identity
        );
    }
}
