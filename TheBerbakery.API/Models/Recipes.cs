﻿//NOTE - RecipeBlurb, RecipeIngredients, and RecipeSteps are not needed for the RecipeCards
// Ingredients and Steps may be needed if we decide to go the route of: front of card has recipe, image, name, and description (click) -> flip the card to show ingredients and steps on the back of the card

// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TheBerbakery.API.Models;

/// <summary>
/// Table that stores general information about a recipe
/// </summary>
public partial class Recipes
{
    /// <summary>
    /// unique numeric id for a recipe
    /// </summary>
    public int RecipeId { get; set; }

    /// <summary>
    /// the name of the recipe
    /// </summary>
    public string RecipeName { get; set; }

    /// <summary>
    /// A Tweet&apos;s length description of the recipe.
    /// </summary>
    public string RecipeDescription { get; set; }

    /// <summary>
    /// Any sort of Blogpost-like blurb you want to include on the recipe page.
    /// </summary>
    public string RecipeBlurb { get; set; }

    /// <summary>
    /// URL pointing to the picture of the recipe
    /// </summary>
    public string RecipeImageUrl { get; set; }
    
    public virtual ICollection<RecipeIngredients> RecipeIngredients { get; set; } = new List<RecipeIngredients>();

    public virtual ICollection<RecipeSteps> RecipeSteps { get; set; } = new List<RecipeSteps>();
}