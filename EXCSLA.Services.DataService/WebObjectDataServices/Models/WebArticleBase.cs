using System;

namespace EXCSLA.Models
{
    public abstract class WebArticleBase : WebObjectBase, IWebArticle
    {
        #region Variables
        private string _article;

        #endregion

        #region Properties
        public string Article {get{return _article;} set{_article = value;}}

        #endregion

        #region Constructors
        public WebArticleBase() {}
        #endregion

        #region Methods
        public string GetDescription(int length)
        {
            string desc = _article.Substring(0, length);
            int lastSpaceLoc = desc.LastIndexOf(" ");

            return desc.Substring(0, lastSpaceLoc) + " ...";
        }
        #endregion
    }
}