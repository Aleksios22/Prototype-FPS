using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script qui gere le fonctionnement des plateformes situant dans le niveau 
//Creér par Abdirahman Omar et modifié par Alexis Rochon

public class Plateforme : MonoBehaviour
{
    public GameObject joueur1; // contiendra la référence du joueur1
    public GameObject joueur2; // contiendra la référence du joueur2

    private void OnTriggerEnter(Collider infoCollision)
    {
        // Quand le joueur est sur une plateforme, ce plateforme sera le parent du personnage 
        if (infoCollision.gameObject == joueur1)
        {
            joueur1.transform.parent = transform;
        }
        if (infoCollision.gameObject == joueur2)
        {
            joueur2.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider infoCollision)
    {
        // Quand le joueur quitte la plateforme, la plateforme ne sera plus le parent du personnage 
        if (infoCollision.gameObject == joueur1)
        {
            joueur1.transform.parent = null;
        }
        if (infoCollision.gameObject == joueur2)
        {
            joueur2.transform.parent = null;
        }
    }


}
