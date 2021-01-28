using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script contenant la gestion d'éliminations
 * Lorsque qu'un joueur élimine quelqu'un, il recevra un point
 * Créer par Abdirahman Omar */

public class elimination : MonoBehaviour
{
  
  // le joueur qui recoit le point
  public int Joueur = 1;

    // le compteur de score
    public GestionPoints CompteurScore;

    public void ChercherPoints()
    {
        // quand un joueur elimine quelqu'un, il recevera un point
        CompteurScore.AjouterPoints(Joueur);
    }
}
