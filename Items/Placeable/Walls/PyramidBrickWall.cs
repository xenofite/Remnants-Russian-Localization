using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Remnants.Walls;
using Remnants.Items.Placeable.Blocks;

namespace Remnants.Items.Placeable.Walls
{
	public class PyramidBrickWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 400;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.StoneWall);
			Item.height = 16;
            Item.createWall = ModContent.WallType<global::Remnants.Walls.PyramidBrickWall>();
		}

		public override void AddRecipes()
		{
			Recipe recipe;

			recipe = Recipe.Create(Type, 4);
			recipe.AddIngredient(ModContent.ItemType<PyramidBrick>());
			recipe.Register();

			recipe = Recipe.Create(ModContent.ItemType<PyramidBrick>());
			recipe.AddIngredient(Type, 4);
			recipe.Register();
		}
	}
}