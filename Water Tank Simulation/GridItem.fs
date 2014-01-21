namespace WaterTank

namespace Components
   open Components

   (* Represents one cell in a Grid *)
   type GridItem = { Coordinate : Location; Payload : WaterTank }
