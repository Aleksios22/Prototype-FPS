using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Script permettant de gerer l'image du viseur du Sniper 
 * Creér par Abdirahman Omar
 */
public class Scope : MonoBehaviour
{

    public string BoutonViser = "Fire2"; // Input pour viser

    public GameObject ViseurSniper; // Image du viseur du sniper
    public GameObject cameraArmes; // La camera des armes
    public Camera mainCamera; // La camera fps
    public static bool sniperVise = false; // Boolean pur savoir si le sniper vise

   

    public float zoom = 15f; // Float pour faire zoomer la vision
    private float zoomOut = 60f; // Float pour faire revenir la vision a son état normal


   

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButton(BoutonViser))
        {
            // Le sniper vise
            sniperVise = true;
            // Appeler la fonction de vise apres un certain temps
            StartCoroutine(Zoom());
         
        }
        else
        {
            //Désactiver le viseur du sniper
            ViseurSniper.SetActive(false);

            // Réactiver la camera des armes
            cameraArmes.SetActive(true);

            // Le sniper ne vise plus
            sniperVise = false;

           

            // Remettre la vision à son état d'origine
            mainCamera.fieldOfView = 60f;
        }
    }

    // Fonction pour faire apparaitre le viseur du sniper
   IEnumerator Zoom()
    {
        // Attendre un peu pour que le viseur apparait
        yield return new WaitForSeconds(.25f);

        //Désactiver la caméra des armes
        cameraArmes.SetActive(false);

        // Afficher l'image du sniper
        ViseurSniper.SetActive(true);

        

        // Zoomer la caméraFps du joueur
        zoomOut = mainCamera.fieldOfView;
        mainCamera.fieldOfView = zoom;
    }

}
