using UnityEngine;

public class GPUWave : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public int instanceCount = 1000;
    private Matrix4x4[] matrices;
    private float time;

    void Start()
    {
        matrices = new Matrix4x4[instanceCount];
        for (int i = 0; i < instanceCount; i++)
        {
            matrices[i] = Matrix4x4.TRS(
                new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f)),
                Quaternion.identity,
                Vector3.one
            );
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        for (int i = 0; i < instanceCount; i++)
        {
            Vector3 position = matrices[i].GetColumn(3);
            float wave = Mathf.Sin(time + position.x * 2.0f);
            matrices[i].SetColumn(3, new Vector4(position.x, wave, position.z, 1.0f));
        }

        Graphics.DrawMeshInstanced(mesh, 0, material, matrices, instanceCount);
    }
}
