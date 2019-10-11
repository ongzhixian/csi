using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Csi.DemoConsole.SamplePrograms
{
    public class IssueClassification : ISampleProgram
    {
        private string appPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private  string trainDataPath => Path.Combine(Environment.CurrentDirectory, "data", "issue-classification", "issues_train.tsv");
        private  string testDataPath => Path.Combine(Environment.CurrentDirectory, "data", "issue-classification", "issues_test.tsv");        
        private  string modelPath => Path.Combine(Environment.CurrentDirectory, "data", "issue-classification", "issue-classification-model.zip");

        private  MLContext mlContext;
        private  PredictionEngine<GitHubIssue, IssuePrediction> predEngine;
        private  ITransformer trainedModel;
        private IDataView trainingDataView;

        public void DoWork()
        {
            // All ML.NET operations start in the MLContext class. 
            // Initializing mlContext creates a new ML.NET environment that can be shared across the model creation workflow objects. 
            // It's similar, conceptually, to DBContext in Entity Framework.

            // initialize the _mlContext global variable with a new instance of MLContext with a random seed (seed: 0) 
            // for repeatable/deterministic results across multiple trainings.
            this.mlContext = new MLContext(seed: 0);


            // ML.NET uses the IDataView class as a flexible, efficient way of describing numeric or text tabular data. 
            // IDataView can load either text files or in real time (for example, SQL database or log files).
            // To initialize and load the _trainingDataView global variable
            // LoadFromTextFile() defines the data schema and reads in the file. It takes in the data path variables and returns an IDataView
            this.trainingDataView = this.mlContext.Data.LoadFromTextFile<GitHubIssue>(this.trainDataPath,hasHeader: true);

            // ProcessData method executes the following tasks:
            // 1. Extracts and transforms the data.
            // 2. Returns the processing pipeline.
            var pipeline = ProcessData();

            // The BuildAndTrainModel method executes the following tasks:
            // 1. Creates the training algorithm class.
            // 2. Trains the model.
            // 3. Predicts area based on training data.
            // 4. Returns the model.
            var trainingPipeline = BuildAndTrainModel(this.trainingDataView, pipeline);

            Evaluate(this.trainingDataView.Schema);

            // PredictIssue method executes the following tasks:
            // 1. Loads the saved model
            // Creates a single issue of test data.
            // Predicts Area based on test data.
            // Combines test data and predictions for reporting.
            // 5. Displays the predicted results.
            PredictIssue();
        }

        private void PredictIssue()
        {
            // Load the saved model into your application
            ITransformer loadedModel = this.mlContext.Model.Load(this.modelPath, out var modelInputSchema);

            // Create a GitHub issue to test the trained model's prediction
            GitHubIssue singleIssue = new GitHubIssue() { 
                Title = "Entity Framework crashes", Description = "When connecting to the database, EF is crashing" };

            // PredictionEngine is a convenience API, which allows you to perform a prediction on a single instance of data. 
            // PredictionEngine is not thread-safe. It's acceptable to use in single-threaded or prototype environments. 
            // For improved performance and thread safety in production environments, use the PredictionEnginePool service, 
            // which creates an ObjectPool of PredictionEngine objects for use throughout your application. 
            // See this guide on how to use PredictionEnginePool in an ASP.NET Core Web API
            // https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/serve-model-web-api-ml-net#register-predictionenginepool-for-use-in-the-application
            this.predEngine = this.mlContext.Model.CreatePredictionEngine<GitHubIssue, IssuePrediction>(loadedModel);

            // PredictionEngine to predict the Area GitHub label by adding the following code to the PredictIssue method for the prediction
            var prediction = this.predEngine.Predict(singleIssue);

            Console.WriteLine($"=============== Single Prediction - Result: {prediction.Area} ===============");
            
        }

        // The Evaluate method executes the following tasks:
        // 1. Loads the test dataset.
        // 2. Creates the multiclass evaluator.
        // 3. Evaluates the model and create metrics.
        // 4. Displays the metrics
        public void Evaluate(DataViewSchema trainingDataViewSchema)
        {
            // load the test dataset 
            var testDataView = this.mlContext.Data.LoadFromTextFile<GitHubIssue>(this.testDataPath,hasHeader: true);

            // Evaluate() method computes the quality metrics for the model using the specified dataset. 
            // It returns a MulticlassClassificationMetrics object that contains the overall metrics computed 
            // by multiclass classification evaluators. 
            // To display the metrics to determine the quality of the model, you need to get them first. 
            // Notice the use of the Transform() method of the machine learning trainedModel global variable (an ITransformer) 
            // to input the features and return predictions
            var testMetrics = this.mlContext.MulticlassClassification.Evaluate(trainedModel.Transform(testDataView));

            //  following metrics are evaluated for multiclass classification:
            // Micro Accuracy - Every sample-class pair contributes equally to the accuracy metric. 
            //                  You want Micro Accuracy to be as close to 1 as possible.
            // Macro Accuracy - Every class contributes equally to the accuracy metric. 
            //                  Minority classes are given equal weight as the larger classes. 
            //                  You want Macro Accuracy to be as close to 1 as possible.
            // Log-loss - see Log Loss. You want Log-loss to be as close to zero as possible.
            // Log-loss reduction - Ranges from [-inf, 100], where 100 is perfect predictions and 0 indicates mean predictions. 
            //                      You want Log-loss reduction to be as close to zero as possible.

            Console.WriteLine($"*************************************************************************************************************");
            Console.WriteLine($"*       Metrics for Multi-class Classification model - Test Data     ");
            Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"*       MicroAccuracy:    {testMetrics.MicroAccuracy:0.###}");
            Console.WriteLine($"*       MacroAccuracy:    {testMetrics.MacroAccuracy:0.###}");
            Console.WriteLine($"*       LogLoss:          {testMetrics.LogLoss:#.###}");
            Console.WriteLine($"*       LogLossReduction: {testMetrics.LogLossReduction:#.###}");
            Console.WriteLine($"*************************************************************************************************************");

            SaveModelAsFile(this.mlContext, trainingDataViewSchema, this.trainedModel);

        }

        private void SaveModelAsFile(MLContext mlContext,DataViewSchema trainingDataViewSchema, ITransformer model)
        {
            //  save model to a file to make predictions at a later time or in another application
            //  Save method to serialize and store the trained model as a zip file.
            mlContext.Model.Save(model, trainingDataViewSchema, this.modelPath);
        }

        public IEstimator<ITransformer> BuildAndTrainModel(IDataView trainingDataView, IEstimator<ITransformer> pipeline)
        {
            // SdcaMaximumEntropy is your multiclass classification training algorithm. 
            // This is appended to the pipeline and accepts the featurized Title and Description (Features) 
            // and the Label input parameters to learn from the historic data.
            var trainingPipeline = pipeline.Append(this.mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
            .Append(this.mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Fit the model to the splitTrainSet data and return the trained model
            this.trainedModel = trainingPipeline.Fit(trainingDataView);

            //  PredictionEngine is a convenience API, which allows you to pass in and then perform a prediction on a single instance of data
            this.predEngine = this.mlContext.Model.CreatePredictionEngine<GitHubIssue, IssuePrediction>(this.trainedModel);

            // a GitHub issue to test the trained model's prediction
            GitHubIssue issue = new GitHubIssue() {
                Title = "WebSockets communication is slow in my machine",
                Description = "The WebSockets communication used under the covers by SignalR looks like is going slow in my development machine.."
            };

            // Predict() function makes a prediction on a single row of data
            var prediction = this.predEngine.Predict(issue);

            Console.WriteLine($"=============== Single Prediction just-trained-model - Result: {prediction.Area} ===============");
            return trainingPipeline;


        }

        public IEstimator<ITransformer> ProcessData()
        {
            // to predict the Area GitHub label for a GitHubIssue, use the MapValueToKey() method 
            // to transform the Area column into a numeric key type Label column 
            // (a format accepted by classification algorithms) and add it as a new dataset column
            var pipeline = this.mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: "Area", outputColumnName: "Label")

            // mlContext.Transforms.Text.FeaturizeText which transforms the text (Title and Description) columns into a numeric vector 
            // for each called TitleFeaturized and DescriptionFeaturized. 
            // Append the featurization for both columns to the pipeline:
            .Append(this.mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Title", outputColumnName: "TitleFeaturized"))
            .Append(this.mlContext.Transforms.Text.FeaturizeText(inputColumnName: "Description", outputColumnName: "DescriptionFeaturized"))

            // last step in data preparation: combines all of the feature columns into the Features column using the Concatenate() method. 
            // By default, a learning algorithm processes only features from the Features column. Append this transformation to the pipeline 
            .Append(this.mlContext.Transforms.Concatenate("Features", "TitleFeaturized", "DescriptionFeaturized"))

            //  append a AppendCacheCheckpoint to cache the DataView so when you iterate over the data multiple times 
            // using the cache might get better performance, 
            .AppendCacheCheckpoint(this.mlContext);

            return pipeline;
        }

        // GitHubIssue is the input dataset class and has the following String fields:
        // the first column ID (GitHub Issue ID)
        // the second column Area (the prediction for training)
        // the third column Title (GitHub issue title) is the first feature used for predicting the Area
        // the fourth column Description is the second feature used for predicting the Area
        public class GitHubIssue
        {
            [LoadColumn(0)]
            public string ID { get; set; }
            [LoadColumn(1)]
            public string Area { get; set; }
            [LoadColumn(2)]
            public string Title { get; set; }
            [LoadColumn(3)]
            public string Description { get; set; }
        }

        // IssuePrediction is the class used for prediction after the model has been trained. 
        // It has a single string (Area) and a PredictedLabel ColumnName attribute. 
        // The PredictedLabel is used during prediction and evaluation
        public class IssuePrediction
        {
            [ColumnName("PredictedLabel")]
            public string Area;
        }
    }
}