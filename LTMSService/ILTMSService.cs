using LTMSClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LTMSService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILTMSService
    {
        #region Lottery 
        [OperationContract]
        bool InsertInLottery(ClsLottery objLottery);

        [OperationContract]
        bool UpdateInLottery(ClsLottery objLottery);

        [OperationContract]
        DataTable GetLotteryDtl();

        [OperationContract]
        DataTable GetLotteryDtlById(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInLottery(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetLotteryDtlByLotteryTypeId(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetLotteryDtlByName(string LotteryName);
        #endregion

        #region Lottery Type
        [OperationContract]
        bool InsertInLotteryType(ClsLotteryType objLotteryType);

        [OperationContract]
        bool UpdateInLotteryType(ClsLotteryType objLotteryType);
        
        [OperationContract]
        DataTable GetLotteryTypeDtl();

        [OperationContract]
        DataTable GetLotteryTypeDtlById(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInLotteryType(Int64 InDataUniqueId);
        #endregion

        #region Government Order 
        [OperationContract]
        bool InsertInGovermentOrder(ClsGovermentOrder objGovermentOrder);

        [OperationContract]
        bool UpdateInGovermentOrder(ClsGovermentOrder objGovermentOrder);

        [OperationContract]
        DataTable GetGovermentOrderDtl();

        [OperationContract]
        DataTable GetGovermentOrderDtlById(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetGovOrderDtlByLotteryId(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInGovermentOrder(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetGovermentOrderDtlbyStatus(int InStatus);

        [OperationContract]
        DataTable GetLotteryByApprovedGovOrder(Int64 InDataUniqueId);
        #endregion

        #region Deposit To
        [OperationContract]
        DataTable GetDepositToDtl();
       
        #endregion

        #region User Role 
        [OperationContract]
        bool InsertInUserRole(ClsUserRole objUserRole, DataTable dtUserAccessMenuDtl);

        [OperationContract]
        bool UpdateInUserRole(ClsUserRole objUserRole, DataTable dtUserAccessMenuDtl);

        [OperationContract]
        DataTable GetUserRoleDtl();

        [OperationContract]
        DataTable GetUserRoleDtlById(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInUserRole(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetGetMenuListForUserRole(Int64 InUserRoleID);

        [OperationContract]
        DataTable GetNavMenuListForUserRoleId(Int64 InUserRoleID);
        #endregion

        #region Prize
        [OperationContract]
        bool InsertInPrize(ClsPrize objPrize,DataTable dtPrizeDtl);

        [OperationContract]
        bool UpdateInPrize(ClsPrize objPrize,DataTable dtPrizeDtl);

        [OperationContract]
        DataTable GetPrizeDtl();

        [OperationContract]
        DataSet GetPrizeDtlById(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInPrize(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetWinnePrizeWinningSlNoDtlById(Int64 InDataUniqueId, Int16 RowNo);

        [OperationContract]
        DataSet GetLotteryWiningSerialNoDtlByLotteryNo(Int64 InDataUniqueId, string LotteryNo);

        [OperationContract]
        bool UpdateApprovalInPrize(ClsPrize objClsPrize);
        #endregion
        
        #region PrizeWinner
        //[OperationContract]
        //bool InsertInPrizeWinner(ClsPrizeWinner objPrizeWinner, DataTable dtPrizeWinnerDtl);

        [OperationContract]
        bool UpdateInPrizeWinner(ClsPrizeWinner objPrizeWinner, DataTable dtPrizeWinnerDtl);
        //[OperationContract]
        //bool UpdateInPrizeClaim(ClsPrizeWinner objPrizeWinner, DataTable dtPrizeWinnerDtl);

        [OperationContract]
        DataTable GetWinneListDtlById(Int64 InDataUniqueId);

        //[OperationContract]        
        //DataTable GetPrizeWinnerDtlById(Int64 InDataUniqueId);

       
        //[OperationContract]
        //DataTable GetWinneEntryDtlByPrizeWinnerId(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetWinneEntryDtlByRequisitionId(Int64 InDataUniqueId);
        
        //[OperationContract]
        //DataTable GetLotteryDtlOfPrizeByLotteryTypeId(Int64 InDataUniqueId);

        //[OperationContract]
        //DataTable IsRecordExistForWinnersForLottery(Int64 InLotteryID, DateTime InDrawDate);

        [OperationContract]
        bool UpdateApprovalInReqWinnerPrize(ClsRequisition objRequisition, string ReqId);

        [OperationContract]
        DataSet GetWinnigSlNoDtlByRequisitionId(Int64 InDataUniqueId);
        #endregion

        #region User 
        [OperationContract]
        bool InsertInUser(ClsUser objUser);

        [OperationContract]
        bool UpdateInUser(ClsUser objUser);

        [OperationContract]
        DataTable GetUserDtl();

        [OperationContract]
        DataTable GetUserDtlById(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInUser(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetIsValidUser(string UserId, string UserPassword);


        [OperationContract]
        bool UpdateUserPassword(ClsUser objUser);

        [OperationContract]
        DataTable GetUserDtlByEmailOrMobile(string EmailId, string MobileNo);

        [OperationContract]
        DataTable GetIsMenuAccessAvailable(Int64 InUserRoleId, string InMenuCode);

        [OperationContract]
        DataTable GetUserDtlByUserId(string InUserId);
        #endregion

        #region Dealer Deposit
        [OperationContract]
        bool InsertInDealerDeposit(ClsDealerDeposit objDealerDeposit);
        [OperationContract]
        bool UpdateInDealerDeposit(ClsDealerDeposit objDealerDeposit);               
        [OperationContract]
        DataTable GetDealerDepositViewDtl(clsInputParameter objInputParameter);
        [OperationContract]
        DataTable GetDealerDepositDtlById(Int64 InDataUniqueId);
        [OperationContract]
        bool DeleteInDealerDeposit(Int64 InDataUniqueId);
        [OperationContract]
        DataTable SpGetDealerDepositReconDtlByStatus(string InReconStatus);
        [OperationContract]
        bool UpdateReconInDealerDeposit(ClsDealerDeposit objDealerDeposit);
        [OperationContract]
        DataTable GetDealerDepositViewForApprovalDtl(clsInputParameter objInputParameter, int DepositToId);
        [OperationContract]
        bool UpdateApprovalInDealerDeposit(ClsDealerDeposit objDealerDeposit, string ReqId);

        [OperationContract]
        bool UpdateApprovalInReqDealerDeposit(ClsRequisition objRequisition, string ReqId);

        [OperationContract]
        DataSet GetDealerDepositInHandDtlById(Int64 InDataUniqueId);
        [OperationContract]
        DataTable GetDealerDepositDtlByReqId(Int64 InRequsitionId, Int16 status);
        #endregion
             
        #region Payment Method Master
        [OperationContract]
        DataTable GetDepositMethodDetails();


       
         #endregion

        #region Email To
        [OperationContract]
        DataTable GetEmailToDtlByType(string EmailType);     
        #endregion

        #region Requisition
        [OperationContract]
        bool InsertInRequisition(ClsRequisition objRequisition, out string TransactionNo);

        [OperationContract]
        bool UpdateInRequisition(ClsRequisition objRequisition);

        [OperationContract]
        bool UpdateCloseTransactionInRequisition(ClsRequisition objRequisition);

        [OperationContract]
        bool UpdateSendForAdjustment(ClsRequisition objRequisition, string ReqId);

        [OperationContract]
        DataTable GetSentAdjustmentDtlByTranNo(Int32 DataUniqueId, int TransactionNo);

        [OperationContract]
        DataTable GetAdjustmentDtlByRequisitionId(Int32 DataUniqueId);

        [OperationContract]
        DataTable GetRequisitionDtl(clsInputParameter objInputParameter);

        [OperationContract]
        DataTable GetRequisitionDtlById(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInRequisition(Int64 InDataUniqueId);
        
        [OperationContract]
        DataTable GetRequisitionDtlByStatus(clsInputParameter objInputParameter);

        [OperationContract]
        DataTable GetRequisitiondataForDDL(int SaveStatus, int Opmode);

        [OperationContract]
        DataTable GetBankGuranteeLedger(clsInputParameter objInputParameter);

        [OperationContract]
        bool UpdateApprovalInRequisition(ClsRequisition objRequisition, string ReqId);

        [OperationContract]
        bool UpdateApprovalInRequisitionUnSoldTicket(ClsRequisition objRequisition, string ReqId);

        [OperationContract]
        bool UpdateApprovalInRequisitionUnClaimTicket(ClsRequisition objRequisition, string ReqId);

        [OperationContract]
        DataTable GetRequisitionDtlByStatusForDealerReq(int InStatus);

        [OperationContract]
        DataTable GetDirectorRequisitionDtlByDealerReqId(Int64 DataUniqueId);

        [OperationContract]
        DataTable GetLastDrawDateAndNoDtlByLotteryId(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetLotteryDtlFromRequisitionDtl(Int64 InLotteryId, int? InDrawNo, DateTime? InDrawDate);

        [OperationContract]
        DataSet GetLotteryDtlInClaimAndUnSold(Int64 InDataUniqueId, string InLotteryNo, int FnNo, string Alphabet, Int64 TnNo);

        [OperationContract]
        DataTable GetUnSoldSummaryById(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetPrintOrderDtlById(Int64 InDataUniqueId);
        #endregion

        #region Requisition Dealer
        [OperationContract]
        bool InsertInRequisitionDealer(ClsRequisition objRequisition,DataTable tblDirectorRequisitionDtl, out string TransactionNo);

        [OperationContract]
        bool UpdateInRequisitionDealer(ClsRequisition objRequisition,DataTable tblDirectorRequisitionDtl);

        [OperationContract]
        DataTable GetRequisitionDealerDtl(clsInputParameter objInputParameter);

        [OperationContract]
        DataTable GetRequisitionDealerDtlById(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInRequisitionDealer(Int64 InDataUniqueId);
        [OperationContract]
        DataTable GetRequisitionDealerDtlByStatus(clsInputParameter objInputParameter);

        [OperationContract]
        bool UpdateApprovalInRequisitionDealer(ClsRequisition objRequisition, string ReqId);
        #endregion

        #region Lottery Claim Entry
        [OperationContract]
        bool InserInLotteryClaimEntry(ClsLotteryClaimDetails objClsLotteryClaimDetails, out string TransactionNo);

        [OperationContract]
        DataTable GetLotteryClaimEntryDtlByReqCode(string ReqCode);

        [OperationContract]
        DataTable GetLotteryClaimEntryDtlByStatus(clsInputParameter objInputParameter, int InClaimType);

        [OperationContract]
        DataSet GetLotteryClaimEntryDtlByReqId(Int64 InDataUniqueId);

        [OperationContract]
        bool UpdateApprovalInLotteryClaimEntry(ClsLotteryClaimApprovalDetails objClsLotteryClaimApprovalDetails);

        [OperationContract]
        bool UpdateApprovalByStausInLotteryClaimEntry(ClsRequisition objRequisition, string ReqId, int ClaimType);

        [OperationContract]
        DataTable GetLotteryClaimSendToGovDtlByStatus(clsInputParameter objInputParameter, int InClaimType, int GovId);

        [OperationContract]
        DataTable GetClaimSendToGovApprovedDtl(clsInputParameter objInputParameter);

        [OperationContract]
        bool UpdateLotteryClaimEntrySendToGov(ClsRequisition objRequisition, string ReqId, out string TransactionNo );

        [OperationContract]
        DataTable GetSendToGovAnnextureIIIByID(Int64 InDataUniqueId);
        #endregion

        #region TicketInventory
        [OperationContract]
        bool InsertInTicketInventory(ClsTicketInventory objTicketInventory);
       
        [OperationContract]
        bool UpdateInTicketInventory(ClsTicketInventory objTicketInventory);
               
        [OperationContract]
        bool GenerateLotteryNumberDtlById(Int64 InDataUniqueId, int TicketStatus);

       
        #endregion

        #region Ticket Inventory Claimed
        [OperationContract]
        bool InserInTicketInventoryClaimed(ClsLottery objClsLottery, DataTable IntblTicketNo);

        [OperationContract]
        DataTable GetVariableClaimPrizeById(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetVariableClaimByClaimId(Int64 InDataUniqueId);
        
        [OperationContract]
        DataTable GetVariableClaimVoucherDtlById(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInVariableClaimByVoucherId(Int64 InDataUniqueId);

        [OperationContract]
        bool UpdateApprovalInVariableClaim(ClsRequisition objRequisition, string ReqId, DataTable IntblTicketNo);
        #endregion

        #region Ticket Inventory Un-Sold
        [OperationContract]
        bool InserInTicketInventoryUnSold(ClsLottery objClsLottery, DataTable IntblTicketNo,int SaveStatus);

        [OperationContract]
        DataTable GetLotteryUnsold(Int64 InDataUniqueId);


        #endregion

        #region Ticket Transaction
        [OperationContract]
        DataTable GetRequisitionTransactionDtlById(Int64 InDataUniqueId);        
        #endregion

        #region Update In Requisition Challan
        [OperationContract]
        bool UpdateInRequisitionChallan(ClsChallan objChallan);

        [OperationContract]
        bool InserInRequisitionChallan(ClsChallan objChallan, out string TransactionNo);
                    
        [OperationContract]
        DataTable GetRequisitionChallanDtlById(Int64 InDataUniqueId);

        [OperationContract]
        DataTable GetChallanDtlById(Int64 InDataUniqueId);

        [OperationContract]
        bool DeleteInChallanById(Int64 InDataUniqueId);

        [OperationContract]
        bool UpdateApprovalInRequisitionChallan(ClsChallan objChallan, string ReqId);
        #endregion
        // TODO: Add your service operations here

        #region MIS Report
        [OperationContract]
        DataTable GetVariableIncentiveByReqId(Int64 InDataUniqueId, string ClaimType);

        [OperationContract]
        DataTable GetDealerListOfTransactionDtl();

        [OperationContract]
        DataSet GetDealerTransactionDtlByReqId(Int64 InDataUniqueId);
        
        [OperationContract]
        DataTable GetProfitLossDtlByReqId(Int64 InDataUniqueId);
        #endregion

        #region [New Series Generation]
        [OperationContract]
        bool InsertInSeriesGeneration(ClsSeriesGeneration obj, out string InsertedID);

        [OperationContract]
        bool UpdateInSeriesGeneration(ClsSeriesGeneration obj);

        [OperationContract]
        DataTable GetSeriesGenerationAll();

        [OperationContract]
        DataTable GetSeriesGenerationById(long ID);

        [OperationContract]
        DataTable GetSeriesGenerationByReqId(int ReqId);

        [OperationContract]
        DataTable GetSeriesGenerationByReqIdSpecific(int ReqId, long Id);

        [OperationContract]
        DataTable GetSeriesGenerationListByReqId(int ReqId);

        [OperationContract]
        DataTable GetSeriesGenerationByReqIdAndSr1End(int ReqId, long Sr1End);

        [OperationContract]
        bool DeleteInSeriesGeneration(long ID);
        #endregion [New Series Generation]

        #region [ZipProgress]
        [OperationContract]
        bool InsertInZipProgress(ZipProgressMaster objZipProgress, out string ZipID);
        [OperationContract]
        bool UpdateInZipProgress(ZipProgressMaster objZipProgress);
        [OperationContract]
        bool DeleteInZipProgress(long ID);
        [OperationContract]
        DataTable GetZipProgressById(long ID);
        #endregion [ZipProgress]
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
