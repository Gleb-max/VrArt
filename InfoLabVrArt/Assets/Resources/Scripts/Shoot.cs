using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{   
    public Transform cannonball;

    public int cannonballSpeed;
    private AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void Shot()
    {
        Transform prefab = Instantiate(cannonball, transform.position, transform.rotation);
        audioData.Play(0);
        prefab.GetComponent<Rigidbody>().AddForce(transform.forward * cannonballSpeed * Time.deltaTime);
    }
}
