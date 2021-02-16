using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExSharpBase.Game;
using ExSharpBase.Modules;
using Newtonsoft.Json.Linq;

namespace ExSharpBase
{
    class Program
    {
        public static Overlay.Base DrawBase = new Overlay.Base();
        public static Menu.BasePlate MenuBasePlate = new Menu.BasePlate();
        public static Memory Driver = new Memory();
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                Process Gameloop = null;
                await Task.Run(() => Utils.EnterDebugMode());
                await Task.Run(() => LogService.Log("Loading Driver"));
                await Task.Run(() => Driver.LoadDriver());
                await Task.Run(() => LogService.Log("Waiting for Gameloop Process (PUBG Mobile)"));
                await Task.Run(() => Gameloop = Utils.GetPubgProcess());
                await Task.Run(() => LogService.Log($"Gameloop Process (PUBG Mobile) found : {Gameloop.Id}"));
                await Task.Run(() => Driver.Attach(Gameloop));
                await Task.Run(() => Engine.Inti());
                await Task.Run(() => LogService.Log("Found Live Instance of The Game."));
                await Task.Run(() => Events.EventsManager.SubscribeToEvents());
                await Task.Run(() => LogService.Log("Initialising Overlay Rendering..."));
                await Task.Run(() => DrawBase.Show());

            }).GetAwaiter().GetResult();
        }
    }
}
