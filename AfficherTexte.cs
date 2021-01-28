using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AfficherTexte : MonoBehaviour
{
    public GameObject texteJoueur1Gagne; // contiendra la référence du texte 
    public GameObject texteJoueur2Gagne; // contiendra la référence du texte 

    // Update is called once per frame
    void Update()
    {
        // si le joueur 1 gagne, on affiche le resultat final
        if (GestionPoints.Joueur1Gagne == true)
        {
            texteJoueur1Gagne.SetActive(true);
            texteJoueur2Gagne.SetActive(false);
        }

        // si le joueur 2 gagne, on affiche le resultat final
        if (GestionPoints.Joueur2Gagne == true)
        {
            texteJoueur2Gagne.SetActive(true);
            texteJoueur1Gagne.SetActive(false);
        }

       
    }
}
