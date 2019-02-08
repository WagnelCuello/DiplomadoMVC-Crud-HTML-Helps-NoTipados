using System.Web;
using System.Web.Mvc;

namespace DiplomadoMVC_Crud_HTML_Helps_NoTipados
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
