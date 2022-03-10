using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.Runtime;
using Newtonsoft.Json;

namespace SQS.Consumidor // Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var awsCredentials = new BasicAWSCredentials(Environment.GetEnvironmentVariable("AWS_KEY"), Environment.GetEnvironmentVariable("AWS_SECRET")); 

            var client = new AmazonSQSClient(awsCredentials, RegionEndpoint.SAEast1);
            var request = new ReceiveMessageRequest
            {
                QueueUrl = Environment.GetEnvironmentVariable("SQS_URL")
            };

            while (true)
            {
                var response = await client.ReceiveMessageAsync(request);
                foreach (var mensagem in response.Messages)
                {
                    var objRecebido = JsonConvert.DeserializeObject<ObjetoRecebido>(mensagem.Body);

                    Console.WriteLine("================================");
                    Console.WriteLine(objRecebido.Codigo);
                    Console.WriteLine(objRecebido.Mensagem);
                    Console.WriteLine(objRecebido.Data);
                    Console.WriteLine("============[Faço meu processamento de envio para quem for, exemplo meio de pagamento]====================");

                    // ao processar deleta a mensagem
                    await client.DeleteMessageAsync(Environment.GetEnvironmentVariable("SQS_URL"), mensagem.ReceiptHandle);
                }
            }
        }
    }
}
