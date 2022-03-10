using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.Runtime;

namespace SQS.Consumidor // Producer
{
    public class ObjetoRecebido
    {
        public int Codigo {get;set;}
        public string Mensagem {get;set;}
        public DateTime Data {get;set;}
    }
}
