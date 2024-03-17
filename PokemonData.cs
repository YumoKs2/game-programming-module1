using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonData : MonoBehaviour
{
    //déclaration des toutes les différentes éléments qui vont composés la fiche Pokémon
    //les éléments en sterializefield sont les éléments que l'on veut afficher dans l'inspector

    [SerializeField] private string pokemonName = "Nymphali";

    //déclaration des types du Pokémon et création d'une liste déroulante dans l'inspector
    [SerializeField]
    private pokemonType Type;
    private pokemonType Fire = pokemonType.Fire;
    private pokemonType Water = pokemonType.Water;
    private pokemonType Fairy = pokemonType.Fairy;
    private pokemonType Dragon = pokemonType.Dragon;
    private pokemonType Ice = pokemonType.Ice;
    private pokemonType Grass = pokemonType.Dark;
    private pokemonType Electric = pokemonType.Electric;

    [SerializeField] private int pokemonBaseHp = 80;
    [SerializeField] private int pokemonAttack = 40;
    [SerializeField] private int pokemonDefense = 20;
    [SerializeField] private float pokemonWeight = 25.5f;
    private int pokemonCurrentHp;

    //création d'une liste des différents types disponible
    private enum pokemonType
    {
        Fire,
        Water,
        Fairy,
        Dragon,
        Ice,
        Dark,
        Electric,
    }

    //déclaration des faiblesses et des résistances du Pokémon
    [SerializeField] private pokemonType[] pokemonWeakness = new pokemonType[2];
    [SerializeField] private pokemonType[] pokemonResistance = new pokemonType[2];

    //création d'un ennemi
    private string enemyName = "Noctali";
    private int enemyAttack = 20;
    private pokemonType enemyType = pokemonType.Dark; 


    void Awake()
    {
        //Exécute les fonctions a l'initialisation de l'application
        InitStatsPoints();
        InitCurrentLife();
    }

    void Start()
    {
        //affiche dans la console les différentes informations du Pokémon
        Debug.Log("Pokemon name is " + pokemonName);
        Debug.Log("Pokemon type is " + Type);
        Debug.Log("Pokemon current life is " + pokemonCurrentHp + " points");
        Debug.Log("Pokemon attack is " + pokemonAttack + " points");
        Debug.Log("Pokemon defense is " + pokemonDefense + " points");
        Debug.Log("Pokemon stats is " + pokemonStatsPoints + " points");
        Debug.Log("Pokemon weight is " + pokemonWeight + " kg");

        //affiche les faiblesses du Pokémon
        for (int i = 0; i < pokemonWeakness.Length; i++)
        {
            Debug.Log("Pokemon is weak against type : " + pokemonWeakness[i]);
        }

        //affiche les résistances du Pokémon
        for (int i = 0; i < pokemonResistance.Length; i++)
        {
            Debug.Log("Pokemon is durable against type : " + pokemonResistance[i]);
        }

        //affiche les informations de l'ennemi
        //Debug.Log("Enemy name is " + enemyName);
        //Debug.Log("Enemy attack is " + enemyAttack + " points");
        //Debug.Log("Enemy type is " + enemyType);
    }

    void Update()
    {
        //exécute les fonctions à chaque frame après lancement de l'application
        //ici on appel notre fonction takedamage jusqu'a que le Pokémon tombe à 0 hp
        if (pokemonCurrentHp > 0)
        {
            pokemonCurrentHp -= TakeDamage(enemyType);
        }

        //ici on vérifie si le Pokémon est toujours en vie sinon on envoie un message pour dire que le Pokémon est K.O et on met fin à la condition
        if (pokemonCurrentHp > 0)
        {
            CheckIfPokemonAlive();
        }

        else if (pokemonCurrentHp < 0)
        {
            pokemonCurrentHp = 0;
            CheckIfPokemonAlive();
        }
    }

    //ici vont se trouver toutes les différentes fonctions

    //création d'une fonction qui va attribuer la valeur de la vie courante par la valeur de vie de base
    private void InitCurrentLife()
    {
        pokemonCurrentHp = pokemonBaseHp;
    }

    //création d'une fonction pour définir les points de statistiques
    private int pokemonStatsPoints;
    private void InitStatsPoints()
    {
        pokemonStatsPoints = pokemonBaseHp + pokemonAttack + pokemonDefense;
    }

    //création d'une fonction qui va renvoyer les points d'attaques
    private int GetAttackDamage()
    {
        return pokemonAttack;
    }

    //création d'une fonction pour attaquer le Pokémon
    private int TakeDamage(pokemonType enemyType)
    {
        int damage = Random.Range(2, 50);
        float damageMultiplier = 1f;

        //condition si la résistance et la faiblesse sont pareilles
        if (pokemonResistance[0] == pokemonWeakness[0] || pokemonResistance[0] == pokemonWeakness[1] || pokemonResistance[1] == pokemonWeakness[0] || pokemonResistance[1] == pokemonWeakness[1] || pokemonWeakness[0] == pokemonResistance[0] || pokemonWeakness[0] == pokemonResistance[1] || pokemonWeakness[1] == pokemonResistance[0] || pokemonWeakness[1] == pokemonResistance[1])
        {
            Debug.Log("You cannot put weakness and resistance with the same type. Please CHANGE");
            damageMultiplier = 0f;
        }

        else
        {
            //condition pour doubler les dégâts si le enemyType et le même que une des faiblesses du Pokémon
            foreach (pokemonType pokemonWeaknessType in pokemonWeakness)
            {
                if (pokemonWeaknessType == enemyType)
                {
                    damageMultiplier = 2f;
                    Debug.Log("The attack is very effective, " + pokemonName + " lost " + (damage * damageMultiplier) + " points");
                }
            }

            //condition pour réduire les dégâts si le enemyType et le même que une des résistances du Pokémon
            foreach (pokemonType pokemonResistanceType in pokemonResistance)
            {
                if (pokemonResistanceType == enemyType)
                {
                    damageMultiplier = 0.5f;
                    int newdamage = (int)(damage * damageMultiplier);
                    Debug.Log("The attack is not very effective, " + pokemonName + " lost " + (newdamage) + " points");
                }
            }

            //condition pour les dégâts de base
            if (damageMultiplier == 1f)
            {
                Debug.Log(pokemonName + " was attack, " + enemyName + " dealt " + (damage * damageMultiplier) + " points");
            }
        }

        int totalDamage = (int)(damage * damageMultiplier);
        return totalDamage;
    }

    //création d'une fonction qui va écrire un message dans la console en fonction de l'état de santé du Pokémon
    private void CheckIfPokemonAlive()
    {
        if (pokemonCurrentHp > 0)
        {
            Debug.Log(pokemonName + " is still alive and has currently " + pokemonCurrentHp + " points");
        }
        else
        {
            Debug.Log(pokemonName + " is K.O she's down to " + pokemonCurrentHp + " points");
        }
    }
}