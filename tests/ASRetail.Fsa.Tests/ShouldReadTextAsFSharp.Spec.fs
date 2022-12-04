module ASRetail.Fsa.ShouldReadTextAsFSharp.Spec

open Xunit

open ASRetail.Fsa.Cli.CommandHandler

[<Fact>]
let ``Should return an error if no arguments are provided`` () =
    let expected = CliError.MissingMainArgument |> Error
    
    let actual =  handle []

    Assert.equal expected actual

[<Theory>]
[<InlineData("")>]
[<InlineData(null)>]
[<InlineData("  ")>]
let ``Should return an error if an argument is empty, whitespace or null`` argument =
    let expected = ErrorMessages.emptyOrNullMainArgument |> CliError.InvalidMainArgument |> Error
    let args = Arguments.TextOrPath [argument] |> List.singleton 

    let actual = handle args

    Assert.equal expected actual