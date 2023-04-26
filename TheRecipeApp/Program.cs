using System;
using System.Collections.Generic;
using System.ComponentModel;


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

        //after saving your recipe, this part will help you display the recipe you just entered

        public void DisplayRecipe()
        {
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in _ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity * _scalingFactor} {ingredient.Unit} {ingredient.Name}");
            }

            Console.WriteLine("\nSteps:");
            foreach (var step in _steps)
            {
                Console.WriteLine($"{step.Number}. {step.Description}");
            }
        }
        //Setting scale 
        public void ScaleRecipe(double factor)
        {
            _scalingFactor = factor;
        }
        //resetting scale
        public void ResetScalingFactor()
        {
            _scalingFactor = 1.0;
        }
        //removing the recent recipe that you entered...
        public void ClearRecipe()
        {
            _ingredients.Clear();
            _steps.Clear();
            _scalingFactor = 1.0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Recipe recipe = new Recipe();

            // Get number of ingredients
            Console.Write("Enter the number of ingredients: ");
            int numIngredients = int.Parse(Console.ReadLine());

            // Get ingredient details
            for (int i = 1; i <= numIngredients; i++)
            {
                Console.Write($"Ingredient {i} name: ");
                string name = Console.ReadLine();

                Console.Write($"Ingredient {i} quantity: ");
                double quantity = double.Parse(Console.ReadLine());

                Console.Write($"Ingredient {i} unit: ");
                string unit = Console.ReadLine();

                recipe.AddIngredient(name, quantity, unit);
            }

        }
    }
}