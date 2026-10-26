using UnityEngine;

public class PokemonTypes
{
    public enum TypeList
    {
        Fire,
        Water,
        Flying,
        Ground,
        Fairy,
        Normal
    }

    public float[,] typStrengths = new float[,]
    {
        //Fire, Water, Flying, Ground, Fairy
        { 1f, 0.5f, 1f, 2f, 1f }, // Fire
        { 2f, 1f, 0.5f, 1f, 1f }, // Water
        { 1f, 2f, 1f, 0.5f, 1f }, // Flying
        { 0.5f, 1f, 2f, 1f, 1f }, // Ground
        { 1f, 1f, 1f, 1f, 2f }, // Fairy
        { 1f, 1f, 1f, 1f, 1f } // Normal
    };
}
