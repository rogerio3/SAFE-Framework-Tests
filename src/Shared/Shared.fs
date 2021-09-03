namespace Shared

open System

type Todo = { Id: Guid; Description: string }

// [<CLIMutable>]
// type palladristransaction1 =
//     {
//         Pairs : string;
//         Provider : string;
//         Price : float;
//         Quantity : int;
//         TransactionDate : DateTime
//     }

module Todo =
    let isValid (description: string) =
        String.IsNullOrWhiteSpace description |> not

    let create (description: string) =
        { Id = Guid.NewGuid()
          Description = description }
