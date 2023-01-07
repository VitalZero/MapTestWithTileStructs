using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapTestWithTileStructs
{
    public class Map
    {
        private Dictionary<uint, Tile> tileMap;
        private Dictionary<uint, TileInfo> tileSet;
        TileInfo defaultTile;
        Point maxMapSize;
        uint tileCount;
        uint tileSetCount;
        Texture2D texture;
        private string textureName;
        Rectangle destinationRectangle;
        int[] level;

        public Map(string textureName)
        {
            this.textureName = textureName;
            destinationRectangle = new Rectangle(0, 0, (int)Sheet.TileSize, (int)Sheet.TileSize);
            maxMapSize = new Point(20, 10);
            tileMap = new Dictionary<uint, Tile>();
            tileSet = new Dictionary<uint, TileInfo>();

            level = new int[] {
                1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 2, 2, 0, 0, 1, 0, 0, 0, 0, 0, 2, 2, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            };
        }

        public Tile GetTile(uint x, uint y)
        {
            uint tileIndex = ConvertCoords(x, y);

            if (tileMap.ContainsKey(tileIndex))
            {
                return tileMap[tileIndex];
            }
            else 
            {
                return null;
            }
        }

        public TileInfo GetDefaultTile()
        {
            return defaultTile;
        }

        public uint GetTilesize()
        {
            return (uint)Sheet.TileSize;
        }

        public Point GetMapSize()
        {
            return maxMapSize;
        }

        private void LoadTiles()
        {
            tileSet.Add(0, new TileInfo(0));
            tileSet.Add(1, new TileInfo(1));
            tileSet.Add(2, new TileInfo(2));
            tileSet.Add(3, new TileInfo(3));
            tileSet.Add(4, new TileInfo(4));
            tileSet.Add(5, new TileInfo(5));
            tileSet.Add(6, new TileInfo(6));
            tileSet.Add(7, new TileInfo(7));

            for (int y = 0; y < maxMapSize.Y; ++y)
            {
                for (int x = 0; x < maxMapSize.X; ++x)
                {
                    if (level[ConvertCoords((uint)x, (uint)y)] - 1 >= 0)
                    {
                        int tileId = level[ConvertCoords((uint)x, (uint)y)];

                        if(tileSet.ContainsKey((uint)tileId))
                        {
                            Tile tile = new Tile();

                            tile.properties = tileSet[(uint)tileId];

                            tileMap.Add(ConvertCoords((uint)x, (uint)y), tile);
                        }
                    }
                }
            }
        }

        private uint ConvertCoords(uint x, uint y)
        {
            return (y * (uint)maxMapSize.X) + x;
        }

        public void Load(ContentManager content)
        {
            LoadTiles();
            texture = content.Load<Texture2D>(textureName);
        }

        public void Update(float dt)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            uint count = 0;

            for(int y = 0; y < maxMapSize.Y; ++y)
            {
                for (int x = 0; x < maxMapSize.X; ++x)
                {
                    Tile tile = GetTile((uint)x, (uint)y);

                    if (tile == null)
                        continue;

                    destinationRectangle.X = x * (int)Sheet.TileSize;
                    destinationRectangle.Y = y * (int)Sheet.TileSize;

                    spriteBatch.Draw(
                        texture,
                        destinationRectangle,
                        tileMap[ConvertCoords((uint)x, (uint)y)].properties.tileBoundaries,
                        Color.White);
                    ++count;
                }
            }
        }
    }
}
