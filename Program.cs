using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace MigrateAPI
{
    class Program
    {
        /// <summary>
        /// constantes utilizadas para pegar a autenticação do cliente
        /// </summary>
        public const string chaveParceiro = "96243042000122"; //passar aqui a chave do cliente
        public const string chaveAlgorix = "x5YYhho092YCr/NRrxtFHw==";
        public const string chaveSecret = "mQjJ/oW3S/JfqgR5JTJ/G+94droQTv1M";

        const string URL_BASE = "https://apibrhomolog.invoicy.com.br/";
       
        static void Main(string[] args)
        {
            DecodeToken();
            //pega o primeiro Token manual e retorna o accessToken
            var accessToken = GerarToken();
            //usa o refreshToken para obter o accesToken nas requisições da API
            EnviarDocumentosNFSE(accessToken);

            Console.ReadKey();
        }
        private static void DecodeToken()
        {
            const string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE2NDk1Mzg3ODAsImV4cCI6MTY0OTUzODkwMCwic3ViIjoiOTYyNDMwNDIwMDAxMjIiLCJwYXJ0bmVyS2V5IjoieDVZWWhobzA5MllDci9OUnJ4dEZIdz09In0.jHOQbAgn17hcReH83alY3EbXgfRhRn7HqXLUCv21ltY";
            const string secret = "mQjJ/oW3S/JfqgR5JTJ/G+94droQTv1M";

            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                var provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                var json = decoder.Decode(token, secret ,verify: true);
                Console.WriteLine(json);
            }
            catch (TokenExpiredException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
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
        private static string RefreshToken(string refreshToken)
        {
            var accessToken = new ResponseGerarToken { refreshToken = refreshToken };
            JavaScriptSerializer js = new JavaScriptSerializer();
            var tokenJson = js.Serialize(accessToken);
            var request = (HttpWebRequest)WebRequest.Create($"{URL_BASE}oauth2/invoicy/auth");
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
                Console.WriteLine($"Token de acesso --------------\n\n{objetoToken.accessToken}\n");
                return objetoToken.accessToken;
            }
        }

        private static string GerarToken()
        {
            //É preciso criar o primeiro Token no site JWT IO https://desenvolvedores.migrate.info/2021/06/autenticacao-api-rest/
            var accessToken = new Tokencls() { accessToken = RotinaToken() };
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
                Console.WriteLine($"RefreshToken -----------------\n\n{objetoToken.refreshToken}\n");
                return objetoToken.refreshToken;
            }
        }

        private static string RotinaToken()
        {
            throw new NotImplementedException();
        }
    }
    public class Tokencls
    {
        public string accessToken { get; set; }
        public int accessTokenExpireAt { get; set; }
        public string refreshToken { get; set; }
        public int refreshTokenExpireAt { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class RPSSubs
    {
        public int SubsNumero { get; set; }
        public string SubsSerie { get; set; }
        public int SubsTipo { get; set; }
        public int SubsNFSeNumero { get; set; }
        public string SubsDEmisNFSe { get; set; }
    }

    public class EnderPrest
    {
        public string TPEnd { get; set; }
        public string xLgr { get; set; }
        public string nro { get; set; }
        public string xCpl { get; set; }
        public string xBairro { get; set; }
        public int cMun { get; set; }
        public string UF { get; set; }
        public int CEP { get; set; }
        public string fone { get; set; }
        public string Email { get; set; }
    }

    public class Prestador
    {
        public string CNPJ_prest { get; set; }
        public string xNome { get; set; }
        public string xFant { get; set; }
        public string IM { get; set; }
        public string IE { get; set; }
        public string CMC { get; set; }
        public EnderPrest enderPrest { get; set; }
    }

    public class ListaIten
    {
        public int ItemSeq { get; set; }
        public string ItemCod { get; set; }
        public string ItemDesc { get; set; }
        public int ItemQtde { get; set; }
        public double ItemvUnit { get; set; }
        public string ItemuMed { get; set; }
        public int ItemvlDed { get; set; }
        public string ItemTributavel { get; set; }
        public string ItemcCnae { get; set; }
        public string ItemTributMunicipio { get; set; }
        public string ItemnAlvara { get; set; }
        public double ItemvIss { get; set; }
        public double ItemvDesconto { get; set; }
        public double ItemAliquota { get; set; }
        public double ItemVlrTotal { get; set; }
        public double ItemBaseCalculo { get; set; }
        public int ItemvlrISSRetido { get; set; }
        public int ItemIssRetido { get; set; }
        public int ItemRespRetencao { get; set; }
        public string ItemIteListServico { get; set; }
        public int ItemExigibilidadeISS { get; set; }
        public int ItemcMunIncidencia { get; set; }
        public string ItemNumProcesso { get; set; }
        public string ItemDedTipo { get; set; }
        public string ItemDedCPFRef { get; set; }
        public string ItemDedCNPJRef { get; set; }
        public int ItemDedNFRef { get; set; }
        public int ItemDedvlTotRef { get; set; }
        public int ItemDedPer { get; set; }
        public double ItemVlrLiquido { get; set; }
        public double ItemValAliqINSS { get; set; }
        public double ItemValINSS { get; set; }
        public double ItemValAliqIR { get; set; }
        public double ItemValIR { get; set; }
        public double ItemValAliqCOFINS { get; set; }
        public double ItemValCOFINS { get; set; }
        public double ItemValAliqCSLL { get; set; }
        public double ItemValCSLL { get; set; }
        public double ItemValAliqPIS { get; set; }
        public double ItemValPIS { get; set; }
        public int ItemRedBC { get; set; }
        public int ItemRedBCRetido { get; set; }
        public int ItemBCRetido { get; set; }
        public double ItemValAliqISSRetido { get; set; }
        public string ItemPaisImpDevido { get; set; }
        public string ItemJustDed { get; set; }
        public int ItemvOutrasRetencoes { get; set; }
        public double ItemDescIncondicionado { get; set; }
        public double ItemDescCondicionado { get; set; }
        public int ItemTotalAproxTribServ { get; set; }
    }

    public class Valores
    {
        public double ValServicos { get; set; }
        public int ValDeducoes { get; set; }
        public double ValPIS { get; set; }
        public int ValBCPIS { get; set; }
        public double ValCOFINS { get; set; }
        public int ValBCCOFINS { get; set; }
        public double ValINSS { get; set; }
        public int ValBCINSS { get; set; }
        public double ValIR { get; set; }
        public int ValBCIRRF { get; set; }
        public double ValCSLL { get; set; }
        public int ValBCCSLL { get; set; }
        public int RespRetencao { get; set; }
        public string Tributavel { get; set; }
        public double ValISS { get; set; }
        public int ISSRetido { get; set; }
        public int ValISSRetido { get; set; }
        public int ValTotal { get; set; }
        public int ValTotalRecebido { get; set; }
        public double ValBaseCalculo { get; set; }
        public int ValOutrasRetencoes { get; set; }
        public double ValAliqISS { get; set; }
        public double ValAliqPIS { get; set; }
        public int PISRetido { get; set; }
        public double ValAliqCOFINS { get; set; }
        public int COFINSRetido { get; set; }
        public double ValAliqIR { get; set; }
        public int IRRetido { get; set; }
        public double ValAliqCSLL { get; set; }
        public int CSLLRetido { get; set; }
        public double ValAliqINSS { get; set; }
        public int INSSRetido { get; set; }
        public int ValAliqCpp { get; set; }
        public int CppRetido { get; set; }
        public int ValCpp { get; set; }
        public int OutrasRetencoesRetido { get; set; }
        public int ValBCOutrasRetencoes { get; set; }
        public int ValAliqOutrasRetencoes { get; set; }
        public int ValAliqTotTributos { get; set; }
        public double ValLiquido { get; set; }
        public double ValDescIncond { get; set; }
        public double ValDescCond { get; set; }
        public int ValAcrescimos { get; set; }
        public double ValAliqISSoMunic { get; set; }
        public string InfValPIS { get; set; }
        public string InfValCOFINS { get; set; }
        public int ValLiqFatura { get; set; }
        public int ValBCISSRetido { get; set; }
        public int NroFatura { get; set; }
        public int CargaTribValor { get; set; }
        public int CargaTribPercentual { get; set; }
        public string CargaTribFonte { get; set; }
        public string JustDed { get; set; }
        public int ValCredito { get; set; }
        public int OutrosImp { get; set; }
        public int ValRedBC { get; set; }
        public int ValRetFederais { get; set; }
        public int ValAproxTrib { get; set; }
    }

    public class LocalPrestacao
    {
        public string SerEndTpLgr { get; set; }
        public string SerEndLgr { get; set; }
        public string SerEndNumero { get; set; }
        public string SerEndComplemento { get; set; }
        public string SerEndBairro { get; set; }
        public string SerEndxMun { get; set; }
        public int SerEndcMun { get; set; }
        public int SerEndCep { get; set; }
        public string SerEndSiglaUF { get; set; }
    }

    public class Servico
    {
        public Valores Valores { get; set; }
        public LocalPrestacao LocalPrestacao { get; set; }
        public string IteListServico { get; set; }
        public int Cnae { get; set; }
        public string fPagamento { get; set; }
        public int tpag { get; set; }
        public string TributMunicipio { get; set; }
        public string TributMunicDesc { get; set; }
        public string Discriminacao { get; set; }
        public int cMun { get; set; }
        public int SerQuantidade { get; set; }
        public string SerUnidade { get; set; }
        public string SerNumAlvara { get; set; }
        public string PaiPreServico { get; set; }
        public int cMunIncidencia { get; set; }
        public string dVencimento { get; set; }
        public string ObsInsPagamento { get; set; }
        public int ObrigoMunic { get; set; }
        public int TributacaoISS { get; set; }
        public string CodigoAtividadeEconomica { get; set; }
        public int ServicoViasPublicas { get; set; }
        public int NumeroParcelas { get; set; }
        public int NroOrcamento { get; set; }
        public string CodigoNBS { get; set; }
    }

    public class Tomador
    {
        public string TomaCNPJ { get; set; }
        public string TomaCPF { get; set; }
        public string TomaIE { get; set; }
        public string TomaIM { get; set; }
        public string TomaRazaoSocial { get; set; }
        public string TomatpLgr { get; set; }
        public string TomaEndereco { get; set; }
        public string TomaNumero { get; set; }
        public string TomaComplemento { get; set; }
        public string TomaBairro { get; set; }
        public int TomacMun { get; set; }
        public string TomaxMun { get; set; }
        public string TomaUF { get; set; }
        public string TomaPais { get; set; }
        public int TomaCEP { get; set; }
        public string TomaTelefone { get; set; }
        public string TomaTipoTelefone { get; set; }
        public string TomaEmail { get; set; }
        public string TomaSite { get; set; }
        public string TomaIME { get; set; }
        public string TomaSituacaoEspecial { get; set; }
        public string DocTomadorEstrangeiro { get; set; }
        public int TomaRegEspTrib { get; set; }
        public int TomaCadastroMunicipio { get; set; }
        public int TomaOrgaoPublico { get; set; }
    }

    public class IntermServico
    {
        public string IntermRazaoSocial { get; set; }
        public string IntermCNPJ { get; set; }
        public string IntermCPF { get; set; }
        public string IntermIM { get; set; }
        public string IntermEmail { get; set; }
        public string IntermEndereco { get; set; }
        public string IntermNumero { get; set; }
        public string IntermComplemento { get; set; }
        public string IntermBairro { get; set; }
        public int IntermCep { get; set; }
        public int IntermCmun { get; set; }
        public string IntermXmun { get; set; }
        public string IntermFone { get; set; }
        public string IntermIE { get; set; }
    }

    public class ConstCivil
    {
        public string CodObra { get; set; }
        public string Art { get; set; }
        public string ObraLog { get; set; }
        public string ObraCompl { get; set; }
        public string ObraNumero { get; set; }
        public string ObraBairro { get; set; }
        public int ObraCEP { get; set; }
        public int ObraMun { get; set; }
        public string ObraUF { get; set; }
        public string ObraPais { get; set; }
        public string ObraCEI { get; set; }
        public string ObraMatricula { get; set; }
        public int ObraValRedBC { get; set; }
        public int ObraTipo { get; set; }
        public string ObraNomeFornecedor { get; set; }
        public int ObraNumeroNF { get; set; }
        public string ObraDataNF { get; set; }
        public string ObraNumEncapsulamento { get; set; }
        public int AbatimentoMateriais { get; set; }
        public List<object> ListaMaterial { get; set; }
    }

    public class Transportadora
    {
        public string TraNome { get; set; }
        public string TraCPFCNPJ { get; set; }
        public string TraIE { get; set; }
        public string TraPlaca { get; set; }
        public string TraEnd { get; set; }
        public int TraMun { get; set; }
        public string TraUF { get; set; }
        public string TraPais { get; set; }
        public int TraTipoFrete { get; set; }
    }

    public class RP
    {
        public int RPSNumero { get; set; }
        public string RPSSerie { get; set; }
        public int RPSTipo { get; set; }
        public DateTime dEmis { get; set; }
        public DateTime dCompetencia { get; set; }
        public int LocalPrestServ { get; set; }
        public int natOp { get; set; }
        public string Operacao { get; set; }
        public string NumProcesso { get; set; }
        public int RegEspTrib { get; set; }
        public int OptSN { get; set; }
        public int IncCult { get; set; }
        public int Status { get; set; }
        public string cVerificaRPS { get; set; }
        public int EmpreitadaGlobal { get; set; }
        public int tpAmb { get; set; }
        public RPSSubs RPSSubs { get; set; }
        public Prestador Prestador { get; set; }
        public List<ListaIten> ListaItens { get; set; }
        public List<object> ListaParcelas { get; set; }
        public Servico Servico { get; set; }
        public Tomador Tomador { get; set; }
        public IntermServico IntermServico { get; set; }
        public ConstCivil ConstCivil { get; set; }
        public List<object> ListaDed { get; set; }
        public Transportadora Transportadora { get; set; }
        public string NFSOutrasinformacoes { get; set; }
        public int RPSCanhoto { get; set; }
        public string Arquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
    }

    public class Documento
    {
        public string ModeloDocumento { get; set; }
        public double Versao { get; set; }
        public List<RP> RPS { get; set; }
    }

    public class Root
    {
        public Documento Documento { get; set; }
    }

}
