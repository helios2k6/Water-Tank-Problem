namespace WaterTank

namespace Solver
   open Components
   open MathNet.Numerics

   type ExhaustiveStrategy() =
      interface IStrategy with
         member this.Solve graphState rules =
            None