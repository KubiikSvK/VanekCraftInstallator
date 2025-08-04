# 🎮 VanekCraft Instalátor 0.1.0-rc1

Vítej u oficiálně-neoficiálního průvodce instalací megamodpacku VanekCraft!  
Máš tu projekt *open source*, vytvořený jako výukový featurkód ve Visual Studio 2022, s podporou AI magií (ChatGPT, Copilot od Microsoftu a Amazon Q & AWS).  
Nic z toho není legální náhrada licence Minecraftu, ale je to moje legální dílo. 😎

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

Minecraft, SKLauncher a jednotlivé mody nejsou moje – vše patří svým původním autorům, já jsem to jen složil jako LEGO. Copyright kódů instalátoru patří mně.
---

### ⚠️ Upozornění k textům v instalátoru

Texty a komentáře v instalátoru jsou psány s nadsázkou, sarkasmem a troškou rejpání.  
Nemají za cíl nikoho urazit, zesměšnit nebo znevážit – jde čistě o uvolněnější a zábavnější komunikaci, aby instalace nebyla suchá a nudná.  

Děkuji za pochopení a doufám, že tě to bude i bavit! 😄
---

## 🧠 Co dělá Instalátor

- Proleze ti komp a zkontroluje Java 17+ (na fotky tvý/ho ex nešahá).
- Stáhne launcher:
  - **Oficiální launcher** – licence potřebná, ale je to fér.
  - **SK Launcher** – žádné složité byrokracie, ideál pro rychlý start.
- Nainstaluje Minecraft verzi 1.21.3 a Fabric Loader 0.16.14.
- Vyčistí dočasný soubory.
- Vše automaticky připravené, abys klikal a hrál.

### 📦 Co se děje pod kapotou (transparentnost first, drama never)

- 🧰 **SKLauncher** je zatím stahován pouze z mého CDN pro rychlou a stabilní instalaci.  
  Podpora stahování přímo z oficiálního zdroje je v plánu a bude přidána brzy.
- 📁 **.minecraft složka** obsahuje předpřipravenou strukturu s verzí **1.21.3**, vygenerovanou samotným SKLauncherem.
- 📝 Během instalace upravujeme:
  - `accounts.json` – aby sis mohl(a) zvolit vlastní nick (nepoužíváme skutečné účty).
  - `launcher_profiles.json` – nastaví se profil s Fabric Loaderem pro snadné spuštění.
- ✨ **Fabric Installer** je stažen z CDN a spuštěn bez keců (a klikání), v tichém režimu.
- 📦 **Modpack** je zabalen jako jeden `.zip` archiv bez úprav jednotlivých modů – prostě tak, jak je autoři vytvořili.

> 💡 Všechno, co se děje, je navržené s respektem k právům vývojářů. Nic nevydáváme za vlastní a nikoho nešidíme. Jen to skládáme dohromady jako LEGO pro pohodlnější start.

---

### 📦 Modpack obsahuje (aktuální k 31.07.2025)

