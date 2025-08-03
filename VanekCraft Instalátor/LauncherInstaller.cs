using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Text.Json;
using System.Diagnostics;

namespace VanekCraftInstaller
{
    public class LauncherInstaller
    {
        private const string LAUNCHER_URL = "https://cdn.vanekgroup.eu/instalator/SKlauncher.jar";
        private const string VERSION_ZIP_URL = "https://cdn.vanekgroup.eu/instalator/verze1213.zip";
        
        private readonly string _launcherDir;
        private readonly string _minecraftDir;
        private readonly string _launcherPath;
        private readonly string _accountsPath;
        
        public LauncherInstaller()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _launcherDir = Path.Combine(appData, "vanekcraftlauncher");
            _minecraftDir = Path.Combine(appData, ".minecraft");
            _launcherPath = Path.Combine(_launcherDir, "sklauncher.jar");
            _accountsPath = Path.Combine(appData, ".sklauncher", "accounts.json");
        }
        
        public async Task DownloadLauncher()
        {
            Directory.CreateDirectory(_launcherDir);
            
            using var client = new HttpClient();
            var response = await client.GetAsync(LAUNCHER_URL);
            response.EnsureSuccessStatusCode();
            
            await File.WriteAllBytesAsync(_launcherPath, await response.Content.ReadAsByteArrayAsync());
        }
        
        public async Task ExtractVersionZip()
        {
            string versionsDir = Path.Combine(_minecraftDir, "versions", "1.21.3");
            
            if (Directory.Exists(versionsDir))
                return;
                
            Directory.CreateDirectory(Path.Combine(_minecraftDir, "versions"));
            
            using var client = new HttpClient();
            var response = await client.GetAsync(VERSION_ZIP_URL);
            response.EnsureSuccessStatusCode();
            
            string tempZip = Path.GetTempFileName();
            await File.WriteAllBytesAsync(tempZip, await response.Content.ReadAsByteArrayAsync());
            
            ZipFile.ExtractToDirectory(tempZip, Path.Combine(_minecraftDir, "versions"));
            File.Delete(tempZip);
        }
        
        public async Task DownloadVanillaVersion(string version)
        {
            string versionDir = Path.Combine(_minecraftDir, "versions", version);
            string jarPath = Path.Combine(versionDir, $"{version}.jar");
            string jsonPath = Path.Combine(versionDir, $"{version}.json");
            
            if (File.Exists(jarPath) && File.Exists(jsonPath))
                return;
                
            Directory.CreateDirectory(versionDir);
            
            using var client = new HttpClient();
            
            // Stáhni manifest verzí
            var manifestResponse = await client.GetAsync("https://launchermeta.mojang.com/mc/game/version_manifest.json");
            manifestResponse.EnsureSuccessStatusCode();
            
            var manifestJson = await manifestResponse.Content.ReadAsStringAsync();
            using var manifestDoc = JsonDocument.Parse(manifestJson);
            
            // Najdi URL pro požadovanou verzi
            string versionUrl = null;
            foreach (var ver in manifestDoc.RootElement.GetProperty("versions").EnumerateArray())
            {
                if (ver.GetProperty("id").GetString() == version)
                {
                    versionUrl = ver.GetProperty("url").GetString();
                    break;
                }
            }
            
            if (string.IsNullOrEmpty(versionUrl))
                throw new Exception($"Verze {version} nebyla nalezena v manifestu");
            
            // Stáhni detailní JSON verze
            var versionResponse = await client.GetAsync(versionUrl);
            versionResponse.EnsureSuccessStatusCode();
            
            var versionJson = await versionResponse.Content.ReadAsStringAsync();
            using var versionDoc = JsonDocument.Parse(versionJson);
            
            // Ulož JSON soubor
            await File.WriteAllTextAsync(jsonPath, versionJson);
            
            // Získej URL pro client.jar
            string clientUrl = versionDoc.RootElement.GetProperty("downloads").GetProperty("client").GetProperty("url").GetString();
            
            // Stáhni client.jar
            var clientResponse = await client.GetAsync(clientUrl);
            clientResponse.EnsureSuccessStatusCode();
            
            await File.WriteAllBytesAsync(jarPath, await clientResponse.Content.ReadAsByteArrayAsync());
        }
        
        public void CreateOfflineProfile(string nickname)
        {
            string uuid = GenerateOfflineUUID(nickname);
            string accountsDir = Path.GetDirectoryName(_accountsPath);
            Directory.CreateDirectory(accountsDir);
            
            var profile = new
            {
                accounts = new[]
                {
                    new
                    {
                        type = "Offline",
                        active = true,
                        profile = new
                        {
                            id = uuid,
                            name = nickname,
                            skin = new
                            {
                                id = "",
                                url = "",
                                variant = ""
                            },
                            capes = new object[0]
                        },
                        ygg = new
                        {
                            token = "0",
                            iat = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                            extra = new
                            {
                                clientToken = GenerateClientToken(nickname),
                                userName = nickname
                            }
                        }
                    }
                },
                formatVersion = 3
            };
            
            string json = JsonSerializer.Serialize(profile, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_accountsPath, json);
        }
        
        public void RunLauncher()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "java",
                    Arguments = $"-jar \"{_launcherPath}\"",
                    UseShellExecute = true
                }
            };
            process.Start();
        }
        
        public void CreateDesktopShortcut()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string shortcutPath = Path.Combine(desktopPath, "VanekCraft Launcher.lnk");
            
            Type shellType = Type.GetTypeFromProgID("WScript.Shell");
            dynamic shell = Activator.CreateInstance(shellType);
            var shortcut = shell.CreateShortcut(shortcutPath);
            
            shortcut.TargetPath = "java";
            shortcut.Arguments = $"-jar \"{_launcherPath}\"";
            shortcut.Description = "VanekCraft Launcher";
            shortcut.WorkingDirectory = _launcherDir;
            
            shortcut.Save();
        }
        
        private string GenerateOfflineUUID(string nickname)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes($"OfflinePlayer:{nickname}"));
            return new Guid(hash).ToString();
        }
        
        private string GenerateClientToken(string nickname)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes($"ClientToken:{nickname}"));
            return new Guid(hash).ToString();
        }
    }
}