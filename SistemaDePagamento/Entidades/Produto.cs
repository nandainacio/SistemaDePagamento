using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDePagamento.Entidades
{
    public class Produto : Pagamento
    {

        private const double DescontoProduto = 0.15;
        private const double BonusGeladeira = 100;

        public Produto(double valor)
        {
            Valor = valor;
            //    Televisao = televisao;
            //    Geladeira = geladeira;

            //    Produto Televisao = new Produto();
        }

        public void DescontoTelevisao()
        {
            var valorDescontoProduto = Valor * DescontoProduto;
            Valor = Valor - valorDescontoProduto;
        }

        public void DescontoGeladeira()
        {
            var valorDescontoProduto = Valor * DescontoProduto;
            Valor = (Valor - valorDescontoProduto) - BonusGeladeira;
        }
    }
}
