﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    class TestMap2
    {
        private string testMap =
            "[1,1,1][2,1,1][3,1,2][4,1,2][5,1,2][6,1,2][7,1,2][8,1,2][9,1,2][10,1,2][11,1,2][12,1,2][13,1,2][14,1,2][15,1,1][16,1,1]" +
            "[1,2,1][2,2,1][3,2,2][4,2,2][5,2,2][6,2,2][7,2,2][8,2,2][9,2,2][10,2,2][11,2,2][12,2,2][13,2,2][14,2,2][15,2,1][16,2,1]" +
            "[1,3,2][2,3,2][3,3,2][4,3,2][5,3,2][6,3,2][7,3,2][8,3,2][9,3,2][10,3,2][11,3,2][12,3,2][13,3,2][14,3,2][15,3,2][16,3,2]" +
            "[1,4,2][2,4,2][3,4,2][4,4,2][5,4,2][6,4,2][7,4,2][8,4,2][9,4,2][10,4,2][11,4,2][12,4,2][13,4,2][14,4,2][15,4,2][16,4,2]" +
            "[1,5,2][2,5,2][3,5,2][4,5,2][5,5,2][6,5,2][7,5,2][8,5,2][9,5,2][10,5,2][11,5,2][12,5,2][13,5,2][14,5,2][15,5,2][16,5,2]" +
            "[1,6,2][2,6,2][3,6,2][4,6,2][5,6,2][6,6,2][7,6,2][8,6,2][9,6,2][10,6,2][11,6,2][12,6,2][13,6,2][14,6,2][15,6,2][16,6,2]" +
            "[1,7,2][2,7,2][3,7,2][4,7,2][5,7,2][6,7,2][7,7,2][8,7,2][9,7,2][10,7,2][11,7,2][12,7,2][13,7,2][14,7,2][15,7,2][16,7,2]" +
            "[1,8,2][2,8,2][3,8,2][4,8,2][5,8,2][6,8,2][7,8,2][8,8,2][9,8,2][10,8,2][11,8,2][12,8,2][13,8,2][14,8,2][15,8,2][16,8,2]" +
            "[1,9,2][2,9,2][3,9,2][4,9,2][5,9,2][6,9,2][7,9,2][8,9,2][9,9,2][10,9,2][11,9,2][12,9,2][13,9,2][14,9,2][15,9,2][16,9,2]" +
            "[1,10,2][2,10,2][3,10,2][4,10,2][5,10,2][6,10,2][7,10,2][8,10,2][9,10,2][10,10,2][11,10,2][12,10,2][13,10,2][14,10,2][15,10,2][16,10,2]" +
            "[1,11,2][2,11,2][3,11,2][4,11,2][5,11,2][6,11,2][7,11,2][8,11,2][9,11,2][10,11,2][11,11,2][12,11,2][13,11,2][14,11,2][15,11,2][16,11,2]" +
            "[1,12,2][2,12,2][3,12,2][4,12,2][5,12,2][6,12,2][7,12,2][8,12,2][9,12,2][10,12,2][11,12,2][12,12,2][13,12,2][14,12,2][15,12,2][16,12,2]" +
            "[1,13,2][2,13,2][3,13,2][4,13,2][5,13,2][6,13,2][7,13,2][8,13,2][9,13,2][10,13,2][11,13,2][12,13,2][13,13,2][14,13,2][15,13,2][16,13,2]" +
            "[1,14,2][2,14,1][3,14,2][4,14,2][5,14,2][6,14,2][7,14,2][8,14,2][9,14,2][10,14,2][11,14,2][12,14,2][13,14,2][14,14,2][15,14,2][16,14,2]" +
            "[1,15,1][2,15,1][3,15,2][4,15,2][5,15,2][6,15,2][7,15,2][8,15,2][9,15,2][10,15,2][11,15,2][12,15,2][13,15,2][14,15,2][15,15,1][16,15,1]" +
            "[1,16,1][2,16,1][3,16,2][4,16,2][5,16,2][6,16,2][7,16,2][8,16,2][9,16,2][10,16,2][11,16,2][12,16,2][13,16,2][14,16,2][15,16,1][16,16,1]";

        private List<Tile> playerPlacedTiles;
        private List<Item> playerPlacedItems;

        public TestMap2()
        {
            playerPlacedItems = new List<Item>();
            playerPlacedTiles = new List<Tile>();
        }

        public string getTestMap()
        {
            return testMap;
        }

        public void addToPlayerPlacedItems(Item item)
        {
            playerPlacedItems.Add(item);
        }

        public List<Item> getPlayerPlacedItems()
        {
            return playerPlacedItems;
        }

        public void addToPlayerPlacedTiles(Tile tile)
        {
            playerPlacedTiles.Add(tile);
        }

        public List<Tile> getPlayerPlacedTiles()
        {
            return playerPlacedTiles;
        }
    }
}
