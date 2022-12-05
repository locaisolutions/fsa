module ASRetail.Fsa.ShouldReadTextAsFSharp.Spec

open Xunit

open ASRetail.Fsa.Core
open ASRetail.Fsa.Cli.CommandHandler

[<Fact>]
let ``Should return an error if no arguments are provided`` () =
    let expected = CliError.MissingMainArgument |> Error

    let actual = handle []

    Assert.equal expected actual

[<Theory>]
[<InlineData("")>]
[<InlineData(null)>]
[<InlineData("  ")>]
let ``Should return an error if an argument is empty, whitespace or null`` argument =
    let expected =
        AnalysisError.EmptyOrNullSourceProvided |> CliError.InvalidMainArgument |> Error

    let args = Arguments.TextOrPath [ argument ] |> List.singleton

    let actual = handle args

    Assert.equal expected actual

[<Fact>]
let ``Should echo the provided argument`` () =
    let arg = "let x = 5"
    let args = [ arg ] |> Arguments.TextOrPath |> List.singleton

    let actual =
        handle args |> Result.map (CliReport.toString) |> Result.defaultValue ""

    Assert.Contains (arg, actual)
