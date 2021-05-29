using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Finder
{
	class TextSearch : ISearch
	{
		private string _text;
		private string _fileName;
		private string _directory;

		public TextSearch(string text, string fileName, string directory)
		{
			_text = text;
			_fileName = fileName;
			_directory = directory;
		}

		public async Task SearchAsync(string text)
		{
			Console.WriteLine();
			_text = text;
			bool searchInAllFiles = false;

			if (string.IsNullOrWhiteSpace(_fileName))
			{
				Console.WriteLine("Filename is empty. Will be searched for {2} in all files.", _fileName, _directory, _text);
			}

			var files = Directory.EnumerateFiles(_directory);
			searchInAllFiles = files.Any(x => x.Contains(_fileName));

			foreach (var file in files)
			{
				Console.WriteLine();
				if (!searchInAllFiles)
				{
					var fileName = this.GetFileName(file);
					await this.SearchInFile(file).ConfigureAwait(false);
					break;
				}

				await this.SearchInFile(file).ConfigureAwait(false);
			}
		}

		private async Task SearchInFile(string file)
		{
			using var reader = new StreamReader(file);
			bool found = false;
			do
			{
				var line = await reader.ReadLineAsync().ConfigureAwait(false);
				if (line == null)
				{
					found = false;
					break;
				}

				if (line.Contains(_text))
				{
					found = true;
					Console.WriteLine("Found in file: {0}", file);
					this.WriteToConsole(line);
					break;
				}

			} while (!found);

			if (!found)
			{
				Console.WriteLine("{0} not found in {1}", _text, file);
			}
		}

		private void WriteToConsole(string lineText)
		{
			int firsIndex = lineText.IndexOf(_text);
			char[] charArray = lineText.ToCharArray();
			for (int i = 0; i < lineText.Length; i++)
			{
				if (i == firsIndex)
				{
					Console.BackgroundColor = ConsoleColor.White;
					Console.ForegroundColor = ConsoleColor.Black;
				}

				if (i == firsIndex + _text.Length)
				{
					Console.ResetColor();
				}

				Console.Write(charArray[i]);
			}
		}

		private string GetFileName(string file)
		{
			int lastIndex = file.LastIndexOf('\\');
			if (lastIndex == 0)
			{
				return file;
			}

			var fileName = file.Substring(lastIndex + 1);
			return fileName;
		}
	}
}
