using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReceiptPrinter.Droid.Printer))]
namespace ReceiptPrinter.Droid
{
    public class Printer : IPrinter
    {
        public void Print(string ipAddress, int port, IList<string> linesToPrint)
        {
            // Creat a stock object // Creare un oggetto stock // Créer un objet de stock
            Socket pSocket = new Socket(System.Net.Sockets.SocketType.Stream, ProtocolType.IP);

            // Set a timeout for attemps to connect, here set to 1500 miliseconds
            // Imposta un timeout per i tentativi di connessione, qui impostato su 1500 miliseconds
            // Définissez un délai d’expiration pour que attemps se connecte, ici réglé sur 1500 milisecondes
            pSocket.SendTimeout = 1500;

            // Connect to the specified ip address and port 
            // Connettersi all'indirizzo IP e alla porta specificati
            // Se connecter à l’adresse IP et au port spécifiés
            pSocket.Connect(ipAddress, port);


            List<byte> outputList = new List<byte>();

            foreach (string txt in linesToPrint)
            {
                // convert the string to list of bytes
                // Convertire la stringa in un elenco di byte
                // convertir la chaîne en liste d’octets
                outputList.AddRange(System.Text.Encoding.UTF8.GetBytes(txt));

                // Add CES/POS Print and line feed command 
                // Aggiungi CES/POS Stampa e comando di avanzamento riga
                // Ajouter une commande d’impression et de saut de ligne CES/POS
                outputList.Add(0x0A); ;
            }

            // Send the command to the printer 
            // Inviare il comando alla stampante
            // Envoyer la commande à l’imprimante
            pSocket.Send(outputList.ToArray());

            // CLose the connection once done 
            // Chiude la connessione una volta terminata
            // Ferme la connexion une fois terminé
            pSocket.Close();
        }
    }
}