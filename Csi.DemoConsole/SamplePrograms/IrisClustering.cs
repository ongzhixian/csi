using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace Csi.DemoConsole.SamplePrograms
{
    public class IrisClustering : ISampleProgram
    {
        readonly string dataPath = Path.Combine(Environment.CurrentDirectory, "data", "iris-clustering", "iris.data");
        readonly string modelPath = Path.Combine(Environment.CurrentDirectory, "data", "iris-clustering", "iris-clustering-model.zip");

        public void DoWork()
        {
            var mlContext = new MLContext(seed: 0);
            
            // The generic MLContext.Data.LoadFromTextFile extension method infers the data set schema from 
            // the provided IrisData type and returns IDataView which can be used as input for transformers.
            IDataView dataView = mlContext.Data.LoadFromTextFile<IrisData>(this.dataPath, hasHeader: false, separatorChar: ',');

            //  the learning pipeline of the clustering task comprises two following steps:
            // 1. concatenate loaded columns into one Features column, which is used by a clustering trainer;
            // 2. use a KMeansTrainer trainer to train the model using the k-means++ clustering algorithm.
            string featuresColumnName = "Features";
            var pipeline = mlContext.Transforms
                .Concatenate(featuresColumnName, "SepalLength", "SepalWidth", "PetalLength", "PetalWidth")
                .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 3));

            var model = pipeline.Fit(dataView);

            // Save the model
            using (var fileStream = new FileStream(this.modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model, dataView.Schema, fileStream);
            }

            // make predictions, use the PredictionEngine<TSrc,TDst> class that takes instances of the 
            // input type through the transformer pipeline and produces instances of the output type
            var predictor = mlContext.Model.CreatePredictionEngine<IrisData, ClusterPrediction>(model);

            IrisData TestSetosa = new IrisData
            {
                SepalLength = 5.1f,
                SepalWidth = 3.5f,
                PetalLength = 1.4f,
                PetalWidth = 0.2f
            };

            var prediction = predictor.Predict(TestSetosa);
            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");


        }
    }

    // input data class and has definitions for each feature from the data set.
    public class IrisData
    {
        [LoadColumn(0)]
        public float SepalLength;

        [LoadColumn(1)]
        public float SepalWidth;

        [LoadColumn(2)]
        public float PetalLength;

        [LoadColumn(3)]
        public float PetalWidth;
    }

    // ClusterPrediction class represents the output of the clustering model applied to an IrisData instance. 
    // Use the ColumnName attribute to bind the PredictedClusterId and Distances fields to the 
    // PredictedLabel and Score columns respectively. In case of the clustering task those columns have the following meaning:
    // 1. PredictedLabel column contains the ID of the predicted cluster.
    // 2. Score column contains an array with squared Euclidean distances to the cluster centroids. 
    //    The array length is equal to the number of clusters.
    public class ClusterPrediction
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId;

        [ColumnName("Score")]
        public float[] Distances;
    }
}