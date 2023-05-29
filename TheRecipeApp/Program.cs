using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace RecipeApp
{
    //this is where im declaring the recipe
    class Recipe
    {
        private string name;
        private List<Ingredient> ingredients;
        private List<string> steps;
        private int totalCalories;

        // this is where the information for recipes will be stored
        public Recipe(string name)
        {
            this.name = name;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
            totalCalories = 0;
        }

        //thise are to get and set the user information for the recipes 
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Ingredient> Ingredients
        {
            get { return ingredients; }
        }

        public List<string> Steps
        {
            get { return steps; }
        }

        public int TotalCalories
        {
            get { return totalCalories; }
        }

        public void AddIngredient(Ingredient ingredient)
        {
            ingredients.Add(ingredient);
            totalCalories += ingredient.Calories;
        }

        public void AddStep(string step)
        {
            steps.Add(step);
        }
    }
    
    
    class Ingredient
    {
        private string name;
        private double quantity;
        private string unitOfMeasurement;
        private int calories;
        private string foodGroup;

        //this is where i will be storing the new name,quantity/UOF,foodgroup more or less like seting
        public Ingredient(string name, double quantity, string unitOfMeasurement, int calories, string foodGroup)
        {
            this.name = name;
            this.quantity = quantity;
            this.unitOfMeasurement = unitOfMeasurement;
            this.calories = calories;
            this.foodGroup = foodGroup;
        }

        //this is the part that gets all the different properties if each recipe
        public string Name
        {
            get { return name; }
        }

        public double Quantity
        {
            get { return quantity; }
        }

        public string UnitOfMeasurement
        {
            get { return unitOfMeasurement; }
        }

        public int Calories
        {
            get { return calories; }
        }

        public string FoodGroup
        {
            get { return foodGroup; }
        }
    }

    //this is my delegate that wll call the method for total calorie count
    delegate void RecipeExceedsCaloriesHandler(string recipeName, int totalCalories);

    //the method for calorie count
    class RecipeBook
    {
        private List<Recipe> recipes;
        public event RecipeExceedsCaloriesHandler RecipeExceedsCaloriesEvent;

        public RecipeBook()
        {
            recipes = new List<Recipe>();
        }

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
            if (recipe.TotalCalories > 300)
            {
                RecipeExceedsCaloriesEvent?.Invoke(recipe.Name, recipe.TotalCalories);
            }
        }

        public List<Recipe> GetRecipes()
        {
            return recipes;
        }

        public void DisplayRecipes()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Recipe Mobile - Recipe List");
            Console.ResetColor();

            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found.");
            }
            else
            {
                recipes.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));

                foreach (Recipe recipe in recipes)
                {
                    Console.WriteLine("- " + recipe.Name);
                }
            }

            Console.WriteLine();
        }

        public Recipe GetRecipe(string recipeName)
        {
            return recipes.Find(r => r.Name == recipeName);
        }
    }

    class Program
    {
        static RecipeBook recipeBook;

        static void Main(string[] args)
        {
            recipeBook = new RecipeBook();
            recipeBook.RecipeExceedsCaloriesEvent += HandleRecipeExceedsCalories;

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Recipe Mobile");
                Console.ResetColor();
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Enter a recipe");
                Console.WriteLine("2. View all recipes");
                Console.WriteLine("3. Delete a recipe");
                Console.WriteLine("4. Close app");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Enter the recipe name: ");
                        string recipeName = Console.ReadLine();

                        Recipe newRecipe = new Recipe(recipeName);

                        bool addIngredients = true;
                        while (addIngredients)
                        {
                            Console.Write("Enter the ingredient name: ");
                            string ingredientName = Console.ReadLine();
                            Console.Write("Enter the quantity: ");
                            double quantity = double.Parse(Console.ReadLine());
                            Console.Write("Enter the unit of measurement: ");
                            string unitOfMeasurement = Console.ReadLine();
                            Console.Write("Enter the number of calories: ");
                            int calories = int.Parse(Console.ReadLine());
                            Console.Write("Enter the food group: ");
                            string foodGroup = Console.ReadLine();

                            Ingredient ingredient = new Ingredient(ingredientName, quantity, unitOfMeasurement, calories, foodGroup);
                            newRecipe.AddIngredient(ingredient);

                            Console.Write("Add another ingredient? (Y/N): ");
                            string addMoreIngredients = Console.ReadLine();
                            addIngredients = (addMoreIngredients.ToUpper() == "Y");
                        }

                        bool addSteps = true;
                        while (addSteps)
                        {
                            Console.Write("Enter a step: ");
                            string step = Console.ReadLine();
                            newRecipe.AddStep(step);

                            Console.Write("Add another step? (Y/N): ");
                            string addMoreSteps = Console.ReadLine();
                            addSteps = (addMoreSteps.ToUpper() == "Y");
                        }

                        recipeBook.AddRecipe(newRecipe);
                        Console.WriteLine("Recipe added successfully!");
                        break;

                    case "2":
                        recipeBook.DisplayRecipes();
                        Console.Write("Enter the recipe name to view details (or '0' to go back to menu): ");
                        string recipeToDisplay = Console.ReadLine();

                        if (recipeToDisplay != "0")
                        {
                            Recipe recipe = recipeBook.GetRecipe(recipeToDisplay);
                            if (recipe != null)
                            {
                                DisplayRecipeDetails(recipe);
                            }
                            else
                            {
                                Console.WriteLine("Recipe not found!");
                            }
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                        }
                        break;

                    case "3":
                        recipeBook.DisplayRecipes();
                        Console.Write("Enter the recipe name to delete (or '0' to go back to menu): ");
                        string recipeToDelete = Console.ReadLine();

                        if (recipeToDelete != "0")
                        {
                            Recipe recipe = recipeBook.GetRecipe(recipeToDelete);
                            if (recipe != null)
                            {
                                recipeBook.GetRecipes().Remove(recipe);
                                Console.WriteLine("Recipe deleted successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Recipe not found!");
                            }
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid input! Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void HandleRecipeExceedsCalories(string recipeName, int totalCalories)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Warning: The recipe '{0}' exceeds 300 calories. Total calories: {1}", recipeName, totalCalories);
            Console.ResetColor();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        static void DisplayRecipeDetails(Recipe recipe)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Recipe: {0}", recipe.Name);
            Console.ResetColor();

            // Prompt for recipe scale
            Console.Write("Enter the recipe scale (0.5 for half, 2 for double, 3 for triple): ");
            double scale = double.Parse(Console.ReadLine());

            Console.WriteLine("Ingredients:");
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                double scaledQuantity = ingredient.Quantity * scale;
                Console.WriteLine("- {0} ({1} {2})", ingredient.Name, scaledQuantity, ingredient.UnitOfMeasurement);
            }

            Console.WriteLine("Steps:");
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, recipe.Steps[i]);
            }

            // Calculate scaled total calories
            int scaledTotalCalories = (int)(recipe.TotalCalories * scale);
            Console.WriteLine("Total Calories: {0}", scaledTotalCalories);

            if (scaledTotalCalories > 300)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Warning: This recipe exceeds 300 calories!");
                Console.ResetColor();
            }

            Console.WriteLine();
        }

    }
}


