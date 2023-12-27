using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    public int pointValue = 5;
    public ParticleSystem explosionParticle;

    Rigidbody rb;
    GameManager gm;
    float minSpeed = 12;
    float maxSpeed = 16;
    float maxTorque = 10;
    float xRange = 4;
    float ySpawnPos = -2;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();

        rb.AddForce(Vector3.up * RandomForce(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (!gm.isGameActive)
        {
            return;
        }

        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gm.UpdateScore(pointValue);
    }

    // The sensor is the only game object that is a trigger, so this method
    // will only run when a target enters the sensor.
    void OnTriggerEnter()
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad"))
        {
            gm.GameOver();
        }
    }

    float RandomForce()
    {
        return Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
