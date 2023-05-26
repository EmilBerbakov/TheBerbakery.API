﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TheBerbakery.API.Models;

/// <summary>
/// Steps for a recipe:
/// recipie_id is a foreign key to recipe_id in recipes
/// recipe_step is used to order the recipe steps
/// </summary>
public partial class RecipeSteps
{
    public int RecipeId { get; set; }

    public int RecipeStep { get; set; }

    public string RecipeStepInstruction { get; set; }

    public virtual Recipes Recipe { get; set; }
}