using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace MigrateAPI
{
    public class Parametros
    {
        public class Tokencls
        {
            public string token { get; set; }
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
            public string dEmis { get; set; }
            public string dCompetencia { get; set; }
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

        public class Raiz
        {
            public Documento Documento { get; set; }
        }

    }
}
