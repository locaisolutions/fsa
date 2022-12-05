namespace ASRetail.Fsa.Core

open System

type AnalysisError = | EmptyOrNullSourceProvided


/// A public type for passing in the source
/// to be analyzed.
type FSharpSource = | Text of string

module FSharpSource =
    let parse =
        function
        | str when str |> String.IsNullOrWhiteSpace -> AnalysisError.EmptyOrNullSourceProvided |> Error
        | str -> str |> FSharpSource.Text |> Ok
