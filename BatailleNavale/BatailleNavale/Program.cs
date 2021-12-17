using System;

public class Program
{
	// Afficher le damier
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
	// Placer les navires sur le navire
	public static void PlaceShips(char[,] board, char[] letters)
	{
		string ship;
		string chooseDirectionString;
		int chooseDirection = 0;
		bool keepAsking = true;
		while (keepAsking)
		{
			Console.WriteLine("Entrez la case où vous souhaitez placer votre premier bateau (comme 'B2' par exemple) :");
			ship = Console.ReadLine();
			// Prendre le premier caractère de la chaine de caractères
			int firstCharShip = 0;
			// Prendre le deuxième caractère de la chaine de caractères
			int secondCharShip = ship[1] - '0';
			// Le programme cherche la case qui correspond dans le tableau
			for (int column = 0; column < 10; column++)
			{
				if (letters[column] == ship[0])
				{
					board[secondCharShip - 1, column] = ship[1];
					firstCharShip = column;
				}
			}
			Console.WriteLine("Choisissez parmis la liste si vous souhaitez placer les bateaux à l'horizontale ou à la verticale :");
			Console.WriteLine("1. Horizontalement vers la droite");
			Console.WriteLine("2. Horizontalement vers la gauche");
			Console.WriteLine("3. Verticalement vers le haut");
			Console.WriteLine("4. Verticalement vers le bas");
			try
            {
				chooseDirectionString = Console.ReadLine();
				chooseDirection = int.Parse(chooseDirectionString);
				// Si la direction = 1 et la position donné par l'utilisateur + 3 (à l'horizontale vers la droite) est disponible :
				if (chooseDirection == 1 && board[secondCharShip - 1, firstCharShip + 3] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = 'x';
					board[secondCharShip - 1, firstCharShip + 1] = 'x';
					board[secondCharShip - 1, firstCharShip + 2] = 'x';
					board[secondCharShip - 1, firstCharShip + 3] = 'x';
					keepAsking = false;

				}
				// Si la direction = 2 et la position donné par l'utilisateur - 3 (à l'horizontale vers la gauche) est disponible :
				else if (chooseDirection == 2 && board[secondCharShip - 1, firstCharShip - 3] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = 'x';
					board[secondCharShip - 1, firstCharShip - 1] = 'x';
					board[secondCharShip - 1, firstCharShip - 2] = 'x';
					board[secondCharShip - 1, firstCharShip - 3] = 'x';
					keepAsking = false;
				}
				// Si la direction = 3 et la position donné par l'utilisateur - 3 (à la verticale vers le haut) est disponible :
				else if (chooseDirection == 3 && board[secondCharShip - 4, firstCharShip] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = 'x';
					board[secondCharShip - 2, firstCharShip] = 'x';
					board[secondCharShip - 3, firstCharShip] = 'x';
					board[secondCharShip - 4, firstCharShip] = 'x';
					keepAsking = false;
				}
				// Si la direction = 4 et la position donné par l'utilisateur + 3 (à la verticale vers le bas) est disponible :
				else if (chooseDirection == 4 && board[secondCharShip + 2, firstCharShip] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = 'x';
					board[secondCharShip, firstCharShip] = 'x';
					board[secondCharShip + 1, firstCharShip] = 'x';
					board[secondCharShip + 2, firstCharShip] = 'x';
					keepAsking = false;
				} else
                {
					Console.WriteLine("\n Vous ne pouvez pas choisir cette case, il n'y a pas de place dans la direction que vous avez choisie.");
					Console.WriteLine("Veuillez choisir une autre case svp. \n");
				}
			} catch (IndexOutOfRangeException ex)
            {
				Console.WriteLine("\n Vous ne pouvez pas choisir cette case, il n'y a pas de place dans la direction que vous avez choisie.");
				Console.WriteLine("Veuillez choisir une autre case svp. \n");
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
		// Le score de firstName[] à l'indice 1 est lié au score[] à l'indice 1
		string[] firstName = new string[2];
		int[] score = new int[2];
		// La taille du damier est de 10 sur 10 cases
		char[,] board = new char[10, 10];
		// On place les lettres de letters[] en haut du tableau et correspondent aux colonnes du tableau
		char[] letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
		// Afficher le damier
		ShowBoard(board, letters);
		// Demander le prénom du premier joueur
		Console.WriteLine("Bonjour premier joueur, quel est votre prénom ?");
		firstName[0] = Console.ReadLine();
		// Placer les navires du premier joueur
		PlaceShips(board, letters);
		// Afficher le damier
		ShowBoard(board, letters);
		// Demander le prénom du deuxième joueur
		Console.WriteLine("Bonjour deuxième joueur, quel est votre prénom ?");
		firstName[1] = Console.ReadLine();
		// Placer les navires du deuxième joueur
		PlaceShips(board, letters);
		// Afficher le damier
		ShowBoard(board, letters);

	}
}