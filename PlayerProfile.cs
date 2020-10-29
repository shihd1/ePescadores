using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfile
{
    public string playerName;
    private int[] trash;
    private int trashFactor;
    private int[,] stars;
    private int[,] starFactor;

    public PlayerProfile()
    {
        trash = new int[10];
        trashFactor = 1;
        stars = new int[10,10];
        starFactor = new int[10,10] {{100,10,10,10,10,10,10,10,10,10},
                                     {100,10,10,10,10,10,10,10,10,10},
                                     {100,10,10,10,10,10,10,10,10,10},
                                     {100,10,10,10,10,10,10,10,10,10},
                                     {100,10,10,10,10,10,10,10,10,10},
                                     {100,10,10,10,10,10,10,10,10,10},
                                     {100,10,10,10,10,10,10,10,10,10},
                                     {100,10,10,10,10,10,10,10,10,10},
                                     {100,10,10,10,10,10,10,10,10,10},
                                     {100,10,10,10,10,10,10,10,10,10}};
    }
    
    public int getLifePoints(int location)
    {
        int lifePoints = trash[location]*trashFactor;
        for(int i = 0; i < starFactor.GetLength(1); i++)
        {
            lifePoints += stars[location,i] * starFactor[location,i];
        }
        return lifePoints;
    }
    public int getTotalLifePoints(int location)
    {
        int lifePoints = trash[location] * trashFactor;
        for (int i = 0; i < starFactor.GetLength(1); i++)
        {
            lifePoints += 5 * starFactor[location, i];
        }
        return lifePoints;
    }
    public int getTotalLevel()
    {
        int lifePoints = 0;
        for (int i = 0; i < trash.Length; i++)
        {
            lifePoints += trash[i]*trashFactor;
        }
        for (int i = 0; i < stars.GetLength(0); i++)
        {
            for(int j = 0; j < stars.GetLength(1); j++)
            {
                lifePoints += stars[i,j] * starFactor[i,j];
            }
        }
        return lifePoints;
    }
    public int getNumStars(int location, int index)
    {
        return stars[location, index];
    }

    public void setStars(int location, int index, int numStars)
    {
        if (numStars > stars[location, index])
        {
            stars[location, index] = numStars;
        }
    }
    public void addDailyTrash(int location)
    {
        trash[location] += 1;
    }
    public void addReportTrash(int location)
    {
        trash[location] += 10;
    }
}
