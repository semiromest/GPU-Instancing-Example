using UnityEngine;

public class CInstance : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public int instanceCount = 1000;

    private GameObject[] instances;

    void Start()
    {
        instances = new GameObject[instanceCount];

        for (int i = 0; i < instanceCount; i++)
        {
            // Yeni bir GameObject oluþtur
            GameObject instance = new GameObject("Instance_" + i);
            instance.transform.position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            instance.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            instance.transform.localScale = Vector3.one;

            // Mesh ve Renderer ekle
            MeshFilter meshFilter = instance.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            MeshRenderer renderer = instance.AddComponent<MeshRenderer>();
            renderer.material = new Material(material); // Her instance için ayrý bir material kullan

            // Oluþturulan instance'ý diziye ekle
            instances[i] = instance;
        }
    }
}
