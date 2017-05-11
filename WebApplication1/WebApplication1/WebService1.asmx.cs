using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Net.Mail;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Data;
using System.Text.RegularExpressions;
// testing 


namespace WebApplication1
{
    [Serializable]
    public class mensaje
    {
        public string cuerpo;
        public string mail;
        public string de;
        public string para;
        public string asunto;
        public string reply;
        public string status;
    }

    [Serializable]
    public class reg
    {
        public string site;
        public string batch;
        public string name;
        public string phone;
        public string email;
        public string RFC;
        public string Activation;
        public string tipo;
        public string status;
    }

    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]


    
    
    public class WebService1 : System.Web.Services.WebService
    {
        string Empresa = "1Qa2Ws3Ed4Rf";

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string mHistory(string cadena)
        {
            SqlConnection micon = new SqlConnection();

            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            micon.ConnectionString = conn;
            micon.Open();
            SqlCommand com = new SqlCommand();
            com.CommandText = "select * from verificar2";
            com.Connection = micon;


            DataSet ds = new DataSet();
            SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
            SqlCommand mySqlCommand = new SqlCommand();
            mySqlCommand.Connection = micon;
            mySqlDataAdapter.SelectCommand = com;
            mySqlDataAdapter.Fill(ds);



            JavaScriptSerializer serializador = new JavaScriptSerializer();
            serializador.MaxJsonLength = Int32.MaxValue;

            return "";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string Grabar(string cadena)
        {
            JavaScriptSerializer serializador = new JavaScriptSerializer();
            string lregresa = "";
            serializador.MaxJsonLength = Int32.MaxValue;
            reg regs = serializador.Deserialize<reg>(cadena);

            SqlConnection micon = new SqlConnection();

            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            micon.ConnectionString = conn;
            micon.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = micon;
            string tipo = "1";
            if (regs.tipo == "Distribuidor")
                tipo = "0";


            com.CommandText = "insert into verificar2 values ('" + regs.Activation + "','" + regs.batch + "','" + regs.name + "','" + tipo + "','" + regs.phone + "','" + regs.email + "','" + regs.RFC + "',getdate(), dateadd(day,365, getdate()),'0')";
            int x = 0;
            try
            {
                x = com.ExecuteNonQuery();
            }
            catch (Exception iiii)
            {
                x = 0;
            }
            if (x == 0)
            {
                lregresa = "Codigo de sitio ya existe";
                string respuesta = mEnviaCorreo(lregresa + " " + regs.Activation);
            }
            else
            {

                lregresa = "Registro Completo, es necesario esperar la activacion";
                string respuesta = mEnviaCorreo(lregresa + " " + regs.Activation);
            }
            micon.Close();
            return lregresa;
        }

        private string mEnviaCorreo(string cuerpo)
        {
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            string lhost = "";
            lhost = "mail.desarrollosoftwarecontable.com";
            //lhost = "smtp3.hp.com";

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential("administrador@desarrollosoftwarecontable.com", Empresa);


            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            /*
            cliente.Port = 587;
            cliente.EnableSsl = true;
            */
            
            cliente.Port = int.Parse("25");
            //cliente.EnableSsl = false; //gmail

            cliente.Host = lhost; //Para Gmail "smtp.gmail.com";


            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                    MailMessage message = new MailMessage("administrador@desarrollosoftwarecontable.com", "hectorvalagui@hotmail.com,hectorvalagui@gmail.com", "Activacion", cuerpo);
                    message.IsBodyHtml = true;
                    //if (reply != "")
                    //{
                    //    message.ReplyTo = new MailAddress(reply);
                    //}
                    //smtp.Send(message);
                    cliente.Send(message);
                return "OK";
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return ex.Message; //Aquí gestionamos los errores al intentar enviar el correo
            }
        }

       /* private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }*/

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string GrabarEmails(string listacorreos2, string sitio, string Empresa)
        {
            JavaScriptSerializer serializador = new JavaScriptSerializer();

            serializador.MaxJsonLength = Int32.MaxValue;
            List<mensaje> listacorreos = serializador.Deserialize<List<mensaje>>(listacorreos2);

            SqlConnection micon = new SqlConnection();

                string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                micon.ConnectionString = conn;
                micon.Open();
            foreach (mensaje mailx in listacorreos)
            {
                SqlCommand com = new SqlCommand();
                com.Connection = micon;

                string sub = "";

                if (mailx.status.Length >= 100)
                    sub = mailx.status.Substring(0, 99);
                else
                    sub = mailx.status;
                 com.CommandText = "insert into historial1 values ('" + sitio + "','" + Empresa + "','" + mailx.de + "','" + mailx.para + "','" + sub + "',GETDATE())";
                int x = com.ExecuteNonQuery();
                

            }
            micon.Close();
            return "";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public string ConXML(string XmlDoc, string correoreply, string correoprueba, string sitio)
        {

            JavaScriptSerializer serializador = new JavaScriptSerializer();

            serializador.MaxJsonLength = Int32.MaxValue;
            /*List<MailMessage> listamails = new List<MailMessage>();
            MailMessage mail = new MailMessage();*/
            List<mensaje> listacorreos = new List<mensaje>();
            

            string messageBody = "";
            XmlDocument xmld = new XmlDocument();
            string mailSubject = "Estado de cuenta";

            xmld.LoadXml(XmlDoc);

            XmlNodeList nodes = xmld.GetElementsByTagName("Cliente");
            

            string from = "administrador@desarrollosoftwarecontable.com";
            string subject = mailSubject;


            //SmtpClient client = new SmtpClient("smtp3.hp.com");

            //SmtpClient client = new SmtpClient("mail.desarrollosoftwarecontable.com", 25);
            //client.Credentials = new System.Net.NetworkCredential("administrador@desarrollosoftwarecontable.com", "1qa2ws3ed4rF");

            string Id = "";
            int inicio = 0;
            foreach (XmlNode node in nodes)
            {

                




                XmlDocument doc = new XmlDocument();
                XmlElement clienteElement = (XmlElement)node;
                XmlElement ecliente = doc.CreateElement("Cliente");


                // VALIDAR si esta activo solo la primer vez
                if (inicio == 0)
                {
                    SqlConnection micon = new SqlConnection();

                    string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    micon.ConnectionString = conn;
                    micon.Open();

                    //XmlElement eRFC = doc.CreateElement("RFC");
                    //string RFC = eRFC.GetElementsByTagName("RFC")[0].InnerText;

                    //XmlElement eRFC = doc.CreateElement("RFC");
                    string RFC = clienteElement.GetElementsByTagName("RFC")[0].InnerText;

                    SqlCommand com = new SqlCommand();
                    com.Connection = micon;
                    com.CommandText = "select * from verificar1 where ltrim(RFC) ='" + RFC.Trim() + "'";

                    com.CommandText = "select FechaVigencia, activo from verificar2 where ltrim(CodigoSitio) ='" + sitio.Trim() + "'";

                    SqlDataReader reader = com.ExecuteReader();

                    reader.Read();
                    mensaje maily = new mensaje();
                    if (reader.HasRows)
                    {
                        
                        string activo = reader[1].ToString();
                        if (activo != "1")
                        {
                            micon.Close();
                            maily.asunto = "No activo";
                            listacorreos.Add(maily);
                            return serializador.Serialize(listacorreos);
                        }
                        else 
                        {
                            DateTime x = DateTime.Today;
                            DateTime y = DateTime.Parse(reader[0].ToString());
                            if ( x > y)
                            {
                                maily.asunto = "No vigente";
                                listacorreos.Add(maily);
                                micon.Close();
                                return serializador.Serialize(listacorreos);
                            }

                        }
                    }
                    else
                    {
                        micon.Close();
                        maily.asunto = "No registrado";
                        listacorreos.Add(maily);
                        return serializador.Serialize(listacorreos);
                    }

                    micon.Close();
                    inicio++;
                }
                // "/journal[@id=2]/"

                Id = clienteElement.GetElementsByTagName("Id")[0].InnerText;



                
                XmlElement eid = doc.CreateElement("Id");
                eid.InnerText = Id;
                XmlElement erazonsocial = doc.CreateElement("RazonSocial");
                erazonsocial.InnerText = clienteElement.GetElementsByTagName("RazonSocial")[0].InnerText;

                XmlElement erfccliente = doc.CreateElement("RFCCliente");
                erfccliente.InnerText = clienteElement.GetElementsByTagName("RFCCliente")[0].InnerText;

                XmlElement eTelefonoCliente = doc.CreateElement("TelefonoCliente");
                eTelefonoCliente.InnerText = clienteElement.GetElementsByTagName("TelefonoCliente")[0].InnerText;

                XmlElement eCalleCliente = doc.CreateElement("CalleCliente");
                

                
                XmlElement eNumeroCliente = doc.CreateElement("NumeroCliente");
                eNumeroCliente.InnerText = clienteElement.GetElementsByTagName("NumeroCliente")[0].InnerText;

                eCalleCliente.InnerText = clienteElement.GetElementsByTagName("CalleCliente")[0].InnerText + " " + clienteElement.GetElementsByTagName("NumeroCliente")[0].InnerText;


                XmlElement eColoniaCliente = doc.CreateElement("ColoniaCliente");
                eColoniaCliente.InnerText = clienteElement.GetElementsByTagName("ColoniaCliente")[0].InnerText;

                XmlElement eEstadoCliente = doc.CreateElement("EstadoCliente");
                eEstadoCliente.InnerText = clienteElement.GetElementsByTagName("EstadoCliente")[0].InnerText;

                XmlElement eBanco = doc.CreateElement("Banco");
                eBanco.InnerText = clienteElement.GetElementsByTagName("Banco")[0].InnerText;
                XmlElement eCuenta = doc.CreateElement("Cuenta");
                eCuenta.InnerText = clienteElement.GetElementsByTagName("Cuenta")[0].InnerText;
                XmlElement eCLABE = doc.CreateElement("CLABE");
                eCLABE.InnerText = clienteElement.GetElementsByTagName("CLABE")[0].InnerText;
                XmlElement ecorreoconfirmacion = doc.CreateElement("correoconfirmacion");
                ecorreoconfirmacion.InnerText = clienteElement.GetElementsByTagName("correoconfirmacion")[0].InnerText;

                XmlElement eRFCBanco = doc.CreateElement("RFCBanco");
                eRFCBanco.InnerText = clienteElement.GetElementsByTagName("RFCBanco")[0].InnerText;

                XmlElement eRazonSocialBanco = doc.CreateElement("RazonSocialBanco");
                eRazonSocialBanco.InnerText = clienteElement.GetElementsByTagName("RazonSocialBanco")[0].InnerText;

                /*
                new XElement("RFCCliente", saldo.rfccliente),
                new XElement("TelefonoCliente", saldo.telefonocliente),
                new XElement("CalleCliente", saldo.callecliente),
                new XElement("NumeroCliente", saldo.numerocliente),
                new XElement("ColoniaCliente", saldo.coloniacliente),
                new XElement("TelefonoCliente", saldo.telefonocliente),
                new XElement("EstadoCliente", saldo.estadocliente));
                 */
                 

                XmlElement eEmail = doc.CreateElement("Email");
                eEmail.InnerText = clienteElement.GetElementsByTagName("Email")[0].InnerText;

                string email = clienteElement.GetElementsByTagName("Email")[0].InnerText;


                XmlElement eEmail1 = doc.CreateElement("email1");
                eEmail1.InnerText = clienteElement.GetElementsByTagName("email1")[0].InnerText;
                string email1 = clienteElement.GetElementsByTagName("email1")[0].InnerText;

                XmlElement eEmail2 = doc.CreateElement("email2");
                eEmail2.InnerText = clienteElement.GetElementsByTagName("email2")[0].InnerText;
                string email2 = clienteElement.GetElementsByTagName("email2")[0].InnerText;

                XmlElement eEmail3 = doc.CreateElement("Email3");
                eEmail3.InnerText = clienteElement.GetElementsByTagName("email3")[0].InnerText;
                string email3 = clienteElement.GetElementsByTagName("email3")[0].InnerText;

                XmlElement etotal = doc.CreateElement("Total");
                etotal.InnerText = clienteElement.GetElementsByTagName("Total")[0].InnerText; ;
                XmlElement evencido = doc.CreateElement("Vencido");
                evencido.InnerText = clienteElement.GetElementsByTagName("Vencido")[0].InnerText;

                XmlElement ePorVencer = doc.CreateElement("PorVencer");
                ePorVencer.InnerText = clienteElement.GetElementsByTagName("PorVencer")[0].InnerText; 

                XmlElement eNombreEmpresa = doc.CreateElement("NombreEmpresa");
                eNombreEmpresa.InnerText = clienteElement.GetElementsByTagName("NombreEmpresa")[0].InnerText;
                


                XmlElement eTelefonoEmpresa = doc.CreateElement("TelefonoEmpresa");
                eTelefonoEmpresa.InnerText = clienteElement.GetElementsByTagName("TelefonoEmpresa")[0].InnerText;

                XmlElement eDireccionEmpresa = doc.CreateElement("DireccionEmpresa");
                eDireccionEmpresa.InnerText = clienteElement.GetElementsByTagName("DireccionEmpresa")[0].InnerText;


                XmlElement eFechaHoy = doc.CreateElement("FechaHoy");

                CultureInfo ci = new CultureInfo("es-MX");
                CultureInfo mexicanSpanishCi = CultureInfo.GetCultureInfo("es-MX");
                Thread.CurrentThread.CurrentCulture = mexicanSpanishCi;
                Thread.CurrentThread.CurrentUICulture = mexicanSpanishCi;
                //Console.WriteLine("{0}/{1}", Thread.CurrentThread.CurrentCulture, Thread.CurrentThread.CurrentUICulture);
                //Console.WriteLine("{0:C}, {1}", 1250.50m, DateTime.Now.ToLongDateString());


                string lfecha = DateTime.Today.ToShortDateString();

                eFechaHoy.InnerText = DateTime.Today.ToShortDateString(); 


                ecliente.AppendChild(eid);
                ecliente.AppendChild(erazonsocial);
                ecliente.AppendChild(eEmail);
                ecliente.AppendChild(etotal);
                ecliente.AppendChild(evencido);
                ecliente.AppendChild(eNombreEmpresa);
                ecliente.AppendChild(eTelefonoEmpresa);
                ecliente.AppendChild(eDireccionEmpresa);
                ecliente.AppendChild(erfccliente);
                ecliente.AppendChild(eTelefonoCliente);
                ecliente.AppendChild(eCalleCliente);
                //ecliente.AppendChild(eNumeroCliente);
                ecliente.AppendChild(eColoniaCliente);
                ecliente.AppendChild(eEstadoCliente);
                ecliente.AppendChild(eFechaHoy);
                ecliente.AppendChild(ePorVencer);

                ecliente.AppendChild(eBanco);
                ecliente.AppendChild(eCuenta);
                ecliente.AppendChild(eCLABE);
                ecliente.AppendChild(ecorreoconfirmacion);
                ecliente.AppendChild(eRFCBanco);
                ecliente.AppendChild(eRazonSocialBanco);
                doc.AppendChild(ecliente);

                XmlElement edocumentos = doc.CreateElement("Documentos");


                XmlNodeList doctos = node.SelectNodes("//Documento[IdCliente=" + Id.ToString() + "]");
                foreach (XmlNode nodedoc in doctos)
                {

                    XmlElement edocumento = doc.CreateElement("Documento");
                    XmlElement doctoElement = (XmlElement)nodedoc;
                    XmlElement eAgente = doc.CreateElement("Agente");
                    eAgente.InnerText = doctoElement.GetElementsByTagName("Agente")[0].InnerText;
                    XmlElement eFecha = doc.CreateElement("Fecha");
                    eFecha.InnerText = doctoElement.GetElementsByTagName("Fecha")[0].InnerText;
                    XmlElement eVencimiento = doc.CreateElement("Vencimiento");
                    eVencimiento.InnerText = doctoElement.GetElementsByTagName("Vencimiento")[0].InnerText;
                    XmlElement eConcepto = doc.CreateElement("Concepto");
                    eConcepto.InnerText = doctoElement.GetElementsByTagName("Concepto")[0].InnerText;
                    XmlElement ePendiente = doc.CreateElement("Pendiente");
                    ePendiente.InnerText = doctoElement.GetElementsByTagName("Pendiente")[0].InnerText;

                    XmlElement eTotal = doc.CreateElement("Total");
                    eTotal.InnerText = doctoElement.GetElementsByTagName("Total")[0].InnerText;

                    XmlElement eProducto = doc.CreateElement("Producto");
                    eProducto.InnerText = doctoElement.GetElementsByTagName("Producto")[0].InnerText;

                    XmlElement eSerie = doc.CreateElement("Serie");
                    eSerie.InnerText = doctoElement.GetElementsByTagName("Serie")[0].InnerText;

                    XmlElement eFolio = doc.CreateElement("Folio");
                    eFolio.InnerText = doctoElement.GetElementsByTagName("Folio")[0].InnerText;

                    XmlElement eDocumentoModelo = doc.CreateElement("DocumentoModelo");
                    eDocumentoModelo.InnerText = doctoElement.GetElementsByTagName("DocumentoModelo")[0].InnerText;

                    XmlElement eObservaciones = doc.CreateElement("Observaciones");
                    eObservaciones.InnerText = doctoElement.GetElementsByTagName("Observaciones")[0].InnerText;

                    XmlElement eMoneda = doc.CreateElement("Moneda");
                    eMoneda.InnerText = doctoElement.GetElementsByTagName("Moneda")[0].InnerText;

                    edocumento.AppendChild(eAgente);
                    edocumento.AppendChild(eFecha);
                    edocumento.AppendChild(eVencimiento);
                    edocumento.AppendChild(eConcepto);
                    edocumento.AppendChild(ePendiente);
                    edocumento.AppendChild(eTotal);
                    edocumento.AppendChild(eProducto);
                    edocumento.AppendChild(eSerie);
                    edocumento.AppendChild(eFolio);
                    edocumento.AppendChild(eDocumentoModelo);
                    edocumento.AppendChild(eObservaciones);
                    edocumento.AppendChild(eMoneda);


                    edocumentos.AppendChild(edocumento);

                }

                ecliente.AppendChild(edocumentos);



                //string pathToFiles = Server.MapPath("/") + "//" + System.Configuration.ConfigurationSettings.AppSettings["xsltnameandlocation"];


                string xslt = Server.MapPath("/") + "Correo.xslt";

                //xslt = "www.desarrollosoftwarecontable.com/correo.xlst";
                xslt = "Correo.xslt";
                messageBody = GetFormattedHtmlDocument(doc, xslt);

                // @"C:\Users\valenche\Documents\Visual Studio 2012\Projects\MvcApplication5\MvcApplication5\Correo.xslt"

                //messageBody = GetFormattedHtmlDocument(doc, @"Correo.xslt");
                //messageBody = GetFormattedHtmlDocument(doc, @"C:\Users\valenche\Documents\Visual Studio 2012\Projects\MvcApplication5\MvcApplication5\Correo.xslt");
                string body = messageBody;
                //string to = "hectorvalagui@hotmail.com";
                //correoprueba = "hectorvalagui@hotmail.com,hectorvalagui@gmail.com";
                string to = "";
                to = correoprueba;
                int lpaso = 0;
                if (correoprueba == "") // usar el del cliente
                {
                    if (email_bien_escrito(email1.Trim()))
                    {
                        to = email1.Trim();
                        lpaso = 1;
                    }
                    if (email_bien_escrito(email2.Trim()))
                    {
                        if (lpaso == 1)
                            to += "," + email2.Trim();
                        else
                            to = email2.Trim();
                        lpaso = 1;
                    }
                    if (email_bien_escrito(email3.Trim()))
                    {
                        if (lpaso == 1)
                            to += "," + email3.Trim();
                        else
                            to = email3.Trim();
                        lpaso = 1;
                    }

                }
                //MailMessage message = new MailMessage(from, to, subject, body);
                mensaje mailx = new mensaje();
                mailx.cuerpo = body;
                mailx.asunto = subject;
                mailx.de = from;
                mailx.para = to;
                mailx.reply = correoreply;
                

               // message.IsBodyHtml = true;
                
                //message.ReplyToList.Add(correoreply);
                //message.ReplyToList.Add(correoreply);
                

                

                try
                {
                //    listamails.Add(message);
                    if (doctos.Count > 0)
                        listacorreos.Add(mailx);

                   //client.Send(message);
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return serializador.Serialize(listacorreos);
            //return listacorreos;
        }

        private string GetFormattedHtmlDocument(XmlDocument xml, string xsltTemplatePath)
        {
            string htmlbody = "";


            StringWriter output = new StringWriter();


            if (xsltTemplatePath != "")
            {
                XslCompiledTransform myXslTransform = new XslCompiledTransform();
                XPathNavigator navigator;
                //XsltArgumentList arguments = new XsltArgumentList();
                //StringWriter output = new StringWriter();
                navigator = xml.CreateNavigator();
                //myXslTransform.Load(xsltTemplatePath);
                XmlUrlResolver resolver = new XmlUrlResolver();
                resolver.Credentials = CredentialCache.DefaultCredentials;

                string ruta = Server.MapPath(xsltTemplatePath);
                //ruta = @"c:\users\valenche\documents\visual studio 2012\Projects\WebApplication1\WebApplication1\Correo.xls";
                myXslTransform.Load(ruta, XsltSettings.Default, resolver);
                myXslTransform.Transform(navigator, null, output);
                htmlbody = output.ToString();
                output.Close();
                output.Dispose();

            }
            return htmlbody;

        }
        private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            
             
            if (Regex.IsMatch(email, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                               @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                               @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            //if (Regex.IsMatch(email, expresion))
            {
                //if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                if (Regex.Replace(email, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                               @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                               @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }


}
