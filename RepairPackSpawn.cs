using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPackSpawn : MonoBehaviour
{

    public GameObject RepairKit;  // 

    public GameObject RepairKit02;  // 

    public GameObject RepairKit03;  // 

    public GameObject RepairKit04;  // 

    public GameObject RepairKit05;  // 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (RepairKit.activeInHierarchy == false) // si ce reapirkit fut utilisé donc, ce cas on le fait repparaitre dans quelque minute
        {
            StartCoroutine(reswpanRepair(25f)); // On déclenche la coroutine reswpanRepair

        }


        if (RepairKit02.activeInHierarchy == false) // si ce reapirkit fut utilisé donc, ce cas on le fait repparaitre dans quelque minute
        {
            StartCoroutine(reswpanRepair02(25f)); // On déclenche la coroutine reswpanRepair

        }

        if (RepairKit03.activeInHierarchy == false) // si ce reapirkit fut utilisé donc, ce cas on le fait repparaitre dans quelque minute
        {
            StartCoroutine(reswpanRepair03(25f)); // On déclenche la coroutine reswpanRepair

        }

        if (RepairKit04.activeInHierarchy == false) // si ce reapirkit fut utilisé donc, ce cas on le fait repparaitre dans quelque minute
        {
            StartCoroutine(reswpanRepair04(25f)); // On déclenche la coroutine reswpanRepair

        }

        if (RepairKit05.activeInHierarchy == false) // si ce reapirkit fut utilisé donc, ce cas on le fait repparaitre dans quelque minute
        {
            StartCoroutine(reswpanRepair05(25f)); // On déclenche la coroutine reswpanRepair

        }


    }

    IEnumerator reswpanRepair(float tempsAttente)//permet de réactiver l'objet
    {

        yield return new WaitForSeconds(25f);
        RepairKit.SetActive(true); // le repairKit apparait
      
    }


    IEnumerator reswpanRepair02(float tempsAttente)//permet de réactiver l'objet
    {

        yield return new WaitForSeconds(25f);
        
        RepairKit02.SetActive(true); // le repairKit apparait
    }

    IEnumerator reswpanRepair03(float tempsAttente)//permet de réactiver l'objet
    {

        yield return new WaitForSeconds(25f);
      
        RepairKit03.SetActive(true); // le repairKit apparait
    }

    IEnumerator reswpanRepair04(float tempsAttente)//permet de réactiver l'objet
    {

        yield return new WaitForSeconds(25f);
       
        RepairKit04.SetActive(true);// le repairKit apparait
    }


    IEnumerator reswpanRepair05(float tempsAttente)//permet de réactiver l'objet
    {

        yield return new WaitForSeconds(25f);

        RepairKit05.SetActive(true);// le repairKit apparait
    }

}
