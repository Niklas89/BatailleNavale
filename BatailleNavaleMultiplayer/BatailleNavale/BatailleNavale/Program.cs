using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

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
			Console.Write(line + 1 + "  | ");
			for (int column = 0; column < 10; column++)
			{

				// s'il y a des bateaux du Server dans une case on ne l'affiche pas
				if (board[line, column] == 'o')
				{
					Console.Write("  | ");
				} 
				//sinon on affiche le contenu de la case
				else
				{
					Console.Write(board[line, column] + "  | ");
				}
			}
			Console.WriteLine();
			Console.WriteLine("-------------------------------------------");
		}

	}
	// Placer les navires sur le damier
	public static void PlaceShips(char[,] board, char[] letters, string[] firstName, string[] playerNow, NetworkStream stream)
	{
		string ship;
		string chooseDirectionString;
		int chooseDirection = 0;
		char shipIcon = '\0';
		bool keepAsking = true;
		// vérifier si le joueur actuel est égal au premier prénom du tableau
		if(playerNow[0] == firstName[0])
        {
			shipIcon = 'x';
        }
		// vérifier si le joueur actuel est égal au deuxième prénom du tableau
		else if(playerNow[0] == firstName[1])
        {
			shipIcon = 'o';
        }
		while (keepAsking)
		{
			// le joueur place ses navires
			Console.WriteLine("Bonjour " + playerNow[0] + " !");
			Console.WriteLine("Vous avez 4 cases pour placer vos navires.");
			Console.WriteLine("Entrez la case où vous souhaitez placer votre premier navire (comme 'B2' par exemple) :");
			ship = Console.ReadLine();
			// Prendre le premier caractère de la chaine de caractères
			int firstCharShip = 0;
			// Prendre le deuxième caractère de la chaine de caractères
			int secondCharShip = ship[1] - '0';
			// Le programme cherche la case qui correspond dans le tableau
			for (int column = 0; column < 10; column++)
			{
				// comparer le premier caractère de la chaîne aux lettres en haut du damier pour trouver la colonne correspondante
				if (letters[column] == ship[0])
				{
					firstCharShip = column;
				}
			}
			// choisir la direction vers laquelle les navires vont être placés
			Console.WriteLine("Choisissez parmis la liste si vous souhaitez placer les navires à l'horizontale ou à la verticale :");
			Console.WriteLine("1. Horizontalement vers la droite");
			Console.WriteLine("2. Horizontalement vers la gauche");
			Console.WriteLine("3. Verticalement vers le haut");
			Console.WriteLine("4. Verticalement vers le bas");
			try
            {
				chooseDirectionString = Console.ReadLine();
				chooseDirection = int.Parse(chooseDirectionString);
				if (board[secondCharShip - 1, firstCharShip] == 'x')
				{
					throw new DejaOccupe();
				}
				// Si la direction = 1 et la position donné par l'utilisateur + 3 (à l'horizontale vers la droite) est disponible :
				if (chooseDirection == 1 && board[secondCharShip - 1, firstCharShip + 3] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip - 1, firstCharShip + 1] = shipIcon;
					board[secondCharShip - 1, firstCharShip + 2] = shipIcon;
					board[secondCharShip - 1, firstCharShip + 3] = shipIcon;
					// envoyer les coordonnées à l'appli server (exemple: B21, B2 (case) + 1 (direction))
					string messageToSend = ship + chooseDirectionString;
					int byteCount = Encoding.UTF8.GetByteCount(messageToSend + 1);
					byte[] sendData = Encoding.UTF8.GetBytes(messageToSend);
					stream.Write(sendData, 0, sendData.Length);
					Console.WriteLine("sending data to server...");
					keepAsking = false;

				}
				// Si la direction = 2 et la position donné par l'utilisateur - 3 (à l'horizontale vers la gauche) est disponible :
				else if (chooseDirection == 2 && board[secondCharShip - 1, firstCharShip - 3] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip - 1, firstCharShip - 1] = shipIcon;
					board[secondCharShip - 1, firstCharShip - 2] = shipIcon;
					board[secondCharShip - 1, firstCharShip - 3] = shipIcon;
					// envoyer les coordonnées à l'appli server (exemple: B21, B2 (case) + 1 (direction))
					string messageToSend = ship + chooseDirectionString;
					int byteCount = Encoding.UTF8.GetByteCount(messageToSend + 1);
					byte[] sendData = Encoding.UTF8.GetBytes(messageToSend);
					stream.Write(sendData, 0, sendData.Length);
					Console.WriteLine("sending data to server...");
					keepAsking = false;
				}
				// Si la direction = 3 et la position donné par l'utilisateur - 3 (à la verticale vers le haut) est disponible :
				else if (chooseDirection == 3 && board[secondCharShip - 4, firstCharShip] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip - 2, firstCharShip] = shipIcon;
					board[secondCharShip - 3, firstCharShip] = shipIcon;
					board[secondCharShip - 4, firstCharShip] = shipIcon;
					// envoyer les coordonnées à l'appli server (exemple: B21, B2 (case) + 1 (direction))
					string messageToSend = ship + chooseDirectionString;
					int byteCount = Encoding.UTF8.GetByteCount(messageToSend + 1);
					byte[] sendData = Encoding.UTF8.GetBytes(messageToSend);
					stream.Write(sendData, 0, sendData.Length);
					Console.WriteLine("sending data to server...");
					keepAsking = false;
				}
				// Si la direction = 4 et la position donné par l'utilisateur + 3 (à la verticale vers le bas) est disponible :
				else if (chooseDirection == 4 && board[secondCharShip + 2, firstCharShip] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip, firstCharShip] = shipIcon;
					board[secondCharShip + 1, firstCharShip] = shipIcon;
					board[secondCharShip + 2, firstCharShip] = shipIcon;
					// envoyer les coordonnées à l'appli server (exemple: B21, B2 (case) + 1 (direction))
					string messageToSend = ship + chooseDirectionString;
					int byteCount = Encoding.UTF8.GetByteCount(messageToSend + 1);
					byte[] sendData = Encoding.UTF8.GetBytes(messageToSend);
					stream.Write(sendData, 0, sendData.Length);
					Console.WriteLine("sending data to server...");
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
			catch (DejaOccupe e)
            {
				Console.WriteLine(e.Message);
            }
		}
	}
	// Réceptionner les navires du serveur et les placer dans le tableaux sans l'afficher
	public static void PlaceShipsServer(char[,] board, char[] letters, string[] firstName, string[] playerNow, string shipCoordinatesServer)
	{
		char shipIcon = 'o';
		bool keepAsking = true;
		while (keepAsking)
		{
			// Prendre le premier caractère de la chaine de caractères
			int firstCharShip = 0;
			// Prendre le deuxième caractère de la chaine de caractères
			int secondCharShip = shipCoordinatesServer[1] - '0';
			// Prendre le troisième caractère de la chaine de caractères
			int thirdCharShip = shipCoordinatesServer[2] - '0';
			// Le programme cherche la case qui correspond dans le tableau
			for (int column = 0; column < 10; column++)
			{
				// chercher la colonne qui correspond premier caractère de la chaine, la lettre en haut du tableau
				if (letters[column] == shipCoordinatesServer[0])
				{
					firstCharShip = column;
				}
			}
			try
			{
				// si la case est déjà occupée on aura le message d'erreur
				if (board[secondCharShip - 1, firstCharShip] == 'x')
				{
					throw new DejaOccupe();
				}
				// Si la direction = 1 et la position donné par l'utilisateur + 3 (à l'horizontale vers la droite) est disponible :
				if (thirdCharShip == 1 && board[secondCharShip - 1, firstCharShip + 3] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip - 1, firstCharShip + 1] = shipIcon;
					board[secondCharShip - 1, firstCharShip + 2] = shipIcon;
					board[secondCharShip - 1, firstCharShip + 3] = shipIcon;
					keepAsking = false;

				}
				// Si la direction = 2 et la position donné par l'utilisateur - 3 (à l'horizontale vers la gauche) est disponible :
				else if (thirdCharShip == 2 && board[secondCharShip - 1, firstCharShip - 3] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip - 1, firstCharShip - 1] = shipIcon;
					board[secondCharShip - 1, firstCharShip - 2] = shipIcon;
					board[secondCharShip - 1, firstCharShip - 3] = shipIcon;
					keepAsking = false;
				}
				// Si la direction = 3 et la position donné par l'utilisateur - 3 (à la verticale vers le haut) est disponible :
				else if (thirdCharShip == 3 && board[secondCharShip - 4, firstCharShip] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip - 2, firstCharShip] = shipIcon;
					board[secondCharShip - 3, firstCharShip] = shipIcon;
					board[secondCharShip - 4, firstCharShip] = shipIcon;
					keepAsking = false;
				}
				// Si la direction = 4 et la position donné par l'utilisateur + 3 (à la verticale vers le bas) est disponible :
				else if (thirdCharShip == 4 && board[secondCharShip + 2, firstCharShip] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip, firstCharShip] = shipIcon;
					board[secondCharShip + 1, firstCharShip] = shipIcon;
					board[secondCharShip + 2, firstCharShip] = shipIcon;
					keepAsking = false;
				}
				else
				{
					Console.WriteLine("\n Vous ne pouvez pas choisir cette case, il n'y a pas de place dans la direction que vous avez choisie.");
					Console.WriteLine("Veuillez choisir une autre case svp. \n");
				}
			}
			catch (IndexOutOfRangeException ex)
			{
				Console.WriteLine("\n Vous ne pouvez pas choisir cette case, il n'y a pas de place dans la direction que vous avez choisie.");
				Console.WriteLine("Veuillez choisir une autre case svp. \n");
			}
			catch (DejaOccupe e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
	// Affichage du score des deux joueurs
	public static void ShowScore(string[] firstName, int[] nbShips, int[] score)
    {
		for (int firstNameIndex = 0; firstNameIndex < firstName.Length; firstNameIndex++)
		{
			Console.WriteLine(firstName[firstNameIndex] + " - SCORE : " + score[firstNameIndex] + "/4 - NOMBRE DE NAVIRES RESTANTS : " + nbShips[firstNameIndex]);
		}
	}
	// Exploser et couler les navires ennemies, ceux de l'appli Server
	public static void SinkEnemyShip(string[] firstName, string[] playerNow, int[] nbShips)
    {
		foreach (string player in firstName)
		{
			// si le prénom du joueur trouvé dans le tableau firstName[] est différent du joueur actuel
			if (player != playerNow[0])
			{
				// On cherche l'indice de ce joueur ennemi
				for (int firstNameIndex = 0; firstNameIndex < firstName.Length; firstNameIndex++)
				{
					// si le prénom du joueur ennemi == au prénom dans le tableau firstName[], on récupère son indice
					if (player == firstName[firstNameIndex])
					{
						// L'indice du joueur ennemi dans le tableau firstName[] correspond à l'indice du tableau nbShips[]
						// diminuer de 1 le nombre de navires ennemie
						nbShips[firstNameIndex]--;
					}
				}
				break;
			}
		}
	}
	// Tirer sur les navires
	public static void ShootShips(char[,] board, char[] letters, string[] firstName, string[] playerNow, char[] playerShipIcon, int[] score, int[] nbShips, NetworkStream stream, StreamReader sr)
    {
		string shoot;
		bool continueBattle = true;
		int playerNowId = 2;
		string serverPlayerShot = "";
		// Tant que le combat continue... on continue
		while (continueBattle)
        {
			// Le programme cherche le prénom de la personne qui joue et prend son indice dans le tableau firstName[]
			for (int firstNameIndex = 0; firstNameIndex < firstName.Length; firstNameIndex++)
			{
				// si le prénom du joueur actuel == au prénom dans le tableau, on récupère son indice
				if (playerNow[0] == firstName[firstNameIndex])
				{
					// enregistrer l'indice du joueur actuel et l'enregistrer dans la variable playerNowId
					playerNowId = firstNameIndex;
				}
			}
			Console.WriteLine(nbShips[playerNowId] + " EST LE NOMBRE DE NAVIRES QUE DISPOSE " + playerNow[0]);
			// Vérifier si le nombre de navires du joueur actuel == 0
			if (nbShips[playerNowId] == 0)
			{
				// Afficher si le joueur actuel a perdu avec son score
				Console.WriteLine(playerNow[0] + ", vous avez perdu !");
				Console.WriteLine("Votre score est : " + score[playerNowId]);
				continueBattle = false;

			} else
            {
				// Si le joueur actuel est le deuxième joueur, l'appli serveur
				if(playerNow[0] == firstName[1])
                {
					serverPlayerShot = sr.ReadLine();
					Console.WriteLine(serverPlayerShot);
					shoot = serverPlayerShot;

				} 
				// sinon si le joueur actuel est le premier joueur, l'appli client
				else
                {
					// Au tour du joueur enregistré dans le tableau playerNow de jouer
					Console.WriteLine("\n" + playerNow[0] + ", à vous le tour de jouer !");
					Console.WriteLine("Entrez la case où vous souhaitez tirer (comme 'B2' par exemple) : ");
					shoot = Console.ReadLine();
				}
				
				// Prendre le deuxième caractère de la chaine de caractères, le chiffre à gauche du damier
				int secondCharShoot = shoot[1] - '0';
				for (int column = 0; column < 10; column++)
				{
					// si la lettre en haut du damier == le premier caractère de shoot
					if (letters[column] == shoot[0])
					{
						if (board[secondCharShoot - 1, column] == 'x' && playerShipIcon[playerNowId] == 'x')
						{
							Console.Write("Vous avez visé votre propre navire, ca ne marche pas. Raté !");
							int byteCount = Encoding.UTF8.GetByteCount(shoot + 1);
							byte[] sendData = Encoding.UTF8.GetBytes(shoot);
							stream.Write(sendData, 0, sendData.Length);
							Console.WriteLine("sending data to server...");
						}
						// Si la case séléctionnée correspond au navire de mon adversaire, je le tire dessus et je gagne un point
						else if (board[secondCharShoot - 1, column] == 'o' && playerShipIcon[playerNowId] == 'x')
						{
							board[secondCharShoot - 1, column] = '\0';
							// Je gagne un point
							score[playerNowId]++;
							SinkEnemyShip(firstName, playerNow, nbShips);
							Console.WriteLine("Vous avez tiré sur un navire ennemi, bravo !");
							int byteCount = Encoding.UTF8.GetByteCount(shoot + 1);
							byte[] sendData = Encoding.UTF8.GetBytes(shoot);
							stream.Write(sendData, 0, sendData.Length);
							Console.WriteLine("sending data to server...");
						}
						// Si la case séléctionnée ne correspond à aucune navire, je ne gagne ou perds aucun point car j'ai raté
						else if (board[secondCharShoot - 1, column] == '\0' && playerShipIcon[playerNowId] == 'x')
						{
							Console.WriteLine("Vous avez tiré dans le vide, raté !");
							int byteCount = Encoding.UTF8.GetByteCount(shoot + 1);
							byte[] sendData = Encoding.UTF8.GetBytes(shoot);
							stream.Write(sendData, 0, sendData.Length);
							Console.WriteLine("sending data to server...");
						}
						// Si la case séléctionnée correspond à mon propre navire, je perds un point car je me tire dessus
						if (board[secondCharShoot - 1, column] == 'o' && playerShipIcon[playerNowId] == 'o')
						{
							Console.Write("Le Server joueur a tiré sur lui-même, ca ne marche pas. Raté !");
						}
						// Si la case séléctionnée correspond au navire de mon adversaire, je le tire dessus et je gagne un point
						else if (board[secondCharShoot - 1, column] == 'x' && playerShipIcon[playerNowId] == 'o')
						{
							board[secondCharShoot - 1, column] = '\0';
							// Je gagne un point
							score[playerNowId]++;
							SinkEnemyShip(firstName, playerNow, nbShips);
							Console.WriteLine("Le Server joueur vous tiré dessus !");
						}
						// Si la case séléctionnée ne correspond à aucune navire, je ne gagne ou perds aucun point car j'ai raté
						else if (board[secondCharShoot - 1, column] == '\0' && playerShipIcon[playerNowId] == 'o')
						{
							Console.WriteLine("Le Server joueur a raté !");
						}

					}
				}
			}
			// Affiche le score du joueur actuel
			Console.WriteLine("Votre score : " + score[playerNowId]);
			// Affiche le damier et les navires du Client, premier joueur
			ShowBoard(board, letters);
			// Vérifier si le nombre de navires du joueur actuel == 0
			Console.WriteLine(nbShips[playerNowId] + " EST LE NOMBRE DE NAVIRES QUE DISPOSE " + playerNow[0]);
			if (nbShips[playerNowId] == 0)
            {
				Console.WriteLine(playerNow[0] + ", vous avez perdu !");
				Console.WriteLine("Votre score est : " + score[playerNowId]);
				// On arrete le combat
				continueBattle = false;

			}
			// boucle qui prend le joueur qui n'a pas encore joué pour qu'il puisse jouer au prochain tour
			foreach(string player in firstName)
            {
				if(player != playerNow[0])
                {
					playerNow[0] = player;
					break;
				}
            }
		}
	}
	public static void Main()
	{
		// Le score de firstName[] à l'indice 1 est lié au score[] à l'indice 1
		// Le premier joueur dans firstName[0] et deuxième joueur dans firstName[1]
		string[] firstName = new string[2];
		// Création des icones correspondants au navires sur le damier
		// 'x' étant l'icone du premier joueur et 'o' l'icone du deuxième joueur
		char[] playerShipIcon = new char[] { 'x', 'o' };
		// PlayerNow détermine à qui est le tour de jouer
		string[] playerNow = new string[1];
		// Le score du joueur 1 dans score[0] et score de joueur 2 dans score[1]
		int[] score = new int[2];
		// Le nombre de bateaux de joueur 1 se trouve dans nbShips[0] et ceux de joueur 2 dans nbShips[1]
		int[] nbShips = new int[2] { 4, 4 };
		// La taille du damier est de 10 sur 10 cases
		char[,] board = new char[10, 10];
		// On place les lettres de letters[] en haut du tableau et correspondent aux colonnes du tableau
		char[] letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
		// Afficher le damier
		ShowBoard(board, letters);
		connection:
			try
			{
				// On créé un objet de type TcpClient défini par son host et son port
				// TcpClient client = new TcpClient("192.168.1.194", 1302);
				//TcpClient client = new TcpClient("192.168.1.45", 1302);
				TcpClient client = new TcpClient("localhost", 11000);

				// Demander le prénom du premier joueur
				Console.WriteLine("Bonjour premier joueur, quel est votre prénom ?");
				firstName[0] = Console.ReadLine();
				playerNow[0] = firstName[0];
				string messageToSend = playerNow[0]; // Message à envoyer
				int byteCount = Encoding.UTF8.GetByteCount(messageToSend + 1);
				byte[] sendData = Encoding.UTF8.GetBytes(messageToSend);

				//Envoi de message
				NetworkStream stream = client.GetStream();
				stream.Write(sendData, 0, sendData.Length);
				Console.WriteLine("sending data to server...");

				// Lecture de messages 
				StreamReader sr = new StreamReader(stream);

				// Récupérer le prénom de l'appli serveur, le deuxième joueur
				firstName[1] = sr.ReadLine();
				Console.WriteLine(firstName[1]);

				// Récupérer les coordonnées de l'appli serveur, le deuxième joueur
				string shipCoordinatesServer = sr.ReadLine();
				Console.WriteLine(shipCoordinatesServer);

				// Réceptionner les navires de l'appli serveur et les placer dans le tableaux sans l'afficher
				PlaceShipsServer(board, letters, firstName, playerNow, shipCoordinatesServer);
				// Afficher le damier
				ShowBoard(board, letters);
				// Placer les navires du deuxième joueur
				PlaceShips(board, letters, firstName, playerNow, stream);
				// Afficher le damier
				ShowBoard(board, letters);
				// Le joueur actuel devient le joueur de l'appli serveur, le deuxième joueur
				playerNow[0] = firstName[1];
				// Les deux joueurs vont se tirer dessus dans la méthode ShootShips
				ShootShips(board, letters, firstName, playerNow, playerShipIcon, score, nbShips, stream, sr);
				// Montrer le score des deux joueurs
				ShowScore(firstName, nbShips, score);
				// Fermer la connection 
				stream.Close();
				client.Close();
				//Console.ReadKey();
			}
			catch (Exception)
			{
				Console.WriteLine("failed to connect...");
				goto connection;
			}
	}
}
// Affichage de message d'erreur si la case est déjà occupée
public class DejaOccupe : Exception
{
	public override string Message
	{
		get
		{
			return "Désolé, la case est déjà occupée ! Veuillez choisir une autre case svp.";
		}
	}
}