using Advanced.Algorithms.DataStructures.Graph.AdjacencyList;
using Advanced.Algorithms.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace The_Mole_Backend.Models
{
    public class MainAlgorithm
    {
        public string Source { get; set; }
        public string Source2 { get; set; }
        public string Target { get; set; }
        static Random random = new Random();

        public MainAlgorithm(string source, string target)
        {
            Source = source;
            Target = target;
        }
        public MainAlgorithm()
        {

        }


        //this function will be called 1 time only. 
        public void GetGraph(List<string> vertecies, List<string[]> edges)
        {
            //get all vertecies and edges from DBservices
            //DBserviecs db = new DBservices;
            //db.GetEdgesAndVertecies(vertecies,edges)
            //use graph.AddVertex('SOMESTRING') in a foreach loop
            //use  graph.AddEdge('A', 'B',0);
        }
        public List<int> GetPathsAdvancedIntersect(ref List<List<string>> Paths)
        {
            List<string> vertecies = new List<string>();
            List<List<string>> edges = new List<List<string>>();

            // get all vertecies and edges from DB
            DBservices db = new DBservices();
            vertecies = db.GetVertecies("ConnectionStringTheMole", "MoviesVertecies");
            edges = db.GetEdges("ConnectionStringTheMole", "MoviesEdges");

            //1. create a graph
            var graph = new WeightedDiGraph<string, int>();
            //2. insert vertecies to the graph
            foreach (string vertex in vertecies)
            {
                graph.AddVertex(vertex);
            }
            //3. insert edges to the graph
            foreach (var edge in edges)
            {
                graph.AddEdge(edge[0], edge[1], 1);
            }
            //remove one-direction edges
            List<List<string>> twoDirectionsEdges = new List<List<string>>();
            foreach (var edge in edges)
            {
                if (!graph.HasEdge(edge[1], edge[0]))
                {
                    graph.RemoveEdge(edge[0], edge[1]);
                }
                if (graph.HasEdge(edge[1], edge[0]))
                {
                    twoDirectionsEdges.Add(edge);
                }
            }
            //4. create dijkstra algorithm
            var algorithm = new DijikstraShortestPath<string, int>(new DijikstraShortestPathOperators());


            List<int> pathsCount = new List<int>();
            //var result = algorithm.FindShortestPath(graph, Source, Target);
            //pathsCount.Add(result.Path.Count);
            //Paths.Add(result.Path

            //5.run the algoritm for 110 random vertecies.
            for (int i = 0; i < 200; i++)
            {
                int sourceVertex = random.Next(0, vertecies.Count);
                int targetVertex = random.Next(0, vertecies.Count);
                //if source and target pages are the same DON'T run the algorithm
                if (sourceVertex != targetVertex)
                {
                    var result = algorithm.FindShortestPath(graph, vertecies[sourceVertex], vertecies[targetVertex]);
                    pathsCount.Add(result.Path.Count);
                    Paths.Add(result.Path);

                }
            }

            return pathsCount;
        }


        public List<int> GetPathsSimple(ref List<List<string>> Paths)
        {
            List<string> vertecies = new List<string>();
            List<List<string>> edges = new List<List<string>>();

            // get all vertecies and edges from DB
            DBservices db = new DBservices();
            vertecies = db.GetVertecies("ConnectionStringTheMole", "MoviesVertecies");
            edges = db.GetEdges("ConnectionStringTheMole", "MoviesEdges");

            //1. create a graph
            var graph = new WeightedDiGraph<string, int>();
            //2. insert vertecies to the graph
            foreach (string vertex in vertecies)
            {
                graph.AddVertex(vertex);
            }
            //3. insert edges to the graph
            foreach (var edge in edges)
            {
                graph.AddEdge(edge[0], edge[1], 1);
            }
           
            //4. create dijkstra algorithm
            var algorithm = new DijikstraShortestPath<string, int>(new DijikstraShortestPathOperators());


            List<int> pathsCount = new List<int>();
            //var result = algorithm.FindShortestPath(graph, Source, Target);
            //pathsCount.Add(result.Path.Count);
            //Paths.Add(result.Path

            //5.run the algoritm for 110 random vertecies.
            for (int i = 0; i < 200; i++)
                {
                    int sourceVertex = random.Next(0, vertecies.Count);
                    int targetVertex = random.Next(0, vertecies.Count);
                    //if source and target pages are the same DON'T run the algorithm
                    if (sourceVertex != targetVertex)
                    {
                        var result = algorithm.FindShortestPath(graph, vertecies[sourceVertex], vertecies[targetVertex]);
                        pathsCount.Add(result.Path.Count);
                        Paths.Add(result.Path);

                    }
                }

            return pathsCount;
        }

    }
    //healper for the algorithm
    public class DijikstraShortestPathOperators : IShortestPathOperators<int>
    {
        public int DefaultValue
        {
            get
            {
                return 0;
            }


        }

        public int MaxValue
        {
            get
            {
                return int.MaxValue;
            }
        }

        public int Sum(int a, int b)
        {
            return checked(a + b);
        }
    }

}