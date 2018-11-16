// Learn more about F# at http://fsharp.org

open System
open NumericalAnalysisF

let f x = 
    Math.Pow(x, -0.25)

let pow n x = 
    Math.Pow(x, n)



[<EntryPoint>]
let main argv =
    let a = Lab6.moment 0.0 1.0 2.0 f
    printfn "%f" <| a
    Console.ReadLine() |> ignore
    0 // return an integer exit code
