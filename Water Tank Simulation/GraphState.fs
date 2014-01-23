namespace WaterTank

(* Basic components of the grid solver *)
namespace Components
   open WaterTank.Core
   open Components

   (* The state of the grid at any given point in time *)
   type GraphState = 
      { Graph : Graph; BoatLocation : WaterTank; }

      override this.ToString() = sprintf "Boat Location: Location %A" this.BoatLocation