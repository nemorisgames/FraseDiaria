using UnityEngine;
using System.Collections;

namespace I2.Loc
{
	public class AuthorLocalization : MonoBehaviour 
		{
			public void OnEnable()
			{
				// Register callback
				Localize loc = GetComponent<I2.Loc.Localize>();
				loc.LocalizeCallBack.Target = this;
				loc.LocalizeCallBack.MethodName = "OnModifyLocalization";
			}

			public void OnModifyLocalization()
			{
				string Language = "Author";
				string term, secondary;
				GetComponent<I2.Loc.Localize>().GetFinalTerms(out term, out secondary);

				var termData = LocalizationManager.GetTermData(term);
				int LanguageIndex = LocalizationManager.Sources[0].GetLanguageIndex(Language);

				if (LanguageIndex>=0)
					Localize.MainTranslation = termData.Languages[LanguageIndex];				
			}
		}
}