﻿namespace CraftHUD
{
    public class CurrencyData
    {
        public Coord Transmutation { get; set; }
        public Coord Augmentation { get; set; }
        public Coord Alteration { get; set; }
        public Coord Regal { get; set; }
        public Coord Scouring { get; set; }
        public Coord Fusing { get; set; }

    }

    public struct Coord
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
