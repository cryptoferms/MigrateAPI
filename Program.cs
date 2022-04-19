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
        public const string chaveParceiro = "96243042000122"; //passar aqui a chave do cliente
        public const string chaveAlgorix = "x5YYhho092YCr/NRrxtFHw=="; //chave única nossa 
        public const string chaveSecret = "mQjJ/oW3S/JfqgR5JTJ/G+94droQTv1M"; // chave verifysignature
        public const string URL_BASE = "https://apibrhomolog.invoicy.com.br/";
       
        static void Main(string[] args)
        {
            //pega o primeiro Token manual e retorna o accessToken
            var accessToken = GerarToken();
            //usa o refreshToken para obter o accesToken nas requisições da API
            EnviarDocumentosNFSE(accessToken);

            Console.ReadKey();
        }
     

        private static void EnviarDocumentosNFSE(string token)
        {
            var payload = @"[
" + "\n" +
   @"    {
" + "\n" +
   @"        ""Documento"": {
" + "\n" +
   @"            ""ModeloDocumento"": ""NFSE"",
" + "\n" +
   @"            ""Versao"": 1.00,
" + "\n" +
   @"            ""RPS"": [
" + "\n" +
   @"                {
" + "\n" +
   @"                    ""RPSNumero"": 5,
" + "\n" +
   @"                    ""RPSSerie"": ""251"",
" + "\n" +
   @"                    ""RPSTipo"": 1,
" + "\n" +
   @"                    ""dEmis"": ""2022-04-09T10:47:00"",
" + "\n" +
   @"                    ""dCompetencia"": ""2022-04-09T10:03:00"",
" + "\n" +
   @"                    ""LocalPrestServ"": 0,
" + "\n" +
   @"                    ""natOp"": 1,
" + "\n" +
   @"                    ""Operacao"": """",
" + "\n" +
   @"                    ""NumProcesso"": ""2"",
" + "\n" +
   @"                    ""RegEspTrib"": 6,
" + "\n" +
   @"                    ""OptSN"": 1,
" + "\n" +
   @"                    ""IncCult"": 2,
" + "\n" +
   @"                    ""Status"": 1,
" + "\n" +
   @"                    ""cVerificaRPS"": """",
" + "\n" +
   @"                    ""EmpreitadaGlobal"": 0,
" + "\n" +
   @"                    ""tpAmb"": 2,
" + "\n" +
   @"                    ""RPSSubs"": {
" + "\n" +
   @"                        ""SubsNumero"": 0,
" + "\n" +
   @"                        ""SubsSerie"": """",
" + "\n" +
   @"                        ""SubsTipo"": 0,
" + "\n" +
   @"                        ""SubsNFSeNumero"": 0,
" + "\n" +
   @"                        ""SubsDEmisNFSe"": ""0000-00-00T00:00:00""
" + "\n" +
   @"                    },
" + "\n" +
   @"                    ""Prestador"": {
" + "\n" +
   @"                        ""CNPJ_prest"": ""96243042000122"", 
" + "\n" +
   @"                        ""xNome"": ""MOBIZZ"",
" + "\n" +
   @"                        ""xFant"": ""MOBIZZ"",
" + "\n" +
   @"                        ""IM"": ""726431"",
" + "\n" +
   @"                        ""IE"": ""0650154223"",
" + "\n" +
   @"                        ""CMC"": """",
" + "\n" +
   @"                        ""enderPrest"": {
" + "\n" +
   @"                            ""TPEnd"": ""RUA"",
" + "\n" +
   @"                            ""xLgr"": ""R 14 DE JULHO"",
" + "\n" +
   @"                            ""nro"": ""72"",
" + "\n" +
   @"                            ""xCpl"": """",
" + "\n" +
   @"                            ""xBairro"": ""CENTRO"",
" + "\n" +
   @"                            ""cMun"": 4310207,
" + "\n" +
   @"                            ""UF"": ""RJ"",
" + "\n" +
   @"                            ""CEP"": 98700000,
" + "\n" +
   @"                            ""fone"": """",
" + "\n" +
   @"                            ""Email"": ""fernando@algorix.com""
" + "\n" +
   @"                        }
" + "\n" +
   @"                    },
" + "\n" +
   @"                    ""ListaItens"": [
" + "\n" +
   @"                        {
" + "\n" +
   @"                            ""ItemSeq"": 1,
" + "\n" +
   @"                            ""ItemCod"": ""1"",
" + "\n" +
   @"                            ""ItemDesc"": """",
" + "\n" +
   @"                            ""ItemQtde"": 5,
" + "\n" +
   @"                            ""ItemvUnit"": 10.00,
" + "\n" +
   @"                            ""ItemuMed"": ""UN"",
" + "\n" +
   @"                            ""ItemvlDed"": 0,
" + "\n" +
   @"                            ""ItemTributavel"": """",
" + "\n" +
   @"                            ""ItemcCnae"": """",
" + "\n" +
   @"                            ""ItemTributMunicipio"": ""9512600"",
" + "\n" +
   @"                            ""ItemnAlvara"": """",
" + "\n" +
   @"                            ""ItemvIss"": 11.97,
" + "\n" +
   @"                            ""ItemvDesconto"": 0.00,
" + "\n" +
   @"                            ""ItemAliquota"": 0.0300,
" + "\n" +
   @"                            ""ItemVlrTotal"": 10.00,
" + "\n" +
   @"                            ""ItemBaseCalculo"": 10.00,
" + "\n" +
   @"                            ""ItemvlrISSRetido"": 0,
" + "\n" +
   @"                            ""ItemIssRetido"": 2,
" + "\n" +
   @"                            ""ItemRespRetencao"": 0,
" + "\n" +
   @"                            ""ItemIteListServico"": ""14.02"",
" + "\n" +
   @"                            ""ItemExigibilidadeISS"": 1,
" + "\n" +
   @"                            ""ItemcMunIncidencia"": 0,
" + "\n" +
   @"                            ""ItemNumProcesso"": """",
" + "\n" +
   @"                            ""ItemDedTipo"": """",
" + "\n" +
   @"                            ""ItemDedCPFRef"": """",
" + "\n" +
   @"                            ""ItemDedCNPJRef"": """",
" + "\n" +
   @"                            ""ItemDedNFRef"": 0,
" + "\n" +
   @"                            ""ItemDedvlTotRef"": 0,
" + "\n" +
   @"                            ""ItemDedPer"": 0,
" + "\n" +
   @"                            ""ItemVlrLiquido"": 399.00,
" + "\n" +
   @"                            ""ItemValAliqINSS"": 0.0000,
" + "\n" +
   @"                            ""ItemValINSS"": 0.00,
" + "\n" +
   @"                            ""ItemValAliqIR"": 0.0000,
" + "\n" +
   @"                            ""ItemValIR"": 0.00,
" + "\n" +
   @"                            ""ItemValAliqCOFINS"": 0.0000,
" + "\n" +
   @"                            ""ItemValCOFINS"": 0.00,
" + "\n" +
   @"                            ""ItemValAliqCSLL"": 0.0000,
" + "\n" +
   @"                            ""ItemValCSLL"": 0.00,
" + "\n" +
   @"                            ""ItemValAliqPIS"": 0.0000,
" + "\n" +
   @"                            ""ItemValPIS"": 0.00,
" + "\n" +
   @"                            ""ItemRedBC"": 0,
" + "\n" +
   @"                            ""ItemRedBCRetido"": 0,
" + "\n" +
   @"                            ""ItemBCRetido"": 0,
" + "\n" +
   @"                            ""ItemValAliqISSRetido"": 0.0000,
" + "\n" +
   @"                            ""ItemPaisImpDevido"": """",
" + "\n" +
   @"                            ""ItemJustDed"": """",
" + "\n" +
   @"                            ""ItemvOutrasRetencoes"": 0,
" + "\n" +
   @"                            ""ItemDescIncondicionado"": 0.00,
" + "\n" +
   @"                            ""ItemDescCondicionado"": 0.00,
" + "\n" +
   @"                            ""ItemTotalAproxTribServ"": 0
" + "\n" +
   @"                        }
" + "\n" +
   @"                    ],
" + "\n" +
   @"                    ""ListaParcelas"": [],
" + "\n" +
   @"                    ""Servico"": {
" + "\n" +
   @"                        ""Valores"": {
" + "\n" +
   @"                            ""ValServicos"": 10.00,
" + "\n" +
   @"                            ""ValDeducoes"": 0,
" + "\n" +
   @"                            ""ValPIS"": 0.00,
" + "\n" +
   @"                            ""ValBCPIS"": 0,
" + "\n" +
   @"                            ""ValCOFINS"": 0.00,
" + "\n" +
   @"                            ""ValBCCOFINS"": 0,
" + "\n" +
   @"                            ""ValINSS"": 0.00,
" + "\n" +
   @"                            ""ValBCINSS"": 0,
" + "\n" +
   @"                            ""ValIR"": 0.00,
" + "\n" +
   @"                            ""ValBCIRRF"": 0,
" + "\n" +
   @"                            ""ValCSLL"": 0.00,
" + "\n" +
   @"                            ""ValBCCSLL"": 0,
" + "\n" +
   @"                            ""RespRetencao"": 0,
" + "\n" +
   @"                            ""Tributavel"": """",
" + "\n" +
   @"                            ""ValISS"": 11.97,
" + "\n" +
   @"                            ""ISSRetido"": 2,
" + "\n" +
   @"                            ""ValISSRetido"": 0,
" + "\n" +
   @"                            ""ValTotal"": 0,
" + "\n" +
   @"                            ""ValTotalRecebido"": 0,
" + "\n" +
   @"                            ""ValBaseCalculo"": 10.00,
" + "\n" +
   @"                            ""ValOutrasRetencoes"": 0,
" + "\n" +
   @"                            ""ValAliqISS"": 0.030000,
" + "\n" +
   @"                            ""ValAliqPIS"": 0.0000,
" + "\n" +
   @"                            ""PISRetido"": 0,
" + "\n" +
   @"                            ""ValAliqCOFINS"": 0.0000,
" + "\n" +
   @"                            ""COFINSRetido"": 0,
" + "\n" +
   @"                            ""ValAliqIR"": 0.0000,
" + "\n" +
   @"                            ""IRRetido"": 0,
" + "\n" +
   @"                            ""ValAliqCSLL"": 0.0000,
" + "\n" +
   @"                            ""CSLLRetido"": 0,
" + "\n" +
   @"                            ""ValAliqINSS"": 0.0000,
" + "\n" +
   @"                            ""INSSRetido"": 0,
" + "\n" +
   @"                            ""ValAliqCpp"": 0,
" + "\n" +
   @"                            ""CppRetido"": 0,
" + "\n" +
   @"                            ""ValCpp"": 0,
" + "\n" +
   @"                            ""OutrasRetencoesRetido"": 0,
" + "\n" +
   @"                            ""ValBCOutrasRetencoes"": 0,
" + "\n" +
   @"                            ""ValAliqOutrasRetencoes"": 0,
" + "\n" +
   @"                            ""ValAliqTotTributos"": 0,
" + "\n" +
   @"                            ""ValLiquido"": 10.00,
" + "\n" +
   @"                            ""ValDescIncond"": 0.00,
" + "\n" +
   @"                            ""ValDescCond"": 0.00,
" + "\n" +
   @"                            ""ValAcrescimos"": 0,
" + "\n" +
   @"                            ""ValAliqISSoMunic"": 0.0000,
" + "\n" +
   @"                            ""InfValPIS"": """",
" + "\n" +
   @"                            ""InfValCOFINS"": """",
" + "\n" +
   @"                            ""ValLiqFatura"": 0,
" + "\n" +
   @"                            ""ValBCISSRetido"": 0,
" + "\n" +
   @"                            ""NroFatura"": 0,
" + "\n" +
   @"                            ""CargaTribValor"": 0,
" + "\n" +
   @"                            ""CargaTribPercentual"": 0,
" + "\n" +
   @"                            ""CargaTribFonte"": """",
" + "\n" +
   @"                            ""JustDed"": """",
" + "\n" +
   @"                            ""ValCredito"": 0,
" + "\n" +
   @"                            ""OutrosImp"": 0,
" + "\n" +
   @"                            ""ValRedBC"": 0,
" + "\n" +
   @"                            ""ValRetFederais"": 0,
" + "\n" +
   @"                            ""ValAproxTrib"": 0
" + "\n" +
   @"                        },
" + "\n" +
   @"                        ""LocalPrestacao"": {
" + "\n" +
   @"                            ""SerEndTpLgr"": """",
" + "\n" +
   @"                            ""SerEndLgr"": """",
" + "\n" +
   @"                            ""SerEndNumero"": """",
" + "\n" +
   @"                            ""SerEndComplemento"": """",
" + "\n" +
   @"                            ""SerEndBairro"": """",
" + "\n" +
   @"                            ""SerEndxMun"": """",
" + "\n" +
   @"                            ""SerEndcMun"": 0,
" + "\n" +
   @"                            ""SerEndCep"": 0,
" + "\n" +
   @"                            ""SerEndSiglaUF"": """"
" + "\n" +
   @"                        },
" + "\n" +
   @"                        ""IteListServico"": ""14.02"",
" + "\n" +
   @"                        ""Cnae"": 0,
" + "\n" +
   @"                        ""fPagamento"": """",
" + "\n" +
   @"                        ""tpag"": 0,
" + "\n" +
   @"                        ""TributMunicipio"": ""9512600"",
" + "\n" +
   @"                        ""TributMunicDesc"": """",
" + "\n" +
   @"                        ""Discriminacao"": ""MAO DE OBRA"",
" + "\n" +
   @"                        ""cMun"": 4310207,
" + "\n" +
   @"                        ""SerQuantidade"": 0,
" + "\n" +
   @"                        ""SerUnidade"": """",
" + "\n" +
   @"                        ""SerNumAlvara"": """",
" + "\n" +
   @"                        ""PaiPreServico"": ""BR"",
" + "\n" +
   @"                        ""cMunIncidencia"": 0,
" + "\n" +
   @"                        ""dVencimento"": ""0000-00-00T00:00:00"",
" + "\n" +
   @"                        ""ObsInsPagamento"": """",
" + "\n" +
   @"                        ""ObrigoMunic"": 0,
" + "\n" +
   @"                        ""TributacaoISS"": 0,
" + "\n" +
   @"                        ""CodigoAtividadeEconomica"": """",
" + "\n" +
   @"                        ""ServicoViasPublicas"": 0,
" + "\n" +
   @"                        ""NumeroParcelas"": 0,
" + "\n" +
   @"                        ""NroOrcamento"": 0,
" + "\n" +
   @"                        ""CodigoNBS"": """"
" + "\n" +
   @"                    },
" + "\n" +
   @"                    ""Tomador"": {
" + "\n" +
   @"                        ""TomaCNPJ"": ""02663790000110"",
" + "\n" +
   @"                        ""TomaCPF"": """",
" + "\n" +
   @"                        ""TomaIE"": """",
" + "\n" +
   @"                        ""TomaIM"": """",
" + "\n" +
   @"                        ""TomaRazaoSocial"": ""ALGORIX"",
" + "\n" +
   @"                        ""TomatpLgr"": ""AV"",
" + "\n" +
   @"                        ""TomaEndereco"": ""Pde Dehon"",
" + "\n" +
   @"                        ""TomaNumero"": ""153"",
" + "\n" +
   @"                        ""TomaComplemento"": """",
" + "\n" +
   @"                        ""TomaBairro"": ""CENTRO"",
" + "\n" +
   @"                        ""TomacMun"": 4302204,
" + "\n" +
   @"                        ""TomaxMun"": ""Boa vista do buricá"",
" + "\n" +
   @"                        ""TomaUF"": ""RS"",
" + "\n" +
   @"                        ""TomaPais"": ""BR"",
" + "\n" +
   @"                        ""TomaCEP"": 98918000,
" + "\n" +
   @"                        ""TomaTelefone"": ""5537481081"",
" + "\n" +
   @"                        ""TomaTipoTelefone"": """",
" + "\n" +
   @"                        ""TomaEmail"": """",
" + "\n" +
   @"                        ""TomaSite"": """",
" + "\n" +
   @"                        ""TomaIME"": """",
" + "\n" +
   @"                        ""TomaSituacaoEspecial"": """",
" + "\n" +
   @"                        ""DocTomadorEstrangeiro"": """",
" + "\n" +
   @"                        ""TomaRegEspTrib"": 0,
" + "\n" +
   @"                        ""TomaCadastroMunicipio"": 0,
" + "\n" +
   @"                        ""TomaOrgaoPublico"": 0
" + "\n" +
   @"                    },
" + "\n" +
   @"                    ""IntermServico"": {
" + "\n" +
   @"                        ""IntermRazaoSocial"": """",
" + "\n" +
   @"                        ""IntermCNPJ"": """",
" + "\n" +
   @"                        ""IntermCPF"": """",
" + "\n" +
   @"                        ""IntermIM"": """",
" + "\n" +
   @"                        ""IntermEmail"": """",
" + "\n" +
   @"                        ""IntermEndereco"": """",
" + "\n" +
   @"                        ""IntermNumero"": """",
" + "\n" +
   @"                        ""IntermComplemento"": """",
" + "\n" +
   @"                        ""IntermBairro"": """",
" + "\n" +
   @"                        ""IntermCep"": 0,
" + "\n" +
   @"                        ""IntermCmun"": 0,
" + "\n" +
   @"                        ""IntermXmun"": """",
" + "\n" +
   @"                        ""IntermFone"": """",
" + "\n" +
   @"                        ""IntermIE"": """"
" + "\n" +
   @"                    },
" + "\n" +
   @"                    ""ConstCivil"": {
" + "\n" +
   @"                        ""CodObra"": """",
" + "\n" +
   @"                        ""Art"": """",
" + "\n" +
   @"                        ""ObraLog"": """",
" + "\n" +
   @"                        ""ObraCompl"": """",
" + "\n" +
   @"                        ""ObraNumero"": """",
" + "\n" +
   @"                        ""ObraBairro"": """",
" + "\n" +
   @"                        ""ObraCEP"": 0,
" + "\n" +
   @"                        ""ObraMun"": 0,
" + "\n" +
   @"                        ""ObraUF"": """",
" + "\n" +
   @"                        ""ObraPais"": """",
" + "\n" +
   @"                        ""ObraCEI"": """",
" + "\n" +
   @"                        ""ObraMatricula"": """",
" + "\n" +
   @"                        ""ObraValRedBC"": 0,
" + "\n" +
   @"                        ""ObraTipo"": 0,
" + "\n" +
   @"                        ""ObraNomeFornecedor"": """",
" + "\n" +
   @"                        ""ObraNumeroNF"": 0,
" + "\n" +
   @"                        ""ObraDataNF"": ""0000-00-00T00:00:00"",
" + "\n" +
   @"                        ""ObraNumEncapsulamento"": """",
" + "\n" +
   @"                        ""AbatimentoMateriais"": 0,
" + "\n" +
   @"                        ""ListaMaterial"": []
" + "\n" +
   @"                    },
" + "\n" +
   @"                    ""ListaDed"": [],
" + "\n" +
   @"                    ""Transportadora"": {
" + "\n" +
   @"                        ""TraNome"": """",
" + "\n" +
   @"                        ""TraCPFCNPJ"": """",
" + "\n" +
   @"                        ""TraIE"": """",
" + "\n" +
   @"                        ""TraPlaca"": """",
" + "\n" +
   @"                        ""TraEnd"": """",
" + "\n" +
   @"                        ""TraMun"": 0,
" + "\n" +
   @"                        ""TraUF"": """",
" + "\n" +
   @"                        ""TraPais"": """",
" + "\n" +
   @"                        ""TraTipoFrete"": 0
" + "\n" +
   @"                    },
" + "\n" +
   @"                    ""NFSOutrasinformacoes"": """",
" + "\n" +
   @"                    ""RPSCanhoto"": 0,
" + "\n" +
   @"                    ""Arquivo"": """",
" + "\n" +
   @"                    ""ExtensaoArquivo"": """"
" + "\n" +
   @"                }
" + "\n" +
   @"            ]
" + "\n" +
   @"        }
" + "\n" +
   @"    }
" + "\n" +
   @"]";
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
            //É preciso criar o primeiro Token no site JWT IO https://desenvolvedores.migrate.info/2021/06/autenticacao-api-rest/
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
                Console.WriteLine($"AccessToken -----------------\n\n{objetoToken.accessToken}\n");
                return objetoToken.accessToken;
            }
        }

        /// <summary>
        /// Rotina responsável para criar e encodar um Token que será utilizado no método GerarToken
        /// </summary>
        /// <returns></returns>
        private static string RotinaToken()
        {
            var now = DateTime.UtcNow;
            var encodedSecret = PegarSecretAsBytes(chaveSecret);
            string token = JWT.Encode(PegarPayload(now), encodedSecret, JwsAlgorithm.HS256);
            return token;
        }

        private static Dictionary<string, object> PegarPayload(DateTime utcnow)
        {
            var payload = new Dictionary<string, object>()
            {
                {"iat", PegarIat(utcnow) },
                {"exp", PegarExp(utcnow) },
                {"sub", chaveParceiro },
                {"partnerKey", chaveAlgorix },
            };
            return payload;
        }

        private static object PegarExp(DateTime utcnow)
        {
            var tokenValidFor = TimeSpan.FromSeconds(120); // 2minutos
            var expiry = utcnow.Add(tokenValidFor);
            return PegarEpochDateTimeAsInt(expiry); // exp	
        }

        private static object PegarIat(DateTime utcnow)
        {
            return PegarEpochDateTimeAsInt(utcnow);
        }

        private static object PegarEpochDateTimeAsInt(DateTime datetime)
        {
            DateTime dateTime = datetime;
            if (datetime.Kind != DateTimeKind.Utc)
                dateTime = datetime.ToUniversalTime();
            if (dateTime.ToUniversalTime() <= UnixEpoch)
                return 0;
            return (long)(dateTime - UnixEpoch).TotalSeconds;
        }

        private static byte[] PegarSecretAsBytes(string chaveSecret)
        {
            return Encoding.UTF8.GetBytes(chaveSecret);
        }
    }
  
}
