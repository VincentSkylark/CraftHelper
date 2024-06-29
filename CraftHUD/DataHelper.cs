using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;


namespace CraftHUD
{
    public static class DataHelper
    {
        public static CurrencyData LoadCoord()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "currency_coord.json");
            string jsonData = File.ReadAllText(filePath);
            var currencyData = JsonConvert.DeserializeObject<CurrencyData>(jsonData);
            return currencyData;
        }

        public static CraftRule[] LoadCraftRules()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "target_affix.json");
            string jsonData = File.ReadAllText(filePath);
            var rules = JsonConvert.DeserializeObject<CraftRule[]>(jsonData);
            return rules;
        }

        public static void FocusGameWindow()
        {
            Process poeProcess = Process.GetProcessesByName("PathOfExile").FirstOrDefault();
            if (poeProcess == null) return;
            User32Helper.SetForegroundWindow(poeProcess.MainWindowHandle);
        }

        public static string GetItemInfo()
        {
            User32Helper.SetCursorPos(334, 386);
            Thread.Sleep(100);
            SendKeys.SendWait("^(%c)");
            var itemInfo = Clipboard.GetText();
            Clipboard.Clear();
            return itemInfo;
        }

        public static Rarity GetItemRarity(string gearInfo)
        {
            string[] lines = gearInfo.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string rarity = lines[1];
            if (rarity.Contains("Normal")) return Rarity.Normal;
            if (rarity.Contains("Magic")) return Rarity.Magic;
            if (rarity.Contains("Rare")) return Rarity.Rare;
            return Rarity.Unique;
        }

        public static string[] GetAffixs(string gearInfo)
        {
            string separator = "--------";
            string[] segments = gearInfo.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < segments.Length; i++)
            {
                segments[i] = segments[i].Trim();
            }

            var affixSegment = segments.Where(segment => segment.Contains("Modifier") && !segment.Contains("Implicit")).First();
            return ParseAffixs(affixSegment);
        }

        private static string[] ParseAffixs(string affix)
        {
            string pattern = @"\{[^\}]+\}";

            string[] segments = Regex.Split(affix, pattern);

            string whiteSpacePattern = @"\s*\r\n\s*";

            for (int i = 0; i < segments.Length; i++)
            {
                segments[i] = segments[i].Trim();
                segments[i] = Regex.Replace(segments[i], whiteSpacePattern, "\r\n");
            }

            segments = Array.FindAll(segments, s => !string.IsNullOrEmpty(s));

            return segments;
        }


    }
}
