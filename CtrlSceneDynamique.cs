using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlSceneDynamique : MonoBehaviour
{


    /*

    Script de changement des éléments dynamiques de l'arène
    par: Alexis Rochon
    modifié le: 04-11-2020
    
    */

    private float delaiElemArene;       // Délai aléatoire entre chaque changement des éléments de l'arène
    public int[] numElem;   // Tableau contenant le nombre de "Sets" à changer 


    /********-----   SET 1   -----************/

    public GameObject batisse1;//-----------// structure 1 (maison) / set 1
    private Animator animateurBatisse1;     // Animator de structure 1 / set 1      
    public GameObject batisse2;//-----------// structure 2 (colonnes) / set 1
    private Animator animateurBatisse2;     // Animator de structure 2  / set 1
    public GameObject volets1;              // Les volets qui se ferment et s'ouvrent du set 1
    private Animator animateurVolets1;      // Animator des volets / set 1
    public GameObject plateforme1;          // Structure 3 (Plateforme) / set 1
    private Animator animateurPlateforme1;  // Animator de structure 3 / set 1




/*-------------------------------------------------   START       -------------------------------------------------------------*/
/* Cette fonction associe les animateurs aux variables, Choisi un délai aléatoire de départ, crée le tableau pour y loger les 8 "Sets" et appelle la fonction  delaiChangementScene()*/

    void Start()
    {

        /********-----   SET 1   -----************/

        /* On asigne les composants aux variables*/
        animateurBatisse1 = batisse1.GetComponent<Animator>();
        animateurBatisse2 = batisse2.GetComponent<Animator>();
        animateurVolets1 = volets1.GetComponent<Animator>();
        animateurPlateforme1 = plateforme1.GetComponent<Animator>();

        delaiElemArene = Random.Range(8f, 9f);      // Délai de départ avant le premier changement de décors
        Invoke("delaiChangementScene", 0.1f);       // On invoque la fonction DELAICHARGEMENTSCENE() sur le champs

        numElem = new int[8];   // On déclare qu'il y a 8 "Sets" à changer dans l'arène
    
    }
    /*---------------------------*/




    /*------------------------------------     delaiChangementScene()      --------------------------------------------------*/
    /* Cette fonction appelle changementScene() après que le délai de départ se soit écoulé */

    private void delaiChangementScene()
    {
        Invoke("changementScene", delaiElemArene);      // Le changement de décors s'exécute après le délai initial
    }
  /*------------------------------------------*/




    /*------------------------------------     changementScene()      --------------------------------------------------*/
    /* Cette fonction Modifie le décors (Chaque "Sets" de structures vont être changés aléatoirement)  */
    private void changementScene()
    {

        /* Une boucle pour assigner à chaque "Set" une structure active*/
        for(int i = 0; i < 1; i++)
        {
            float chiffreHasardDec = Random.Range(1f, 3f);      // Choix aléatoire d'un état de set entre 3 sets différents 
                                                                // (Les 8 sets ont chacun 3 états différents/ 3 structures / position)
            int chiffreHasardEntier = Mathf.RoundToInt(chiffreHasardDec);   // En faire un nombre entier
            numElem[i] = chiffreHasardEntier;       // Attribuer l'état au Set en question
        }


        /*****        LES CHANGEMENTS       ******/

        /*----- SET_1   -----*/
        if(numElem[0] == 1 && !animateurBatisse1.GetBool("Monte"))
        {
            animateurBatisse1.SetBool("Baisse", false);
            animateurBatisse1.SetBool("Monte", true);
            animateurBatisse2.SetBool("Monte", false);
            animateurBatisse2.SetBool("Baisse", true);
            animateurPlateforme1.SetBool("Monte", false);
            animateurPlateforme1.SetBool("Baisse", true);
            animateurVolets1.SetBool("Monte", false);
            animateurVolets1.SetBool("Baisse", true);
        }

        else if(numElem[0] == 2 && !animateurBatisse2.GetBool("Monte"))
        {
            animateurBatisse1.SetBool("Baisse", true);
            animateurBatisse1.SetBool("Monte", false);
            animateurBatisse2.SetBool("Monte", true);
            animateurBatisse2.SetBool("Baisse", false);
            animateurPlateforme1.SetBool("Monte", false);
            animateurPlateforme1.SetBool("Baisse", true);
            animateurVolets1.SetBool("Monte", true);
            animateurVolets1.SetBool("Baisse", false);


        }

        else if(numElem[0] == 3 && !animateurPlateforme1.GetBool("Monte"))
        {
            animateurBatisse1.SetBool("Baisse", true);
            animateurBatisse1.SetBool("Monte", false);
            animateurBatisse2.SetBool("Monte", false);
            animateurBatisse2.SetBool("Baisse", true);
            animateurPlateforme1.SetBool("Monte", true);
            animateurPlateforme1.SetBool("Baisse", false);
            animateurVolets1.SetBool("Monte", false);
            animateurVolets1.SetBool("Baisse", true);
        }


        
        delaiElemArene = Random.Range(8f, 9f);      // Nouveau délai aléatoire avant le prochain chamgement du décors
        Invoke("delaiChangementScene", 0.1f);

    }
  /*-------------------------------------*/



}
