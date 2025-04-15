
function RecipeCard() {
    return (
        <section className="mt-12">
            <h2 className="text-3xl font-semibold text-yellow-800 text-center mb-6">
                Recipe Recommendations
            </h2>
            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
                {[1, 2, 3, 4].map((recipe) => (
                    <div key={recipe} className="bg-white p-6 rounded-lg shadow-lg">
                        <h3 className="text-2xl font-bold text-yellow-600 mb-2">
                            Recipe Title {recipe}
                        </h3>
                        <p className="text-yellow-700 mb-4">
                            A short description of Recipe {recipe} to entice users to click through.
                        </p>
                        <button className="px-4 py-2 bg-yellow-500 text-white rounded-lg hover:bg-yellow-600">
                            View Recipe
                        </button>
                    </div>
                ))}
            </div>
        </section>
    )
}

export default RecipeCard