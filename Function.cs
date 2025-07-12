using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.DependencyInjection;
using StockPicker.Lambda.Persistence.Models;
using StockPicker.Lambda.Persistence.Repository;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace StockPicker.Lambda;

public class Function
{
    private readonly IStockInfoRepository _stockInfoRepository;

    public Function()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IAmazonDynamoDB>(sp => new AmazonDynamoDBClient());

        services.AddSingleton<IDynamoDBContext>(sp =>
        {
            var client = sp.GetRequiredService<IAmazonDynamoDB>();

            var context = new DynamoDBContextBuilder()
                .WithDynamoDBClient(() => client)
                .ConfigureContext(config =>
                {
                    config.ConsistentRead = true;
                    config.DisableFetchingTableMetadata = true; 
                })
                .Build();

            return context;
        });

        services.AddSingleton<IStockInfoRepository, StockInfoRepository>();

        var provider = services.BuildServiceProvider();

        _stockInfoRepository = provider.GetRequiredService<IStockInfoRepository>();
    }

    public async Task Handle(SQSEvent sqsEvent, ILambdaContext context)
    {
        foreach (var message in sqsEvent.Records)
        {
            try
            {
                // Process each SQS message
                string body = message.Body;
                context.Logger.LogLine($"Processing message: {body}");

                // Here you could parse JSON, call an API, save to DB, etc.
                await ProcessMessage(body);
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Error: {ex.Message}");
            }
        }
    }

    private async Task ProcessMessage(string messageBody)
    {
        var stockInfo = JsonSerializer.Deserialize<StockInfo>(messageBody);

        try
        {
            if (stockInfo != null)
            {
                await _stockInfoRepository.UpdateStockInfoAsync(stockInfo);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}