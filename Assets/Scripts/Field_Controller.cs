using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class Particle_Controller : MonoBehaviour
{

    public class ColliderContainer : MonoBehaviour {

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders () { return colliders; }

    private void OnTriggerEnter (Collider other) {
        if (!colliders.Contains(other)) { colliders.Add(other); }
    }

    private void OnTriggerExit (Collider other) {
        colliders.Remove(other);
    }
 
    public Vector3 fieldVector = new Vector3(5f, 0, 0);
    

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject c in colliders){
            Vector3 vel = c.attachedRigidbody;
            Particle_Controller particle = c.GameObject.GetComponent<Particle_Controller>();
            Vector3 force = Vector3.Cross(fieldVector, vel) * particle.charge;
            c.attachedRigidbody.AddForce(force);
        }
    }

}