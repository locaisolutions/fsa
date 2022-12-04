module ASRetail.Fsa.Cli.CommandHandler
open Argu 

[<NoEquality; NoComparison>]
type Arguments = 
    | [<MainCommand>] TextOrPath of string list

[<RequireQualifiedAccess>]
type CliError = 
    | MissingMainArgument

type CliReport = private CliReport of unit  

let handle (args: Arguments list) = 
    match args with 
    | [] -> Error CliError.MissingMainArgument
    | _ -> () |> CliReport |> Ok
