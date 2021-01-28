using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script qui contiendra le contrôle de la cameraFps du joueur
   Créer par : Abdirahman Omar
*/
public class CameraFps : MonoBehaviour
{
        public string MouseXInput;
        public string MouseYInput;
       
        public float sensibiliteSouris;
        
        public Vector2 FPS_MinMaxAngles;// Angle minimun et maximum

        Transform FPSController; // Transformer la caméra selon la position du controlleur du joueur
        float positionX;
        

        private void Awake()
        {
            // Enlever le curseur
            Cursor.lockState = CursorLockMode.Locked;

            // Position initiale de la caméra
            positionX = 0;

            // Chercher le controlleur qui se trouve dans le script du parent de la cameraFps
            FPSController = GetComponentInParent<mouvementJoueur>().transform;
        }

     

        // Update is called once per frame
        void Update()
        {
        //Chercher la fonction de la rotation de la caméra 
        rotationCamera();
        }

      
        /* Fonction permettant de faire bouger la caméra */
        void rotationCamera()
        {
            // Chercher l'input du controle vertical
            float mouseX = Input.GetAxis(MouseXInput) * (sensibiliteSouris * Time.deltaTime); 

            // Chercher l'input du controle honrizontal
            float mouseY = Input.GetAxis(MouseYInput) * (sensibiliteSouris * Time.deltaTime);
 
            Vector3 angleRotation = transform.eulerAngles;

            positionX += mouseY;

            positionX = Mathf.Clamp(positionX, FPS_MinMaxAngles.x, FPS_MinMaxAngles.y);
           
            angleRotation.x = -positionX;
            transform.eulerAngles = angleRotation;
            FPSController.Rotate(Vector3.up * mouseX);
        }

      
    }
