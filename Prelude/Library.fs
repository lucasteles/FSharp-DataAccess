namespace Prelude

module Option =
    let mapIfNull = (fun x -> if box x = null then None else Some x)