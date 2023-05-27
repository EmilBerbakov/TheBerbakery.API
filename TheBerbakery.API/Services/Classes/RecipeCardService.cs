using Microsoft.EntityFrameworkCore;
using System.Linq;
using TheBerbakery.API.Models;
using TheBerbakery.API.Services.Interfaces;

namespace TheBerbakery.API.Services.Classes
{
    public class RecipeCardService: IRecipeCardService
    {
        #region Database Constructor
        private readonly TheBerbakeryContext _theBerbakeryContext;

        public RecipeCardService(TheBerbakeryContext theBerbakeryContext)
        {
            _theBerbakeryContext = theBerbakeryContext;
        }
        #endregion

        #region Recipe Card
        public async Task<List<Recipe>> GetRecipeCards(int[] recipeIds)
        {
            HashSet<int> recipeHash = recipeIds.ToHashSet<int>();
            var recipeCards = await _theBerbakeryContext.Recipes.Where(x => recipeHash.Contains(x.RecipeId)).Select(x => new Recipe
            {
                RecipeId = x.RecipeId,
                RecipeName = x.RecipeName,
                RecipeBlurb = x.RecipeBlurb,
                RecipeDescription = x.RecipeDescription,
                RecipeImageUrl = x.RecipeImageUrl,
                RecipeSteps = x.RecipeSteps,
                RecipeIngredients = x.RecipeIngredients
            }).ToListAsync();
            return recipeCards;
        }

        public async Task<List<Recipe>> GetTopXRecentRecipeCards(int topX)
        {
            var recipeCards = await _theBerbakeryContext.Recipes.OrderByDescending(x => x.RecipeId).Take(topX).Select(x => new Recipe
            {
                RecipeId = x.RecipeId,
                RecipeName = x.RecipeName,
                RecipeBlurb = x.RecipeBlurb,
                RecipeDescription = x.RecipeDescription,
                RecipeImageUrl = x.RecipeImageUrl,
                RecipeSteps = x.RecipeSteps,
                RecipeIngredients = x.RecipeIngredients
            }).ToListAsync();
            return recipeCards;
        }
        #endregion
    }
}
