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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : ILTMSService
    {
        BusinessLogicDbTrx objBusinessLogicDbTrx = new BusinessLogicDbTrx();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        #region Lottery 
        public bool InsertInLottery(ClsLottery objLottery)
        {
            return objBusinessLogicDbTrx.InsertInLottery(objLottery);
        }
        public bool UpdateInLottery(ClsLottery objLottery)
        {
            return objBusinessLogicDbTrx.UpdateInLottery(objLottery);
        }
        public bool DeleteInLottery(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.DeleteInLottery(InDataUniqueId);
        }
        public DataTable GetLotteryDtl()
        {
            return objBusinessLogicDbTrx.GetLotteryDtl();
        }
        public DataTable GetLotteryDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetLotteryDtlById(InDataUniqueId);
        }
        public DataTable GetLotteryDtlByLotteryTypeId(Int64 InDataUniqueId)
        { 
            return objBusinessLogicDbTrx.GetLotteryDtlByLotteryTypeId(InDataUniqueId);
        }
        public DataTable GetLotteryDtlByName(string LotteryName)
        {
            return objBusinessLogicDbTrx.GetLotteryDtlByName(LotteryName);
        }
        #endregion

        #region Lottery Type
        public bool InsertInLotteryType(ClsLotteryType objLotteryType)
        {
            return objBusinessLogicDbTrx.InsertInLotteryType(objLotteryType);
        }

        public bool UpdateInLotteryType(ClsLotteryType objLotteryType)
        {
            return objBusinessLogicDbTrx.UpdateInLotteryType(objLotteryType);
        }

        public bool DeleteInLotteryType(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.DeleteInLotteryType(InDataUniqueId);
        }

        public DataTable GetLotteryTypeDtl()
        {
            return objBusinessLogicDbTrx.GetLotteryTypeDtl();
        }

        public DataTable GetLotteryTypeDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetLotteryTypeDtlById(InDataUniqueId);
        }

        #endregion

        #region Government Order
        public bool InsertInGovermentOrder(ClsGovermentOrder objGovermentOrder)
        {
            return objBusinessLogicDbTrx.InsertInGovermentOrder(objGovermentOrder);
        }

        public bool UpdateInGovermentOrder(ClsGovermentOrder objGovermentOrder)
        {
            return objBusinessLogicDbTrx.UpdateInGovermentOrder(objGovermentOrder);
        }

        public bool DeleteInGovermentOrder(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.DeleteInGovermentOrder(InDataUniqueId);
        }

        public DataTable GetGovermentOrderDtl()
        {
            return objBusinessLogicDbTrx.GetGovermentOrderDtl();
        }

        public DataTable GetGovermentOrderDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetGovermentOrderDtlById(InDataUniqueId);
        }
        public DataTable GetGovOrderDtlByLotteryId(Int64 InDataUniqueId)
        { 
            return objBusinessLogicDbTrx.GetGovOrderDtlByLotteryId(InDataUniqueId);
        }
        public DataTable GetGovermentOrderDtlbyStatus(int InStatus)
        { 
            return objBusinessLogicDbTrx.GetGovermentOrderDtlbyStatus(InStatus);
        }

        public DataTable GetLotteryByApprovedGovOrder(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetLotteryByApprovedGovOrder(InDataUniqueId);
        }
        #endregion

        #region Deposit To
        public DataTable GetDepositToDtl() {
            return objBusinessLogicDbTrx.GetDepositToDtl();
        }

        #endregion

        #region User Role
        public bool InsertInUserRole(ClsUserRole objUserRole, DataTable dtUserAccessMenuDtl)
        {
            return objBusinessLogicDbTrx.InsertInUserRole(objUserRole, dtUserAccessMenuDtl);
        }

        public bool UpdateInUserRole(ClsUserRole objUserRole, DataTable dtUserAccessMenuDtl)
        {
            return objBusinessLogicDbTrx.UpdateInUserRole(objUserRole,dtUserAccessMenuDtl);
        }

        public bool DeleteInUserRole(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.DeleteInUserRole(InDataUniqueId);
        }

        public DataTable GetUserRoleDtl()
        {
            return objBusinessLogicDbTrx.GetUserRoleDtl();
        }

        public DataTable GetUserRoleDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetUserRoleDtlById(InDataUniqueId);
        }
        public DataTable GetGetMenuListForUserRole(Int64 InUserRoleID)
        { 
            return objBusinessLogicDbTrx.GetGetMenuListForUserRole(InUserRoleID);
        }
        public DataTable GetNavMenuListForUserRoleId(Int64 InUserRoleID)
        { 
            return objBusinessLogicDbTrx.GetNavMenuListForUserRoleId(InUserRoleID);
        }
        #endregion

        #region Prize
        public bool InsertInPrize(ClsPrize objPrize,DataTable dtPrizeDtl)
        {
            return objBusinessLogicDbTrx.InsertInPrize(objPrize, dtPrizeDtl);
        }
        public bool UpdateInPrize(ClsPrize objPrize,DataTable dtPrizeDtl)
        {
            return objBusinessLogicDbTrx.UpdateInPrize(objPrize, dtPrizeDtl);
        }
        public bool DeleteInPrize(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.DeleteInPrize(InDataUniqueId);
        }
        public DataTable GetPrizeDtl()
        {
            return objBusinessLogicDbTrx.GetPrizeDtl();
        }
        public DataSet GetPrizeDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetPrizeDtlById(InDataUniqueId);
        }

        public DataTable GetWinnePrizeWinningSlNoDtlById(Int64 InDataUniqueId, Int16 RowNo)
        {
            return objBusinessLogicDbTrx.GetWinnePrizeWinningSlNoDtlById(InDataUniqueId, RowNo);
        }

        public DataSet GetLotteryWiningSerialNoDtlByLotteryNo(Int64 InDataUniqueId, string LotteryNo)
        {
            return objBusinessLogicDbTrx.GetLotteryWiningSerialNoDtlByLotteryNo(InDataUniqueId, LotteryNo);
        }

        public bool UpdateApprovalInPrize(ClsPrize objClsPrize)
        {
            return objBusinessLogicDbTrx.UpdateApprovalInPrize(objClsPrize);
        }
        #endregion

        #region PrizeWinner
        //public bool InsertInPrizeWinner(ClsPrizeWinner objPrizeWinner, DataTable dtPrizeWinnerDtl)
        //{
        //    return objBusinessLogicDbTrx.InsertInPrizeWinner(objPrizeWinner, dtPrizeWinnerDtl);
        //}
        public bool UpdateInPrizeWinner(ClsPrizeWinner objPrizeWinner, DataTable dtPrizeWinnerDtl)
        {
            return objBusinessLogicDbTrx.UpdateInPrizeWinner(objPrizeWinner, dtPrizeWinnerDtl);
        }

        //public bool UpdateInPrizeClaim(ClsPrizeWinner objPrizeWinner, DataTable dtPrizeWinnerDtl)
        //{
        //    return objBusinessLogicDbTrx.UpdateInPrizeClaim(objPrizeWinner, dtPrizeWinnerDtl);
        //}

      
        public DataTable GetWinneListDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetWinneListDtlById(InDataUniqueId);
        }

        //public DataTable GetPrizeWinnerDtlById(Int64 InDataUniqueId)
        //{
        //    return objBusinessLogicDbTrx.GetPrizeWinnerDtlById(InDataUniqueId);
        //}
        //public DataTable GetWinneEntryDtlByPrizeWinnerId(Int64 InDataUniqueId)
        //{
        //    return objBusinessLogicDbTrx.GetWinneEntryDtlByPrizeWinnerId(InDataUniqueId);
        //}
        public DataTable GetWinneEntryDtlByRequisitionId(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetWinneEntryDtlByRequisitionId(InDataUniqueId);
        }
        
        //public DataTable GetLotteryDtlOfPrizeByLotteryTypeId(Int64 InDataUniqueId)
        //{
        //    return objBusinessLogicDbTrx.GetLotteryDtlOfPrizeByLotteryTypeId(InDataUniqueId);
        //}
        //public DataTable IsRecordExistForWinnersForLottery(Int64 InLotteryID, DateTime InDrawDate)
        //{
        //    return objBusinessLogicDbTrx.IsRecordExistForWinnersForLottery(InLotteryID, InDrawDate);
        //}

        public bool UpdateApprovalInReqWinnerPrize(ClsRequisition objRequisition, string ReqId)
        {
            return objBusinessLogicDbTrx.UpdateApprovalInReqWinnerPrize(objRequisition, ReqId);
        }
        public DataSet GetWinnigSlNoDtlByRequisitionId(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetWinnigSlNoDtlByRequisitionId(InDataUniqueId);
        }
        public bool UpdateApprovalInVariableClaim(ClsRequisition objRequisition, string ReqId, DataTable IntblTicketNo)
        { 
            return objBusinessLogicDbTrx.UpdateApprovalInVariableClaim( objRequisition,  ReqId,  IntblTicketNo);
        }
        #endregion

        #region User
        public bool InsertInUser(ClsUser objUser)
        {
            return objBusinessLogicDbTrx.InsertInUser(objUser);
        }

        public bool UpdateInUser(ClsUser objUser)
        {
            return objBusinessLogicDbTrx.UpdateInUser(objUser);
        }

        public bool DeleteInUser(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.DeleteInUser(InDataUniqueId);
        }

        public DataTable GetUserDtl()
        {
            return objBusinessLogicDbTrx.GetUserDtl();
        }

        public DataTable GetUserDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetUserDtlById(InDataUniqueId);
        }
        public DataTable GetIsValidUser(string UserId, string UserPassword)
        {
            return objBusinessLogicDbTrx.GetIsValidUser(UserId, UserPassword);
        }
        public DataTable GetUserDtlByEmailOrMobile(string EmailId, string MobileNo)
        {
            return objBusinessLogicDbTrx.GetUserDtlByEmailOrMobile(EmailId, MobileNo);
        }
        public bool UpdateUserPassword(ClsUser objUser)
        {
            return objBusinessLogicDbTrx.UpdateUserPassword(objUser);
        }
        public DataTable GetIsMenuAccessAvailable(Int64 InUserRoleId, string InMenuCode)
        {
            return objBusinessLogicDbTrx.GetIsMenuAccessAvailable(InUserRoleId, InMenuCode);
        }

        public DataTable GetUserDtlByUserId(string InUserId)
        {
            return objBusinessLogicDbTrx.GetUserDtlByUserId(InUserId);
        }
        #endregion

        #region Email To
        public DataTable GetEmailToDtlByType(string EmailType)
        {
            return objBusinessLogicDbTrx.GetEmailToDtlByType(EmailType);
        }
        #endregion

        #region Dealer Deposit
        public bool InsertInDealerDeposit(ClsDealerDeposit objDealerDeposit) {
            return objBusinessLogicDbTrx.InsertInDealerDeposit(objDealerDeposit);
        }
        public bool UpdateInDealerDeposit(ClsDealerDeposit objDealerDeposit)
        {
            return objBusinessLogicDbTrx.UpdateInDealerDeposit(objDealerDeposit);
        }
        public bool DeleteInDealerDeposit(Int64 InDataUniqueId)
        { 
            return objBusinessLogicDbTrx.DeleteInDealerDeposit(InDataUniqueId);
        }
        public DataTable GetDealerDepositViewDtl(clsInputParameter objInputParameter)
        {
            return objBusinessLogicDbTrx.GetDealerDepositViewDtl(objInputParameter);
        }
        public DataTable GetDealerDepositDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetDealerDepositDtlById(InDataUniqueId);
        }
        public DataTable SpGetDealerDepositReconDtlByStatus(string InReconStatus)
        {
            return objBusinessLogicDbTrx.SpGetDealerDepositReconDtlByStatus(InReconStatus);
        }
        public bool UpdateReconInDealerDeposit(ClsDealerDeposit objDealerDeposit)
        {
            return objBusinessLogicDbTrx.UpdateReconInDealerDeposit(objDealerDeposit);
        }
        public DataTable GetDealerDepositViewForApprovalDtl(clsInputParameter objInputParameter, int DepositToId)
        {
            return objBusinessLogicDbTrx.GetDealerDepositViewForApprovalDtl(objInputParameter, DepositToId);
        }
        public bool UpdateApprovalInDealerDeposit(ClsDealerDeposit objDealerDeposit, string ReqId)
        {
            return objBusinessLogicDbTrx.UpdateApprovalInDealerDeposit(objDealerDeposit, ReqId);
        }

        public bool UpdateApprovalInReqDealerDeposit(ClsRequisition objRequisition, string ReqId)
        { 
            return  objBusinessLogicDbTrx.UpdateApprovalInReqDealerDeposit(objRequisition, ReqId);
        }
        public DataSet GetDealerDepositInHandDtlById(Int64 InDataUniqueId)
        { 
            return  objBusinessLogicDbTrx.GetDealerDepositInHandDtlById(InDataUniqueId);
        }
        public DataTable GetDealerDepositDtlByReqId(Int64 InRequsitionId, Int16 status)
        {
            return objBusinessLogicDbTrx.GetDealerDepositDtlByReqId( InRequsitionId, status);
        }
        #endregion

        #region Payment Method Master
        public DataTable GetDepositMethodDetails()
        {
            return objBusinessLogicDbTrx.GetDepositMethodDetails();
        }
        #endregion

        #region Requisition
        public bool InsertInRequisition(ClsRequisition objRequisition, out string TransactionNo)
        {
            return objBusinessLogicDbTrx.InsertInRequisition(objRequisition, out TransactionNo);
        }
        public bool UpdateInRequisition(ClsRequisition objRequisition)
        {
            return objBusinessLogicDbTrx.UpdateInRequisition(objRequisition);
        }
        public bool UpdateCloseTransactionInRequisition(ClsRequisition objRequisition)
        { 
            return  objBusinessLogicDbTrx.UpdateCloseTransactionInRequisition(objRequisition);
        }
        public bool UpdateSendForAdjustment(ClsRequisition objRequisition, string ReqId)
        {

            return objBusinessLogicDbTrx.UpdateSendForAdjustment( objRequisition,  ReqId);
        }

        public DataTable GetSentAdjustmentDtlByTranNo(Int32 DataUniqueId, int TransactionNo)
        { 
            return  objBusinessLogicDbTrx.GetSentAdjustmentDtlByTranNo( DataUniqueId, TransactionNo);
        }

        public bool DeleteInRequisition(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.DeleteInRequisition(InDataUniqueId);
        }
        public DataTable GetRequisitionDtl(clsInputParameter objInputParameter)
        {
            return objBusinessLogicDbTrx.GetRequisitionDtl(objInputParameter);
        }
        public DataTable GetRequisitionDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetRequisitionDtlById(InDataUniqueId);
        }
        public DataTable GetRequisitionDtlByStatus(clsInputParameter objInputParameter)
        {
            return objBusinessLogicDbTrx.GetRequisitionDtlByStatus(objInputParameter);
        }
        public DataTable GetRequisitiondataForDDL(int SaveStatus, int Opmode)
        {
            return objBusinessLogicDbTrx.GetRequisitiondataForDDL(SaveStatus, Opmode);
        }

        public DataTable GetBankGuranteeLedger(clsInputParameter objInputParameter)
        {
            return objBusinessLogicDbTrx.GetBankGuranteeLedger(objInputParameter);
        }
        public DataTable GetAdjustmentDtlByRequisitionId(Int32 DataUniqueId)
        {
            return objBusinessLogicDbTrx.GetAdjustmentDtlByRequisitionId(DataUniqueId);
        }

        public bool UpdateApprovalInRequisition(ClsRequisition objRequisition, string ReqId)
        {
            return objBusinessLogicDbTrx.UpdateApprovalInRequisition(objRequisition, ReqId);
        }

        public bool UpdateApprovalInRequisitionUnSoldTicket(ClsRequisition objRequisition, string ReqId)
        { 
          return objBusinessLogicDbTrx.UpdateApprovalInRequisitionUnSoldTicket(objRequisition, ReqId);
        }

        public bool UpdateApprovalInRequisitionUnClaimTicket(ClsRequisition objRequisition, string ReqId)
        {
            return objBusinessLogicDbTrx.UpdateApprovalInRequisitionUnClaimTicket(objRequisition, ReqId);
        }
        public DataTable GetLastDrawDateAndNoDtlByLotteryId(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetLastDrawDateAndNoDtlByLotteryId(InDataUniqueId);
        
        }

        public DataTable GetLotteryDtlFromRequisitionDtl(Int64 InLotteryId, int? InDrawNo, DateTime? InDrawDate)
        { 
            return objBusinessLogicDbTrx.GetLotteryDtlFromRequisitionDtl( InLotteryId,  InDrawNo,  InDrawDate);
        }
        public DataSet GetLotteryDtlInClaimAndUnSold(Int64 InDataUniqueId, string InLotteryNo,int FnNo, string Alphabet,Int64 TnNo)
        {
            return objBusinessLogicDbTrx.GetLotteryDtlInClaimAndUnSold( InDataUniqueId,  InLotteryNo, FnNo,  Alphabet, TnNo);
        }

        public DataTable GetUnSoldSummaryById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetUnSoldSummaryById(InDataUniqueId);
        }

        //public DataTable GetPrintOrderDtlById(Int64 InDataUniqueId)
        //{
        //    return objBusinessLogicDbTrx.GetUnSoldSummaryById(InDataUniqueId);
        //}

        public DataTable GetPrintOrderDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetPrintOrderDtlById(InDataUniqueId);
        }

        #endregion

        #region Requisition Dealer
        public bool InsertInRequisitionDealer(ClsRequisition objRequisition,DataTable tblDirectorRequisitionDtl, out string TransactionNo)
        {
            return objBusinessLogicDbTrx.InsertInRequisitionDealer(objRequisition,tblDirectorRequisitionDtl, out TransactionNo);
        }

        public bool UpdateInRequisitionDealer(ClsRequisition objRequisition,DataTable tblDirectorRequisitionDtl)
        {
            return objBusinessLogicDbTrx.UpdateInRequisitionDealer(objRequisition,tblDirectorRequisitionDtl);
        }

        public bool DeleteInRequisitionDealer(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.DeleteInRequisitionDealer(InDataUniqueId);
        }

        public DataTable GetRequisitionDealerDtl(clsInputParameter objInputParameter)
        {
            return objBusinessLogicDbTrx.GetRequisitionDealerDtl(objInputParameter);
        }

        public DataTable GetRequisitionDealerDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetRequisitionDealerDtlById(InDataUniqueId);
        }
        public DataTable GetRequisitionDealerDtlByStatus(clsInputParameter objInputParameter)
        {
            return objBusinessLogicDbTrx.GetRequisitionDealerDtlByStatus(objInputParameter);
        }

        public bool UpdateApprovalInRequisitionDealer(ClsRequisition objRequisition, string ReqId)
        {
            return objBusinessLogicDbTrx.UpdateApprovalInRequisitionDealer(objRequisition, ReqId);
        }

        public DataTable GetRequisitionDtlByStatusForDealerReq(int InStatus)
        { 
            return objBusinessLogicDbTrx.GetRequisitionDtlByStatusForDealerReq(InStatus);
        }

        public DataTable GetDirectorRequisitionDtlByDealerReqId(Int64 DataUniqueId)
        {
            return objBusinessLogicDbTrx.GetDirectorRequisitionDtlByDealerReqId(DataUniqueId);
        }
        #endregion

        #region Lottery Claim Entry
        public bool InserInLotteryClaimEntry(ClsLotteryClaimDetails objClsLotteryClaimDetails, out string TransactionNo)
        {
            return objBusinessLogicDbTrx.InserInLotteryClaimEntry(objClsLotteryClaimDetails,out TransactionNo);
        }

        public DataTable GetLotteryClaimEntryDtlByReqCode(string ReqCode)
        {
            return objBusinessLogicDbTrx.GetLotteryClaimEntryDtlByReqCode(ReqCode);
        }
        public DataTable GetLotteryClaimEntryDtlByStatus(clsInputParameter objInputParameter, int InClaimType)
        {
            return objBusinessLogicDbTrx.GetLotteryClaimEntryDtlByStatus(objInputParameter, InClaimType);
        }
        public DataSet GetLotteryClaimEntryDtlByReqId(Int64 InDataUniqueId)
        { 
            return objBusinessLogicDbTrx.GetLotteryClaimEntryDtlByReqId(InDataUniqueId);
        }

        public bool UpdateApprovalInLotteryClaimEntry(ClsLotteryClaimApprovalDetails objClsLotteryClaimApprovalDetails)
        { 
            return objBusinessLogicDbTrx.UpdateApprovalInLotteryClaimEntry(objClsLotteryClaimApprovalDetails);
        }
        public bool UpdateApprovalByStausInLotteryClaimEntry(ClsRequisition objRequisition, string ReqId, int ClaimType)
        {
            return objBusinessLogicDbTrx.UpdateApprovalByStausInLotteryClaimEntry(objRequisition, ReqId, ClaimType);
        }
        public DataTable GetLotteryClaimSendToGovDtlByStatus(clsInputParameter objInputParameter, int InClaimType, int GovId)
        {
            return objBusinessLogicDbTrx.GetLotteryClaimSendToGovDtlByStatus(objInputParameter, InClaimType, GovId);
        }

        public DataTable GetClaimSendToGovApprovedDtl(clsInputParameter objInputParameter)
        {
            return objBusinessLogicDbTrx.GetClaimSendToGovApprovedDtl(objInputParameter);
        }

        public bool UpdateLotteryClaimEntrySendToGov(ClsRequisition objRequisition, string ReqId, out string TransactionNo)
        {
            return objBusinessLogicDbTrx.UpdateLotteryClaimEntrySendToGov(objRequisition, ReqId, out TransactionNo);
        }

        public DataTable GetSendToGovAnnextureIIIByID(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetSendToGovAnnextureIIIByID(InDataUniqueId);
        }
        #endregion

        #region Ticket Inventory
        public bool InsertInTicketInventory(ClsTicketInventory objTicketInventory)
        {
            return objBusinessLogicDbTrx.InsertInTicketInventory(objTicketInventory);
        }

        public bool UpdateInTicketInventory(ClsTicketInventory objTicketInventory)
        {
            return objBusinessLogicDbTrx.UpdateInTicketInventory(objTicketInventory);
        }       

        public bool GenerateLotteryNumberDtlById(Int64 InDataUniqueId, int TicketStatus)
        {
            return objBusinessLogicDbTrx.GenerateLotteryNumberDtlById(InDataUniqueId, TicketStatus);
        }

       
        
        #endregion

        #region Ticket Inventory Claimed
        public bool InserInTicketInventoryClaimed(ClsLottery objClsLottery, DataTable IntblTicketNo)
        {
            return objBusinessLogicDbTrx.InserInTicketInventoryClaimed(objClsLottery, IntblTicketNo);
        }
        public DataTable GetVariableClaimPrizeById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetVariableClaimPrizeById(InDataUniqueId);
        }
        public DataTable GetVariableClaimByClaimId(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetVariableClaimByClaimId(InDataUniqueId);
        }        
        public DataTable GetVariableClaimVoucherDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetVariableClaimVoucherDtlById(InDataUniqueId);
        }
        public bool DeleteInVariableClaimByVoucherId(Int64 InDataUniqueId)
        { 
            return objBusinessLogicDbTrx.DeleteInVariableClaimByVoucherId(InDataUniqueId);
        }
        #endregion

        #region Ticket Inventory Un- Sold
        public bool InserInTicketInventoryUnSold(ClsLottery objClsLottery, DataTable IntblTicketNo, int SaveStatus)
        {
            return objBusinessLogicDbTrx.InserInTicketInventoryUnSold(objClsLottery, IntblTicketNo,  SaveStatus);
        }

        public DataTable GetLotteryUnsold(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetLotteryUnsold( InDataUniqueId);
        }
        #endregion

        #region Ticket Transaction
        public DataTable GetRequisitionTransactionDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetRequisitionTransactionDtlById(InDataUniqueId);
        }
        #endregion

        #region Update In Requisition Challan
        public bool UpdateInRequisitionChallan(ClsChallan objChallan)
        {
            return objBusinessLogicDbTrx.UpdateInRequisitionChallan(objChallan);
        }

        public bool InserInRequisitionChallan(ClsChallan objChallan, out string TransactionNo)
        {
            return objBusinessLogicDbTrx.InserInRequisitionChallan(objChallan, out TransactionNo);
        }
        public DataTable GetRequisitionChallanDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetRequisitionChallanDtlById(InDataUniqueId);
        }
        public DataTable GetChallanDtlById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.GetChallanDtlById(InDataUniqueId);
        }

        public bool DeleteInChallanById(Int64 InDataUniqueId)
        {
            return objBusinessLogicDbTrx.DeleteInChallanById(InDataUniqueId);
        }
        public bool UpdateApprovalInRequisitionChallan(ClsChallan objChallan, string ReqId)
        {
            return objBusinessLogicDbTrx.UpdateApprovalInRequisitionChallan(objChallan, ReqId);
        }
        #endregion

        #region MIS Report
        public DataTable GetVariableIncentiveByReqId(Int64 InDataUniqueId, string ClaimType)
        {
            return objBusinessLogicDbTrx.GetVariableIncentiveByReqId(InDataUniqueId, ClaimType);
        }

        public DataSet GetDealerTransactionDtlByReqId(Int64 InDataUniqueId)
        { 
            return objBusinessLogicDbTrx.GetDealerTransactionDtlByReqId(InDataUniqueId);
        }

        public DataTable GetDealerListOfTransactionDtl()
        {
            return objBusinessLogicDbTrx.GetDealerListOfTransactionDtl();
        }
        
        public DataTable GetProfitLossDtlByReqId(Int64 InDataUniqueId)
        { 
            return objBusinessLogicDbTrx.GetProfitLossDtlByReqId(InDataUniqueId);
        }
        #endregion

        #region [New Series Generation]
        public bool InsertInSeriesGeneration(ClsSeriesGeneration obj, out string InsertedID)
        {
            return objBusinessLogicDbTrx.InsertInSeriesGeneration(obj, out InsertedID);
        }
        public bool UpdateInSeriesGeneration(ClsSeriesGeneration obj)
        {
            return objBusinessLogicDbTrx.UpdateInSeriesGeneration(obj);
        }
        public bool DeleteInSeriesGeneration(long ID)
        {
            return objBusinessLogicDbTrx.DeleteInSeriesGeneration(ID);
        }
        public DataTable GetSeriesGenerationAll()
        {
            return objBusinessLogicDbTrx.GetSeriesGenerationAll();
        }
        public DataTable GetSeriesGenerationById(long ID)
        {
            return objBusinessLogicDbTrx.GetSeriesGenerationById(ID);
        }
        public DataTable GetSeriesGenerationByReqId(int ReqId)
        {
            return objBusinessLogicDbTrx.GetSeriesGenerationByReqId(ReqId);
        }
        public DataTable GetSeriesGenerationByReqIdSpecific(int ReqId, long Id)
        {
            return objBusinessLogicDbTrx.GetSeriesGenerationByReqIdSpecific(ReqId, Id);
        }
        public DataTable GetSeriesGenerationListByReqId(int ReqId)
        {
            return objBusinessLogicDbTrx.GetSeriesGenerationListByReqId(ReqId);
        }
        public DataTable GetSeriesGenerationByReqIdAndSr1End(int ReqId, long Sr1End)
        {
            return objBusinessLogicDbTrx.GetSeriesGenerationByReqIdAndSr1End(ReqId, Sr1End);
        }
        #endregion [New Series Generation]

        #region [ZipProgress]
        public bool InsertInZipProgress(ZipProgressMaster objZipProgress, out string ZipID)
        {
            return objBusinessLogicDbTrx.InsertInZipProgress(objZipProgress, out ZipID);
        }
        public bool UpdateInZipProgress(ZipProgressMaster objZipProgress)
        {
            return objBusinessLogicDbTrx.UpdateInZipProgress(objZipProgress);
        }
        public bool DeleteInZipProgress(long ID)
        {
            return objBusinessLogicDbTrx.DeleteInZipProgress(ID);
        }
        public DataTable GetZipProgressById(long ID)
        {
            return objBusinessLogicDbTrx.GetZipProgressById(ID);
        }
        #endregion [ZipProgress]
    }
} 
