using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.Utils
{
    public interface ILocalisationService
    {
        string HtmlTemplate { get; }
        string HtmlRoot { get; }
    }

    public class LocalisationService : ILocalisationService
    {
        public string HtmlTemplate => "front/front-v3.2.1";
        public string HtmlRoot => "";
    }
}
