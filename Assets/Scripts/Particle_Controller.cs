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
    public float emissionIntensityScalar = 1;
    public float forceScalar = 1;
    public Vector3 resultantForce;
    Material mymat;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        mymat = GetComponent<Renderer>().material;
        
        if(randomCharge){
            charge = Random.Range(-10f, 10f);
        }


       
    }

    // Update is called once per frame
    void Update()
    {
        mymat.SetColor("_EmissionColor", new Color(Mathf.Clamp(charge * emissionIntensityScalar, 0f, 255f), 0f, 
        Mathf.Abs(Mathf.Clamp(charge * emissionIntensityScalar, -255f, 0f))));

        particles = GameObject.FindGameObjectsWithTag("Particle");

        resultantForce = new Vector3(0,0,0);

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
            resultantForce += force;
        }
        Debug.DrawRay(this.transform.position, resultantForce);
        rigidbody.AddForce(resultantForce);
    }
}
