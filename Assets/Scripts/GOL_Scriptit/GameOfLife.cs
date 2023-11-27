using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public GameObject TileObject;
    private static readonly int Width = 10;
    private static readonly int Height = 10;
    bool[,] grid = new bool[Width, Height];
    GameObject[,] tiles = new GameObject[Width, Height];

    private float TimeAccu = 0.0f;

    void Start()
    {
        for (int x = 0; x < Width; x++) //Generate tiles
        {
            for (int y = 0; y < Height; y++)
            {
                grid[x, y] = false; //Clear the grid
                tiles[x,y] = Instantiate(TileObject, 
                                         new Vector3(x, 0f, y) * 1.05f, 
                                         TileObject.transform.rotation); //Instantiate the tiles

                //tiles[x, y].SetActive(false); <<>> Jos ei haluta spawnata laatikoita
                tiles[x, y].GetComponent<MeshRenderer>().material.color = Color.black; // Voidaan vaihtaa väriä

            }
        }

        grid[5, 5] = true;
        tiles[5, 5].GetComponent<MeshRenderer>().material.color = Color.yellow;
        grid[3, 5] = true;
        tiles[3, 5].GetComponent<MeshRenderer>().material.color = Color.yellow;
        grid[4, 5] = true;
        tiles[4, 5].GetComponent<MeshRenderer>().material.color = Color.yellow;
        grid[4, 4] = true;
        tiles[4, 4].GetComponent<MeshRenderer>().material.color = Color.yellow;

    }

    private int GetLiveNeighbours(int x, int y)
    {
        int liveneighbours = 0;

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (!(i == x && j == y))
                {
                    //Current i,j is not x,y
                    if (grid[i, j] == true)
                    {
                        liveneighbours++;
                    }
                }
            }
        }
        return liveneighbours;
    }

    void Update()
    {
        TimeAccu += Time.deltaTime;

        if (TimeAccu > 2.0f)
        {
            //SÄÄNNÖT:
            //1. Vähemmän kuin kaksi elävää naapuria --> kuole
            //2. 2 tai 3 elävää naapuria --> jatka elämistä
            //3 Enemmäin kuon 3 elävää naapuria --> kuole
            //4. Kuollut celli (?) tasan 3 elävällä naapurilla --> Uudelleensyntyminen?'

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int live = GetLiveNeighbours(x, y);
                    if (live < 1)
                    {
                        grid[x, y] = false;
                    }
                    else if (live < 4 && grid[x, y] == true)
                    {
                        // Sääntö kaksi
                    }
                    else if (live > 3 && grid[x, y] == true)
                    {
                        grid[x, y] = false;
                    }
                    else if (live == 3 && grid[x, y] == false)
                    {
                        grid[x, y] = true;
                    }
                }
            }

            //Update the tiles
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (grid[x, y] == true)
                    {
                        tiles[x, y].GetComponent<MeshRenderer>().material.color = Color.red;
                    }
                    else
                    {
                        tiles[x, y].GetComponent<MeshRenderer>().material.color = Color.black;
                    }
                }
            }
        }
    }
}
