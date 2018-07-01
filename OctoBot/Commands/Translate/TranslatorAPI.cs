using System.Collections.Generic;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace OctoBot.Commands.Translate
{
    class TranslatorApi
    {
        private const string ApiKey = "trnsl.1.1.20180618T205918Z.a763a99319f8542b.8fbf7a0020684168484aa3c4b66182def0f0d142";
        private const string Endpoint = "https://translate.yandex.net/api/v1.5/tr.json/";

        public static readonly Dictionary<string, string> Dictionary = new Dictionary<string, string>
            {
                {"af", "Afrikaans"},
                {"am", "Amharic"},
                {"ar", "Arabic"},
                {"az", "Azerbaijani"},
                {"ba", "Bashkir"},
                {"be", "Belarusian"},
                {"bg", "Bulgarian"},
                {"bn", "Bengali"},
                {"bs", "Bosnian"},
                {"ca", "Catalan"},
                {"ceb", "Cebuano"},
                {"cs", "Czech"},
                {"cy", "Welsh"},
                {"da", "Danish"},
                {"de", "German"},
                {"el", "Greek"},
                {"en", "English"},
                {"eo", "Esperanto"},
                {"es", "Spanish"},
                {"et", "Estonian"},
                {"eu", "Basque"},
                {"fa", "Persian"},
                {"fi", "Finnish"},
                {"fr", "French"},
                {"ga", "Irish"},
                {"gd", "Scottish Gaelic"},
                {"gl", "Galician"},
                {"gu", "Gujarati"},
                {"he", "Hebrew"},
                {"hi", "Hindi"},
                {"hr", "Croatian"},
                {"ht", "Haitian"},
                {"hu", "Hungarian"},
                {"hy", "Armenian"},
                {"id", "Indonesian"},
                {"is", "Icelandic"},
                {"it", "Italian"},
                {"ja", "Japanese"},
                {"jv", "Javanese"},
                {"ka", "Georgian"},
                {"kk", "Kazakh"},
                {"km", "Khmer"},
                {"kn", "Kannada"},
                {"ko", "Korean"},
                {"ky", "Kyrgyz"},
                {"la", "Latin"},
                {"lb", "Luxembourgish"},
                {"lo", "Lao"},
                {"lt", "Lithuanian"},
                {"lv", "Latvian"},
                {"mg", "Malagasy"},
                {"mhr", "Mari"},
                {"mi", "Maori"},
                {"mk", "Macedonian"},
                {"ml", "Malayalam"},
                {"mn", "Mongolian"},
                {"mr", "Marathi"},
                {"mrj", "Hill Mari"},
                {"ms", "Malay"},
                {"mt", "Maltese"},
                {"my", "Burmese"},
                {"ne", "Nepali"},
                {"nl", "Dutch"},
                {"no", "Norwegian"},
                {"pa", "Punjabi"},
                {"pap", "Papiamento"},
                {"pl", "Polish"},
                {"pt", "Portuguese"},
                {"ro", "Romanian"},
                {"ru", "Russian"},
                {"si", "Sinhalese"},
                {"sk", "Slovak"},
                {"sl", "Slovenian"},
                {"sq", "Albanian"},
                {"sr", "Serbian"},
                {"su", "Sundanese"},
                {"sv", "Swedish"},
                {"sw", "Swahili"},
                {"ta", "Tamil"},
                {"te", "Telugu"},
                {"tg", "Tajik"},
                {"th", "Thai"},
                {"tl", "Tagalog"},
                {"tr", "Turkish"},
                {"tt", "Tatar"},
                {"udm", "Udmurt"},
                {"uk", "Ukrainian"},
                {"ur", "Urdu"},
                {"uz", "Uzbek"},
                {"vi", "Vietnamese"},
                {"xh", "Xhosa"},
                {"yi", "Yiddish"},
                {"zh", "Chinese"}
            };

        public static string[] Translate(string toLang , string query)
        {
            var searchUrl = $"{Endpoint}translate?key={ApiKey}&lang={toLang}&text={query}";

            string json;
            using (var client = new WebClient())
            {
                client.Encoding=Encoding.UTF8;
                json = client.DownloadString(searchUrl);
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

            if (dataObject.code.ToString() != "200")
            {
                return new[] {$"Failed to translate. Error code {dataObject.code}"};
            }

            var translatedFrom = Dictionary[(string)dataObject.lang.ToString().Split('-')[0]];
            var translatedTo = Dictionary[(string)dataObject.lang.ToString().Split('-')[1]];


            string translatedText = dataObject.text[0].ToString();

            return new[] {translatedFrom, translatedTo, translatedText};

        }

        public static string DetectLanguageCode(string query)
        {
            var searchUrl = $"{Endpoint}detect?key={ApiKey}&text={query}";

            string json;
            using (var client = new WebClient())
            {
                client.Encoding=Encoding.UTF8;
                json = client.DownloadString(searchUrl);
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);
            return dataObject.code.ToString() != "200" ? null : (string) dataObject.lang;
        }

        public static string DetectLanguageName(string query)
        {
            var shortForm = DetectLanguageCode(query);
            
            if (string.IsNullOrEmpty(shortForm))
            {
                return "Not Found";
            }
            
            return Dictionary[shortForm];
        }

    }
}
