using SistemaDePagamento.Entidades.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDePagamento.Entidades
{
    public class Dinheiro : Pagamento
    {

        private const double Desconto = 0.05;
        public Dinheiro (double valor, double valorRecebido)
        {

        Valor = valor;
        ValorRecebido = valorRecebido;
        PagamentoConfirmado = false;


        }

        public override void Pagar()
        {
            var valorDesconto = Valor * Desconto;
            Valor = Valor - valorDesconto;
            base.Pagar();
        }


       public double ValorRecebido { get; set; }
       public bool PagamentoConfirmado { get; set; }


       public void pgtoDinheiroConfirmado()
         {
             if(Valor > ValorRecebido)
        {
            
            PagamentoConfirmado = true;

        }

    }

    }

    
}
