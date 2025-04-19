import { useState } from "react";
import { getRecipes } from "../api/api"
import { Recipe } from "../api/Types/recipe";


// interface RecipeFetcherProps {
//   onGetRecipes : (recipes: Recipe[]) => void;
// }

export default function IngredientsCard() {
  const [recipes, setRecipes] = useState<Recipe[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  async function handleGetRecipes(){
    setLoading(true);
    try{
      const recipes = await getRecipes(["Tamati", "Onion"])
      setRecipes(recipes);
    }catch(errr){
      console.error(errr);
    }finally{
      setLoading(false);
    }
  }

  return (
    <div className="max-w-lg mx-auto bg-white p-8 rounded-lg shadow-md">
    <label className="block text-yellow-800 font-medium mb-2">
      Enter Ingredients (comma-separated):
    </label>
    <input
      type="text"
      className="w-full p-3 border border-yellow-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-yellow-500"
      placeholder="e.g., chicken, garlic, rice"
    />
    <button
      className="w-full mt-4 px-4 py-3 bg-yellow-600 text-white font-semibold rounded-lg hover:bg-yellow-700"
      onClick={handleGetRecipes}
    >
      Find Recipes
    </button>
  </div>
  )
}

