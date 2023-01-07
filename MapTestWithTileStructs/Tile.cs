using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapTestWithTileStructs
{
    public enum Sheet { TileSize = 16, SheetWidth = 144, SheetHeight = 16 }
    public enum TileType { None, Solid, OneWay, Coin, Powerup, Star }
    
    public class Tile
    {
        public TileInfo properties;
        public bool warp;
    }

    public struct TileInfo
    {
        public uint id;
        public Rectangle tileBoundaries;
        public Vector2 friction;
        public bool deadly;
        public TileType type;

        public TileInfo(uint id = 0)
        {
            this.id = id;
            tileBoundaries = new Rectangle(
                (int)id % ((int)Sheet.SheetWidth / (int)Sheet.TileSize) * (int)Sheet.TileSize,
                (int)id / ((int)Sheet.SheetWidth / (int)Sheet.TileSize) * (int)Sheet.TileSize,
                (int)Sheet.TileSize, (int)Sheet.TileSize);
            friction = Vector2.Zero;
            deadly = false;
            type = TileType.None;
        }
    }
}
