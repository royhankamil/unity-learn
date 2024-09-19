using UnityEngine;

public class Box : MonoBehaviour
{
    public float velocity;
    public Vector3 startPosition;
    public Vector3 endPosition;

    private MeshRenderer meshRenderer;

    public void Setup(Transform parent)
    {
        Transform.SetParent(parent, false);
        Transform.localPosition = startPosition;
    }

    private void Update()
    {
        if (transform.localPosition.x >= endPosition.x)
        {
            DestroyBox();
            return;
        }

        transform.Translate(Vector3.right * (velocity * Time.deltaTime), Space.Self)
    }
}

private void DestroyBox()
{
    var spawner = FindObjectOfType<BoxSpawner>();
    spawner.DestroyedBox(this);
}
