
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public float spawnDelay = 0.1f;
    public Box boxPrefab;

    public Transform[] belts;

    private float currentCooldown;

    private List<Box> activeBoxes = new List<Box>();

    public void DestroyedBox(Box destroyedBox)
    {
        activeBoxes.Remove(destroyedBox);
        Destroy(destroyedBox.gameObject);
    }

    private void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0)
        {
            foreach (var boxParent in belts)
            {
                Box box = Instantiate(boxPrefab);
                box.Setup(boxParent);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
            gameObject.SetActive(false);
    }

    private void ResetCooldown()
    {
        currentCooldown = spawnDelay;
    }

    private void Awake()
    {
        ResetCooldown();
    }
}
