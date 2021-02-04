using SistemaDePagamento.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaDePagamento
{
    class Program
    {
        private static List<Boleto> listaBoletos;
        private static List<Dinheiro> listaDinheiro;
        static void Main(string[] args)
        {
            listaBoletos = new List<Boleto>();
            listaDinheiro = new List<Dinheiro>();
            while (true)
            {
                Console.WriteLine("=======================================");
                Console.WriteLine("=========== LOJA DA FERNANDA ==========");
                Console.WriteLine("=======================================");
                Console.WriteLine("\n");
                Console.WriteLine("Selecione uma opção: ");
                Console.WriteLine("1 - COMPRA | 2 - PAGAMENTO | 3 - RELATÓRIO");

                var opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Comprar();
                        break;


                    case 2:
                        Pagamento();
                        break;

                    case 3:
                        Relatorio();
                        break;

                }
            }
        }

        public static void Comprar()
        {
            Console.WriteLine("Qual produto deseja comprar: ");
            Console.WriteLine("1 - TELEVISÃO | 2 - GELADEIRA");
            var opcaoProduto = int.Parse(Console.ReadLine());

            switch (opcaoProduto)
            {
                case 1:
                    CompraTelevisao();
                    break;
                case 2:
                    CompraGeladeira();
                    break;

            }

             static void CompraTelevisao()
            {
                var valorTV = 2000;
                var televisao = new Produto(valorTV);
                televisao.DescontoTelevisao();

                Console.WriteLine("==== PRODUTO: TELEVISÃO ====");

                Console.WriteLine($"Valor da Televisão: R${valorTV}");
                Console.WriteLine($"Valor com desconto de 15%: R${televisao.Valor}");
                Console.WriteLine("=======================================");


            }

            static void CompraGeladeira()
            {
                var valorGeladeira = 2000;
                var geladeira = new Produto(valorGeladeira);
                geladeira.DescontoGeladeira();

                Console.WriteLine("==== PRODUTO: GELADEIRA ====");

                Console.WriteLine($"Valor da Geladeira: R${valorGeladeira}");
                Console.WriteLine($"Valor com desconto de 15% + bônus de R$100: R${geladeira.Valor}");
                Console.WriteLine("=======================================");
                Console.WriteLine("\n");



            }

            Console.WriteLine("Qual será a forma de pagamento: ");
            Console.WriteLine("1 - DINHEIRO | 2 - BOLETO");
            Console.WriteLine("\n");

            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    PgtoDinheiro();
                    break;
                case 2:
                    PgtoBoleto();
                    break;

            }

        }


    public static void PgtoDinheiro()
        {
            Console.WriteLine("Digite o valor da compra:");
            var valor = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o valor recebido em dinheiro:");
            var valorRecebido = int.Parse(Console.ReadLine());

            var dinheiro = new Dinheiro(valor, valorRecebido);
            
            dinheiro.Pagar();
            var troco = valorRecebido - dinheiro.Valor;
            
            var valorDevido = valor - valorRecebido;


            if (valorRecebido < valor)
            {

                Console.WriteLine($"\n - Valor da compra: R${valor}.\n - Pagamento recebido: R${valorRecebido}.\n - Falta pagar: R${valorDevido}");
                Console.WriteLine("\n");
                Console.WriteLine("***** COMPRA CANCELADA - PAGAMENTO INSUFICIENTE *****");
                Console.WriteLine("***** SE DESEJAR, REGISTRE A COMPRA NOVAMENTE *****");
                Console.WriteLine("\n");

            }
            else
            {
                Console.WriteLine($"\n - Valor da compra: R${valor}. \n - Valor com Desconto de 5% - Pagamento em dinheiro : {dinheiro.Valor}\n" +
                    $" - Pagamento recebido: R${valorRecebido}.\n - Troco: R${troco}");
                Console.WriteLine("\n");

                
                dinheiro.pgtoDinheiroConfirmado();
                Console.WriteLine($"Compra finalizada com sucesso no valor de R${dinheiro.Valor}");
               

                Console.WriteLine("\n");

                listaDinheiro.Add(dinheiro);

            }

        }

        public static void PgtoBoleto()
        {
            Console.WriteLine("Digite o valor da compra:");
            var valor = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o CPF do cliente:");
            var cpf = (Console.ReadLine());

            Console.WriteLine("Preencha uma descrição para o boleto:");
            var descricao = (Console.ReadLine());
            Console.WriteLine("\n");

            var boleto = new Boleto(cpf, valor, descricao);

            boleto.GerarBoleto();

            Console.WriteLine($"*** Boleto gerado com sucesso! Dados abaixo: ***");
            Console.WriteLine("\n");
            Console.WriteLine($"Número do boleto: {boleto.CodigoBarra}");
            Console.WriteLine($"Data de vencimento: {boleto.DataVencimento}");
            Console.WriteLine($"Descrição do boleto: {boleto.Descricao}");
            Console.WriteLine("\n");

            
            listaBoletos.Add(boleto);
        }


        public static void Pagamento()
        {
            Console.WriteLine("Digite o número do código de barras: ");
            var numero = Guid.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            var boleto = listaBoletos
                                .Where(item => item.CodigoBarra == numero)
                                .FirstOrDefault();
            if (boleto is null)
            {
                Console.WriteLine($"Boleto de código {numero} não encontrado!");
                return;

            }


            if (boleto.EstaPago())
            {
                Console.WriteLine($"Boleto já foi pago no dia {boleto.DataPagamento}!");

            }
            if (boleto.EstaVencido())
            {
                boleto.CalcularJuros();
                Console.WriteLine($"Boleto está vencido, terá acrescimo de 10% === R$ {boleto.Valor}");
            }

            boleto.Pagar();
            Console.WriteLine($"Boleto de código {numero} foi pago com SUCESSO!");
            Console.WriteLine("\n");
        }



        public static void Relatorio()
        {
            Console.WriteLine("Qual opção de relatório: ");
            Console.WriteLine("1- BOLETOS PAGOS | 2 - BOLETOS À PAGAR | 3 - BOLETOS VENCIDOS | 4 - PAGAMENTOS EM DINHEIRO");

            var opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    BoletosPagos();
                    break;
                case 2:
                    BoletosAPagar();
                    break;
                case 3:
                    BoletosVencidos();
                    break;
                case 4:
                    PagamentoDinheiro();
                    break;
            }
        }
        public static void BoletosPagos()
        {
            Console.WriteLine("====== BOLETOS PAGOS ======");
            var boletos = listaBoletos
                            .Where(item => item.Confirmacao)
                            .ToList();


            foreach (var item in boletos)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor: R${item.Valor}\nData Pagamento: {item.DataPagamento}");
            }


            Console.WriteLine("\n");
            Console.WriteLine("====== FIM BOLETOS PAGOS ======");
            Console.WriteLine("\n");


        }

        public static void BoletosAPagar()
        {
            Console.WriteLine("====== BOLETOS À PAGAR ======");
            var boletos = listaBoletos
                            .Where(item => item.Confirmacao == false
                                && item.DataVencimento > DateTime.Now)
                            .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento{item.DataPagamento}");
            }
            Console.WriteLine("\n");
            Console.WriteLine("====== FIM BOLETOS À PAGAR ======");
            Console.WriteLine("\n");
        }

        public static void BoletosVencidos()
        {
            Console.WriteLine("====== BOLETOS VENCIDOS ======");
            var boletos = listaBoletos
                            .Where(item => item.Confirmacao == false
                                && item.DataVencimento < DateTime.Now)
                            .ToList();

            foreach (var item in boletos)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Codigo de Barra: {item.CodigoBarra}\nValor:{item.Valor}\nData Pagamento{item.DataPagamento}");
            }
            Console.WriteLine("\n");
            Console.WriteLine("====== FIM BOLETOS VENCIDOS ======");
            Console.WriteLine("\n");
        }

        public static void PagamentoDinheiro()
        {
            Console.WriteLine("====== PAGAMENTOS EM DINHIERO ======");
            var dinheiro = listaDinheiro
                                   .ToList();

            foreach(var item in dinheiro)
            {
     
                Console.WriteLine($"Valor:{item.Valor}\n");

            }
       


        }
    }
}
