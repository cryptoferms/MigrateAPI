using Jose;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using static MigrateAPI.Parametros;

namespace MigrateAPI
{
    class Program
    {
        /// <summary>
        /// constantes utilizadas para pegar a autenticação do cliente
        /// </summary>
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public const string chaveCNPJ = "96243042000122"; //passar aqui a chave do cliente que é o CNPJ
        public const string chaveAlgorix = "x5YYhho092YCr/NRrxtFHw=="; //chave única nossa 
        public const string chaveSecret = "mQjJ/oW3S/JfqgR5JTJ/G+94droQTv1M"; // chave verifysignature
        public const string URL_BASE = "https://apibrhomolog.invoicy.com.br/";
       
        static void Main(string[] args)
        {
            //GeraPayload();
            //pega o primeiro Token manual e retorna o accessToken
            var accessToken = GerarToken();
            //usa o refreshToken para obter o accesToken nas requisições da API
            EnviarDocumentosNFSE(accessToken);

            Console.ReadKey();
        }

        private static void GeraPayload()
        {
            var payload = new Root()
            {
             Documento = {
                ModeloDocumento = "NSFE",
                Versao = 1.0,
                RPS = new List<RP>()
                { new RP()
                 {
                    RPSNumero = 5,
                    RPSTipo = 1,
                    dEmis = DateTime.Now,
                    dCompetencia = DateTime.Now.AddDays(3),
                    LocalPrestServ = 0,
                    natOp = 1,
                    Operacao = "",
                    NumProcesso = "",
                    RegEspTrib = 6,
                    OptSN = 1,
                    IncCult = 2,
                    Status = 1,
                    cVerificaRPS = "",
                    EmpreitadaGlobal = 0,
                    tpAmb = 2,
                    RPSSubs = new RPSSubs()
                     {
                        SubsNumero = 0,
                        SubsSerie = "",
                        SubsTipo = 0,
                        SubsNFSeNumero = 0,
                        SubsDEmisNFSe = DateTime.Now.ToString(),
                    },
                    Prestador = new Prestador()
                    {
                        CNPJ_prest = chaveCNPJ,
                        xNome = "ALGORIX",
                        xFant = "ALGORIX",
                        IM = "726431",
                        IE = "0650154223",
                        CMC = "",
                        enderPrest = new EnderPrest
                        {
                            TPEnd = "RUA",
                            xLgr = "R 14 DE JULHO",
                            nro = "10",
                            xCpl = "",
                            xBairro = "CENTRO",
                            cMun = 4310207,
                            UF = "RS",
                            CEP = 98700000,
                            fone = "",
                            Email = "",
                        }

                    },
                    ListaItens = new List<ListaIten>()
                    {
                        new ListaIten()
                        {
                            ItemSeq = 1,
                            ItemCod = "1",
                            ItemDesc = "",
                            ItemQtde = 1,
                            ItemvUnit = 399.0,
                            ItemuMed = "UN",
                            ItemTributavel = "",
                            ItemcCnae = "",
                            ItemTributMunicipio = "9512600",
                            ItemnAlvara = "",
                            ItemvIss = 11.97,
                            ItemvDesconto = 0.00,
                            ItemAliquota = 0.0300,
                            ItemVlrTotal = 399.00,
                            ItemvlrISSRetido = 0,
                            ItemIssRetido = 2,
                            ItemRespRetencao = 0,
                            ItemIteListServico = "14.02",
                            ItemExigibilidadeISS =1,
                            ItemcMunIncidencia = 0,
                            ItemNumProcesso = "",
                            ItemDedTipo = "",
                            ItemDedCPFRef = "",
                            ItemDedCNPJRef = "",
                            ItemDedNFRef = 0,
                            ItemDedvlTotRef= 0,
                            ItemDedPer = 0,
                            ItemVlrLiquido = 399.00,
                            ItemValAliqINSS = 0.0000,
                            ItemValINSS = 0.00,
                            ItemValAliqIR = 0.0000,
                            ItemValIR = 0.00,
                            ItemValAliqCOFINS = 0.0000,
                            ItemValCOFINS =0.00,
                            ItemValAliqCSLL = 0.0000,
                            ItemValCSLL = 0.00,
                            ItemValAliqPIS = 0.0000,
                            ItemValPIS = 0.00,
                            ItemRedBC = 0,
                            ItemRedBCRetido = 0,
                            ItemValAliqISSRetido = 0.0000,
                            ItemPaisImpDevido = "",
                            ItemJustDed = "",
                            ItemvOutrasRetencoes = 0,
                            ItemDescIncondicionado = 0.00,
                            ItemDescCondicionado = 0.00,
                            ItemTotalAproxTribServ = 0,

                        }
                    },
                  ListaParcelas = new List<object>(),
                  Servico = new Servico()
                  {
                      Valores = new Valores()
                      {
                          ValServicos = 399.00,
                          ValDeducoes = 0,
                          ValPIS = 0,
                      }
                  }
                }
                }
                }
            };
            string fileName = "NSFE.json";
            var js = new JavaScriptSerializer();
            string jsonString = js.Serialize(payload);
            File.WriteAllText(fileName, jsonString);

            Console.WriteLine(File.ReadAllText(fileName));
        }

