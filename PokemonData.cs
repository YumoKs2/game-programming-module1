using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonData : MonoBehaviour
{
    //d�claration des toutes les diff�rentes �l�ments qui vont compos�s la fiche Pok�mon
    //les �l�ments en sterializefield sont les �l�ments que l'on veut afficher dans l'inspector

    [SerializeField] private string pokemonName = "Nymphali";

    //d�claration des types du Pok�mon et cr�ation d'une liste d�roulante dans l'inspector
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

    //cr�ation d'une liste des diff�rents types disponible
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

    //d�claration des faiblesses et des r�sistances du Pok�mon
    [SerializeField] private pokemonType[] pokemonWeakness = new pokemonType[2];
    [SerializeField] private pokemonType[] pokemonResistance = new pokemonType[2];

    //cr�ation d'un ennemi
    private string enemyName = "Noctali";
    private int enemyAttack = 20;
    private pokemonType enemyType = pokemonType.Dark; 


    void Awake()
    {
        //Ex�cute les fonctions a l'initialisation de l'application
        InitStatsPoints();
        InitCurrentLife();
    }

    void Start()
    {
        //affiche dans la console les diff�rentes informations du Pok�mon
        Debug.Log("Pokemon name is " + pokemonName);
        Debug.Log("Pokemon type is " + Type);
        Debug.Log("Pokemon current life is " + pokemonCurrentHp + " points");
        Debug.Log("Pokemon attack is " + pokemonAttack + " points");
        Debug.Log("Pokemon defense is " + pokemonDefense + " points");
        Debug.Log("Pokemon stats is " + pokemonStatsPoints + " points");
        Debug.Log("Pokemon weight is " + pokemonWeight + " kg");

        //affiche les faiblesses du Pok�mon
        for (int i = 0; i < pokemonWeakness.Length; i++)
        {
            Debug.Log("Pokemon is weak against type : " + pokemonWeakness[i]);
        }

        //affiche les r�sistances du Pok�mon
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
        //ex�cute les fonctions � chaque frame apr�s lancement de l'application
        //ici on appel notre fonction takedamage jusqu'a que le Pok�mon tombe � 0 hp
        if (pokemonCurrentHp > 0)
        {
            pokemonCurrentHp -= TakeDamage(enemyType);
        }

        //ici on v�rifie si le Pok�mon est toujours en vie sinon on envoie un message pour dire que le Pok�mon est K.O et on met fin � la condition
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

    //ici vont se trouver toutes les diff�rentes fonctions

    //cr�ation d'une fonction qui va attribuer la valeur de la vie courante par la valeur de vie de base
    private void InitCurrentLife()
    {
        pokemonCurrentHp = pokemonBaseHp;
    }

    //cr�ation d'une fonction pour d�finir les points de statistiques
    private int pokemonStatsPoints;
    private void InitStatsPoints()
    {
        pokemonStatsPoints = pokemonBaseHp + pokemonAttack + pokemonDefense;
    }

    //cr�ation d'une fonction qui va renvoyer les points d'attaques
    private int GetAttackDamage()
    {
        return pokemonAttack;
    }

    //cr�ation d'une fonction pour attaquer le Pok�mon
    private int TakeDamage(pokemonType enemyType)
    {
        int damage = Random.Range(2, 50);
        float damageMultiplier = 1f;

        //condition si la r�sistance et la faiblesse sont pareilles
        if (pokemonResistance[0] == pokemonWeakness[0] || pokemonResistance[0] == pokemonWeakness[1] || pokemonResistance[1] == pokemonWeakness[0] || pokemonResistance[1] == pokemonWeakness[1] || pokemonWeakness[0] == pokemonResistance[0] || pokemonWeakness[0] == pokemonResistance[1] || pokemonWeakness[1] == pokemonResistance[0] || pokemonWeakness[1] == pokemonResistance[1])
        {
            Debug.Log("You cannot put weakness and resistance with the same type. Please CHANGE");
            damageMultiplier = 0f;
        }

        else
        {
            //condition pour doubler les d�g�ts si le enemyType et le m�me que une des faiblesses du Pok�mon
            foreach (pokemonType pokemonWeaknessType in pokemonWeakness)
            {
                if (pokemonWeaknessType == enemyType)
                {
                    damageMultiplier = 2f;
                    Debug.Log("The attack is very effective, " + pokemonName + " lost " + (damage * damageMultiplier) + " points");
                }
            }

            //condition pour r�duire les d�g�ts si le enemyType et le m�me que une des r�sistances du Pok�mon
            foreach (pokemonType pokemonResistanceType in pokemonResistance)
            {
                if (pokemonResistanceType == enemyType)
                {
                    damageMultiplier = 0.5f;
                    int newdamage = (int)(damage * damageMultiplier);
                    Debug.Log("The attack is not very effective, " + pokemonName + " lost " + (newdamage) + " points");
                }
            }

            //condition pour les d�g�ts de base
            if (damageMultiplier == 1f)
            {
                Debug.Log(pokemonName + " was attack, " + enemyName + " dealt " + (damage * damageMultiplier) + " points");
            }
        }

        int totalDamage = (int)(damage * damageMultiplier);
        return totalDamage;
    }

    //cr�ation d'une fonction qui va �crire un message dans la console en fonction de l'�tat de sant� du Pok�mon
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