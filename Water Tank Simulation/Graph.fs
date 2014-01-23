namespace WaterTank

namespace Components
   open Components
   open System.Collections.Generic

   (* A representation of the actual grid of water tanks *)
   type Graph = 
      { Start: WaterTank; End : WaterTank; WaterTanks : WaterTank seq}
      override this.ToString() = sprintf "Start: %A | End: %A" this.Start this.End
