using UnityEngine;
using System.Collections;

// WARNING WARNING WARNING: The sources I found to assist with this indicate these were/might still be undocumented APIs,
// and there's no undoing the changes made to your tree and terrain - it WILL NOT SNAP BACK when you click Stop in the Editor.
// There could be other complications as well. Use at your risk.

// Step 1. Create a tree/prefab and put it into the Terrain Tree Painter
// Step 2. Create the same tree/model/etc but change the color to simulate a Fall color scheme and add it to Terrain Tree Painter
// Step 3. Attach this to something

public class QM_TreeSeasons : MonoBehaviour
{
    public Terrain terrain;
    public TerrainData terrainData;             // drag the Terrain Data in Editor
    public int treeCount;
    public int treeTypes;
    public TreeInstance[] currentTreeList;
    private bool season;

    // These values are the prefab/prototype index value spat out by Start()

    public int after;
    public int before;

    public int tree1 = 0;
    public int tree1dead = 4;
    public int tree2 = 2;
    public int tree2dead = 3;
    public int tree3 = 5;
    public int tree3dead = 14;
    public int grass1 = 6;

    public int grass2 = 7;

    public double changePercentage = 0.25;

    public ArrayList[] objectArray;
    public int[] total;

    void Awake()
    {
        treeCount = 0;
        treeTypes = 0;
        after = 0;
        before = 3;
    }

    void Start()
    {

        //Set active terrain
        terrainData = terrain.terrainData;

        // Setup a holding array
        currentTreeList = new TreeInstance[terrainData.treeInstances.Length];

        // Get some numbers
        treeTypes = terrainData.treePrototypes.Length;
        treeCount = terrainData.treeInstances.Length;

        // Displays some numbers
        Debug.Log("Tree types (i.e # of prefabs in Terrain tree painter: " + treeTypes);
        Debug.Log("They are: ");

        objectArray = new ArrayList[treeTypes];
        // Search the trees and print the name and index
        for (int cnt = 0; cnt < treeTypes; cnt++)
        {
            objectArray[cnt] = new ArrayList();
            Debug.Log("name: " + terrainData.treePrototypes[cnt].prefab.name + " @ prototype index " + cnt);
        }

        System.Array.Copy(terrainData.treeInstances, currentTreeList, terrainData.treeInstances.Length);
        for (int tcnt = 0; tcnt < currentTreeList.Length; tcnt++)
        {
            Debug.Log(currentTreeList[tcnt].prototypeIndex);
            objectArray[currentTreeList[tcnt].prototypeIndex].Add(tcnt);
        }

        total = new int[treeTypes];
        for(int i = 0; i < treeTypes; i++)
        {
            total[i] = objectArray[i].Count;
            //Debug.Log(i+" "+total[i]);
        }
    }
    // STOP STOP STOP: 
    // ChangeSeasons() changes your TerrainData and there's no going back.
    // If you run this next block you will be changing, permanently,
    // your TerrainData trees. Don't press "X" unless you are sure.


    void ChangeSeasons()
    {

        // Copy existing TreeInstances array contents into holding array
        System.Array.Copy(terrainData.treeInstances, currentTreeList, terrainData.treeInstances.Length);

        if (terrainData.treeInstances.Length == currentTreeList.Length)
        {

            for (int tcnt = 0; tcnt < currentTreeList.Length; tcnt++)
            {

                if (season)
                {
                    if (currentTreeList[tcnt].prototypeIndex == after)
                    {
                        currentTreeList[tcnt].prototypeIndex = before;
                    }
                }
                if (!season)
                {
                    if (currentTreeList[tcnt].prototypeIndex == before)
                    {
                        currentTreeList[tcnt].prototypeIndex = after;
                    }
                }
            }
            terrainData.treeInstances = currentTreeList;
            season = !season;
        }

    }
    public void debugTree()
    {
        updateEnvironment(changePercentage, 0, 4);
        updateEnvironment(changePercentage, 2, 3);
        updateEnvironment(changePercentage, 5, 14);
        while (changePercentage < 1)
        {
            Debug.Log("change Percentage:" + changePercentage);
            changePercentage += 0.25;
        }
    }
    public void changeTrees()
    {
        
        updateEnvironment(changePercentage, 4, 0);
        updateEnvironment(changePercentage, 3, 2);
        updateEnvironment(changePercentage, 14, 5);
        while (changePercentage < 1)
        {
            Debug.Log("change Percentage:" + changePercentage);
            changePercentage += 0.25;
        }
    }

    public void updateEnvironment(double percentage, int typeBefore, int typeAfter)
    {
        Debug.Log("Percentage: " + percentage + ", typeBefore: " + typeBefore + ", typeAfter: " + typeAfter);

        // Copy existing TreeInstances array contents into holding array
        System.Array.Copy(terrainData.treeInstances, currentTreeList, terrainData.treeInstances.Length);

        int numChange = (int)(total[typeBefore] * percentage);
        Debug.Log("numChange: " + numChange);
        while(objectArray[typeBefore].Count > (total[typeBefore]-numChange))
        {
            System.Random random = new System.Random();
            int num = random.Next(objectArray[typeBefore].Count);
            int index = (int)objectArray[typeBefore][num];
            currentTreeList[index].prototypeIndex = typeAfter;
            objectArray[typeBefore].RemoveAt(num);
        }
        terrainData.treeInstances = currentTreeList;
    }

    // Having giving all the warnings above, if you want to toggle your trees, press X again
    // I don't know if other stuff in your terrain data might be affected. Use at your own risk.

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (after == -1 || before == -1)
            {
                Debug.Log("You have to set these values matching the desired models");
                Debug.Log("prefab index values shown in Debug.Log");
            }
            else
            {
                ChangeSeasons();
            }
        }
    }
}