﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using gwcWeatherConnect;
using System.IO;
using gwcFileSystem;

namespace DiscordTest
{
    class WeatherUnderGround : Module
    {

        private Dictionary<string, Queue> queuesRunning;
        wuAPI wu;
        public WeatherUnderGround(FileSystemFile file)
        {
            command = "wu";
            wu = new wuAPI(file);
            methods = new Dictionary<string, Func<CommandEventArgs, Task>>();
            queuesRunning = new Dictionary<string, Queue>();
            methods.Add("get", async (command) =>
            {
                if(command.GetArg(1).Equals(""))
                {
                    await command.Channel.SendMessage(command.Message.User.Mention + " location was not specified.");
                    return;
                }
                await command.Channel.SendMessage(command.User.Name + " searched for weather for " + command.GetArg(1));
                await command.Channel.SendMessage(wu.getCurrentWeather(command.GetArg(1)));
                await command.Message.Delete();
            });
            /*methods.Add("queue", async (command) =>
            {
                int delay;
                string query = command.GetArg(1);
                if (command.GetArg(2) == null || !Int32.TryParse(command.GetArg(2), out delay))
                {
                    await command.Channel.SendMessage("!weather queue <search> <delay in mins>");
                    return;
                }
                await command.Message.Delete();
                if (!queuesRunning.ContainsKey(query))
                {
                    await command.Channel.SendMessage(query + " queue has been started");
                    queuesRunning.Add(query, new WeatherQueue(query, new TimeSpan(0, delay, 0), false, command, owm));
                    queuesRunning[query].Start();
                }
                else
                {
                    await command.Channel.SendMessage(query + " queue has already been started");
                }
            });
            methods.Add("stopqueue", async (command) =>
            {
                string query = command.GetArg(1);
                await command.Message.Delete();
                if (queuesRunning.ContainsKey(query))
                {
                    queuesRunning[query].Stop();
                    queuesRunning.Remove(query);
                    await command.Channel.SendMessage(query + " queue stopped");
                }
                else
                {
                    await command.Channel.SendMessage(query + " queue not found");
                }
            });
            methods.Add("stopqueues", async (command) =>
            {
                lock (queuesRunning)
                {
                    foreach (string entry in queuesRunning.Keys.ToArray())
                    {
                        command.Channel.SendMessage(entry + " queue stopped");
                        if (queuesRunning.ContainsKey(entry))
                        {
                            queuesRunning[entry].Stop();
                            queuesRunning.Remove(entry);
                        }
                        else
                            command.Channel.SendMessage("huh where did it go? " + entry);
                    }
                }
                await command.Message.Delete();
                await command.Channel.SendMessage("All Image queues stopped");
            });*/
            methods.Add("help", async (command) =>
            {
                await command.Channel.SendMessage(getHelp());
            });
        }

        public override string getHelp()
        {
            string help = "!wu commands:\n" +
                "get <query>: query weather for location <query>\n";
            return help;
        }
        
    }
}
