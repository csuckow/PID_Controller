using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TeleportOnTrigger : MonoBehaviour
{
    [SerializeField]
    float range = 100;

    private void OnTriggerEnter(Collider other) {
        float randX = Random.Range(0, range);
        float randY = Random.Range(0, range);
        float randZ = Random.Range(0, range);
        transform.position = new Vector3(randX, randY, randZ);
    }
}
