using System;
using System.Web.Services;
using WebSecureBookings.Controllers.IndexController;
using System.Collections.Generic;

namespace WebSecureBookings.Views.Index
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Guardar Agente Aduanal
        [WebMethod]
        public static List<string> LlenarComboLiga()
        {
            IndexController indexController = new IndexController();
            return indexController.TablaEquipos();
        }
        #endregion
    }
}