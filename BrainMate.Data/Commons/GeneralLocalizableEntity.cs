﻿using System.Globalization;
namespace BrainMate.Data.Commons;
public class GeneralLocalizableEntity
{
	public string Localize(string textAr, string textEn)
	{
		CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
		if (cultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
			return textAr;
		return textEn;
	}
}
