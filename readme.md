VanekCraft Instalátor 0.1.0-rc1

„Když už se má Minecraft spustit, tak ať to aspoň nedá zabrat.“ – každej, kdo to musel dělat ručně

💾 Co tahle verze umí:

Stáhne launcher (SKL nebo ofiko) a připraví ti profil jak dělaný pro hraní na vanekCraft serveru.
Automaticky nainstaluje modpack s Fabricem – žádný klikání, žádný bloudění.
Přidá ti ikonu na plochu, protože jsme prostě tak hodný.
Umí si i sama stáhnout Javu, pokud ji nemáš – ne že bychom ti lezli do kompu… ale trošku jo.
Celý to probíhá skoro bez kecání – jen pár statusů, protože sarkasmus je potřeba všude.

🧪 Testováno na:

✅ Windows 11

⏳ Windows 10 (brzy proklepneme)

🔄 Windows 7 (v plánu)

🐧 Linux verze (někdy… v budoucnu… až bude čas, nálada a Red Bull)

⚠️ Poznámky:

Instalátor sice rozumí launcherům a modpackům, ale nevaří kafe. Zatím.

Pokud něco spadne – to je v pohodě, je to RCčko. Hlášení bugů vítáno na GitHub issues nebo poštovním holubem.

Minecraft, SKLauncher a jednotlivé mody nejsou moje – vše patří svým původním autorům, já jsem to jen složil jako LEGO. Copyright kódů patří mně.

# 🎮 VanekCraft Instalátor

Vítej u oficiálně-neoficiálního průvodce instalací megamodbalu VanekCraft!  
Máš tu projekt *open source*, vytvořený jako výukový featurkód ve Visual Studio 2022, s podporou AI magií (ChatGPT, Copilot od Microsoftu a Amazon Q & AWS).  
Nic z toho není legální náhrada licence Minecraftu, ale je to tvoje legální dílo. 😎

---

## 🧠 Co dělá Instalátor

- Proleze ti komp a zkontroluje Java 17+ (na fotky tvý/ho ex nešahá).
- Stáhne launcher:
  - **Oficiální launcher** – ano, budeš si muset zaplatit.
  - **SK Launcher** – licence? Cálovat? Ha! Tady běží všechno bez ní.
- Nainstaluje Minecraft verzi 1.21.3 a Fabric Loader 0.16.14.
- Vyčistí dočasný soubory.
- Vše automaticky připravené, abys klikal a hrál.

### 📦 Co se děje pod kapotou (transparentnost first, drama never)

- 🧰 **SKLauncher** je stažen ve své originální, neupravené podobě – buď z oficiálního odkazu, nebo z mého CDN pro rychlejší a stabilnější instalaci.
- 📁 **.minecraft složka** obsahuje předpřipravenou strukturu s verzí **1.21.3**, vygenerovanou samotným SKLauncherem.2
- 📝 Během instalace upravujeme:
  - `accounts.json` – aby sis mohl(a) zvolit vlastní nick (nepoužíváme skutečné účty).
  - `launcher_profiles.json` – nastaví se profil s Fabric Loaderem pro snadné spuštění VanekCraftu.
- ✨ **Fabric Installer** je stažen z CDN a spuštěn bez keců (a klikání), v tichém režimu.
- 📦 **Modpack** je zabalen jako jeden `.zip` archiv bez úprav jednotlivých modů – prostě tak, jak je autoři vytvořili.

> 💡 Všechno, co se děje, je navržené s respektem k právům vývojářů. Nic nevydáváme za vlastní a nikoho nešidíme. Jen to skládáme dohromady jako LEGO pro pohodlnější start.

---

## 📦 Modpack obsahuje (aktuální 31.07.2025):

**Aktuální seznam modů (k 31.07.2025):**

| Mod (verze)                             | Popis                                | Licence a odk0
 azy                                    |
