using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*Script pour permettre de gérer le système de point des personnages
 * Créer par Abdirahman Omar et Jean-Luc Ho*/


public class GestionPoints : MonoBehaviour
{

  public int LimitScore = 5; // Le score limite

  private int ScoreJoueur1 = 0; // Le score du joueur 1
  private int ScoreJoueur2 = 0; // Le score du joueur 2


  public Text ScoreUIJoueur1; // Texte contenant le score du joueur 1
  public Text ScoreUIJoueur2; // Texte contenant le score du joueur 2

  public static bool Joueur1Gagne;
  public static bool Joueur2Gagne;

 



  public void AjouterPoints(int joueur)
  {
    // si le joueur 1 élimine quelqu'un, il recevra un point
    if (joueur == 1)
    {
      ScoreJoueur1++;
    }

    // si le joueur 2 élimine quelqu'un, il recevra un point
    else if (joueur == 2)
    {
      ScoreJoueur2++;
    }


    // Verifier si les joueurs ont atteint le score limit
    if (ScoreJoueur1 >= LimitScore || ScoreJoueur2 >= LimitScore)
    {
      // si le joueur 1 a atteint le score limit avant le joueur 2
      if (ScoreJoueur1 > ScoreJoueur2)
       
         
        Joueur1Gagne = true;
        Invoke("chargementScene", 4f);
          
     
       // si le joueur 2 a atteint le score limit avant le joueur 1
      if (ScoreJoueur2 > ScoreJoueur1)
    
        Joueur2Gagne = true;
        // Invoquer la scene de fin
        Invoke("chargementScene", 4f);
    }

        // Incrémenter le score du joueur 1 au texte du score du joueur 1
        ScoreUIJoueur1.text = ScoreJoueur1.ToString();

        // Incrémenter le score du joueur 2 au texte du score du joueur 2
        ScoreUIJoueur2.text = ScoreJoueur2.ToString();
  }

    // Fonction qui permet gerer la scene de fin 
    void chargementScene()
    {
        // Remettre le cursor
        Cursor.lockState = CursorLockMode.None;

        // Charger la scene de fin
        SceneManager.LoadScene("SceneFin");
    }



}
