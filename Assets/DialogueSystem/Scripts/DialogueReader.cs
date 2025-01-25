using CoreTeamGamesSDK.Localization;
using System.Collections.Generic;

namespace CoreTeamGamesSDK.DialogueSystem
{
    public class DialogueReader
    {
        #region Variables
        private Dictionary<string, string> _localizedDialogue;
        private Dictionary<string, string> _localizedNames;
        #endregion

        #region Properties
        public Dictionary<string, string> LocalizedDialogue => _localizedDialogue;
        public Dictionary<string, string> LocalizedNames => _localizedNames;
        #endregion

        #region Constructors
        public DialogueReader(string dialogueFileName, string namesFileName, LocalizationService localizationService)
        {
            GetDialogue(localizationService, dialogueFileName);
            GetNames(localizationService, namesFileName);
        }
        #endregion

        #region Getters
        private void GetDialogue(LocalizationService localizationService, string dialogueFileName)
        {
            _localizedDialogue = localizationService.CurrentLocalizator.GetLocalizedTextFile(dialogueFileName);
        }
        private void GetNames(LocalizationService localizationService, string namesFileName)
        {
            _localizedNames = localizationService.CurrentLocalizator.GetLocalizedTextFile(namesFileName);
        }
        #endregion
    }
}