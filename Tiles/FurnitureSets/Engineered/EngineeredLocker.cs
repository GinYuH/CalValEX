using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Enums;
using CalValEX.Items.Tiles.FurnitureSets.Engineered;
using Terraria.Audio;
using System;

namespace CalValEX.Tiles.FurnitureSets.Engineered
{
    public class EngineeredLocker : ModTile
    {   
        //Warning the drawing of the tile is disabled and then reenabled in PostDraw, 
        //this is because the chest code handles the animation of the chest and will cause visual bugs. HPU 2/25/26
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileContainer[Type] = true;

            TileID.Sets.BasicChest[Type] = true;
            TileID.Sets.HasOutlines[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            //Size rules
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new int[] {16, 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;

            //Placement - Anchor must be above alternates HPU 2/25/26
            TileObjectData.newTile.Origin = new Point16(1, 3); 
            TileObjectData.newTile.UsesCustomCanPlace = true; 
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 2);
            //Placement - Chest behavior
            TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(Chest.FindEmptyChest, -1, 0, true);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(Chest.AfterPlacement_Hook, -1, 0, false);
            TileObjectData.newTile.AnchorInvalidTiles = new int[] { TileID.MagicalIceBlock };
            TileObjectData.newTile.LavaDeath = false;
            //Placement - Flipping
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);

            TileObjectData.addTile(Type); // Making a tile that doesn't use a predefined style required so much reading of the tmod docs god. - 2/24/26 HPU 
            
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(110, 52, 52), name, MapChestName);
            DustType = DustID.Clay;
            AdjTiles = new int[] { TileID.Containers };
        }
        //Behavour
        public override ushort GetMapOption(int i, int j) => 0;
        public override bool HasSmartInteract(int i, int j, Terraria.GameContent.ObjectInteractions.SmartInteractScanSettings settings) => true;
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Chest.DestroyChest(i, j);
        }
        public override bool RightClick(int i, int j)
            {
                Player player = Main.LocalPlayer;
                Tile tile = Main.tile[i, j];
                Main.mouseRightRelease = false;

                //had to do this since the chest code assumes 2 by 2, and I am doing 2 by 4, so I have to find the top left of the chest manually. - 2/24/26 HPU
                int left = i - (Main.tile[i, j].TileFrameX / 18 % 2); 
                int top = j - (Main.tile[i, j].TileFrameY / 18 % 4);

                if (player.chest == Chest.FindChest(left, top) && player.chest != -1) 
                {
                    player.chest = -1;
                    SoundEngine.PlaySound(SoundID.MenuClose);
                } 
                else 
                {
                    int chestIndex = Chest.FindChest(left, top);
                    if (chestIndex == -1) {
                        chestIndex = Chest.CreateChest(left, top);
                    }

                    if (chestIndex != -1) {
                        Main.stackSplit = 600;
                        if (chestIndex == player.chest) {
                            player.chest = -1;
                            SoundEngine.PlaySound(SoundID.MenuClose);
                        } else {
                            player.chest = chestIndex;
                            Main.playerInventory = true;
                            Main.recBigList = false;
                            player.chestX = left;
                            player.chestY = top;
                            SoundEngine.PlaySound(SoundID.MenuOpen);
                        }
                    }
                }
                Recipe.FindRecipes();
                return true;
            }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];
            
            //Same deal as above - 2/24/26 HPU
            int left = i - (Main.tile[i, j].TileFrameX / 18 % 2); 
            int top = j - (Main.tile[i, j].TileFrameY / 18 % 4);

            int chestIndex = Chest.FindChest(left, top);
            player.cursorItemIconID = -1; // Reset icon
            if (chestIndex < 0) {
                player.cursorItemIconText = Language.GetTextValue("LegacyChestType.0");
            } else {

                player.cursorItemIconText = Main.chest[chestIndex].name.Length > 0 ? Main.chest[chestIndex].name : "Engineered Locker";
                if (player.cursorItemIconText == "Engineered Locker") {
                    player.cursorItemIconID = ModContent.ItemType<Items.Tiles.FurnitureSets.Engineered.EngineeredLockerItem>();
                    player.cursorItemIconText = ""; // Clear text if using an icon
                }
            }
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
        }

        public override void MouseOverFar(int i, int j)
        {
            MouseOver(i, j);
            Player player = Main.LocalPlayer;
            if (player.cursorItemIconText == "")
            {
                player.cursorItemIconEnabled = false;
                player.cursorItemIconID = ItemID.None;
            }
        }
        //Drawing 
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        { 
            /*
            To disable the drawing of the tile since BasicChest handles the animation of the chest and will cause visual bugs. 
            However the sprite can't just be a transparent because then the tile prewiew will not appear.
            This is cursed but it is what it is. - 2/25/26 HPU 
            */
            Tile tile = Main.tile[i, j];
            Texture2D texture = Request<Texture2D>("CalValEX/Tiles/FurnitureSets/Engineered/EngineeredLocker").Value;
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;
            spriteBatch.Draw(texture, drawPosition, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), Lighting.GetColor(i, j));
            return false;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            //The ACTUAL drawing of the tile. - 2/25/26 HPU 
            Tile tile = Main.tile[i, j];

            int left = i - (tile.TileFrameX / 18 % 2);
            int top = j - (tile.TileFrameY / 18 % 4);

            int chestIndex = Chest.FindChest(left, top);
            bool isOpen = false;

            if (chestIndex != -1) 
            {
                for (int k = 0; k < Main.maxPlayers; k++) {
                    if (Main.player[k].active && Main.player[k].chest == chestIndex) {
                        isOpen = true;
                        break;
                    }
                }
            }

            int mouseTileX = Player.tileTargetX;
            int mouseTileY = Player.tileTargetY;

            // Check if the targeted tile is within the 2x4 bounds of this specific locker - 2/25/26 HPU 
            bool isHoveringThisLocker = (mouseTileX == left || mouseTileX == left + 1) && (mouseTileY >= top && mouseTileY <= top + 3);

            int yOffset = 0;
            if (isOpen) 
            {
                yOffset = 148; 
            }
            else if (isHoveringThisLocker) 
            {
                yOffset = 74;
            
            }
            Texture2D glowTexture = Request<Texture2D>("CalValEX/Tiles/FurnitureSets/Engineered/EngineeredLockerPostDraw").Value;
            Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero;

            spriteBatch.Draw(glowTexture, drawPosition, new Rectangle(tile.TileFrameX, tile.TileFrameY + yOffset, 16, 16), Lighting.GetColor(i, j));
        }
        public string MapChestName(string name, int i, int j)
        {
            int left = i;
            int top = j;
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameX % 36 != 0)
            {
                left--;
            }
            if (tile.TileFrameY != 0)
            {
                top--;
            }
            int chest = Chest.FindChest(left, top);
            if (chest < 0)
            {
                return Language.GetTextValue("LegacyChestType.0");
            }
            else if (Main.chest[chest].name == "")
            {
                return name;
            }
            else
            {
                return name + ": " + Main.chest[chest].name;
            }
        }
        
    }
}