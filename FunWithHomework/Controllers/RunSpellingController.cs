using System;
using System.Collections.Generic;
using Toolbelt.Blazor.SpeechSynthesis;

namespace FunWithHomework.Controllers
{
    public class RunSpellingController
    {
        private SpeechSynthesis _speechSynthesis;
        private bool _displayHint;
        private string _language;
        private string _anwser;

        public string VerificationMessage { get; protected set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string LastAnswer { get; protected set; } = string.Empty;
        public string Hint { get; protected set; } = string.Empty;
        public string CurrentWord { get; protected set; } = string.Empty;
        protected List<string> Words { get; set; }
        public string Language
        {
            get => _language;
            set
            {
                if (value.Equals("fr", StringComparison.InvariantCultureIgnoreCase))
                    _language = "fr-CA";
                else if (value.Equals("en", StringComparison.InvariantCultureIgnoreCase))
                    _language = "en-CA";
                else
                    _language = value;
            }
        }

        public RunSpellingController(SpeechSynthesis speechSynthesis)
        {
            _speechSynthesis = speechSynthesis;
        }

        public void Verify()
        {
            if(Answer.Equals(CurrentWord, StringComparison.InvariantCulture))
            {
                VerificationMessage = "Bonne réponse!";
                LastAnswer = CurrentWord;
                
                if(Words.Count > 0)
                    Words.RemoveAt(0);
                
                SetNextWord();
            }
            else
            {
                VerificationMessage = "Mauvaise réponse.";
                UpdateHint();
                LastAnswer = string.Empty;
            }
        }

        public void ToggleHint()
        {
            _displayHint = !_displayHint;
            UpdateHint();

        }

        public void UpdateHint()
        {
            if (_displayHint)
            {
                Hint = GetHint();
            }
            else
            {
                Hint = string.Empty;
            }
        }

        public void SetWords(List<string> words)
        {
            Words = words;
            Answer = string.Empty;
            LastAnswer = string.Empty;
            Hint = string.Empty;
            VerificationMessage = string.Empty;

            SetNextWord();
        }

        private void SetNextWord()
        {
            Answer = string.Empty;

            if (Words != null && Words.Count > 0)
            {
                CurrentWord = Words[0];
                UpdateHint();
                Speak();
            }
            else
            {
                VerificationMessage = "Bonne réponse! La dictée est complétée.";
            }
        }

        private string GetHint()
        {
            var hint = string.Empty;
            for (int index = 0; index < CurrentWord.Length; index++)
            {
                if (Answer.Length > index && Answer[index] == CurrentWord[index])
                {
                    hint += Answer[index];                    
                }
                else
                {
                    if (CurrentWord[index] == ' ')
                        hint += " ";
                    else
                        hint += "_";
                }
            }
            return hint;
        }

        public void Speak()
        {
            if (string.IsNullOrEmpty(CurrentWord))
                return;

            var utterance = new SpeechSynthesisUtterance
            {
                Text = CurrentWord,
                Lang = Language, // BCP 47 language tag
                Pitch = 1.0, // 0.0 ~ 2.0 (Default 1.0)
                Rate = 0.9, // 0.1 ~ 10.0 (Default 1.0)
                Volume = 1.0
            };
         
            _speechSynthesis.Speak(utterance);
        }

     
    }

}