| Mod (verze) | Popis | Licence a odkazy |
|-------------|-------|------------------|
| architectury-14.0.4-fabric.jar | Abstraktní API pro více‑loader modů | [Modrinth (MIT License)](https://modrinth.com/mod/architectury) |
| balm-fabric-1.21.3-21.3.5.jar | Usnadňuje vývoj napříč mod-loadery | [Modrinth (MIT License)](https://modrinth.com/mod/balm) |
| cloth-config-16.0.143-fabric.jar | Konfigurační API pro mody | [Modrinth (MIT License)](https://modrinth.com/mod/cloth-config) |
| fabric-api-0.114.1+1.21.3.jar | Základní API pro Fabric modding | [Modrinth (MIT License)](https://modrinth.com/mod/fabric-api) |
| fabric-language-kotlin-1.13.4+kotlin.2.2.0.jar | Kotlin rozhraní pro Fabric | [Modrinth (MIT License)](https://modrinth.com/mod/fabric-language-kotlin) |
| ferritecore-7.1.0-hotfix-fabric.jar | Optimalizace paměti | [Modrinth (MIT License)](https://modrinth.com/mod/ferrite-core) |
| ForgeConfigAPIPort-v21.3.4-1.21.3-Fabric.jar | Port Forge konfiguračního API | [Modrinth (MIT License)](https://modrinth.com/mod/forge-config-api-port) |
| fpsdisplay-4.2.3+1.21.3.jar | Zobrazení FPS a debug info | [GitHub (MIT License)](https://github.com/lambdAurora/FPS-Display) |
| inventoryessentials-fabric-1.21.3-21.3.2.jar | Uživatelské UI v inventáři | [Modrinth (MIT License)](https://modrinth.com/mod/inventory-essentials) |
| libIPN-fabric-1.21.3-6.4.0.jar | Síťovky a IP propojení | [Modrinth (MIT License)](https://modrinth.com/mod/libipn) |
| lithium-fabric-0.14.6+mc1.21.3.jar | Herní logika optimalizace | [Modrinth (MIT License)](https://modrinth.com/mod/lithium) |
| modmenu-12.0.0.jar | Zobrazení modů v launcheru | [Modrinth (MIT License)](https://modrinth.com/mod/modmenu) |
| open-parties-and-claims-fabric-1.21.3-0.24.0.jar | Ochrany a party systémy | [Modrinth (MIT License)](https://modrinth.com/mod/open-parties-and-claims) |
| RoughlyEnoughItems-17.0.807-fabric.jar | GUI pro předmětový cheat/help list | [Modrinth (MIT License)](https://modrinth.com/mod/rei) |
| sodium-extra-fabric-0.6.0+mc1.21.3.jar | DLC pro Sodium performance mod | [Modrinth (MIT License)](https://modrinth.com/mod/sodium-extra) |
| sodium-fabric-0.6.13+mc1.21.3.jar | Extrémní grafické optimalizace | [Modrinth (MIT License)](https://modrinth.com/mod/sodium) |
| tree-chopping-v4.1.4.jar | Napůlpá zářez pro stromování | [CurseForge (MIT License)](https://www.curseforge.com/minecraft/mc-mods/tree-chopping) |
| Xaeros_Minimap_25.2.10_Fabric_1.21.3.jar | Minimapka s waypointy | [Modrinth (Proprietary)](https://modrinth.com/mod/xaeros-minimap) |
| XaerosWorldMap_1.39.12_Fabric_1.21.3.jar | Celosvětová mapa pro snadnou orientaci | [Modrinth (Proprietary)](https://modrinth.com/mod/xaeros-world-map) |
| yet_another_config_lib_v3-3.7.1+1.21.3-fabric.jar | Další konfigurační knihovna | [Modrinth (MIT License)](https://modrinth.com/mod/yacl) |

📝 *Poznámka: Modpack a jeho mody nejsou moje dílo. Všechna autorská práva patří jejich vývojářům. Instalátor je jen chytrý pomocník, který to všechno pohodlně hodí do hry.*


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
- **Fabric a módy** jsou vlastnictvím jejich autorů a instalátor je jen distribuuje ve spoustě pro lepší uživatelský zážitek.  
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

### 📚 Použité zkratky a jejich významy

- **SKL** – SK Launcher (neoficiální Minecraft launcher)  
- **MC** – Minecraft  
- **MSA** – Microsoft Account (přihlašovací systém od Microsoftu)  
- **VS2022** – Visual Studio 2022 (vývojové prostředí)  
- **AI** – Artificial Intelligence (umělá inteligence využitá v projektu)  
  - **ChatGPT** – jazykový model od OpenAI využitý při vývoji  
  - **GitHub Copilot** – AI asistent od Microsoftu  
  - **AWS** – Amazon Web Services (cloudové služby Amazonu)  
- **GitHub** – platforma pro správu verzí a spolupráci na kódu  
- **CDN** – Content Delivery Network (síť serverů pro rychlé doručování souborů)  
- **RC** – Release Candidate (předběžná verze připravená k vydání)  
- **SDK** – Software Development Kit (soubor nástrojů pro vývoj software)  
- **API** – Application Programming Interface (rozhraní pro komunikaci mezi programy)  
- **CLI** – Command Line Interface (programy ovládané příkazovým řádkem)  

---

## 🚀 Autor

**Jakub Vanek (KubiikSvK)**  
Zkušenosti: pixelový chaos, vtipné instalátory, rádoby kvalitní modpacky.  
GitHub: [kubiiksvk](https://github.com/KubiikSvK)

---

## 🔚 Závěr

Díky za to, že jsi to dočetl/a. A pokud tohle skutečně dočetl až sem, máš můj respekt – jsi legenda. ✨  
Stačí kliknout a hrát. Gl hf! (Good luck, have fun!)

