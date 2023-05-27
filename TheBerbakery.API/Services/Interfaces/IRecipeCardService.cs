﻿using TheBerbakery.API.Models;


namespace TheBerbakery.API.Services.Interfaces
{
    public interface IRecipeCardService
    {
        Task<List<Recipe>> GetRecipeCards(int[] recipeIds);
    }
}