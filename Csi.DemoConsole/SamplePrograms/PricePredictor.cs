using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Csi.DemoConsole.SamplePrograms
{
    public class PricePredictor : ISampleProgram
    {
        readonly string trainDataPath = Path.Combine(Environment.CurrentDirectory, "data", "price-predictor", "taxi-fare-train.csv");
        readonly string testDataPath = Path.Combine(Environment.CurrentDirectory, "data", "price-predictor", "taxi-fare-test.csv");
        readonly string modelPath = Path.Combine(Environment.CurrentDirectory, "data", "price-predictor", "taxi-price-predictor.zip");
        

        public void DoWork()
        {
            MLContext mlContext = new MLContext(seed: 0);

            // Train() method executes the following tasks:
            // Loads the data.
            // Extracts and transforms the data.
            // Trains the model.
            // Returns the model.
            var model = Train(mlContext, this.trainDataPath);

            // The Evaluate method executes the following tasks:
            // Loads the test dataset.
            // Creates the regression evaluator.
            // Evaluates the model and creates metrics.
            // Displays the metrics.
            Evaluate(mlContext, model);

            // The TestSinglePrediction method executes the following tasks:
            // Creates a single comment of test data.
            // Predicts fare amount based on test data.
            // Combines test data and predictions for reporting.
            // Displays the predicted results.
            TestSinglePrediction(mlContext, model);


        }

        private void TestSinglePrediction(MLContext mlContext, ITransformer model)
        {
            // Use the PredictionEngine to predict the fare
            // The PredictionEngine is a convenience API, which allows you to perform a 
            // prediction on a single instance of data. 
            // PredictionEngine is not thread-safe. 
            // It's acceptable to use in single-threaded or prototype environments. 
            // For improved performance and thread safety in production environments, 
            // use the PredictionEnginePool service, which creates an ObjectPool of PredictionEngine objects 
            // for use throughout your application. See this guide on how to use PredictionEnginePool in an ASP.NET Core Web API
            // https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/serve-model-web-api-ml-net#register-predictionenginepool-for-use-in-the-application
            var predictionFunction = mlContext.Model.CreatePredictionEngine<TaxiTrip, TaxiTripFarePrediction>(model);

            // This tutorial uses one test trip within this class. Later you can add other scenarios to experiment with the model.
            var taxiTripSample = new TaxiTrip()
            {
                VendorId = "VTS",
                RateCode = "1",
                PassengerCount = 1,
                TripTime = 1140,
                TripDistance = 3.75f,
                PaymentType = "CRD",
                FareAmount = 0 // To predict. Actual/Observed = 15.5
            };

            // predict the fare based on a single instance of the taxi trip data and pass it to the PredictionEngine
            // The Predict() function makes a prediction on a single instance of data.
            var prediction = predictionFunction.Predict(taxiTripSample);

            // To display the predicted fare of the specified trip
            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted fare: {prediction.FareAmount:0.####}, actual fare: 15.5");
            Console.WriteLine($"**********************************************************************");

        }

        private void Evaluate(MLContext mlContext, ITransformer model)
        {
            // Load the test dataset using the LoadFromTextFile() method. 
            // Evaluate the model using this dataset as a quality check
            IDataView dataView = mlContext.Data.LoadFromTextFile<TaxiTrip>(this.testDataPath, hasHeader: true, separatorChar: ',');

            // transform the Test data
            // The Transform() method makes predictions for the test dataset input rows.
            var predictions = model.Transform(dataView);

            // The RegressionContext.Evaluate method computes the quality metrics for the 
            // PredictionModel using the specified dataset. 
            // It returns a RegressionMetrics object that contains the overall metrics computed by regression evaluators.
            // To display these to determine the quality of the model, you need to get the metrics first. 

            // Once you have the prediction set, the Evaluate() method assesses the model, 
            // which compares the predicted values with the actual Labels in the test dataset 
            // and returns metrics on how the model is performing.
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");

            //
            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       Model quality metrics evaluation         ");
            Console.WriteLine($"*------------------------------------------------");

            // RSquared is another evaluation metric of the regression models. 
            // RSquared takes values between 0 and 1. The closer its value is to 1, the better the model is. 
            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");

            // RMS is one of the evaluation metrics of the regression model. The lower it is, the better the model is. 
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");

        }

        public ITransformer Train(MLContext mlContext, string dataPath)
        {
            // ML.NET uses the IDataView class as a flexible, efficient way of describing numeric or text tabular data. 
            // IDataView can load either text files or in real time (for example, SQL database or log files). 
            IDataView dataView = mlContext.Data.LoadFromTextFile<TaxiTrip>(dataPath, hasHeader: true, separatorChar: ',');

            // We want to predict the taxi trip fare, the FareAmount column is the Label that you will predict 
            // (the output of the model)Use the CopyColumnsEstimator transformation class to copy FareAmount
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName:"FareAmount")

            // The algorithm that trains the model requires numeric features, 
            // so you have to transform the categorical data (VendorId, RateCode, and PaymentType) values into numbers 
            // (VendorIdEncoded, RateCodeEncoded, and PaymentTypeEncoded). 
            // To do that, use the OneHotEncodingTransformer transformation class, 
            // which assigns different numeric key values to the different values in each of the columns, 
            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "VendorIdEncoded", inputColumnName:"VendorId"))
            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "RateCodeEncoded", inputColumnName: "RateCode"))
            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "PaymentTypeEncoded", inputColumnName: "PaymentType"))

            // The last step in data preparation combines all of the feature columns into the Features column using the 
            // mlContext.Transforms.Concatenate transformation class. 
            // By default, a learning algorithm processes only features from the Features column
            .Append(mlContext.Transforms.Concatenate(
                "Features", "VendorIdEncoded", "RateCodeEncoded", "PassengerCount", "TripTime", "TripDistance", "PaymentTypeEncoded"))

            // This problem is about predicting a taxi trip fare in New York City. 
            // At first glance, it may seem to depend simply on the distance traveled. 
            // However, taxi vendors in New York charge varying amounts for other factors 
            // such as additional passengers or paying with a credit card instead of cash. 
            // You want to predict the price value, which is a real value, based on the other factors in the dataset. 
            // To do that, you choose a regression machine learning task.
            // Append the FastTreeRegressionTrainer machine learning task to the data transformation definitions
            .Append(mlContext.Regression.Trainers.FastTree());

            // Fit the model to the training dataview and return the trained model 
            // The Fit() method trains your model by transforming the dataset and applying the training.
            var model = pipeline.Fit(dataView);

            // Return the model
            return model;
        }

        public class TaxiTrip
        {
            [LoadColumn(0)]
            public string VendorId;

            [LoadColumn(1)]
            public string RateCode;

            [LoadColumn(2)]
            public float PassengerCount;

            [LoadColumn(3)]
            public float TripTime;

            [LoadColumn(4)]
            public float TripDistance;

            [LoadColumn(5)]
            public string PaymentType;

            [LoadColumn(6)]
            public float FareAmount;
        }

        public class TaxiTripFarePrediction
        {
            [ColumnName("Score")]
            public float FareAmount;
        }
    }
}