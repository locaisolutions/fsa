module ASRetail.Fsa.Cli.CommandHandler
open Argu 
open System
open FsToolkit.ErrorHandling

[<NoEquality; NoComparison>]
type Arguments = 
    | [<MainCommand>] TextOrPath of string list

module Arguments = 
    let ofArray = 
        Option.ofObj
        >> Option.defaultValue [||]
        >> List.ofArray
        >> Arguments.TextOrPath
        >> List.singleton

    let echo args =
        let toString =
            function
            | TextOrPath content -> "Main Arguments:" :: content |> String.concat "\n\t"

        args
        |> List.head
        |> toString

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

type CliReport = private CliReport of string  

module CliReport = 
    let zero = "" |> CliReport

    let toString (CliReport str) = str

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
        |> Result.ignore

let handle (args: Arguments list) = 
    result {
        match args with 
        | [] -> return! Error CliError.MissingMainArgument
        | args -> 
            let! _ = args |> List.traverseResultM parseArgument 
            return  
                args
                |> Arguments.echo
                |> CliReport.CliReport
    }
