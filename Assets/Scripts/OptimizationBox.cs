using System;
using UnityEngine;

public class OptimizationBox : MonoBehaviour
{
    public float velocity;
    public Vector3 startPosition;
    public Vector3 endPosition;

    private MeshRenderer meshRenderer;

    private OptimizationBoxSpawner boxSpawner;
    private bool useCache;

    public void Setup(OptimizationBoxSpawner spawner, Transform parent, bool cacheSpawner)
    {
        transform.SetParent(parent, false);
        transform.localPosition = startPosition;

        useCache = cacheSpawner;
        boxSpawner = spawner;

        meshRenderer.enabled = true;
        enabled = true;
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
        enabled = false;
    }

    private void Update()
    {
        if (transform.localPosition.x >= endPosition.x)
        {
            DestroyBox();
            return;
        }

        transform.Translate(Vector3.right * (velocity * Time.deltaTime), Space.Self);
    }

    private void DestroyBox()
    {
        if (useCache)
            boxSpawner.DestroyedBox(this);

        else
        {
            var spawner = FindObjectOfType<OptimizationBoxSpawner>();

            if (spawner != null)
                spawner.DestroyedBox(this);
            else
                Destroy(gameObject);
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
}
