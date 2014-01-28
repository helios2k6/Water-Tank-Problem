namespace WaterTank

namespace Solver
   open MathNet.Numerics
   open System.Collections.Generic
   (* 
    * Represents all of the moves that have occured so far
    *)
   type Itinerary(moves : Move seq)=
      let makeMoveSet (moves : Move seq) =
         let hashSet = new HashSet<Move>()
         for move in moves do
            ignore(hashSet.Add(move))

         hashSet
      
      let _setOfMoves = makeMoveSet moves

      (* Gets the sequence of moves *)
      member this.Moves with get() = moves

      (* Detects whether or not the proprosed move is a backtracking move *)
      member this.IsBackTrack (proposedMove : Move) =
         let reverseMove = { proposedMove with Start = proposedMove.End; End = proposedMove.Start }
         _setOfMoves.Contains(reverseMove)