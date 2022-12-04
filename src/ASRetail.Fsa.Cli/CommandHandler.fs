module ASRetail.Fsa.Cli.CommandHandler
open Argu 
open System
open FsToolkit.ErrorHandling

[<NoEquality; NoComparison>]
type Arguments = 
    | [<MainCommand>] TextOrPath of string list

module ErrorMessages = 
    let emptyOrNullMainArgument = "One or more of the main arguments was invalid since it was empty, null or whitespace."

[<RequireQualifiedAccess>]
type CliError = 
    | MissingMainArgument
    | InvalidMainArgument of string

type TextOrPath = 
    private 
    | Text of string
    | Path of string    

type CliReport = private CliReport of unit  

let private parseTextOrPath arg = 
    if arg |> String.IsNullOrWhiteSpace then 
        ErrorMessages.emptyOrNullMainArgument
        |> CliError.InvalidMainArgument
        |> Error
        
    else 
        arg |> TextOrPath.Text |> Ok

let private parseArgument = 
    function 
    | TextOrPath textOrPaths ->
        textOrPaths 
        |> List.traverseResultM parseTextOrPath
        |> Result.map (ignore >> CliReport)

let handle (args: Arguments list) = 
    match args with 
    | [] -> Error CliError.MissingMainArgument
    | args -> args |> List.traverseResultM parseArgument
