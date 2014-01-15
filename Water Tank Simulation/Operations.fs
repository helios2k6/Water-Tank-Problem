namespace WaterTank

module Operations =
   open Components

   let internal MoveBoat (state : GridState) (direction : CardinalDirection) =
      let waterTankWithBoat = fst state.BoatLocation
      let previousLocation = snd state.BoatLocation
      let neighbor = waterTankWithBoat.GetNeighbor(direction)

      match neighbor with
      | Some(tank) ->
         let newLocation = 
            match direction with
            | North -> { previousLocation with Row = previousLocation.Row - 1UL }
            | South -> { previousLocation with Row = previousLocation.Row + 1UL }
            | East -> { previousLocation with Col = previousLocation.Col + 1UL }
            | West -> { previousLocation with Col = previousLocation.Col - 1UL }

         ({ state with BoatLocation = (tank, newLocation) }, true)

      | None -> (state, false)
