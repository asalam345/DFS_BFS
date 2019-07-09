using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS_BFS
{
	class Program
	{
		
		public static void Main(string[] args)
			{
				// Create a graph given in the above diagram
				var g = new Graph();

				g.AddEdge(1, 2);
				g.AddEdge(1, 3);
				g.AddEdge(1, 4);

				g.AddEdge(2, 5);
				g.AddEdge(2, 6);
				g.AddEdge(4, 7);
				g.AddEdge(4, 8);

				g.AddEdge(5, 9);
				g.AddEdge(5, 10);
				g.AddEdge(7, 11);
				g.AddEdge(7, 12);


				Console.WriteLine("Following is Breadth First Traversal (starting from vertex 1)");
				g.BFSWalkWithStartNode(1);

				Console.WriteLine("Following is Depth First Traversal (starting from vertex 1)");
				g.DFSWalkWithStartNode(1);

				Console.WriteLine("Following is Depth First Traversal USING RECURSION (starting from vertex 1)");
				g.DFSWithRecursion(1);

				Console.ReadKey();
			}

		public class Graph
		{
			public Graph()
			{
				Adj = new Dictionary<int, HashSet<int>>();
			}

			public Dictionary<int, HashSet<int>> Adj { get; private set; }

			public void AddEdge(int source, int target)
			{
				if (Adj.ContainsKey(source))
				{
					try
					{
						Adj[source].Add(target);
					}
					catch
					{
						Console.WriteLine("This edge already exists: " + source + " to " + target);
					}
				}
				else
				{
					var hs = new HashSet<int>();
					hs.Add(target);
					Adj.Add(source, hs);
				}
			}

			public void BFSWalkWithStartNode(int vertex)
			{
				var visited = new HashSet<int>();
				// Mark this node as visited
				visited.Add(vertex);
				// Queue for BFS
				var q = new Queue<int>();
				// Add this node to the queue
				q.Enqueue(vertex);

				while (q.Count > 0)
				{
					var current = q.Dequeue();
					Console.WriteLine(current);
					// Only if the node has a any adj notes
					if (Adj.ContainsKey(current))
					{
						// Iterate through UNVISITED nodes
						foreach (int neighbour in Adj[current].Where(a => !visited.Contains(a)))
						{
							visited.Add(neighbour);
							q.Enqueue(neighbour);
						}
					}
				}
			}

			public int BFSFindNodeWithStartNode(int vertex, int lookingFor)
			{
				if (vertex == lookingFor)
				{
					Console.WriteLine("Found it!");
					Console.WriteLine("Steps Took: 0");
					return 0;
				}
				var visited = new HashSet<int>();
				// Mark this node as visited
				visited.Add(vertex);
				// Queue for BFS
				var q = new Queue<int>();
				// Add this node to the queue
				q.Enqueue(vertex);

				int count = 0;

				while (q.Count > 0)
				{
					var current = q.Dequeue();
					Console.WriteLine(current);
					if (current == lookingFor)
					{
						Console.WriteLine("Found it!");
						Console.WriteLine("Steps Took: " + count);
						return visited.Count();
					}

					// Only if the node has a any adj notes
					if (Adj.ContainsKey(current))
					{
						// Iterate through UNVISITED nodes
						foreach (int neighbour in Adj[current].Where(a => !visited.Contains(a)))
						{
							visited.Add(neighbour);
							q.Enqueue(neighbour);
						}
					}
					count++;
				}
				Console.WriteLine("Could not find node!");
				return count;
			}

			public void DFSWalkWithStartNode(int vertex)
			{
				var visited = new HashSet<int>();
				// Mark this node as visited
				visited.Add(vertex);
				// Stack for DFS
				var s = new Stack<int>();
				// Add this node to the stack
				s.Push(vertex);

				while (s.Count > 0)
				{
					var current = s.Pop();
					Console.WriteLine(current);
					// ADD TO VISITED HERE
					if (!visited.Contains(current))
					{
						visited.Add(current);
					}
					// Only if the node has a any adj notes
					if (Adj.ContainsKey(current))
					{
						// Iterate through UNVISITED nodes
						foreach (int neighbour in Adj[current].Where(a => !visited.Contains(a)))
						{
							visited.Add(neighbour);
							s.Push(neighbour);
						}
					}
				}
			}

			public void DFSWithRecursion(int vertex)
			{
				var visited = new HashSet<int>();
				Traverse(vertex, visited);
			}

			private void Traverse(int v, HashSet<int> visited)
			{
				// Mark this node as visited
				visited.Add(v);
				Console.WriteLine(v);
				// Only if the node has a any adj notes
				if (Adj.ContainsKey(v))
				{
					// Iterate through UNVISITED nodes
					foreach (int neighbour in Adj[v].Where(a => !visited.Contains(a)))
					{
						Traverse(neighbour, visited);
					}
				}
			}
		}
	}
}
