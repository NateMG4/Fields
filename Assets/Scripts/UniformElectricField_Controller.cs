using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniformElectricField_Controller : MonoBehaviour
{
    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders () { return colliders; }

    private void OnTriggerEnter (Collider other) {
        Debug.Log(other.gameObject.name);
        if (!colliders.Contains(other)) { 
            colliders.Add(other); 
            Debug.Log(other.gameObject.name);
        }
    }

    private void OnTriggerExit (Collider other) {
        colliders.Remove(other);
    }
 
    public Vector3 fieldVector = new Vector3(5f, 0, 0);
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach (Collider c in colliders){
            Vector3 vel = c.attachedRigidbody.velocity;
            Particle_Controller particle = c.gameObject.GetComponent<Particle_Controller>();
            Vector3 force = fieldVector * particle.charge;
            Debug.DrawRay(c.gameObject.transform.position, fieldVector, Color.green);
            Debug.DrawRay(c.gameObject.transform.position, force, Color.blue);
            particle.addForceToParticle(force, true);
        }
    }
}
