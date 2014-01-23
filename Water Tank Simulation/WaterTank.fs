namespace WaterTank

namespace Components
   open WaterTank.Core
   open MathNet.Numerics

  (* The water tank object itself *)
   type WaterTank =
      { Id : uint64; AmountOfWater : BigRational; MaxCapacity : BigRational; Neighbors : WaterTank seq }
      override this.ToString() = sprintf "Id = %A | Amount of Water = %A | Max Capacity = %A" this.Id this.AmountOfWater this.MaxCapacity