const API_BASE = "https://localhost:7204/api";

export const getRecipes = async (ingredients: string[]) => {
  const response = await fetch(`${API_BASE}/Recommend`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ ingredients })
  });

  if (!response.ok) {
    throw new Error("Failed to fetch recipes");
  }

  return response.json();
};
