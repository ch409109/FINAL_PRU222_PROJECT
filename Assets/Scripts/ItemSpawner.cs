using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Sprite[] itemSprite;

    [SerializeField] BoxCollider2D cd;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] private float cooldown;
    private float timer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = cooldown;

            GameObject newTarget = Instantiate(itemPrefab);

            float randomX = Random.Range(cd.bounds.min.x, cd.bounds.max.x);

            newTarget.transform.position = new Vector2(randomX, transform.position.y);

            int randomIndex = Random.Range(0, itemSprite.Length);
            newTarget.GetComponent<SpriteRenderer>().sprite = itemSprite[randomIndex];
        }
    }
}