|----------------------------------------|------------------------------------|----------------------------------------------------|
| architectury-14.0.4-fabric.jar          | Abstraktní API pro více‑loader modů| [Modrinth](https://modrinth.com/mod/architectury) (MIT License) |
| balm-fabric-1.21.3-21.3.5.jar           | Usnadňuje vývoj napříč mod-loadery | [Modrinth](https://modrinth.com/mod/balm) (MIT License) |
| cloth-config-16.0.143-fabric.jar        | Konfigurační API pro mody           | [Modrinth](https://modrinth.com/mod/cloth-config) (MIT License) |
| fabric-api-0.114.1+1.21.3.jar           | Základní API pro Fabric modding    | [Modrinth](https://modrinth.com/mod/fabric-api) (MIT License) |
| fabric-language-kotlin-1.13.4+kotlin... | Kotlin rozhraní pro Fabric          | [Modrinth](https://modrinth.com/mod/fabric-language-kotlin) (MIT License) |
| ferritecore-7.1.0-hotfix-fabric.jar     | Optimalizace paměti                 | [Modrinth](https://modrinth.com/mod/ferritecore) (MIT License) |
| ForgeConfigAPIPort-v21.3.4-1.21.3-Fabric.jar | Port Forge konfiguračního API  | [Modrinth](https://modrinth.com/mod/forgeconfigapiport) (MIT License) |
| fpsdisplay-4.2.3+1.21.3.jar             | Zobrazení FPS a debug info          | (MIT License, [GitHub](https://github.com/FabricMC/fpsdisplay)) |
| inventoryessentials-fabric-1.21.3-21.3.2.jar | Uživatelské UI v inventáři      | [Modrinth](https://modrinth.com/mod/inventory-essentials) (MIT License) |
| libIPN-fabric-1.21.3-6.4.0.jar          | Síťovky a IP propojení              | [Modrinth](https://modrinth.com/mod/libipn) (MIT License) |
| lithium-fabric-0.14.6+mc1.21.3.jar      | Herní logika optimalizace          | [Modrinth](https://modrinth.com/mod/lithium) (MIT License) |
| modmenu-12.0.0.jar                       | Zobrazení modů v launcheru          | [Modrinth](https://modrinth.com/mod/modmenu) (MIT License) |
| open-parties-and-claims-fabric-1.21.3-0.24.0.jar | Ochrany a party systémy          | [Modrinth](https://modrinth.com/mod/open-parties-and-claims) (MIT License) |
| RoughlyEnoughItems-17.0.807-fabric.jar  | GUI pro předmětový cheat/help list | [Modrinth](https://modrinth.com/mod/roughly-enough-items) (MIT License) |
| sodium-extra-fabric-0.6.0+mc1.21.3.jar | DLC pro Sodium performance mod     | [Modrinth](https://modrinth.com/mod/sodium-extra) (MIT License) |
| sodium-fabric-0.6.13+mc1.21.3.jar       | Extrémní grafické optimalizace     | [Modrinth](https://modrinth.com/mod/sodium) (MIT License) |
| tree-chopping-v4.1.4.jar                 | Napůlpá zářez pro stromování       | [CurseForge](https://www.curseforge.com/minecraft/mc-mods/tree-chopping) (MIT License) |
| Xaeros_Minimap_25.2.10_Fabric_1.21.3.jar | Minimapka s waypointy             | [Modrinth](https://modrinth.com/mod/xaeros-minimap) (Proprietary) |
| XaerosWorldMap_1.39.12_Fabric_1.21.3.jar | Celosvětová mapa pro snadnou orientaci | [Modrinth](https://modrinth.com/mod/xaeros-world-map) (Proprietary) |
| yet_another_config_lib_v3-3.7.1+1.21.3-fabric.jar | Další konfigurační knihovna  | [Modrinth](https://modrinth.com/mod/yacl) (MIT License) |


**Poznámka:** Modpack a jeho mody nejsou můj produkt. Všechny autorská práva patří jejich vývojářům. Instalátor modpacku je pouze distribuční most.

---

## 🧩 Jak si to nainstalovat

1. Vyber `.exe` → spusť jako admin
2. Sledu kroky v instalátoru
3. Zaškrtni „Spustit launcher“ nebo užívej kliknutím
4. Pokud Launcher zlobí, zkus restart nebo mi napiš (existuji 😏)

---

## Licence a Práva

### O mojí práci
Tento instalátor je můj vlastní výtvor, napsaný s pomocí AI (ChatGPT, Copilot, Amazon Q & AWS).  
Je to takovej malej love project, abych si ulehčil život při instalaci Minecraft modpacků.  
Pokud někdo chce můj kód použít, klidně ať ho použije, ale:
- Musí mě uvést jako autora (díky, to je slušnost).
- Pokud z toho bude někdo profitovat, musí mít moje svolení a nějaký to procento z toho taky patří mě.  
- Jinak je to moje práce, tak s tím prosím nemanipulujte, abych nepřišel o své práva.

### Co je ve hře a odkud pochází
- **Minecraft** je majetkem Microsoftu, jeho licence najdeš [tady](https://account.mojang.com/documents/minecraft_eula).  
  Mojang si drží svoje autorský práva a instalátor pouze usnadňuje legální instalaci oficiálního klienta.
- **SKLauncher (SKL)** je open-source projekt dostupný na [GitHubu](https://github.com/skmedix/SKlauncher) pod licencí MIT (respektive pod jejich licencí, kterou najdeš v repozitáři).  
  Instalátor jej stahuje z mého CDN, aby byl proces rychlejší a jednodušší, ale SKL zůstává vlastnictvím původních vývojářů.
-410 **Fabric a módy** jsou vlastnictvím jejich autorů a instalátor je jen distribuuje ve spoustě pro lepší uživatelský zážitek.  
  Licence jednotlivých modů najdeš v jejich repozitářích nebo na stránkách autorů.

### Souhrn licencí použitého softwaru:
- Instalátor: [MIT Licence](./LICENSE) (tohle je moje)
- Minecraft: [EULA Mojang](https://account.mojang.com/documents/minecraft_eula)
- SKLauncher: [MIT Licence SKL](https://github.com/skmedix/SKlauncher/blob/master/LICENSE)
- Fabric: [Fabric Licence](https://fabricmc.net/terms/)

---

Pokud si nejsi jistý/á, co můžeš a co ne, tak radši nespouštěj instalátor nebo si přečti podrobně licence jednotlivých částí.  
Hraní Minecraftu je radost, ale respekt k autorům musí bejt taky.

---

## 🤖 Využité AI / Nástroje

- **ChatGPT** (OpenAI) – generování textů a promptů  
- **GitHub Copilot** (Microsoft) – při psaní C# a XAML kódu  
- **Amazon Q & AWS** – testování, automatizace a kontrola kódování  
- Vytvářeno jako *learn‑projekt* a důkaz, že i AI spoluautor může být sranda.

---

## 🚀 Autorka / Autor

**Jakub Vanek(KubiikSvK)**  
Zkušenosti: pixelový chaos, vtipné instalátory, rádoby kvalitní modpacky.  
GitHub: [kubiiksvk](https://github.com/KubiikSvK)

---

## 🔚 Závěr

Díky za to, že jsi to dočetl/a. A pokud tohle skutečně běží, dáváš si respekt – jsi legenda. ✨  
Stačí kliknout a hrát. Gl hf! (Good luck, have fun!)

