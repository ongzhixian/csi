# Installation

Note: Requires .Net Core 2.2+

## ML.NET CLI tools

dotnet tool install -g mlnet

Note: .NET tools installed using the global flag (-g) are installed in:
$HOME/.dotnet/tools

## Tutorial

Binary classification - 
    Use this when you want to analyze text and predict if it belongs in either category A or category B (e.g. analyzing sentiment of customer reviews as either positive or negative)

Multiclass classification - 
    Use this when you want to analyze text and classify into multiple categories (e.g. labeling new GitHub issues).

Regression - 
    Use this when you want to predict a numeric value (e.g. predicting house price). This task is called regression.


```
mlnet auto-train --task binary-classification --dataset "wikipedia-detox-250-line-data.tsv" --label-column-name "Sentiment" --max-exploration-time 10
```

Best accuracy - 
    This shows you the accuracy of the best model so far. 
    Higher accuracy means the model predicted more correctly on test data.
Best algorithm - 
    This shows you which algorithm has performed the best so far.
Last algorithm - 
    This shows you the last algorithm that was explored.

