﻿using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using WinHue3.ExtensionMethods;
using WinHue3.Philips_Hue.HueObjects.Common;
using WinHue3.Resources;
using IHueObject = WinHue3.Philips_Hue.HueObjects.Common.IHueObject;

namespace WinHue3.Interface
{
    /// <summary>
    /// Bridge object Type description.
    /// </summary>
    public class TypeGroupDescription : PropertyGroupDescription
    {
        /// <summary>
        /// Create the group name from Item.
        /// </summary>
        /// <param name="item">Selected Item.</param>
        /// <param name="level">Level of the Item (Not used)</param>
        /// <param name="culture">Desired culture.</param>
        /// <returns></returns>
        public override object GroupNameFromItem(object item, int level, CultureInfo culture)
        {
            if (item == null) return GUI.ListView_others;
            dynamic t = item;
            string type = t.huetype.ToString();
            return GUI.ResourceManager.GetString("ListView_" + type);

        }
    }
}
