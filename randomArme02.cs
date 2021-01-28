using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Script permettant de changer d'arme en aléatoire
 * Créer par Fabrizio Herrera Segovia*/

public class randomArme02 : MonoBehaviour
{

    public int armeChoisi = 0; // Position de l'arme qui se touve dans le parent oû se trouve ce script

    public string InputArme; // Input dans l'inspecteur permettant de changer l'arme à l'appuis du bouton

    public static GameObject WeaponInHand; // l'arme entrainn d'être tenu


    //public static GameObject;


    public GameObject Advertissement; // Texte qui averti le changement


    public AudioClip SwitchGun;

    AudioSource AudioSwitchGun;

    // Start is called before the first frame update
    void Start()
    {

        AudioSwitchGun = GetComponent<AudioSource>();

        //selectionArme();

        Invoke("summonGuns", armeChoisi); //ce qui permet d'invoker le void qui permettre de choisir une arme aléatoirement



        // weaponInHand = GameObject.Find("Armes");//Pour trouver en main


        Advertissement.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        


        //weaponInHand = armeChoisi;//Pour trouver en main





        // Si le joueur recharge, on arrete la fonction ici
        if (fonctionnementArme.enRechargement == true)
        {
            return;
        }

        // Si le joueur vise on arrête la fonction ici
        if (fonctionnementArme.joueurVise == true)
        {
            return;
        }

        // Si le joueur vise on arrête la fonction ici
        if (Scope.sniperVise == true)
        {
            return;
        }



        // L'arme en main est égale a l'arme choisi
        int armeEnMain = armeChoisi;

        //int armeEnMain = static WeaponInHand;
        //static WeaponInHand = armeEnMain;

        //static weaponInHand == armeChoisi;


        // Si l'arme en main depasse le le nombre maximum d'armes, on remet l'arme qui se trouve dans la position zero du parent
        if (armeChoisi >= transform.childCount - 1)
                armeChoisi = 0;

            // Sinon on incrémente les armes
            else armeChoisi++;
            

    }

    void messageRandom()//le void qui donne le message de changement d'arme
    {


        Advertissement.SetActive(true); // on montre le message que le changement arrive
        StartCoroutine(PutMessageAway(2f)); // On déclenche la coroutine PutMessageAway

        //Invoke("messageRandom", 12f);
    }

    void summonGuns()//le void exact de donner une arme a la main du joueur aléatoirement
    {
        armeChoisi = Random.Range(0, 8); //ce qui permet d'invoker le sorte d'armes a avoirr

        // Jouer le son de chnagement
        AudioSwitchGun.PlayOneShot(SwitchGun);

        int i = 0;
        foreach (Transform arme in transform)
        {
            // Activer l'arme que le joueur a en main
            if (i == armeChoisi)
                arme.gameObject.SetActive(true);

            // Désactiver l'arme que le joueur n'a pas en main
            else arme.gameObject.SetActive(false);
            i++;

        }





        Invoke("summonGuns", 15f);//le délai pour faire ensorte ne chnage pas a chaque seconde, apres ce delai, l'arme tenu du joueur changera
                                  //Invoke("summonGuns", 30f);

        Invoke("messageRandom", 12f);// le delai de faire que le changement d'arme arrive
    }




    IEnumerator PutMessageAway(float tempsAttente)//permet d'enlever le message
    {

        yield return new WaitForSeconds(3f);
        Advertissement.SetActive(false);
        


    }



}
