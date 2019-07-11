using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Services;
using UnityEngine;

// ZARZĄDZANIE (POD)OBIEKTAMI I KOMPONENTAMI ENEMY
public class StaticEnemyController : MonoBehaviour
{
    private GameObject parentObject;
    
    //public EnemyDamageDealer damageDealer;
    //public EnemyDamageReceiver damageReceiver;

    public EnemyDamageDealer damageDealerGobj;

    public EnemyDamageReceiver damageReceiverGobj;

    // Initialize references to children-objects
    void Start()
    {
        //instantiate 2 child objects?

        //t. pobieranie child-obiektów podpiętych do rodzica - zmienic na tworzenie ich przez rodzica
        //damageDealerGobj   = transform.GetChild(0).gameObject;
        //damageReceiverGobj = transform.GetChild(1).gameObject;
    }

    /// <summary>
    /// Zasadniczo ma usuwac wszystkie komponenty martwego przeciwnika, i zmieniac sprite na ostatni z animacji smierci
    /// </summary>
    void OnDestroy()
    {
        Debug.Log("Death of Enemy Controller script");
    }
}
