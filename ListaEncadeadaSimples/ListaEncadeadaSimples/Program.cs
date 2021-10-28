using System;
using System.Collections.Generic;
using System.Linq;

namespace ListaEncadeadaSimples
{
    class Program
    {
        public static List<Carta> CartasDeLetras = new() { new Carta("A", 1), new Carta("J", 11), new Carta("Q", 12), new Carta("K", 13) };

        static void Main()
        {
            Console.WriteLine("Implementação de lista usando vetores");
            Console.WriteLine();

            var actualNode = new Node();
            var jogo = new Jogo
            {
                Cartas = actualNode
            };

            for (var i = 0; i < 7; i++)
            {
                Console.Write("Digite a carta que você recebeu: ");
                var carta = PegaCarta(Console.ReadLine());
                while (carta == null)
                {
                    Console.WriteLine("Carta inválida digitada!");
                    Console.Write("Digite a carta que você recebeu: ");
                    carta = PegaCarta(Console.ReadLine());
                }

                var node = new Node
                {
                    Data = carta
                };

                actualNode.Next = node;
                actualNode = node;

                jogo.InsercaoOrdenada();
            }

            Console.WriteLine("--------------------------------------------");
            jogo.VerListaCartas();

            Console.WriteLine("--------------------------------------------");
        }

        public static Carta PegaCarta(string carta)
        {
            if (!int.TryParse(carta, out var numero))
                return CartasDeLetras.FirstOrDefault(x => x.Nome == carta.ToUpper());

            return numero > 0 && numero < 11 ? new Carta(carta, numero) : null;
        }
    }

    public class Jogo
    {
        public Node Cartas { get; set; } = new Node();

        public void InsercaoOrdenada()
        {
            Carta temp;
            bool needRestart;
            do
            {
                needRestart = false;
                var actualNode = Cartas.Next;
                while (!needRestart && actualNode.Next != null)
                {
                    if (actualNode.Data == null || actualNode.Next.Data == null)
                        break;

                    if (actualNode.Next.Data.Valor >= actualNode.Data.Valor)
                        actualNode = actualNode.Next;
                    else
                    {
                        temp = actualNode.Data;
                        actualNode.Data = actualNode.Next.Data;
                        actualNode.Next.Data = temp;

                        needRestart = true;
                    }
                }
            }
            while (needRestart);
        }

        public void VerListaCartas()
        {
            Console.WriteLine("Suas Cartas são: ");
            var carta = Cartas.Next;
            while (carta != null)
            {
                Console.WriteLine(carta.Data?.Nome);
                carta = carta.Next;
            }
        }

    }

    public class Node
    {
        public Carta Data;
        public Node Next;
    };

    public class Carta
    {
        public Carta(string nome, int valor)
        {
            Valor = valor;
            Nome = nome;
        }

        public int Valor { get; set; }
        public string Nome { get; set; }
    }
}
