using UnityEngine;

public class GPUInstancingExample : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public int instanceCount = 1000;

    private Matrix4x4[] matrices;
    private Vector4[] colors;
    private MaterialPropertyBlock materialPropertyBlock;

    private const int MAX_INSTANCES = 1023;

    void Start()
    {
        matrices = new Matrix4x4[instanceCount];
        colors = new Vector4[instanceCount];
        materialPropertyBlock = new MaterialPropertyBlock();

        for (int i = 0; i < instanceCount; i++)
        {
            matrices[i] = Matrix4x4.TRS(
                new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f)),
                Quaternion.Euler(0f, Random.Range(0f, 360f), 0f),
                Vector3.one
            );
        }
    }

    void Update()
    {
        for (int i = 0; i < instanceCount; i += MAX_INSTANCES)
        {
            int count = Mathf.Min(MAX_INSTANCES, instanceCount - i);
            Matrix4x4[] tempMatrices = new Matrix4x4[count];
            Vector4[] tempColors = new Vector4[count];

            for (int j = 0; j < count; j++)
            {
                tempMatrices[j] = matrices[i + j];
                tempColors[j] = colors[i + j];
            }

            materialPropertyBlock.SetVectorArray("_Color", tempColors);
            Graphics.DrawMeshInstanced(mesh, 0, material, tempMatrices, count, materialPropertyBlock);
        }
    }
}
