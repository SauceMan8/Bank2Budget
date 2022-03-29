# How to use the Description Replacement Json

``` json
{
    "transactions": [ 
    {
        "transactionDescription": "string",
        "transactionType" : 0, // -1 for charge, 1 for credit, 0 for either
        // Don't set value to keep AspireTransaction default
        // use "{Amount}" to use the incoming transaction amount
        "transactionMax" : 0,
        "budgetEntry": [ 
        {
            "date" : "dd/mm/yyyy",
            "outflow" : "0.00",
            "inflow" : "0.00",
            "category" : "string",
            "account" : "string",
            "memo" : "string",
            "status" : "string"
        }]
    }]
}

```
