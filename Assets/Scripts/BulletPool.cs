using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    private List<Bullet> pool = new();
    public GameObject bulletPrefab;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Bullet GetBulletFromPool()
    {
        for (var i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }

        var result = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
        pool.Add(result);
        return result;
    } 
}
