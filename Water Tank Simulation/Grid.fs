namespace WaterTank

namespace Components
   open Components
   open System.Collections.Generic

   (* A representation of the actual grid of water tanks *)
   type Grid(Start : WaterTank, End : WaterTank, GridItemSequence : GridItem seq) = 
      static let GenerateLookupMap (gridItemSequence : GridItem seq) =
         let map = new Dictionary<Location, GridItem>()
         ignore (gridItemSequence |> Seq.map (fun item -> map.Add(item.Coordinate, item)))
         map

      let _lookupMap = GenerateLookupMap GridItemSequence

      member this.Start with get() = Start
      member this.End with get() = End
      member this.GridItems with get() = GridItemSequence
      
      member this.TryGetGridItem location =
         match _lookupMap.TryGetValue location with
         | true, item -> Some(item)
         | false, _ -> None

      override this.ToString() = sprintf "Start: %A | End: %A" Start End
