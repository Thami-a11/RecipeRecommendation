import { Recipe } from "./Types/recipe";

const API_BASE = "https://localhost:7204/api";

export const getRecipes = async (ingredients: string[]): Promise<Recipe[]> => {
  const response = await fetch(`${API_BASE}/RecipeRecommedation/recommend`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ ingredients })
  });

  if (!response.ok) {
    throw new Error("Failed to fetch recipes");
  }
  
  const recipes: Recipe[] = await response.json();


  return recipes;
};
