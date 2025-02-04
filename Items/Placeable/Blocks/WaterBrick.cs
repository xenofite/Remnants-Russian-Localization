using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Remnants.Tiles;

namespace Remnants.Items.Placeable.Blocks
{
	public class WaterBrick : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 100;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.StoneBlock);
            Item.createTile = ModContent.TileType<Tiles.Blocks.MarineSlab>();
		}
	}
}