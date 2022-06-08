using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class Particle_Controller : MonoBehaviour
{
    Rigidbody rigidbody;
    public GameObject[] particles;
    public float charge = 1f;
    public bool randomCharge = false;
    public bool randomVelocity = false;
    public Vector3 randomVelocityVector = new Vector3(0,0,-1);
    public float emissionIntensityScalar = 1;
    public float forceScalar = 1;
    public Vector3 startingVel = new Vector3(0,0,0);
    public float velocityMag = 0;
    public Vector3 resultantForce;
    public bool interactWithOtherParticals = true;
    Material mymat;
    // Start is called before the first frame update
    void Start()
    {
        resultantForce = new Vector3(0,0,0);
        rigidbody = GetComponent<Rigidbody>();
        mymat = GetComponent<Renderer>().material;
        // rigidbody.AddForce()
        if(randomVelocity){
            startingVel = randomVelocityVector * Random.Range(0f,10f);
        }
        rigidbody.velocity = startingVel;
        if(randomCharge){
            charge = Random.Range(-10f, 10f);
        }



       
    }

    // Update is called once per frame
    void Update()
    {
        velocityMag = rigidbody.velocity.magnitude;
        mymat.SetColor("_EmissionColor", new Color(Mathf.Clamp(charge * emissionIntensityScalar, 0f, 255f), 0f, 
        Mathf.Abs(Mathf.Clamp(charge * emissionIntensityScalar, -255f, 0f))));
        if(interactWithOtherParticals){
            particles = GameObject.FindGameObjectsWithTag("Particle");

        }
        foreach (GameObject particle in particles){
            if(particle == this.gameObject)
            {
                continue;
            }
            Particle_Controller particleScript = particle.GetComponent<Particle_Controller>();

            Vector3 vector = particle.transform.position - this.transform.position;
            Vector3 unitVector = vector / vector.magnitude;

            Vector3 force = unitVector * (Mathf.Abs(particleScript.charge*this.charge)/vector.magnitude) 
                * -Mathf.Clamp(particleScript.charge*this.charge,-1,1);
            addForceToParticle(force, true);
        }
        Debug.DrawRay(this.transform.position, resultantForce);
        if(resultantForce.magnitude > 0){
            // Debug.Log(resultantForce);
        }
        rigidbody.AddForce(resultantForce);
        resultantForce = new Vector3(0,0,0);
    }
    
    public void addForceToParticle(Vector3 force, bool changeVelMag){
        float prevVelMag = rigidbody.velocity.magnitude;
        if(changeVelMag){
            resultantForce += force;
        }
        else{
            Vector3 v = rigidbody.velocity + (force/rigidbody.mass)*Time.fixedDeltaTime;
            Vector3 test = v.normalized * prevVelMag;
            rigidbody.velocity = test;
        }
    }
}
