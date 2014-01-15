namespace WaterTank

(* Basic components of the grid solver *)
module Components =
   open System.Collections.Generic

   (* The basic cardinal directions *)
   type CardinalDirection = 
      | North
      | South
      | East
      | West

   (* The water tank object itself *)
   type WaterTank(Id : uint64, AmountOfWater : uint64, MaxCapacity : uint64, Neighbors : (WaterTank * CardinalDirection) list) =
      let rec createLookupMap (arrayOfMappings : (WaterTank * CardinalDirection) list) (map : Dictionary<CardinalDirection, WaterTank>) =
         match arrayOfMappings with
         | h::t -> 
            do map.Add((snd h), (fst h))
            createLookupMap t map
         | _ -> map

      let lookupMap = createLookupMap Neighbors (new Dictionary<CardinalDirection, WaterTank>())

      override this.ToString() = sprintf "Id = %A | Amount of Water = %A | Max Capacity = %A" Id AmountOfWater MaxCapacity

      member this.GetNeighbor (direction : CardinalDirection) = 
         match lookupMap.TryGetValue(direction) with
         | true, output -> Some(output)
         | false, _ -> None

   (* A simple POD that designates the location of some object *)
   type Location = 
      { Row : uint64;Col : uint64; }
      override this.ToString() = sprintf "(%A, %A)" this.Row this.Col

   (* A representation of the actual grid of water tanks *)
   type WaterTankGrid = 
      { StartTank : WaterTank; EndTank : WaterTank; TankMatrix : WaterTank list list }
      override this.ToString() = sprintf "Start Tank: %A | End Tank: %A" this.StartTank this.EndTank

   (* The state of the grid at any given point in time *)
   type GridState = 
      { Grid : WaterTankGrid; BoatLocation : WaterTank * Location; }
      override this.ToString() = sprintf "Boat Location: (WaterTank %A, Location %A)" (fst this.BoatLocation) (snd this.BoatLocation)