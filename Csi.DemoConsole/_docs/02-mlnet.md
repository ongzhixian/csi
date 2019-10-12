# Installation

Note: Requires .Net Core 2.2+

## ML.NET CLI tools

dotnet tool install -g mlnet

Note: .NET tools installed using the global flag (-g) are installed in:
$HOME/.dotnet/tools

## Tutorial (ML.NET CLI)

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

## Tutorial (API)

1. Sentiment analysis: (SentimentAnalysis.cs)
    demonstrates how to apply a **binary classification task** using ML.NET.
2. GitHub issue classification: 
    demonstrates how to apply a **multiclass classification task** using ML.NET.
3. Price predictor: 
    demonstrates how to apply a **regression task** using ML.NET.
4. Iris clustering: 
    demonstrates how to apply a **clustering task** using ML.NET.
5. Recommendation: 
    generate movie **recommendations** based on previous user ratings

6. Image classification: 
    demonstrates how to **retrain an existing TensorFlow model** to create a custom image classifier using ML.NET.
7. Anomaly detection: 
    demonstrates how to build an anomaly detection application for product sales data analysis.
8. Detect objects in images: 
    demonstrates how to detect objects in images using a **pre-trained ONNX model**.
9. Classify sentiment of movie reviews: 
    learn to load a **pre-trained TensorFlow model** to classify the sentiment of movie reviews.

SentimentAnalysis       -- Supervised learning; classify data into 2 groups
IssueClassification     -- Supervised learning; classify data into multiple groups
PricePredictor          -- Supervised learning; predicting values
IrisClustering          -- Unsupervised learning; classification
MovieRecommendation
ImageClassification
AnomalyDetection
ObjectDetection
SentimentClassification


https://docs.microsoft.com/en-us/dotnet/machine-learning/resources/glossary