using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script permettant de faire fonctionner l'arme du double rocket,
 * Ceci est une modififcation du script fonctionnement arme d'Abdirahman
 * Créer par: Fabrizio Herrera Segovia*/

public class fonctionnementBazooka : MonoBehaviour
{

    [Header("Inputs")]
    public string BoutonTir = "Fire1";
    public string BoutonTir2 = "Fire2"; //tirer la deuxieme rocket
    public string BoutonRecharger = "Fire3";

    public Camera CameraFps;  // La caméra du personnage auquelle est attaché ce script
    public GameObject viseur;  // Image du viseur
    public GameObject PointDeTir;

    public GameObject effetCollision; // Contient la reference de l'eefet de collision

    public AudioClip recharge;
    public AudioClip tirBalle;

    public float Degats;     // Degat de l'arme
    public float range;     // La portée de l'arme
    public float cadenceDeTir;   // La cadence de tir
    public float TempsDeRechargement;  // Le temps de chargement

    public static bool enRechargement = false; // Savoir si l'arme recharge
    public static bool estEliminer = false; // Savoir si le joueur est éliminé
    public static bool joueurVise = false;  // Savoir si le joueur vise

    public int maxAmmo;  // Le nombre maximun du munition de l'arme
    private int munitionCourrante;  // Le nombre de munition courante

    private float prochainTir = 0f; // La prochaine fois que l'on peut tirer.

    Animator animGun;
    AudioSource AudioGun;

    public Text munitionDeArme; // Texte contenant la munition de l'arme

    public GameObject rocketPrefab;
    public GameObject fumee;

    void Start()
    {
        // Au début de la partie ou au début d'un changement d'arme, les munitions vont être égale au max de munitions
        munitionCourrante = maxAmmo;

        // Chercher l'animation de l'arme
        animGun = GetComponent<Animator>();

        //Chercher l'audio de l'arme
        AudioGun = GetComponent<AudioSource>();


    }


    // Update is called once per frame
    void Update()
    {

        //Afficher les munitions dans le jeu
        munitionDeArme.text = munitionCourrante.ToString() + "/" + maxAmmo;


        // Si l'arme recharge, on arrete la fonction
        if (enRechargement)
            return;

        // Si la munition de l'arme tombe à zéro, le joueur sera oubligé de recharger l'arme
        if (munitionCourrante <= 0)
        {
            StartCoroutine(Rechargement());
            return;
        }



        // Le joueur pourra recharger par lui même si il appuis sur le bouton de rechargement  quand ses munitions ne sont pas au max
        if (Input.GetButtonDown(BoutonRecharger) && munitionCourrante != maxAmmo)
        {

            StartCoroutine(Rechargement());
            return;
        }


        // Si le joueur appuis sur le bouton de tir, on appelle la fonction de tir, donc on tire la rocket de gauche
        if (Input.GetButton(BoutonTir) && Time.time >= prochainTir)
        {

         


            // Tirer selon la cadence de tir
            prochainTir = Time.time + 1f / cadenceDeTir;
            Tirer();

            // On instancie un flasheur pour l'arme
            GameObject cloneEffet = Instantiate(fumee, fumee.transform.position, fumee.transform.rotation);
            cloneEffet.SetActive(true);

            // Detruire le projectile après 1 seconde
            Destroy(cloneEffet, .1f);

            // Jouer le son de coup de feu
            AudioGun.Play();

        }



        // Si le joueur appuis sur le deuxieme bouton de tir (tir02), on tir l'autre rocket de droite
        if (Input.GetButton(BoutonTir2) && Time.time >= prochainTir)
        {

            // Tirer selon la cadence de tir
            prochainTir = Time.time + 1f / cadenceDeTir;
            Tirer();


            // Jouer le son de coup de feu
            AudioGun.Play();

        }

  

    }


    // Fonction permettant de tirer avec l'arme
    void Tirer()
    {

        GameObject cloneProjectille = Instantiate(rocketPrefab, PointDeTir.transform.position, rocketPrefab.transform.rotation);
        cloneProjectille.SetActive(true);
        cloneProjectille.GetComponent<Rigidbody>().velocity = rocketPrefab.transform.forward * 30;
        Destroy(cloneProjectille, 3f);


        // Quand on appelle la fonction de tir, on diminue les munitions
        munitionCourrante--;

        // Jouer le son de balle
        AudioGun.PlayOneShot(tirBalle);

        RaycastHit hitInfo;
        if (Physics.Raycast(CameraFps.transform.position, CameraFps.transform.forward, out hitInfo, range))


        {

            // Tous ceux qui auront un script GestionVies seront une cible
            GestionVies cible = hitInfo.transform.GetComponent<GestionVies>();

            // Si le raycast touche une cible ayant le script GestionVies
            if (cible != null)
            {
                // On lui inflige des dégats selon le dégat de l'arme
                cible.prendDegats(Degats);

                // Si la cible est éliminé, nous allons envoyer un message à la fonction ChercherPoints qui se trouve dans le script eliminations pour accorder un point au joueur
                if (GestionVies.estMort == true)
                {
                    cible.SendMessage("ChercherPoints", SendMessageOptions.DontRequireReceiver);

                }


            }
            // Créer une particle d'impact lorsque la balle aurait toucher quelque chose
            GameObject impact = Instantiate(effetCollision, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(impact, 3f);
        }
    }






    IEnumerator Rechargement()
    {
        // L'arme recharge
        enRechargement = true;

        // Jouer l'animation de rechargement
        animGun.GetComponent<Animator>().SetBool("recharger", true);

        // Jouer l'audio
        AudioGun.PlayOneShot(recharge);

        //Attendre que l'arme se recharge avant que l'on puisse tirer à nouveau
        yield return new WaitForSeconds(TempsDeRechargement);


        animGun.GetComponent<Animator>().SetBool("recharger", false);

        munitionCourrante = maxAmmo;

        enRechargement = false;

    }



}
