using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

public class Tuile
{
    public int _index;
    public int _richesse;

    public Tuile(int index, int richesse)
    {
        _index = index;
        _richesse = richesse;
    }
}

public class Arbre : IComparable<Arbre>
{
    public int _index_case;
    public int points;
    bool mine;

    public Arbre(int index_case, bool isMine, List<Tuile> Cases)
    {
        _index_case = index_case;
        mine = isMine;

        switch( Cases.Where(x => x._index == index_case).First()._richesse)
        {
            case 1:
                points = 0;
                break;
            case 2:
                points = 2;
                break;
            case 3:
                points = 4;
                break;
        }
    }

    public int CompareTo(Arbre other)
    {
        return points.CompareTo(other.points);
    }
}

class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        int numberOfCells = int.Parse(Console.ReadLine()); // 37

        List<Tuile> liste_tuiles = new List<Tuile>();

        for (int i = 0; i < numberOfCells; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int index = int.Parse(inputs[0]); // 0 is the center cell, the next cells spiral outwards
            int richness = int.Parse(inputs[1]); // 0 if the cell is unusable, 1-3 for usable cells
            int neigh0 = int.Parse(inputs[2]); // the index of the neighbouring cell for each direction
            int neigh1 = int.Parse(inputs[3]);
            int neigh2 = int.Parse(inputs[4]);
            int neigh3 = int.Parse(inputs[5]);
            int neigh4 = int.Parse(inputs[6]);
            int neigh5 = int.Parse(inputs[7]);
            liste_tuiles.Add(new Tuile(index, richness));
        }

        // game loop
        while (true)
        {
            int day = int.Parse(Console.ReadLine()); // the game lasts 24 days: 0-23
            int nutrients = int.Parse(Console.ReadLine()); // the base score you gain from the next COMPLETE action
            inputs = Console.ReadLine().Split(' ');
            int sun = int.Parse(inputs[0]); // your sun points
            int score = int.Parse(inputs[1]); // your current score
            inputs = Console.ReadLine().Split(' ');
            int oppSun = int.Parse(inputs[0]); // opponent's sun points
            int oppScore = int.Parse(inputs[1]); // opponent's score
            bool oppIsWaiting = inputs[2] != "0"; // whether your opponent is asleep until the next day
            int numberOfTrees = int.Parse(Console.ReadLine()); // the current amount of trees

            //Ajout CBE
            List<Arbre> liste_arbres = new List<Arbre>();
            for (int i = 0; i < numberOfTrees; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int cellIndex = int.Parse(inputs[0]); // location of this tree
                int size = int.Parse(inputs[1]); // size of this tree: 0-3
                bool isMine = inputs[2] != "0"; // 1 if this is your tree
                bool isDormant = inputs[3] != "0"; // 1 if this tree is dormant
                if (isMine)
                {
                    liste_arbres.Add(new Arbre(cellIndex, isMine, liste_tuiles));
                }
                
            }
            liste_arbres.Sort();
            liste_arbres.Reverse();
            foreach (Arbre _arbre in liste_arbres)
            {
                Console.Error.WriteLine(_arbre.points);
            }

            int numberOfPossibleMoves = int.Parse(Console.ReadLine());
            for (int i = 0; i < numberOfPossibleMoves; i++)
            {
                string possibleMove = Console.ReadLine();
                Console.Error.WriteLine("possibleMove : " + possibleMove);
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // GROW cellIdx | SEED sourceIdx targetIdx | COMPLETE cellIdx | WAIT <message>
            Console.WriteLine("COMPLETE " + liste_arbres.First()._index_case);
        }
    }
}