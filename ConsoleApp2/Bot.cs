using System;
using System.IO;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Bot
    {
        //References to the client and command objects
        public DiscordSocketClient client { get; private set; }
        public CommandService commands { get; private set; }

        public async Task RunBot()
        {
            //Load Custom Config Code
            ConfigLoader.Load();

            //Check if there is a prefix actually set
            if (ConfigLoader.Prefix.Equals("") || ConfigLoader.Token.Equals(""))
            {
                Console.WriteLine($"Token and/or Prefix not set in config.json. " +
                    $"Please add them at {Directory.GetCurrentDirectory()}/configs.json");
                return;
            }

            //The config of your client
            var config = new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Info
            };

            //Sets the client with the given config
            client = new DiscordSocketClient(config);

            //Manages Events
            client.Log += ClientLog;
            client.GuildAvailable += ClientGuildAvailable;
            client.Ready += ClientReady;

            //Sets the bots presence
            await client.SetGameAsync("My custom bot!");

            //Takes in the token type and the token
            await client.LoginAsync(TokenType.Bot, ConfigLoader.Token );
            //Starts and connects to the bot
            await client.StartAsync();

            //Prevents the console application closing early
            await Task.Delay(-1);
        }

        private Task ClientReady()
        {
            //When the client is ready and connected print the following
            Console.WriteLine("Client ready...");
            return Task.CompletedTask;
        }

        private Task ClientGuildAvailable(SocketGuild guild)
        {
            //When the bot is able to see a guild print the following with the guild name
            Console.WriteLine("Logging into guild: " + guild.Name);
            return Task.CompletedTask;
        }

        private Task ClientLog(LogMessage log)
        {
            //When the bot receives log data display it to the console
            Console.WriteLine(log.Message);
            return Task.CompletedTask;
        }
    }
}
