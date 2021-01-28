using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monteeLave : MonoBehaviour
{
    
    /*

    Script de changement du niveau de la lave
    par: Alexis Rochon
    modifié le: 04-11-2020
    
    */

    private float delaiLave;
    private float tempsAttente = 6f;
    private float timer = 0.00f;
    private bool peutMonter;


    // Start is called before the first frame update
    void Start()
    {
        peutMonter = true;
       
        delaiLave = Random.Range(2f, 5f);      // Délai de départ avant la première montée de lave

        Invoke("DelaiMonteeLave", 1.0f);
    }

    // Update is called once per frame
    private void DelaiMonteeLave()
    {
        if(transform.position.y < -16 && peutMonter)
        {
            transform.position = transform.position + new Vector3(0, 0.05f, 0);
        }
        else
        {
            peutMonter = false;
            timer += Time.deltaTime * 10;
            print(timer);

            // Regarder si ça fait plus de 6 secondes que la lave est en haut
            // Ré-initialiser le Timer
            if (timer > tempsAttente)
            {
                print("le 6 secest passé");
                if (transform.position.y > -22.5)
                {
                    transform.position = transform.position + new Vector3(0, -0.05f, 0);
                } 
                else
                {
                    peutMonter = true;
                    delaiLave = Random.Range(30f, 80f);      // Nouveau délai aléatoire avant le prochain chamgement du décors
                    Invoke("MonteeDeLave", 0.1f);
                }

            }
        }

    }
}
