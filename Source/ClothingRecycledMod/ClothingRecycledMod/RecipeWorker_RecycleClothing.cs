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
                if (material is null) { return; }

                var hitpointsModifier = (double)ingredient.HitPoints / (double)ingredient.MaxHitPoints;
                int materialReturned = (int)((double)(apparel.def.costStuffCount) * hitpointsModifier);

                if (materialReturned < 1)
                {
                    base.ConsumeIngredient(ingredient, recipe, map);
                    return; 
                }

                Thing recycledMaterial = ThingMaker.MakeThing(material);
                recycledMaterial.stackCount = materialReturned;
                GenPlace.TryPlaceThing(recycledMaterial, apparel.PositionHeld, map, ThingPlaceMode.Near);
            }
            base.ConsumeIngredient(ingredient, recipe, map);
        }
    }
}
