using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CraftHUD
{
    public static class CraftHelper
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;

        private static Random random = new Random();

        public static void Craft(CraftMethod method, CurrencyData currencyData)
        {
            int itemX = 334;
            int itemY = 386;

            int orbX = 0;
            int orbY = 0;

            switch (method)
            {
                case CraftMethod.Trans:
                    orbX = currencyData.Transmutation.X;
                    orbY = currencyData.Transmutation.Y;
                    break;
                case CraftMethod.Aug:
                    orbX = currencyData.Augmentation.X;
                    orbY = currencyData.Augmentation.Y;
                    break;
                case CraftMethod.Alt:
                    orbX = currencyData.Alteration.X;
                    orbY = currencyData.Alteration.Y;
                    break;
                case CraftMethod.Regal:
                    orbX = currencyData.Regal.X;
                    orbY = currencyData.Regal.Y;
                    break;
                case CraftMethod.Scour:
                    orbX = currencyData.Scouring.X;
                    orbY = currencyData.Scouring.Y;
                    break;
                case CraftMethod.Fusing:
                    orbX = currencyData.Fusing.X;
                    orbY = currencyData.Fusing.Y;
                    break;
            }

            int delay = random.Next(120, 150);

            User32Helper.SetCursorPos(orbX, orbY);
            Thread.Sleep(delay);
            User32Helper.mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            Thread.Sleep(delay);
            User32Helper.mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            Thread.Sleep(delay);
            User32Helper.SetCursorPos(itemX, itemY);
            Thread.Sleep(delay);
            User32Helper.mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(delay);
            User32Helper.mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            Thread.Sleep(delay);
        }

        public static void SixLink(CurrencyData currencyData, CancellationToken token)
        {
            // Key codes for left shift key
            const byte VK_LSHIFT = 0xA0;
            const uint KEYEVENTF_KEYDOWN = 0x0000;
            const uint KEYEVENTF_KEYUP = 0x0002;

            int itemX = 334;
            int itemY = 386;

            int orbX = currencyData.Fusing.X;
            int orbY = currencyData.Fusing.Y;

            int delay = random.Next(50, 90);
            User32Helper.SetCursorPos(orbX, orbY);
            Thread.Sleep(delay);
            User32Helper.mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            Thread.Sleep(delay);
            User32Helper.mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            Thread.Sleep(delay);
            User32Helper.keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            Thread.Sleep(delay);
            User32Helper.SetCursorPos(itemX, itemY);
            Thread.Sleep(delay);
            for (int j = 0; j < 100; j++)
            {
                if (token.IsCancellationRequested)
                {
                    User32Helper.keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                    break;
                }
                delay = random.Next(50, 90);
                User32Helper.mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                Thread.Sleep(delay);
                User32Helper.mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                Thread.Sleep(delay);
            }

            User32Helper.keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            return;
        }

        public static void CraftFlask(CurrencyData currencyData, CancellationToken token)
        {
            CraftRule[] rules = FlaskAffixs.Rules;
            int matchingAffix = 0;

            while (matchingAffix < 2)
            {
                if(token.IsCancellationRequested) break;

                var itemInfo = DataHelper.GetItemInfo();
                var affixs = DataHelper.GetAffixs(itemInfo);
                matchingAffix = affixs.Count(item => rules.Any(rule => CheckCraftRule(item, rule)));

                switch (matchingAffix)
                {
                    case 0:
                        Craft(CraftMethod.Alt, currencyData);
                        break;
                    case 1:
                        Craft(affixs.Length == 1 ? CraftMethod.Aug : CraftMethod.Alt, currencyData);
                        break;
                    default:
                        break;
                }
            }
            return;
        }

        public static void AltSpamCraft(CurrencyData currencyData, CancellationToken token)
        {
            CraftRule[] rules = DataHelper.LoadCraftRules();

            bool hasTargetAffix = false;

            while (true)
            {
                if(token.IsCancellationRequested) break;
                var itemInfo = DataHelper.GetItemInfo();
                var affixs = DataHelper.GetAffixs(itemInfo);
                hasTargetAffix = affixs.Any(item => rules.Any(rule => CheckCraftRule(item, rule)));

                if (hasTargetAffix) break;
                if (affixs.Length == 1)
                {
                    Craft(CraftMethod.Aug, currencyData);
                }
                else
                {
                    Craft(CraftMethod.Alt, currencyData);
                }
            }
            return;
        }

        private static bool CheckCraftRule(string affix, CraftRule rule)
        {
            Match patternMatch = Regex.Match(affix, rule.Pattern);
            if (!patternMatch.Success) return false;

            if (int.TryParse(patternMatch.Groups[1].Value, out int value))
            {
                return rule.IsMinValue ? (value >= rule.Threashold) : (value <= rule.Threashold);
            }
            return false;
        }
    }

    public enum CraftMethod
    {
        Trans, Aug, Alt, Regal, Scour, Fusing
    }

    public enum Rarity
    {
        Normal, Magic, Rare, Unique
    }

    public class CraftRule
    {
        public string Pattern { get; set; }
        public int Threashold { get; set; }
        public bool IsMinValue { get; set; }

        public CraftRule(string pattern, int threashold, bool isMinValue = true)
        {
            this.Pattern = pattern;
            this.Threashold = threashold;
            this.IsMinValue = isMinValue;
        }
    }
}
