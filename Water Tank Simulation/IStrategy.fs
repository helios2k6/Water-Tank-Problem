namespace WaterTank

namespace Solver
   open Components

   type IStrategy = 
      abstract member Solve : GraphState -> StrategyRules -> Itinerary option