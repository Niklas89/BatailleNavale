using System;

public class Program
{
	public static void ShowBoard(char[,] board, char[] letters)
	{
		Console.Write("  | ");
		foreach (char c in letters)
        {
			Console.Write(c + " | ");
        }
		Console.WriteLine();
		Console.WriteLine("-------------------------------------------");
		for (int line = 0; line < 10; line++)
		{
			Console.Write(line + 1 + " | ");
			for (int column = 0; column < 10; column++)
			{
				Console.Write(board[line, column] + " | ");
			}
			Console.WriteLine();
			Console.WriteLine("-------------------------------------------");
		}

	}
	public static void PlaceShips(char[,] board, char[] letters)
	{
		int randomColumn;
		int randomLine;
		bool occupied;
		string firstBoat;
		string lastBoat;
		Console.WriteLine("Entrez la case où vous souhaitez placer votre premier bateau (comme 'B2' par exemple) :");
		firstBoat = Console.ReadLine();
		// Console.WriteLine(firstBoat[0]);
		int firstCharFirstBoat;
		int secondCharFirstBoat = firstBoat[1] - '0';
		for (int column = 0; column < 10; column++)
		{
			if (letters[column] == firstBoat[0])
			{
					board[secondCharFirstBoat - 1, column] = firstBoat[1];
					firstCharFirstBoat = column;
			}
		}
		


		Console.WriteLine("Le programme mettra les autres bateaux entre votre premier bateau et votre dernier bateau");
		Console.WriteLine("Entrez la case où vous souhaitez placer votre dernier bateau (comme 'D2' par exemple) :");
		lastBoat = Console.ReadLine();
		Console.WriteLine(lastBoat[0]);
		int secondCharLastBoat = lastBoat[1] - '0';
		int firstCharLastBoat;
		for (int column = 0; column < 10; column++)
		{
			if (letters[column] == lastBoat[0])
			{
				board[secondCharLastBoat - 1, column] = lastBoat[1];
				firstCharLastBoat = column;
			}
		}
	}
	public static int RandomTenNumber()
    {
		Random randomNumber = new Random();
		int returnRandomNumber = randomNumber.Next(1, 11);
		return returnRandomNumber;
	}
	public static void Main()
	{
		char[,] board = new char[10, 10];
		char[] letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
		//Console.WriteLine(RandomTenByTenNumber());
		PlaceShips(board, letters);
		ShowBoard(board, letters);

	}
}