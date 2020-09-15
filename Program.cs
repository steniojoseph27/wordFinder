using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		var wordstream = new string[] { "chill", "wind", "cold" };
		var matrix = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
		var result = new WordFinder(wordstream).Find(matrix);
		Console.WriteLine(string.Join(",", result));
	}

	public class WordFinder
	{
		private readonly HashSet<string> dictionary;

		public WordFinder(IEnumerable<string> matrix)
		{
			this.dictionary = new HashSet<string>(matrix);
		}

		public IList<string> Find(IEnumerable<string> wordstream)
		{
			var leftRightSearchString = string.Join(string.Empty, wordstream);

			var characterMatrix = wordstream
				.Select(row => row.ToCharArray())
				.ToArray();
			var topDownSearchStringBuilder = new StringBuilder();
			for (var i = 0; i < characterMatrix.Length; i++)
			{
				for (var j = 0; j < characterMatrix[i].Length; j++)
				{
					topDownSearchStringBuilder.Append(characterMatrix[j][i]);
				}
			}
			var topDownSearchString = topDownSearchStringBuilder.ToString();

			var resultSet = new HashSet<string>();
			resultSet.UnionWith(dictionary.Where(searchTerm =>
												 leftRightSearchString.Contains(searchTerm)));
			resultSet.UnionWith(dictionary.Where(searchTerm =>
												 topDownSearchString.Contains(searchTerm)));

			return resultSet.ToList();
		}
	}
}
