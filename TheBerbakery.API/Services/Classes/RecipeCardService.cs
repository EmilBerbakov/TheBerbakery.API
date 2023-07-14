using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Recipe>> GetRecipeCards(int[] recipeIds, string recipeName)
        {
            HashSet<int> recipeHash = new();
            if (recipeIds.Length > 0)
            {
                recipeHash = recipeIds.ToHashSet<int>();
            }
            var recipeCards = await _theBerbakeryContext.Recipes.Where(x => (recipeIds.Length == 0 || recipeHash.Contains(x.RecipeId)) && (string.IsNullOrEmpty(recipeName) || x.RecipeName.Contains(recipeName.Trim())) ).Select(x => new Recipe
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
