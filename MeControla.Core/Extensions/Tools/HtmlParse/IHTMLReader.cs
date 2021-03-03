using System.Collections.Generic;

namespace MeControla.Core.Tools.HtmlParse
{
    public interface IHTMLReader
    {
        HTMLElement Find(string html, string tag);
        HTMLElement Find(string html, string tag, HTMLAttributes attributes);
        IList<HTMLElement> FindAll(string html, string tag);
        IList<HTMLElement> FindAll(string html, string tag, HTMLAttributes attributes);
    }
}