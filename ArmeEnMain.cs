using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArmeEnMain : MonoBehaviour
{

    public int armeChoisi; // Position de l'arme qui se touve dans le parent où se trouve ce script

    public string InputArme; // Input dans l'inspecteur permettant de changer l'arme à l'appuis du bouton

    static GameObject WeaponInHand; // l'arme entrain d'être tenu

    // Update is called once per frame
    void Update()
    {

        // L'arme en main est égale a l'arme choisi
        int armeEnMain = armeChoisi;

    }
}
