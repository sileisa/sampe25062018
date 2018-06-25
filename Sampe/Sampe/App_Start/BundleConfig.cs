using System.Web;
using System.Web.Optimization;

namespace Sampe
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


			bundles.Add(new ScriptBundle("~/bundles/globalize").Include(
						"~/Scripts/globalize.js",
						"~/Scripts/jquery.validate.globalize.js"));
			bundles.Add(new ScriptBundle("~/bundles/pt-br").Include(
						"~/Scripts/methods-pt.js",
						"~/Scripts/helper.js"));

			// Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
			// pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                     "~/Scripts/Sampe.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.css",
                      "~/Content/Site.css"));
        }
    }
}
