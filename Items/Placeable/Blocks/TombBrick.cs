using Terraria.ID;
using Terraria.ModLoader;
using Remnants.Tiles;

namespace Remnants.Items.Placeable.Blocks
{
	public class TombBrick : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 100;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.GrayBrick);
            Item.createTile = ModContent.TileType <Tiles.Blocks.TombBrick>();
		}
	}
}