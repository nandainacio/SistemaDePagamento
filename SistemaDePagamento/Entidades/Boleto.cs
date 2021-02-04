using SistemaDePagamento.Entidades.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDePagamento.Entidades
{
    public class Boleto : Pagamento, IPagar
    {
        private const int DiasVencimento = 15;
        private const double Juros = 0.10;


        public Boleto(string cpf,
                      double valor,
                      string descricao)
        {

            Descricao = descricao;
            DataEmissao = DateTime.Now;
            Cpf = cpf;
            Valor = valor;



        }
        public Guid CodigoBarra { get; set; }

        public DateTime DataVencimento { get; set; }
        public DateTime DataEmissao { get; set; }

        public string Descricao { get; set; }


        public void GerarBoleto()
        {
            CodigoBarra = Guid.NewGuid();
            DataVencimento = DataEmissao.AddDays(DiasVencimento);
        }

        public bool EstaPago()
        {
            return Confirmacao;
        }

        public bool EstaVencido()
        {
            return DataVencimento < DateTime.Now;
        }

        public void CalcularJuros()
        {
            var taxa = Valor * Juros;
            Valor = Valor + taxa;
        }


    }
}