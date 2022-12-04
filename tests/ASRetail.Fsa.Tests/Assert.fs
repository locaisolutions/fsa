namespace Xunit

/// While it may seem premature to add helpers like this
/// Unqoute has not been updated in nearly 2 years and 
/// is roughly 80x slower than the XUnit Asserts. In case
/// we decide to switch later this will make that transition 
/// a tad bit easier. The reason we are still using it is 
/// because of how nice it handles error messages.
[<RequireQualifiedAccess>]
module Assert =
    open Swensen.Unquote

    let equal expected actual = 
        test <@ expected = actual @>