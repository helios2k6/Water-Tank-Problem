namespace WaterTank

module Core =
   (* Check an F# reference against null *)
   let internal TranslateNullToOption item =
      match box item with
      | null -> None
      | _ -> Some(item)

   (* The Maybe Monad for F# *)
   type internal MaybeMonad() =
      member this.Bind(x, f) = 
         match x with
         | Some(a) -> f(a)
         | _ -> None

      member this.Delay(f) = f()
      member this.Return(x) = Some x