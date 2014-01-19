namespace WaterTank

namespace Components
   open Components
   (* Represents one cell in a Grid *)
   type GridItem<'a> = { Coordinate : Location; Payload : 'a }
