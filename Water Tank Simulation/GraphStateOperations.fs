namespace WaterTank

namespace Operations

module GraphState =
   open Components
   open WaterTank.Core

   (* Generate the new start and end water tanks if there's enough water to transfer *)
   let private TryGenerateNewStartEndWaterTanks (oldStartTank : WaterTank) (oldDestinationTank : WaterTank) amountOfWaterToTransfer =
      if amountOfWaterToTransfer > oldStartTank.AmountOfWater then
         None
      else
         let newStartTank = { oldStartTank with AmountOfWater = oldStartTank.AmountOfWater - amountOfWaterToTransfer }
         let newDestinationTank = { oldDestinationTank with AmountOfWater = oldDestinationTank.AmountOfWater + amountOfWaterToTransfer }
         Some (newStartTank, newDestinationTank)
  
  (* Generates a new Grid Item sequence based on the old start tank and the new start tank *)
   let private GenerateNewWaterTankSequence (oldGridItems : WaterTank seq) oldStart oldDestination newStart newDestination =
      query {
         for gridItem in oldGridItems do
         where (gridItem <> oldStart && gridItem <> oldDestination)
         select gridItem
      }
      |> Seq.append (seq { yield newStart; yield newDestination; })

   (* Try to move the boat from one tank to the other *)
   let internal TryMoveBoat (state : GraphState) (destination : WaterTank) amount = 
         (* Old grid *)
         let oldGrid = state.Graph

         (* Get the old start tank *)
         let oldStartTank = state.BoatLocation
            
         (* Get the old destination tank *)
         let oldDestinationTank = destination

         maybe {
            (* Attempt to generate the start and end tank *)
            let! newStartAndDestinationTank = TryGenerateNewStartEndWaterTanks oldStartTank oldDestinationTank amount
            let newStartTank = fst newStartAndDestinationTank
            let newDestinationTank = snd newStartAndDestinationTank

            (* Query the sequence of grid items from the old state *)
            let newWaterTankSequence = 
               GenerateNewWaterTankSequence 
                  oldGrid.WaterTanks
                  oldStartTank 
                  oldDestinationTank 
                  newStartTank 
                  newDestinationTank

            (* Create the new Grid *)
            let newGraph = { oldGrid with WaterTanks = newWaterTankSequence }

            (* Create the new Grid State *)
            return { Graph = newGraph; BoatLocation = newDestinationTank }
         }

