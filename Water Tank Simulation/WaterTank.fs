namespace WaterTank

namespace Components
   open WaterTank.Core
   open Components
   open MathNet.Numerics

  (* The water tank object itself *)
   type WaterTank =
      { Id : uint64; AmountOfWater : BigRational; MaxCapacity : BigRational; Neighbors : (WaterTank * CardinalDirection) seq }
      override this.ToString() = sprintf "Id = %A | Amount of Water = %A | Max Capacity = %A" this.Id this.AmountOfWater this.MaxCapacity

      member this.TryGetNeighbor (direction : CardinalDirection) = 
         let neighborQuery = 
            query {
               for neighbor in this.Neighbors do
               where ((snd neighbor) = direction)
               select (fst neighbor)
               exactlyOneOrDefault
            }

         TranslateNullToOption neighborQuery