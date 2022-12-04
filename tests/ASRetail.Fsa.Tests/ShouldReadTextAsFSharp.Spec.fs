module ASRetail.Fsa.ShouldReadTextAsFSharp.Spec

open Xunit

open ASRetail.Fsa.Cli.CommandHandler

[<Fact>]
let ``Should return an error if no arguments are provided`` () =
    let expected = CliError.MissingMainArgument |> Error
    
    let actual =  handle []

    Assert.equal expected actual
