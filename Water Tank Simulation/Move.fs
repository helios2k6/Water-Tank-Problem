namespace WaterTank

namespace Solver
   open MathNet.Numerics

   (* 
    * Represents one move between two water tanks. Does not reference the water tanks to avoid
    * ambiguitity between the "new" water tanks and the "old" water tanks
    *)
   type Move = 
      { Start : uint64; End : uint64; Amount : BigRational }
      override this.ToString() = sprintf "Move from %A to %A with %A amount of water transfered" this.Start this.End this.Amount