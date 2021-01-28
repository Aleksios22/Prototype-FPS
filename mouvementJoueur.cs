using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script importé de AljebroS et modifié par Abdirahman Omar
   Script contenant le contrôle du personnage avec les inputs de project settings 
   Contient aussi les animations du personnage  
   Source du script: https://assetstore.unity.com/packages/templates/systems/full-body-fps-controller-134060
*/


public enum PlayerState
{
    Idle,
    Marcher,
    Courir,
    Sauter
}

public class mouvementJoueur : MonoBehaviour


{


    public PlayerState playerStates;


    public float gravity = 20.0F; // Gravité du joueur
    private Vector3 moveDirection = Vector3.zero; // Direction de la force


    // Inputs du joueur que l'on peut modifier à partir de l'inspecteur
    [Header("Inputs")]
    public string InputHorizontal; // Mettre dans l'inspecteur l'input honrizontal correspondant a celui du joueur
    public string InputVertical;   // Mettre dans l'inspecteur l'input vertical correspondant a celui du joueur
    public string InputCourse;     // Mettre dans l'inspecteur l'input de course correspondant a celui du joueur
    public string InputSaut;       // Mettre dans l'inspecteur l'input de saut correspondant a celui du joueur

    // Parametres du joueur
    [Header("Caractéristiques du personnage")]
    [Range(1f, 20f)] // Vitesse minimum et maximum de marche que l'on peut configuer dans l'inspecteur
    public float vitesseMarche;
    [Range(1f, 20f)] // Vitesse minimum et maximum de course que l'on peut configuer dans l'inspecteur
    public float vitesseCourse;
    [Range(1f, 20f)] // Force minimum et maximum de saut que l'on peut configuer dans l'inspecteur
    public float JumpForce;

    public Transform SourcePied; // Source permettant de savoir où sont les pieds du personnage

    public bool JumpAnimation; // Savoir si le joueur saute
   
    // Le paramètre des sons
    [Header("Sounds")]
    public List<AudioClip> FootstepSounds;
    public List<AudioClip> JumpSounds;
    public List<AudioClip> LandSounds;

    CharacterController characterController; // Le controlleur du personnage

   Animator animPerso; // L'animator du personnage

    float _footstepDelay;
    AudioSource _audioSource;
    float footstep_et = 0;


    // Use this for initialization
    void Start()
    {
        // Controlleur du personnage
        characterController = GetComponent<CharacterController>();

        characterController.detectCollisions = true;

        //Chercher l'animator du personnage
        animPerso = GetComponent<Animator>();

        //Chercher la source audio du personnage
        _audioSource = gameObject.AddComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // Fonction des contrôles
        controleDuJoueur();

        // Fonction des animations
        animationDuPersonnage();

        //Fonctions des bruits de pas
        sonsDesPieds();
    }


    /* Fonction permettant de gérer le contrôle du personnage avec les inputs*/
    void controleDuJoueur()
    {

        // Les inputs pour les mouvements verticals et horizontal.
        float hInput = Input.GetAxisRaw(InputHorizontal);
        float vInput = Input.GetAxisRaw(InputVertical);

        // Si le personnage est au sol, alors il pourra marcher à la direction du joueur
        Vector3 fwdMovement = characterController.isGrounded == true ? transform.forward * vInput : Vector3.zero;
        Vector3 rightMovement = characterController.isGrounded == true ? transform.right * hInput : Vector3.zero;


        // Si le joueur appuis sur l'input de courir, le joueur pourra alors courir
        float _speed = Input.GetButton(InputCourse) ? vitesseCourse : vitesseMarche;
        characterController.SimpleMove(Vector3.ClampMagnitude(fwdMovement + rightMovement, 1f) * _speed);


        // Si le joueur est au sol
        if (characterController.isGrounded)
        {
            // Il pourra alors sauter lorsqu'il appuis sur l'input de saut
            if (Input.GetButton(InputSaut))
                moveDirection.y = JumpForce;
                JumpAnimation = true;


        }

        // Sauter selon la gravité du joueur
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);




        //Gerer l'état du personnage
        if (characterController.isGrounded)
        {
            // Si le joueur n'appuis sur rien, le personnage sera en Idle
            if (hInput == 0 && vInput == 0)
                playerStates = PlayerState.Idle;
            else
            {
                // Si le joueur va à une vitesse égale à celui de la marche, le personnage sera en marche
                if (_speed == vitesseMarche)
                    playerStates = PlayerState.Marcher;

                // Sinon, il sera en course 
                else
                    playerStates = PlayerState.Courir;

                // Jouer le sons des pieds
                _footstepDelay = (2 / _speed);
            }
        }
        else
            playerStates = PlayerState.Sauter;
    }


    /* Fonction pour gérer les animations du personnage  */
    void animationDuPersonnage()
    {

        switch (playerStates)
        {
            // Si le joueur est en idle, jouer l'animation de l'idle
            case PlayerState.Idle:
                animPerso.GetComponent<Animator>().SetBool("marche", false);
                animPerso.GetComponent<Animator>().SetBool("jump", false);
                JumpAnimation = false;
                break;

            // Si le joueur est en marche, jouer l'animation de marche
            case PlayerState.Marcher:
                animPerso.GetComponent<Animator>().SetBool("marche", true);
                animPerso.GetComponent<Animator>().SetBool("jump", false);
                break;

            // Si le joueur est en course, jouer l'animation de course
            case PlayerState.Courir:
                animPerso.GetComponent<Animator>().SetBool("jump", false);
                break;

            // Si le joueur saut, jouer l'animation de saut
            case PlayerState.Sauter:
                if (JumpAnimation)
                {
                    animPerso.GetComponent<Animator>().SetBool("jump", true);
                    JumpAnimation = false;
                    if (_audioSource)
                        _audioSource.PlayOneShot(JumpSounds[Random.Range(0, JumpSounds.Count)]);
                }
                break;
        }

    

    }

    // Condition pour savoir si le joueur est au sol
    bool onGround()
    {
        bool retVal = false;

        if (Physics.Raycast(SourcePied.position, Vector3.down, 0.1f))
            retVal = true;
        else
            retVal = false;

        return retVal;
    }


    /*Fonctions pour gerer les sons de pieds du personnage */
    void sonsDesPieds()
    {
        // Si le joueur est en état d'idle et de saut, alors on ne joue pas le son de pas
        if (playerStates == PlayerState.Idle || playerStates == PlayerState.Sauter)
            return;

        // Si le joueur est en mouvement au sol, alors nous jouerons le son de pas
        if (footstep_et < _footstepDelay)
            footstep_et += Time.deltaTime;
        else
        {
            footstep_et = 0;
            _audioSource.PlayOneShot(FootstepSounds[Random.Range(0, FootstepSounds.Count)]);
        }
    }








}

