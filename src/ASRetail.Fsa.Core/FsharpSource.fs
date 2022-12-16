namespace ASRetail.Fsa.Core

open System

type AnalysisError =
    | EmptyOrNullSourceProvided
    | InvalidSourceCode of string


/// A public type for passing in the source
/// to be analyzed.
type FSharpSource = | Text of string

module FSharpSource =
    let parse =
        function
        | str when str |> String.IsNullOrWhiteSpace -> AnalysisError.EmptyOrNullSourceProvided |> Error
        | str when str.Contains("let") -> str |> FSharpSource.Text |> Ok
        | _ -> "Invalid F#" |> AnalysisError.InvalidSourceCode |> Error
