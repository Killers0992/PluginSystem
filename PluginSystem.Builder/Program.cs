using ICSharpCode.SharpZipLib.Tar;
using Ionic.Zip;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;

namespace PluginSystem.Builder
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunBuilder().Wait();
            Console.WriteLine($"Press any key to exit...");
            Console.ReadKey();
        }

        static void SendLog(string type, string message, ConsoleColor color = ConsoleColor.Green)
        {
            var defaultColor = Console.ForegroundColor;
            Console.Write(" [");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(DateTime.Now.ToString("T"));
            Console.ForegroundColor = defaultColor;
            Console.Write("] ");
            Console.Write($" [");
            Console.ForegroundColor = color;
            Console.Write(type);
            Console.ForegroundColor = defaultColor;
            Console.Write($"] ");
            Console.Write(message);
            Console.WriteLine();
        }

        static async Task RunBuilder()
        {
            var mainDirectory = Environment.CurrentDirectory;

            var directory = Path.Combine(mainDirectory, "Builder");

            SendLog("Builder", $"Working in directory {directory}...");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!Directory.Exists(Path.Combine(directory, "SteamCMD")))
                Directory.CreateDirectory(Path.Combine(directory, "SteamCMD"));

            if (!Directory.Exists(Path.Combine(directory, "SCPSL")))
                Directory.CreateDirectory(Path.Combine(directory, "SCPSL"));

            if (!Directory.Exists(Path.Combine(directory, "PluginSystem")))
                Directory.CreateDirectory(Path.Combine(directory, "PluginSystem"));

            if (!Directory.Exists(Path.Combine(directory, "Output")))
                Directory.CreateDirectory(Path.Combine(directory, "Output"));

            if (!Directory.Exists(Path.Combine(directory, "MonoMod")))
                Directory.CreateDirectory(Path.Combine(directory, "MonoMod"));


            if (!Directory.Exists(Path.Combine(directory, "APublicizer")))
                Directory.CreateDirectory(Path.Combine(directory, "APublicizer"));


            if (!Directory.Exists(Path.Combine(directory, "temp")))
                Directory.CreateDirectory(Path.Combine(directory, "temp"));

            if (!File.Exists(Path.Combine(directory, "SteamCMD", "steamcmd.exe")))
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip"))
                    {
                        SendLog("Builder", $"Downloading steamcmd...");
                        using (Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(), stream = new FileStream(Path.Combine(directory, "temp", "steamcmd.zip"), FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await contentStream.CopyToAsync(stream);
                        }
                        SendLog("Builder", $"Downloaded steamcmd!");
                        SendLog("Builder", $"Extracting steamcmd.zip...");

                        var ins = Ionic.Zip.ZipFile.Read(Path.Combine(directory, "temp", "steamcmd.zip"));
                        ins.ExtractAll(Path.Combine(directory, "SteamCMD"), ExtractExistingFileAction.OverwriteSilently);
                        ins.Dispose();
                        SendLog("Builder", $"Extracted steamcmd.zip!");
                        File.Delete(Path.Combine(directory, "temp", "steamcmd.zip"));
                    }
                }
            }
            SendLog("Builder", $"Start SCPSL files validation...");

            using (var process = new Process())
            {
                process.StartInfo.WorkingDirectory = Path.Combine(directory, "SteamCMD");
                process.StartInfo.FileName = Path.Combine(directory, "SteamCMD", "steamcmd.exe");
                process.StartInfo.Arguments = $"+login anonymous +force_install_dir \"{Path.Combine(directory, "SCPSL")}\" +app_update 996560 validate +quit";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += (obj, ev) =>
                {
                    SendLog("SteamCMD", $"{ev.Data}", ConsoleColor.Red);
                };
                process.Start();
                process.BeginOutputReadLine();
                await process.WaitForExitAsync();
            }

            SendLog("Builder", $"Ended SCPSL files validation!");

            if (!File.Exists(Path.Combine(directory, "APublicizer", "APublicizer.exe")))
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, "https://github.com/iRebbok/APublicizer/releases/download/1.0.3/native-win-x64-release.tar.gz"))
                    {
                        SendLog("Builder", $"Downloading APublicizer...");
                        using (Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(), stream = new FileStream(Path.Combine(directory, "temp", "APublicizer.tar.gz"), FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await contentStream.CopyToAsync(stream);
                        }
                        SendLog("Builder", $"Downloaded APublicizer!");
                        SendLog("Builder", $"Extracting APublicizer.tar.gz...");
                        using (GZipStream decompressionStream = new GZipStream(File.OpenRead(Path.Combine(directory, "temp", "APublicizer.tar.gz")), CompressionMode.Decompress))
                        {
                            using (TarArchive tarArchive = TarArchive.CreateInputTarArchive(decompressionStream, nameEncoding: System.Text.Encoding.UTF8))
                            {
                                tarArchive.ExtractContents(Path.Combine(directory, "APublicizer"));
                            }
                        }
     


                        SendLog("Builder", $"Extracted APublicizer.tar.gz!");
                        File.Delete(Path.Combine(directory, "temp", "APublicizer.tar.gz"));
                    }
                }
            }

            SendLog("Builder", $"Run publicizer...");

            using (var process = new Process())
            {
                process.StartInfo.FileName = Path.Combine(directory, "APublicizer", "APublicizer.exe");
                process.StartInfo.WorkingDirectory = Path.Combine(directory, "SCPSL", "SCPSL_Data", "Managed");
                process.StartInfo.Arguments = $"./Assembly-CSharp.dll";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += (obj, ev) =>
                {
                    SendLog("APublicizer", $"{ev.Data}", ConsoleColor.Cyan);
                };
                process.Start();
                process.BeginOutputReadLine();
                await process.WaitForExitAsync();
            }

            SendLog("Builder", $"Ended SCPSL files validation!");


            SendLog("Builder", $"Build Patcher...");

            using (var process = new Process())
            {
                process.StartInfo.FileName = "dotnet";
                process.StartInfo.Arguments = $"build \"{Path.Combine(mainDirectory, "PluginSystem.Patcher", "PluginSystem.Patcher.csproj")}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += (obj, ev) =>
                {
                    SendLog("DotNet", $"{ev.Data}", ConsoleColor.Cyan);
                };
                process.Start();
                process.BeginOutputReadLine();
                await process.WaitForExitAsync();
            }

            SendLog("Builder", $"Ended SCPSL files validation!");
            SendLog("Builder", $"Move Assembly-CSharp and PluginSystem dlls to SCPSL directory...");

            File.Copy(Path.Combine(directory, "PluginSystem", "Assembly-CSharp.mm.dll"), Path.Combine(directory, "SCPSL", "SCPSL_Data", "Managed", "Assembly-CSharp.mm.dll"), true);
            File.Copy(Path.Combine(directory, "PluginSystem", "PluginSystem.dll"), Path.Combine(directory, "SCPSL", "SCPSL_Data", "Managed" , "PluginSystem.dll"), true);
            File.Copy(Path.Combine(directory, "PluginSystem", "PluginSystem.dll"), Path.Combine(directory, "Output", "PluginSystem.dll"), true);

            SendLog("Builder", $"Moved Assembly-CSharp and PluginSystem dlls to SCPSL directory!");

            if (!File.Exists(Path.Combine(directory, "MonoMod", "MonoMod.exe")))
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, "https://github.com/MonoMod/MonoMod/releases/download/v22.01.04.03/MonoMod-22.01.04.03-net50.zip"))
                    {
                        SendLog("Builder", $"Downloading monomod...");
                        using (Stream contentStream = await (await client.SendAsync(request)).Content.ReadAsStreamAsync(), stream = new FileStream(Path.Combine(directory, "temp", "monomod.zip"), FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await contentStream.CopyToAsync(stream);
                        }
                        SendLog("Builder", $"Downloaded monomod!");
                        SendLog("Builder", $"Extracting monomod.zip...");

                        var ins = Ionic.Zip.ZipFile.Read(Path.Combine(directory, "temp", "monomod.zip"));
                        ins.ExtractAll(Path.Combine(directory, "MonoMod"), ExtractExistingFileAction.OverwriteSilently);
                        ins.Dispose();
                        SendLog("Builder", $"Extracted monomod.zip!");
                        File.Delete(Path.Combine(directory, "temp", "monomod.zip"));
                    }
                }
            }

            SendLog("Builder", $"Run Patcher...");

            using (var process = new Process())
            {
                process.StartInfo.WorkingDirectory = Path.Combine(directory, "SCPSL", "SCPSL_Data", "Managed");
                process.StartInfo.FileName = Path.Combine(directory, "MonoMod", "MonoMod.exe");
                process.StartInfo.Arguments = $"Assembly-CSharp.dll";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += (obj, ev) =>
                {
                    SendLog("Patcher", $"{ev.Data}", ConsoleColor.Yellow);
                };
                process.Start();
                process.BeginOutputReadLine();
                await process.WaitForExitAsync();
            }

            SendLog("Builder", $"Ended patching!");
            File.Delete(Path.Combine(directory, "SCPSL", "SCPSL_Data", "Managed", "Assembly-CSharp.mm.dll"));

            File.Move(Path.Combine(directory, "SCPSL", "SCPSL_Data", "Managed", "MONOMODDED_Assembly-CSharp.dll"), Path.Combine(directory, "Output", "Assembly-CSharp.dll"), true);
        }
    }
}

