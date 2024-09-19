using System.Collections.Generic;
using UnityEngine;

public class OptimizationBoxSpawner : MonoBehaviour
{
    public float spawnDelay = 0.1f;
    public OptimizationBox boxPrefab;

    public int prewarmPoolPerBelt = 19;
    public Transform[] belts;

    [Header("Optimizations")]
    public bool usePool;
    public bool cacheSpawner;

    private float currentCooldown;
    private int totalBoxesSent = 0;

    private List<OptimizationBox> pool;

    public void DestroyedBox(OptimizationBox destroyedBox)
    {
        if (usePool)
        {
            destroyedBox.Hide();
            pool.Add(destroyedBox);
        }
        else
            Destroy(destroyedBox.gameObject);

        totalBoxesSent++;
    }

    private void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0)
        {
            foreach (var boxParent in belts)
                GetAvailableBox().Setup(this, boxParent, cacheSpawner);

            ResetCooldown();
        }

        if (Input.GetKeyDown(KeyCode.Return))
            gameObject.SetActive(false);
    }

    private OptimizationBox GetAvailableBox()
    {
        if (pool.Count == 0)
            return Instantiate(boxPrefab);

        var box = pool[0];
        pool.RemoveAt(0);
        return box;
    }

    private void ResetCooldown()
    {
        currentCooldown = spawnDelay;
    }

    private void SetupObjectPool()
    {
        pool = new List<OptimizationBox>();

        for (int i = 0; i < prewarmPoolPerBelt * belts.Length; i++)
        {
            var newBox = Instantiate(boxPrefab);
            newBox.Hide();
            pool.Add(newBox);
        }
    }

    private void Awake()
    {
        ResetCooldown();
        SetupObjectPool();
    }
}
