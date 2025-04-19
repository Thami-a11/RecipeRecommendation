import RecipeCard from '../components/RecipeCard';
import IngredientsCard from '../components/IngredientsCard';


function Home() {
  return (
      <div className="min-h-screen bg-gradient-to-r from-yellow-100 via-white to-yellow-100">
        <div className="container mx-auto p-6">
          <header className="text-center mb-12">
            <h1 className="text-5xl font-extrabold text-yellow-600">Recipe Recommendation App</h1>
            <p className="text-xl text-yellow-700 mt-4">
              Discover delicious recipes using ingredients you already have!
            </p>
          </header>
  
          {/* Ingredient Input Section */}
          <IngredientsCard />
  
          {/* Recipes Section */}
          <RecipeCard />
        </div>
      </div>
  )
}

export default Home;
