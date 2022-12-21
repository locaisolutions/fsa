namespace ASRetail.Fsa.Core

open System
open FSharp.Compiler.CodeAnalysis

type AnalysisError =
    | EmptyOrNullSourceProvided
    | InvalidSourceCode of string


/// A public type for passing in the source
/// to be analyzed.
type FSharpSource = | Text of string

type FsaContext = { typeChecker : FSharpChecker }

module FsaContext =
    let create () =
        let typeChecker = FSharpChecker.Create () 
        { typeChecker = typeChecker }

module Ast =
    open FSharp.Compiler.Text

    let fromText { typeChecker = tc } input =
        let file = "/temp/Temp.fsx"
        let input = SourceText.ofString input

        let projOptions, diagnostics =
            tc.GetProjectOptionsFromScript (file, input, assumeDotNetFramework = false)
            |> Async.RunSynchronously

        let parsingOptions, _errors = tc.GetParsingOptionsFromProjectOptions (projOptions)

        let parseFileResults =
            tc.ParseFile (file, input, parsingOptions) |> Async.RunSynchronously

        parseFileResults.ParseTree

module FSharpSource =
    let parse =
        function
        | str when str |> String.IsNullOrWhiteSpace -> AnalysisError.EmptyOrNullSourceProvided |> Error
        | str when str.Contains ("let") ->
            let ctx = FsaContext.create ()
            let _ = Ast.fromText ctx str
            str |> FSharpSource.Text |> Ok
        | _ -> "Invalid F#" |> AnalysisError.InvalidSourceCode |> Error
