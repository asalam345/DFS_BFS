using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS_BFS
{
	public class Node
	{
		public Node()
		{
			Neighbours = new List<Node> ();
		}

		public int Value { get; set; }
		public IList<Node> Neighbours { get; set; }
	}
	class Program
	{
		static void Main(string[] args)
		{
			Node topNode = new Node() { Value = 1 };
			topNode.Neighbours.Add(new Node() { Value = 2, Neighbours = new List<Node> { new Node() { Value = 5, Neighbours = new List<Node> { new Node() { Value = 9 } } }	}});
			topNode.Neighbours.Add(new Node() { Value = 3, Neighbours = new List<Node> { new Node() { Value = 6, Neighbours = new List<Node> () { new Node() { Value = 10 } } }, new Node() { Value = 7 } }});
			topNode.Neighbours.Add(new Node() { Value = 4, Neighbours = new List<Node> () { new Node() { Value = 8 } } });
 
 
			Queue<Node> queue = new Queue<Node>();
			queue.Enqueue(topNode);
 
			while (queue.Count > 0)
			{
			   var element = queue.Dequeue();
				if (element.Neighbours.Count > 0)
				{
					Console.WriteLine(element.Value);
					foreach (var item in element.Neighbours)
					{
						queue.Enqueue(item);
					}
				}
				else
					Console.WriteLine(element.Value);
			}




			//type 2
			var g = new Graph<int>();
			var n1 = new Node<int>(1);
			var n2 = new Node<int>(2);
			var n3 = new Node<int>(3);
			var n4 = new Node<int>(4);
			var n5 = new Node<int>(5);
			var n6 = new Node<int>(6);
			var n7 = new Node<int>(7);
			g.AddEdge(n1, n2);
			g.AddEdge(n1, n3);
			g.AddEdge(n1, n4);
			g.AddEdge(n4, n5);
			g.AddEdge(n2, n6);
			g.AddEdge(n4, n7);
			g.AddEdge(n5, n6);
			g.AddEdge(n6, n7);

			Console.WriteLine("Shortest path is: {0}", string.Join("->", g.ShortestPath(n3, n5)));

			Console.Read(); 
		}

		//another process
		public class Node<T>
		{
			public T Value;

			public Node(T val)
			{
				Value = val;
			}

			public override string ToString()
			{
				return Value.ToString();
			}
		}

		public class Graph<T>
		{
			//private readonly List<Node<T>> _nodes;
			private readonly Dictionary<Node<T>, List<Node<T>>> _adj;

			public Graph()
			{
				//_nodes = new List<Node<T>>();
				_adj = new Dictionary<Node<T>, List<Node<T>>>();
			}

			public void AddEdge(Node<T> node1, Node<T> node2)
			{
				if (!_adj.ContainsKey(node1))
					_adj[node1] = new List<Node<T>>();
				if (!_adj.ContainsKey(node2))
					_adj[node2] = new List<Node<T>>();
				_adj[node1].Add(node2);
				_adj[node2].Add(node1);
			}

			public Stack<Node<T>> ShortestPath(Node<T> source, Node<T> dest)
			{
				var path = new Dictionary<Node<T>, Node<T>>();
				var distance = new Dictionary<Node<T>, int>();
				foreach (var node in _adj.Keys)
				{
					distance[node] = -1;
				}
				distance[source] = 0;
				var q = new Queue<Node<T>>();
				q.Enqueue(source);
				while (q.Count > 0)
				{
					var node = q.Dequeue();
					foreach (var adj in _adj[node].Where(n => distance[n] == -1))
					{
						distance[adj] = distance[node] + 1;
						path[adj] = node;
						q.Enqueue(adj);
					}
				}
				var res = new Stack<Node<T>>();
				var cur = dest;
				while (cur != source)
				{
					res.Push(cur);
					cur = path[cur];
				}
				res.Push(source);
				return res;
			}
		}
	}
}
