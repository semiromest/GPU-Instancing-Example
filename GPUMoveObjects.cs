using UnityEngine;

public class GPUMoveObjects : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public int instanceCount = 1000;
    public float wanderRadius = 10f;
    public float wanderSpeed = 1f;

    private Matrix4x4[] matrices;
    private Vector3[] targets;
    private float[] speeds;

    private const int MAX_INSTANCES = 1023;

    void Start()
    {
        matrices = new Matrix4x4[instanceCount];
        targets = new Vector3[instanceCount];
        speeds = new float[instanceCount];

        for (int i = 0; i < instanceCount; i++)
        {
            Vector3 startPosition = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0f, Random.Range(-wanderRadius, wanderRadius));
            matrices[i] = Matrix4x4.TRS(startPosition, Quaternion.identity, Vector3.one);

            // Rastgele hedef noktalar ve hýzlar
            targets[i] = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0f, Random.Range(-wanderRadius, wanderRadius));
            speeds[i] = Random.Range(0.5f, wanderSpeed);
        }
    }

    void Update()
    {
        for (int i = 0; i < instanceCount; i++)
        {
            Vector3 currentPosition = matrices[i].GetColumn(3); // Pozisyonu al
            Vector3 direction = (targets[i] - currentPosition).normalized;
            float distance = Vector3.Distance(currentPosition, targets[i]);

            if (distance < 0.5f)
            {
                // Hedefe ulaþýldýðýnda yeni hedef belirle
                targets[i] = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0f, Random.Range(-wanderRadius, wanderRadius));
                direction = (targets[i] - currentPosition).normalized;
            }

            // Hareket
            Vector3 newPosition = currentPosition + direction * speeds[i] * Time.deltaTime;
            matrices[i].SetColumn(3, new Vector4(newPosition.x, newPosition.y, newPosition.z, 1.0f));
        }

        for (int i = 0; i < instanceCount; i += MAX_INSTANCES)
        {
            int count = Mathf.Min(MAX_INSTANCES, instanceCount - i);
            Matrix4x4[] tempMatrices = new Matrix4x4[count];

            for (int j = 0; j < count; j++)
            {
                tempMatrices[j] = matrices[i + j];
            }

            Graphics.DrawMeshInstanced(mesh, 0, material, tempMatrices, count);
        }
    }
}
