using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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
				// s'il y a un navire du Client dans une case on ne l'affiche pas
				if(board[line, column] == 'x')
                {
					Console.Write("  | ");
				}
				// sinon on affiche le contenu de la case
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
	public static void PlaceShips(char[,] board, char[] letters, string[] firstName, string[] playerNow, StreamWriter? sw, StreamReader? sr)
	{
		string ship;
		string chooseDirectionString;
		int chooseDirection = 0;
		char shipIcon = '\0';
		bool keepAsking = true;
		// vérifier si le joueur actuel est égal au premier prénom du tableau
		if (playerNow[0] == firstName[0])
        {
			shipIcon = 'x';
        }
		// vérifier si le joueur actuel est égal au deuxième prénom du tableau
		else if (playerNow[0] == firstName[1])
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
			Console.WriteLine("Choisissez parmis la liste si vous souhaitez placer les bateaux à l'horizontale ou à la verticale :");
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
					// envoyer les coordonnées à l'appli Client (exemple: B21, B2 (case) + 1 (direction))
					sw.WriteLine(ship + chooseDirectionString);
					sw.Flush();
					keepAsking = false;

				}
				// Si la direction = 2 et la position donné par l'utilisateur - 3 (à l'horizontale vers la gauche) est disponible :
				else if (chooseDirection == 2 && board[secondCharShip - 1, firstCharShip - 3] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip - 1, firstCharShip - 1] = shipIcon;
					board[secondCharShip - 1, firstCharShip - 2] = shipIcon;
					board[secondCharShip - 1, firstCharShip - 3] = shipIcon;
					// envoyer les coordonnées à l'appli Client (exemple: B21, B2 (case) + 1 (direction))
					sw.WriteLine(ship + chooseDirectionString);
					sw.Flush();
					keepAsking = false;
				}
				// Si la direction = 3 et la position donné par l'utilisateur - 3 (à la verticale vers le haut) est disponible :
				else if (chooseDirection == 3 && board[secondCharShip - 4, firstCharShip] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip - 2, firstCharShip] = shipIcon;
					board[secondCharShip - 3, firstCharShip] = shipIcon;
					board[secondCharShip - 4, firstCharShip] = shipIcon;
					// envoyer les coordonnées à l'appli Client (exemple: B21, B2 (case) + 1 (direction))
					sw.WriteLine(ship + chooseDirectionString);
					sw.Flush();
					keepAsking = false;
				}
				// Si la direction = 4 et la position donné par l'utilisateur + 3 (à la verticale vers le bas) est disponible :
				else if (chooseDirection == 4 && board[secondCharShip + 2, firstCharShip] == '\0')
				{
					board[secondCharShip - 1, firstCharShip] = shipIcon;
					board[secondCharShip, firstCharShip] = shipIcon;
					board[secondCharShip + 1, firstCharShip] = shipIcon;
					board[secondCharShip + 2, firstCharShip] = shipIcon;
					// envoyer les coordonnées à l'appli Client (exemple: B21, B2 (case) + 1 (direction))
					sw.WriteLine(ship + chooseDirectionString);
					sw.Flush();
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
	// Placer les navires du premier joueur, l'appli Client, sur le damier
	public static void PlaceShipsClient(char[,] board, char[] letters, string[] firstName, string[] playerNow, string shipCoordinatesClient)
	{
		char shipIcon = 'x';
		bool keepAsking = true;
		while (keepAsking)
		{
			// Prendre le premier caractère de la chaine de caractères
			int firstCharShip = 0;
			// Prendre le deuxième caractère de la chaine de caractères
			int secondCharShip = shipCoordinatesClient[1] - '0';
			// Prendre le troisième caractère de la chaine de caractères
			int thirdCharShip = shipCoordinatesClient[2] - '0';
			// Le programme cherche la case qui correspond dans le tableau
			for (int column = 0; column < 10; column++)
			{
				if (letters[column] == shipCoordinatesClient[0])
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
	// Exploser et couler les navires ennemies, ceux de l'appli Client
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
	public static void ShootShips(char[,] board, char[] letters, string[] firstName, string[] playerNow, char[] playerShipIcon, int[] score, int[] nbShips, StreamWriter? sw, StreamReader? sr, byte[] buffer, NetworkStream stream)
	{
		string shoot;
		bool continueBattle = true;
		int playerNowId = 2;
		string clientPlayerShot = "";
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
				Console.WriteLine(playerNow[0] + ", vous avez perdu !");
				Console.WriteLine("Votre score est : " + score[playerNowId]);
				// On arrete le combat
				continueBattle = false;

			} else
            {
				if(playerNow[0] == firstName[0])
                {
					// Récupérer les coordonnées du premier joueur
					buffer = new byte[1024];
					stream.Read(buffer, 0, buffer.Length);
					int recv = 0;
					foreach (byte b in buffer)
					{
						if (b != 0)
						{
							recv++;
						}
					}
					clientPlayerShot = Encoding.UTF8.GetString(buffer, 0, recv);
					shoot = clientPlayerShot;
				} else
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
							Console.Write("Le Client a tiré sur son propre navire, ca ne marche pas. Raté !");
							// recevoir les coordonnées du client
						}
						// Si la case séléctionnée correspond au navire de mon adversaire, je le tire dessus et je gagne un point
						else if (board[secondCharShoot - 1, column] == 'o' && playerShipIcon[playerNowId] == 'x')
						{
							board[secondCharShoot - 1, column] = '\0';
							// Je gagne un point
							score[playerNowId]++;
							SinkEnemyShip(firstName, playerNow, nbShips);
							Console.WriteLine("Le client vous a tiré dessus, dommage pour vous !");
							// recevoir les coordonnées du client
						}
						// Si la case séléctionnée ne correspond à aucune navire, je ne gagne ou perds aucun point car j'ai raté
						else if (board[secondCharShoot - 1, column] == '\0' && playerShipIcon[playerNowId] == 'x')
						{
							Console.WriteLine("Le client a tiré dans le vide, raté !");
							// recevoir les coordonnées du client
						}
						// Si la case séléctionnée correspond à mon propre navire, je perds un point car je me tire dessus
						if (board[secondCharShoot - 1, column] == 'o' && playerShipIcon[playerNowId] == 'o')
						{
							Console.Write("Vous avez visé votre propre navire, ca ne marche pas. Raté !");
							sw.WriteLine(shoot);
							sw.Flush();
						}
						// Si la case séléctionnée correspond au navire de mon adversaire, je le tire dessus et je gagne un point
						else if (board[secondCharShoot - 1, column] == 'x' && playerShipIcon[playerNowId] == 'o')
						{
							board[secondCharShoot - 1, column] = '\0';
							// Je gagne un point
							score[playerNowId]++;
							SinkEnemyShip(firstName, playerNow, nbShips);
							Console.WriteLine("Vous avez tiré sur un navire ennemi, bravo !");
							sw.WriteLine(shoot);
							sw.Flush();
						}
						// Si la case séléctionnée ne correspond à aucune navire, je ne gagne ou perds aucun point car j'ai raté
						else if (board[secondCharShoot - 1, column] == '\0' && playerShipIcon[playerNowId] == 'o')
						{
							Console.WriteLine("Vous avez tiré dans le vide, raté !");
							sw.WriteLine(shoot);
							sw.Flush();
						}

					}
				}
			}
			Console.WriteLine("Votre score : " + score[playerNowId]);
			ShowBoard(board, letters);
			// Vérifier si le nombre de navires du joueur actuel == 0
			Console.WriteLine(nbShips[playerNowId] + " EST LE NOMBRE DE NAVIRES QUE DISPOSE " + playerNow[0]);
			if (nbShips[playerNowId] == 0)
            {
				Console.WriteLine(playerNow[0] + ", vous avez perdu !");
				Console.WriteLine("Votre score est : " + score[playerNowId]);
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
		TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 11000);
		listener.Start();
		while (true)
		{
			// Attendre le début de la connection avec le client. 
			// L'appli Serveur doit démarrer en premier, puis lancer l'appli Client quand le message ci-dessous est affiché
			Console.WriteLine("Waiting for a connection.");
			TcpClient client = listener.AcceptTcpClient();
			// La connection a bien été faite
			Console.WriteLine("Client accepted.");
			NetworkStream stream = client.GetStream();
			StreamReader sr = new StreamReader(client.GetStream());
			StreamWriter sw = new StreamWriter(client.GetStream());
			try
			{
				byte[] buffer = new byte[1024];
				stream.Read(buffer, 0, buffer.Length);
				int recv = 0;
				foreach (byte b in buffer)
				{
					if (b != 0)
					{
						recv++;
					}
				}
				// Demander le prénom du premier joueur et l'enregistrer dans request
				string request = Encoding.UTF8.GetString(buffer, 0, recv);
				// enregistrer le prénom du premier joueur dans la première case du tableau firstName[]
				firstName[0] = request;
				Console.WriteLine("request received " + firstName[0]);
				Console.WriteLine("Le premier joueur est : " + firstName[0]);
				// demander au joueur de l'appli Serveur, le deuxième joueur, de rentrer son prénom
				Console.WriteLine("Deuxième joueur, quel est votre prénom ?");
				firstName[1] = Console.ReadLine();
				// enregistrer le deuxième joueur, celui de l'appli Serveur, comme joueur actuel
				playerNow[0] = firstName[1];
				// Envoyer le prénom du deuxème joueur, appli Serveur, à l'appli Client
				sw.WriteLine(playerNow[0]);
				sw.Flush();
				// Placer les navires du premier joueur, Client
				PlaceShips(board, letters, firstName, playerNow, sw, sr);
				// Afficher le damier
				ShowBoard(board, letters);

				// Récupérer les coordonnées du premier joueur, l'appli Client
				buffer = new byte[1024];
				stream.Read(buffer, 0, buffer.Length);
				recv = 0;
				foreach (byte b in buffer)
				{
					if (b != 0)
					{
						recv++;
					}
				}
				string shipCoordinatesClient = Encoding.UTF8.GetString(buffer, 0, recv);
				// Le joueur actuel devient le deuxième joueur, celui de l'appli Serveur
				playerNow[0] = firstName[1];
				// Placer les navires du joueur de l'appli Client
				PlaceShipsClient(board, letters, firstName, playerNow, shipCoordinatesClient);
				// Afficher le damier
				ShowBoard(board, letters);
				// Méthode pour tirer sur les navires, les deux joueurs vont s'échanger les données ici
				ShootShips(board, letters, firstName, playerNow, playerShipIcon, score, nbShips, sw, sr, buffer, stream);
				// Montrer le score des deux joueurs
				ShowScore(firstName, nbShips, score);
			}
			catch (Exception e)
			{
				Console.WriteLine("Something went wrong.");
				sw.WriteLine(e.ToString());
			}
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