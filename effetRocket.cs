using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effetRocket : MonoBehaviour
{

    public GameObject particulesHit;
    private float delay = 0.8f;


    Rigidbody rocket;

    void Start()
    {
        rocket = GetComponent<Rigidbody>();

    }


    public void OnCollisionEnter(Collision infoCollision) // le type de la variable est Collision
    {
        // Si la balle touche l'environnement 
        if (infoCollision.gameObject.tag == "untagged")
        {

            GameObject cloneParticule = Instantiate(particulesHit, transform.position, transform.rotation);
            cloneParticule.SetActive(true);
            Destroy(gameObject);
            Destroy(cloneParticule, delay);

        }


        // Si la balle touche un ennemi 
        if (infoCollision.gameObject.tag == "Player")
        {

            GameObject cloneParticule = Instantiate(particulesHit, transform.position, transform.rotation);
            cloneParticule.SetActive(true);
            Destroy(gameObject);
            Destroy(cloneParticule, delay);


        }


    }



}