using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NLPBot.Services
{
    public sealed class LuisInterpreterService : IRecognizer
    {
        LuisRecognizer luisRecognizer;

        public LuisInterpreterService(IConfiguration configuration)
        {
            //We need to create a new Luis Application object to connect our Luis app to our bot.
            var luisApp = new LuisApplication(configuration["LuisAppId"],
                configuration["LuisEndpointKey"],
                configuration["LuisEndpoint"]);

            var recognizerOptions = new LuisRecognizerOptionsV3(luisApp);

            luisRecognizer = new LuisRecognizer(recognizerOptions);
        }

        public async Task<RecognizerResult> RecognizeAsync(ITurnContext turnContext, CancellationToken cancellationToken) =>
            await luisRecognizer.RecognizeAsync(turnContext, cancellationToken);


        public async Task<T> RecognizeAsync<T>(ITurnContext turnContext, CancellationToken cancellationToken) where T : IRecognizerConvert, new() =>
            await luisRecognizer.RecognizeAsync<T>(turnContext, cancellationToken);
    }
}
