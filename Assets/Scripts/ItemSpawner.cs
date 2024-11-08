using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] BoxCollider2D cd;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject poopPrefab;
    [SerializeField] private float cooldown;
    private float timer;

    void Start()
    {
        timer = cooldown;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = cooldown;

            bool spawnHeart = Random.value > 0.5f;

            GameObject newObject;
            if (spawnHeart)
            {
                newObject = Instantiate(heartPrefab);
            }
            else
            {
                newObject = Instantiate(poopPrefab);
            }

            float randomX = Random.Range(cd.bounds.min.x, cd.bounds.max.x);
            newObject.transform.position = new Vector2(randomX, transform.position.y);

            Collider2D newObjectCollider = newObject.GetComponent<Collider2D>();
            if (newObjectCollider != null)
            {
                Collider2D spawnCollider = cd;
                Physics2D.IgnoreCollision(newObjectCollider, spawnCollider);

                IgnoreCollisionWithTags(newObjectCollider, new string[] { "Bullet1", "SuperBullet1", "Bullet2", "SuperBullet2" });
            }
        }
    }

    void IgnoreCollisionWithTags(Collider2D newObjectCollider, string[] tags)
    {
        foreach (string tag in tags)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject taggedObject in taggedObjects)
            {
                Collider2D taggedObjectCollider = taggedObject.GetComponent<Collider2D>();
                if (taggedObjectCollider != null)
                {
                    Physics2D.IgnoreCollision(newObjectCollider, taggedObjectCollider);
                }
            }
        }
    }
}
