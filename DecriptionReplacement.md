# How to use the Description Replacement Json

``` json

[ 
    {
        "transactionDescription": "string",
        "transactionType" : 0, // 1 for charge, 2 for credit, 0 for either
        // Don't set value to keep AspireTransaction default
        // use "{Amount}" to use the incoming transaction amount
        "transactionMin" : 0,
        "transactionMax" : 0,
        "budgetEntry": [ 
        {
            "date" : "dd/mm/yyyy",
            "outflow" : "0.00",
            "inflow" : "0.00",
            "category" : "string",
            "account" : "string", // Defaults to incoming transaction account
            "memo" : "string",
            "status" : "string" //Defaults to Complete
        }]
    }
]


```