        private static void EnviarDocumentosNFSE(string token)
        {
            var caminho = @"C:\Users\nando\Dropbox\PC\Documents\ALGORIX SERVIÇO\Repos tasks Joao\MigrateSO\MigrateAPI\bin\Debug\NSFE.json";
            var payload = File.OpenRead(caminho);
            var js = new JavaScriptSerializer();



            var request = (HttpWebRequest)WebRequest.Create($"{URL_BASE}senddocuments/nfse?type=Emissao&tpAmb=2");
            request.Method = "POST";
            request.Headers.Add("Authorization", token);
            request.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(payload);
            }

            using (var response = (HttpWebResponse)request.GetResponse())  
            {
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                var result = streamReader.ReadToEnd();
                Console.WriteLine($"payload result----------------\n Status de resposta:{response.StatusCode}\n\n{result}");
            }
        }
        private static string GerarToken()
        {
            var accessToken = new Tokencls() { token = RotinaToken() };
            JavaScriptSerializer js = new JavaScriptSerializer();
            var tokenJson = js.Serialize(accessToken);
            var request = (HttpWebRequest)WebRequest.Create($"{URL_BASE}/oauth2/invoicy/auth");
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(tokenJson);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var objetoToken = js.Deserialize<Tokencls>(result);
                Console.WriteLine($"AccessToken : \n\n{objetoToken.accessToken}\n");
                return objetoToken.accessToken;
            }
        }

        /// <summary>
        /// Rotina responsável para criar e encodar um Token que será utilizado no método GerarToken
        /// Igual no site JWT IO https://desenvolvedores.migrate.info/2021/06/autenticacao-api-rest/
        /// </summary>
        /// <returns></returns>
        private static string RotinaToken()
        {
            var now = DateTime.UtcNow;
            var encodedSecret = PegarSecretAsBytes(chaveSecret);
            string token = JWT.Encode(PegarPayload(now), encodedSecret, JwsAlgorithm.HS256);
            Console.WriteLine("iat = " + PegarIat(now));
            Console.WriteLine("exp = " + PegarExp(now));
            Console.WriteLine("token = " + token);
            return token;
        }
        /// <summary>
        /// método que cria o payload do Token
        /// </summary>
        /// <param name="utcnow"></param>
        /// <returns></returns>
        private static Dictionary<string, object> PegarPayload(DateTime utcnow)
        {
            var payload = new Dictionary<string, object>()
            {
                {"iat", PegarIat(utcnow) },
                {"exp", PegarExp(utcnow) },
                {"sub", chaveCNPJ },
                {"partnerKey", chaveAlgorix },
            };
            return payload;
        }

        private static object PegarExp(DateTime utcnow)
        {
            var tokenValidFor = TimeSpan.FromSeconds(120); // Adiciona o tempo de expiração de 2 minutos conforme descrito na documentação
            var expiry = utcnow.Add(tokenValidFor);
            return PegarEpochDateTimeAsInt(expiry); // exp	
        }

        private static object PegarIat(DateTime utcnow)
        {
            return PegarEpochDateTimeAsInt(utcnow);
        }
        /// <summary>
        /// método responsável por transformar a data Epoch em Int
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        private static object PegarEpochDateTimeAsInt(DateTime datetime)
        {
            DateTime dateTime = datetime;
            if (datetime.Kind != DateTimeKind.Utc)
                dateTime = datetime.ToUniversalTime();
            if (dateTime.ToUniversalTime() <= UnixEpoch)
                return 0;
            return (long)(dateTime - UnixEpoch).TotalSeconds;
        }
        /// <summary>
        /// método que faz o encode para byte UTF8 da chave secret
        /// </summary>
        /// <param name="chaveSecret"></param>
        /// <returns></returns>
        private static byte[] PegarSecretAsBytes(string chaveSecret)
        {
            return Encoding.UTF8.GetBytes(chaveSecret);
        }
    }
  
}
