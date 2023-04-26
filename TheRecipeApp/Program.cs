using System;
using System.Collections.Generic;


//step class for the umber of steps required for your recipe
class Step
{
    public int Number { get; set; }
    public string Description { get; set; }
}


//this is a ingredient class, stores the Name, Quantity and units of all ingredients
namespace TheRecipeApp
{
    class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
    }
    


    // this is the recipe class that will be taking the recipe step by step
    class Recipe
    {
        private List<Ingredient> _ingredients = new List<Ingredient>();
        private List<Step> _steps = new List<Step>();
        private double _scalingFactor = 1.0;

        public void AddIngredient(string name, double quantity, string unit)
        {
            _ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit });
        }

        public void AddStep(int number, string description)
        {
            _steps.Add(new Step { Number = number, Description = description });
        }

    }
}