namespace WaterTank

(* Basic components of the grid solver *)
namespace Components
   open WaterTank.Core
   open Components

   (* The state of the grid at any given point in time *)
   type GridState = 
      { Matrix : Grid; BoatLocation : Location; }

      override this.ToString() = sprintf "Boat Location: Location %A" this.BoatLocation