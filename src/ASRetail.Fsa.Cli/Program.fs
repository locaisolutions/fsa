open FsToolkit.ErrorHandling

open ASRetail.Fsa.Cli.CommandHandler

[<EntryPoint>]
let main args =

    let args = args |> Arguments.ofArray

    let printReport report =
        printfn $"%s{report |> CliReport.toString}"
        0

    let printErrors =
        function
        | CliError.InvalidMainArgument err ->
            printfn $"The main argument is not valid.\n\n Error Message: %A{err}"
            1
        | CliError.MissingMainArgument ->
            printfn
                "%s"
                "The main argument is required and should be either:\n\t- F# Text to check\n\t- Path to F# file(s), project(s) or solution"

            1

    let printer =
        function
        | Error err -> printErrors err
        | Ok report -> printReport report

    args |> handle |> printer
