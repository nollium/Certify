using System;
using System.Diagnostics;

namespace EnterpriseAdmin
{
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Info.ShowLogo();
                Info.ShowUsage();
                return 1;
            }

            var sw = new Stopwatch();
            sw.Start();

            try
            {
                var commands = new CommandCollection();
                var parser = new ArgumentParser(args);

                if (!parser.ParseSuccess)
                {
                    Info.ShowLogo();
                    Info.ShowUsage();
                    return 1;
                }

                if (!commands.ExecuteCommand(parser.Arguments))
                {
                    Info.ShowLogo();
                    Info.ShowUsage();
                    return 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\r\n[!] Unhandled DirectoryAdmin exception:\r\n");
                Console.WriteLine(e);
                return 1;
            }
            finally
            {
                sw.Stop();
                if (!args[0].Equals("enumerate") || !args.Contains("/json"))
                {
                    Console.WriteLine($"\r\n\r\nDirectoryAdmin completed in {sw.Elapsed}");
                }
            }

            return 0;
        }

        public static string MainString(string command)
        {
            var originalOut = Console.Out;
            var writer = new System.IO.StringWriter();
            Console.SetOut(writer);

            try
            {
                var args = ArgumentParser.TokenizeString(command);
                Main(args);
            }
            catch (Exception e)
            {
                Console.WriteLine("\r\n[!] Unhandled DirectoryAdmin exception:\r\n");
                Console.WriteLine(e);
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            return writer.ToString();
        }
    }
}
