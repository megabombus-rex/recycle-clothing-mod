using RimWorld;
using Verse;

namespace ClothingRecycledMod
{
    public class RecipeWorker_RecycleClothing : RecipeWorker
    {
        public override void ConsumeIngredient(Thing ingredient, RecipeDef recipe, Map map)
        {
            if (ingredient is Apparel apparel)
            {
                var material = ingredient.Stuff;

                Log.Message($"Material defName is: {material.defName}");

                if (material is null) { return; }

                var hitpointsModifier = (double)ingredient.HitPoints / (double)ingredient.MaxHitPoints;

                Log.Message($"Hitpoints modifier is: {hitpointsModifier}");

                int materialReturned = (int)((double)(apparel.def.costStuffCount) * hitpointsModifier);

                Log.Message($"Material amount returned: {materialReturned}");

                if (materialReturned < 1) { return; }

                Thing recycledMaterial = ThingMaker.MakeThing(material);
                recycledMaterial.stackCount = materialReturned;

                Log.Message("Created the recycled material.");

                GenPlace.TryPlaceThing(recycledMaterial, apparel.PositionHeld, map, ThingPlaceMode.Near);

                Log.Message($"Recycled material placed near {apparel.PositionHeld}");
            }
        }
    }
}
