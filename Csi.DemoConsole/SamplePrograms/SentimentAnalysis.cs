using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using static Microsoft.ML.DataOperationsCatalog;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms.Text;
using Csi.Helpers;

namespace Csi.DemoConsole.SamplePrograms
{
    // Based on:
    // Tutorial: Analyze sentiment of website comments with binary classification in ML.NET
    // https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/sentiment-analysis#create-a-console-application

    public class SentimentAnalysis : ISampleProgram
    {
        readonly string dataPath = Path.Combine(Environment.CurrentDirectory, "data", "sentiment-analysis", "yelp_labelled.txt");

        public void DoWork()
        {
            Console.WriteLine("Data path is [{0}]", this.dataPath);

            MLContext mlContext = new MLContext();

            TrainTestData splitDataView = LoadData(mlContext);

            ITransformer model = BuildAndTrainModel(mlContext, splitDataView.TrainSet);

            Evaluate(mlContext, model, splitDataView.TestSet);

            UseModelWithSingleItem(mlContext, model);

            UseModelWithBatchItems(mlContext, model);

        }

        // The UseModelWithBatchItems() method executes the following tasks:
        // Creates batch test data.
        // Predicts sentiment based on test data.
        // Combines test data and predictions for reporting.
        // Displays the predicted results.
        public static void UseModelWithBatchItems(MLContext mlContext, ITransformer model)
        {
            IEnumerable<SentimentData> sentiments = new[]
            {
                new SentimentData
                {
                    SentimentText = "This was a horrible meal"
                },
                new SentimentData
                {
                    SentimentText = "I love this spaghetti."
                }
            };

            IDataView batchComments = mlContext.Data.LoadFromEnumerable(sentiments);

            IDataView predictions = model.Transform(batchComments);

            // Use model to predict whether comment data is Positive (1) or Negative (0).
            IEnumerable<SentimentPrediction> predictedResults = mlContext.Data.CreateEnumerable<SentimentPrediction>(predictions, reuseRowObject: false);

            Console.WriteLine();

            Console.WriteLine("=============== Prediction Test of loaded model with multiple samples ===============");

            foreach (SentimentPrediction prediction  in predictedResults)
            {
                Console.WriteLine($"Sentiment: {prediction.SentimentText} | Prediction: {(Convert.ToBoolean(prediction.Prediction) ? "Positive" : "Negative")} | Probability: {prediction.Probability} ");

            }
            Console.WriteLine("=============== End of predictions ===============");

        }


        // The UseModelWithSingleItem() method executes the following tasks:
        // Creates a single comment of test data.
        // Predicts sentiment based on test data.
        // Combines test data and predictions for reporting.
        // Displays the predicted results.
        private void UseModelWithSingleItem(MLContext mlContext, ITransformer model)
        {
            // convenience API, which allows you to perform a prediction on a single instance of data. 
            // PredictionEngine is not thread-safe.
             // For improved performance and thread safety in production environments, use the PredictionEnginePool service, 
             // which creates an ObjectPool of PredictionEngine objects for use throughout your application.
             // See https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/serve-model-web-api-ml-net#register-predictionenginepool-for-use-in-the-application
            PredictionEngine<SentimentData, SentimentPrediction> predictionFunction = mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

            SentimentData sampleStatement = new SentimentData
            {
                SentimentText = "This was a very bad steak"
            };

            var resultPrediction = predictionFunction.Predict(sampleStatement);

            Console.WriteLine();
            Console.WriteLine("=============== Prediction Test of model with a single sample and test dataset ===============");

            Console.WriteLine();
            Console.WriteLine($"Sentiment: {resultPrediction.SentimentText} | Prediction: {(Convert.ToBoolean(resultPrediction.Prediction) ? "Positive" : "Negative")} | Probability: {resultPrediction.Probability} ");

            Console.WriteLine("=============== End of Predictions ===============");
            Console.WriteLine();

        }

        // The Evaluate() method executes the following tasks:
        // Loads the test dataset.
        // Creates the BinaryClassification evaluator.
        // Evaluates the model and creates metrics.
        // Displays the metrics.
        public void Evaluate(MLContext mlContext, ITransformer model, IDataView splitTestSet)
        {
            Console.WriteLine("=============== Evaluating Model accuracy with Test data===============");

            // make predictions for multiple provided input rows of a test dataset.
            IDataView predictions = model.Transform(splitTestSet);

            // once you have prediction set (predictions), the Evaluate() method assesses the model, 
            // which compares the predicted values with the actual Labels in the test dataset and 
            // returns a CalibratedBinaryClassificationMetrics object on how the model is performing.
            CalibratedBinaryClassificationMetrics metrics = mlContext.BinaryClassification.Evaluate(predictions, "Label");

            // Displaying the metrics for model validation
            Console.WriteLine();
            Console.WriteLine("Model quality metrics evaluation");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"Auc: {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
            Console.WriteLine("=============== End of model evaluation ===============");

        }

        public ITransformer BuildAndTrainModel(MLContext mlContext, IDataView splitTrainSet)
        {
            // converts the text column (SentimentText) into a numeric key type Features column used by the machine learning algorithm 
            // and adds it as a new dataset column:
            var estimator = mlContext.Transforms.Text.FeaturizeText(
                outputColumnName: "Features", inputColumnName: nameof(SentimentData.SentimentText))
            
            // Append the machine learning task to the data transformation definitions
            // This app uses a classification algorithm that categorizes items or rows of data. 
            // categorizes website comments as either positive or negative, so use the binary classification task.
            .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                labelColumnName: "Label", featureColumnName: "Features"));

            Console.WriteLine("=============== Create and Train the Model ===============");
            var model = estimator.Fit(splitTrainSet);
            Console.WriteLine("=============== End of training ===============");
            Console.WriteLine();

            return model;

        }

        // LoadData() method executes the following tasks:
        // Loads the data.
        // Splits the loaded dataset into train and test datasets.
        // Returns the split train and test datasets.
        public TrainTestData LoadData(MLContext mlContext)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<SentimentData>(this.dataPath, hasHeader: false);

            // split the loaded dataset into train and test datasets and return them in the TrainTestData class. 
            // Specify the test set percentage of data with the testFractionparameter. 
            // The default is 10%, in this case you use 20% to evaluate more data.
            TrainTestData splitDataView = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

            return splitDataView;
        }
    }

    // input dataset class, SentimentData
    // has a string for user comments (SentimentText) and 
    // a bool (Sentiment) value of either 1 (positive) or 0 (negative) for sentiment. 
    public class SentimentData
    {
        [LoadColumn(0)]
        public string SentimentText;

        [LoadColumn(1), ColumnName("Label")]
        public bool Sentiment;
    }

    // SentimentPrediction is the prediction class used after model training. 
    // It inherits from SentimentData so that the input SentimentText can be displayed along with the output prediction. 
    // The Prediction boolean is the value that the model predicts when supplied with new input SentimentText.
    // The output class SentimentPrediction contains two other properties calculated by the model: 
    // Score - the raw score calculated by the model, and 
    // Probability - the score calibrated to the likelihood of the text having positive sentiment.
    public class SentimentPrediction : SentimentData
    {

        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        public float Probability { get; set; }

        public float Score { get; set; }
    }
}