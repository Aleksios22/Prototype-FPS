using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Script qui le fonctionnement de la vie du personnage
 * Ce script gere aussi la gestion de mort et de la réapparition du personnage
 * Créer par : Abdirahman Omar et quelque modification par : Fabrizio
*/
public class GestionVies : MonoBehaviour
{
    public static float Vies; // Le nombre de vie que le joueur aura au départ
    float vieMaximum = 100;
    float medKit = 30;
    

    public Image damageScreen; // L'écran qui apparait lentement lorsque le joueur recoit des dégats

    // public GameObject WarningMessage = false; // 

    Color alphaColor; // La couleur de l'image

    public GameObject armes;  // L'arme en main
    public GameObject CameraMort;    // contiendra la référence à la caméra qui visera le personnage quand il sera mort
    public GameObject CameraJoueur; // La camera du joueur
    public GameObject RepairEffect; //l'effet du boost

    public GameObject WarningMessage;

    public AudioClip sonMort; // Le son de mort du personnage

    public static bool estMort = false;

    //public bool RepairMode = false; // la fonction  qui permettra d'activer le mode repair

    public Text vieDuPersonnage; // Texte contenant la vie du personnage

    //public Text WarningMessage; // Texte qui dira que la vie du personnage est remplie

    Animator animMort;
    AudioSource AudioPersonnage;
    CharacterController characterController;

    void Start()
    {
        animMort = GetComponent<Animator>();

        AudioPersonnage = GetComponent<AudioSource>();

        characterController = GetComponent<CharacterController>();

        characterController.detectCollisions = true;

        Vies = vieMaximum;

        //WarningMessage.gameObject.SetActive(false);

        // La couleur de l'image 
        alphaColor = damageScreen.color;
    }

    

    //Fonction qui permet que le joueur recoit des dégats
    public void prendDegats(float quantite)
    {


        // Diminuer la vie du personnage selon le dégat de l'arme
        Vies -= quantite;

        //Afficher la vie du personnage
        vieDuPersonnage.text = Vies.ToString() + "%";

        // Si la vie es inférieur à 75%, on fait apparaitre un peu l'image de dégat
        if (Vies <= 75f)
        {
            alphaColor.a = .2f;
            damageScreen.color = alphaColor;
        }

        // Si la vie es inférieur à 50%, on fait apparaitre moyennement l'image de dégat
        if (Vies <= 50f)
        {
            alphaColor.a = .4f;
            damageScreen.color = alphaColor;
        }

        // Si la vie es inférieur à 25%, on fait apparaitre complètement l'image de dégat
        if (Vies <= 25f)
        {
            alphaColor.a = 1f;
            damageScreen.color = alphaColor;
        }

        // Si le joueur n'a plus de vie, alors on appelle la fonction défaite
        if (Vies <= 0f)
        {

            Defaite();
        }



    }

    public void Defaite()
    {
        // Désactivé le controlleur
        characterController.enabled = false;

        // Jouer l'animation de la mort
        animMort.GetComponent<Animator>().SetBool("mort", true);


        // Le joueur est mort
        estMort = true;

        // Activer la camera de mort
        CameraMort.SetActive(true);

        // Desactiver la camera du joueur
        CameraJoueur.SetActive(false);

        // Desactiver le texte de vie
        vieDuPersonnage.enabled = false;

        // Jouer le son de mort
        AudioPersonnage.PlayOneShot(sonMort);

        // Invoquer la fonction du respawn après 4 secondes
        Invoke("Reapparition", 4f);




    }
    float x;
    float y;
    float z;

    void Reapparition()
    {

        // Réactiver le controlleur
        characterController.enabled = true;

        // Le joueur n'est plus mort
        estMort = false;

        // Désactiver la camera de mort
        CameraMort.SetActive(false);

        // Réactiver la camera du joueur
        CameraJoueur.SetActive(true);

        //Remmettre sa vie à 100
        Vies = 100;

        // Mettre des positions aléatoire en x 
        x = Random.Range(-10, 10);

        //Mettre des positions aléatoire en y
        y = Random.Range(0, 10);

        //Mettre des positions aléatoire en z
        z = Random.Range(-10, 10);

        // Positionner le joueur de faacon aléatoire sur la scene de jeu
        transform.position = new Vector3(x, 0, z);

        // Mettre sa rotation à zéro
        transform.rotation = Quaternion.Euler(0, 0, 0);

        // Arrêter son animation de mort
        animMort.GetComponent<Animator>().SetBool("mort", false);


        // Ne plus afficher l'image de dégat
        alphaColor.a = 0f;
        damageScreen.color = alphaColor;

        //Afficher la vie du personnage
        vieDuPersonnage.text = Vies.ToString() + "%";

        // Réactiver le texte de vie
        vieDuPersonnage.enabled = true;



    }
    

    void OnControllerColliderHit(ControllerColliderHit infoCollision)
    {

        ///mettre des comentaire

        if (infoCollision.gameObject.tag == "vie")
        {
            
            if (Vies == 100f) //si la vie du joueur est complet
            {

                WarningMessage.SetActive(true); // on montre le message que la vie est complet
                StartCoroutine(PutMessageAway(1f)); // On déclenche la coroutine PutMessageAway
            }

            else if (Vies >= 71f) // si la vie du joueur est plus haut que 70 pourcentage
            {

                //ajouter un if
                infoCollision.gameObject.SetActive(false); //déactiver le repairKit ongjet

                Vies = vieMaximum;// on remmet la vie a 100%

                vieDuPersonnage.text = Vies.ToString() + "%";

                RepairEffect.SetActive(true); // creer l'effet de lumiere de repair
                StartCoroutine(effetDown(1f)); // On déclenche la coroutine effetDown
            }

            else if (Vies <= 70f)  // si la vie du joueur est plus bas que 70 pourcentage
            {

                //ajouter un if
                infoCollision.gameObject.SetActive(false);

                Vies += medKit; // on donne 30 points de vie
                //Vies = Vies + 30;
                vieDuPersonnage.text = Vies.ToString() + "%";

                RepairEffect.SetActive(true); // creer l'effet de lumiere de repair
                StartCoroutine(effetDown(1f)); // On déclenche la coroutine effetDown
            }


        }

        // Si le joueur touche la lave, il perdera des vies
        if (infoCollision.gameObject.tag == "lave")
        {

            Vies -= 0.1f;
            vieDuPersonnage.text = Vies.ToString() + "%";

          

            if (Vies <= 0f) //si la vie du joueur est à zéro, il meurt
             {
                Defaite();
            }
        }
    }



 



    IEnumerator effetDown(float tempsAttente)//permet de déactiver le(s) effet(s)
    {

        yield return new WaitForSeconds(3f);
        RepairEffect.SetActive(false); // déactiver l'effet de repair
    }



    IEnumerator PutMessageAway(float tempsAttente)//permet d'enlever le message
    {

        yield return new WaitForSeconds(2f);
        WarningMessage.SetActive(false);
    }

    


}
