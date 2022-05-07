using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.Runtime;
using Newtonsoft.Json;

namespace SQS.Fornecedor // Producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var objEnvio = new ObjetoEnvio()
            {
                Codigo = 1,
                Mensagem = "Uma mensagem",
                Data = DateTime.Now
            };

            var awsCredentials = new BasicAWSCredentials(Environment.GetEnvironmentVariable("AWS_KEY"), Environment.GetEnvironmentVariable("AWS_SECRET")); 
            var client = new AmazonSQSClient(awsCredentials, RegionEndpoint.USEast1);
            var request = new SendMessageRequest
            {
                QueueUrl = Environment.GetEnvironmentVariable("SQS_URL"),
                MessageBody = JsonConvert.SerializeObject(objEnvio)
            };

            await client.SendMessageAsync(request);
        }
    }
}
