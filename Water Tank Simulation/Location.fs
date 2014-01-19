namespace WaterTank

namespace Components
   (* A simple POD that designates the location of some object *)
   type Location = 
      { Row : uint64; Col : uint64; }
      override this.ToString() = sprintf "(%A, %A)" this.Row this.Col

