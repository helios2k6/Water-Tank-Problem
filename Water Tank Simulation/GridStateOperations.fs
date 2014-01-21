namespace WaterTank

namespace Operations

module GridState =
   open Components
   open WaterTank.Core

   (* Calculates and creates a new location object based on the direction the boat went *)
   let private CalculateLocation previousLocation direction =
      match direction with
      | North -> { previousLocation with Row = previousLocation.Row - 1UL }
      | South -> { previousLocation with Row = previousLocation.Row + 1UL }
      | West -> { previousLocation with Col = previousLocation.Row - 1UL }
      | East -> { previousLocation with Col = previousLocation.Col + 1UL}

   (* Generate the new start and end water tanks if there's enough water to transfer *)
   let private TryGenerateNewStartEndWaterTanks (oldStartTank : WaterTank) (oldDestinationTank : WaterTank) amountOfWaterToTransfer =
      if amountOfWaterToTransfer > oldStartTank.AmountOfWater then
         None
      else
         let newStartTank = { oldStartTank with AmountOfWater = oldStartTank.AmountOfWater - amountOfWaterToTransfer }
         let newDestinationTank = { oldDestinationTank with AmountOfWater = oldDestinationTank.AmountOfWater + amountOfWaterToTransfer }
         Some (newStartTank, newDestinationTank)
  
  (* Generates a new Grid Item sequence based on the old start tank and the new start tank *)
   let private GenerateNewGridItemSequence (oldGridItems : GridItem seq) oldStart oldDestination newStart newDestination =
      query {
         for gridItem in oldGridItems do
         where (gridItem <> oldStart && gridItem <> oldDestination)
         select gridItem
      }
      |> Seq.append (seq { yield newStart; yield newDestination; })

   (* Try to move the boat from one tank to the other *)
   let internal TryMoveBoat (state : GridState) (direction : CardinalDirection) amount = 
      maybe {
         (* Old grid *)
         let oldGrid = state.Matrix

         (* Get the old start tank *)
         let! oldStartTankGridItem = oldGrid.TryGetGridItem state.BoatLocation
         let oldStartTank = oldStartTankGridItem.Payload
            
         (* Calculate the location of the destination tank *)
         let locationOfDestinationTank = CalculateLocation state.BoatLocation direction

         (* Get the old destination tank *)
         let! oldDestinationTankGridItem = oldGrid.TryGetGridItem locationOfDestinationTank
         let oldDestinationTank = oldDestinationTankGridItem.Payload

         (* Calculate the new start tank and destination tank after transfer *)
         let! newStartAndDestinationTank = TryGenerateNewStartEndWaterTanks oldStartTank oldDestinationTank amount
         let newStartTank = fst newStartAndDestinationTank
         let newDestinationTank = snd newStartAndDestinationTank

         (* Get the new grid items *)
         let newStartTankGridItem = { oldStartTankGridItem with Payload = newStartTank }
         let newDestinationTankGridItem = { oldDestinationTankGridItem with Payload = newDestinationTank }

         (* Query the sequence of grid items from the old state *)
         let newGridItemSequence = 
            GenerateNewGridItemSequence 
               oldGrid.GridItems 
               oldStartTankGridItem 
               oldDestinationTankGridItem 
               newStartTankGridItem 
               newDestinationTankGridItem

         (* Create the new Grid *)
         let newGrid = new Grid(oldGrid.Start, oldGrid.End, newGridItemSequence)

         (* Create the new Grid State *)
         let newGridState = { Matrix = newGrid; BoatLocation = locationOfDestinationTank }

         return newGridState
      }
