using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZSpawner : MonoBehaviour
{
    public GameObject zombie;
    public float startTime;
    private float timeBtw;
    public bool hasSpawned;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        timeBtw = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtw <= 0)
        {
            if (Vector3.Distance(transform.position,target.position) <= 10)
            {
                Instantiate(zombie, transform.position, Quaternion.identity);
                hasSpawned = true;
                timeBtw = startTime;
            }
        }
        else
        {
            timeBtw -= Time.deltaTime;
        }
        if (hasSpawned == true)
        {
            StartCoroutine(Despawn());
        }
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5f);
        Destroy(zombie);
    }
}
