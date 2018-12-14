using LTMSClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LTMSService
{

    public class BusinessLogicDbTrx
    {
        DataBaseUtility objDbUlility = new DataBaseUtility();

        #region Payment Method Master
        public DataTable GetDepositMethodDetails()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDepositMethod");
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion

        #region Lottery 
        public bool InsertInLottery(ClsLottery objLottery)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInLottery");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InLotteryName", objLottery.LotteryName);
                cmd.Parameters.AddWithValue("@InLotteryTypeId", objLottery.LotteryTypeId);
                cmd.Parameters.AddWithValue("@InNoOfDigit", objLottery.NoOfDigit);
                cmd.Parameters.AddWithValue("@InSyndicateRate", objLottery.SyndicateRate);
                cmd.Parameters.AddWithValue("@InRateForSpl", objLottery.RateForSpl);
                cmd.Parameters.AddWithValue("@InTotTicketRate", objLottery.TotTicketRate);
                cmd.Parameters.AddWithValue("@InGstRate", objLottery.GstRate);
                cmd.Parameters.AddWithValue("@InPrizeCategory", objLottery.PrizeCategory);
                cmd.Parameters.AddWithValue("@InIncludeConsPrize", objLottery.IncludeConsPrize);
                cmd.Parameters.AddWithValue("@InClaimDays", objLottery.ClaimDays);
                cmd.Parameters.AddWithValue("@InClaimDaysVariable", objLottery.ClaimDaysVariable);
                cmd.Parameters.AddWithValue("@InSizeOfTicket", objLottery.SizeOfTicket);
                cmd.Parameters.AddWithValue("@InPaperQuality", objLottery.PaperQuality); 
                cmd.Parameters.AddWithValue("@InUserId", objLottery.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objLottery.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateInLottery(ClsLottery objLottery)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInLottery");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objLottery.DataUniqueId);
                cmd.Parameters.AddWithValue("@InLotteryName", objLottery.LotteryName);
                cmd.Parameters.AddWithValue("@InLotteryTypeId", objLottery.LotteryTypeId);
                cmd.Parameters.AddWithValue("@InNoOfDigit", objLottery.NoOfDigit);
                cmd.Parameters.AddWithValue("@InSyndicateRate", objLottery.SyndicateRate);
                cmd.Parameters.AddWithValue("@InRateForSpl", objLottery.RateForSpl);
                cmd.Parameters.AddWithValue("@InTotTicketRate", objLottery.TotTicketRate);
                cmd.Parameters.AddWithValue("@InGstRate", objLottery.GstRate);
                cmd.Parameters.AddWithValue("@InPrizeCategory", objLottery.PrizeCategory);
                cmd.Parameters.AddWithValue("@InIncludeConsPrize", objLottery.IncludeConsPrize);
                cmd.Parameters.AddWithValue("@InClaimDays", objLottery.ClaimDays);
                cmd.Parameters.AddWithValue("@InClaimDaysVariable", objLottery.ClaimDaysVariable);
                cmd.Parameters.AddWithValue("@InSizeOfTicket", objLottery.SizeOfTicket);
                cmd.Parameters.AddWithValue("@InPaperQuality", objLottery.PaperQuality); 
                cmd.Parameters.AddWithValue("@InUserId", objLottery.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objLottery.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool DeleteInLottery(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInLottery");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLotteryDtl()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLotteryDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLotteryDtlByLotteryTypeId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryDtlByLotteryTypeId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLotteryDtlByName(string LotteryName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryDtlByName");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InLotteryName", LotteryName);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion
        
        #region Lottery Type
        public bool InsertInLotteryType(ClsLotteryType objLotteryType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInLotteryType");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InLotteryType", objLotteryType.LotteryType);
                cmd.Parameters.AddWithValue("@InUserId", objLotteryType.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objLotteryType.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateInLotteryType(ClsLotteryType objLotteryType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInLotteryType");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objLotteryType.DataUniqueId);
                cmd.Parameters.AddWithValue("@InLotteryType", objLotteryType.LotteryType);
                cmd.Parameters.AddWithValue("@InUserId", objLotteryType.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objLotteryType.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool DeleteInLotteryType(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInLotteryType");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetLotteryTypeDtl()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryTypeDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLotteryTypeDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryTypeDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        #endregion

        #region Government Order
        public bool InsertInGovermentOrder(ClsGovermentOrder objGovermentOrder)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInGovermentOrder");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InGovermentOrder", objGovermentOrder.GovermentOrder);
                cmd.Parameters.AddWithValue("@InUnSoldPercentage", objGovermentOrder.UnSoldPercentage);
                cmd.Parameters.AddWithValue("@InNoOfAlphabet", objGovermentOrder.NoOfAlphabet);
                cmd.Parameters.AddWithValue("@InAlphabetName", objGovermentOrder.AlphabetName);
                cmd.Parameters.AddWithValue("@InTicketSlNoFrom", objGovermentOrder.TicketSeriallNoFrom);
                cmd.Parameters.AddWithValue("@InTicketSlNoTo", objGovermentOrder.TicketSerialNoTo);
                cmd.Parameters.AddWithValue("@InTotalTickets", objGovermentOrder.TotalTickets);
                cmd.Parameters.AddWithValue("@InLotteryId", objGovermentOrder.LotteryId);
                cmd.Parameters.AddWithValue("@InUserId", objGovermentOrder.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objGovermentOrder.IpAdd);
                cmd.Parameters.AddWithValue("@ModifiedLotteryName", objGovermentOrder.ModifiedLotteryName);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateInGovermentOrder(ClsGovermentOrder objGovermentOrder)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInGovermentOrder");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objGovermentOrder.DataUniqueId);
                cmd.Parameters.AddWithValue("@InGovermentOrder", objGovermentOrder.GovermentOrder);
                cmd.Parameters.AddWithValue("@InUnSoldPercentage", objGovermentOrder.UnSoldPercentage);
                cmd.Parameters.AddWithValue("@InNoOfAlphabet", objGovermentOrder.NoOfAlphabet);
                cmd.Parameters.AddWithValue("@InAlphabetName", objGovermentOrder.AlphabetName);
                cmd.Parameters.AddWithValue("@InTicketSlNoFrom", objGovermentOrder.TicketSeriallNoFrom);
                cmd.Parameters.AddWithValue("@InTicketSlNoTo", objGovermentOrder.TicketSerialNoTo);
                cmd.Parameters.AddWithValue("@InTotalTickets", objGovermentOrder.TotalTickets);
                cmd.Parameters.AddWithValue("@InLotteryId", objGovermentOrder.LotteryId);
                cmd.Parameters.AddWithValue("@InUserId", objGovermentOrder.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objGovermentOrder.IpAdd);
                cmd.Parameters.AddWithValue("@ModifiedLotteryName", objGovermentOrder.ModifiedLotteryName);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool DeleteInGovermentOrder(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInGovermentOrder");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetGovermentOrderDtl()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetGovermentOrderDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetGovermentOrderDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetGovermentOrderDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetGovOrderDtlByLotteryId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetGovOrderDtlByLotteryId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetGovermentOrderDtlbyStatus(int InStatus)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetGovermentOrderDtlbyStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InStatus",InStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetLotteryByApprovedGovOrder(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryByApprovedGovOrder");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        #endregion

        #region Deposit To
        public DataTable GetDepositToDtl()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDepositToDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion

        #region Prize
        public bool InsertInPrize(ClsPrize objPrize,DataTable dtPrizeDtl)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInPrize");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InGovermentOrderId", objPrize.DataUniqueId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objPrize.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objPrize.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objPrize.IpAdd);
                cmd.Parameters.AddWithValue("@InTblPrizeDtl", dtPrizeDtl);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateInPrize(ClsPrize objPrize,DataTable dtPrizeDtl)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInPrize");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InGovermentOrderId", objPrize.DataUniqueId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objPrize.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objPrize.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objPrize.IpAdd);
                cmd.Parameters.AddWithValue("@InTblPrizeDtl", dtPrizeDtl);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool DeleteInPrize(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInPrize");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetPrizeDtl()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetPrizeDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataSet GetPrizeDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetPrizeDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataSet ObjDataTable = objDbUlility.GetDataSet(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataSet GetLotteryWiningSerialNoDtlByLotteryNo(Int64 InDataUniqueId, string LotteryNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryWiningSerialNoDtlByLotteryNo");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                cmd.Parameters.AddWithValue("@InLotteryNo", LotteryNo);
                DataSet ObjDataSet = objDbUlility.GetDataSet(cmd);
                return ObjDataSet;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateApprovalInPrize(ClsPrize objClsPrize)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInPrize");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objClsPrize.DataUniqueId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objClsPrize.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objClsPrize.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objClsPrize.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        #endregion

        #region Prize Winner 
       
        public bool UpdateInPrizeWinner(ClsPrizeWinner objPrizeWinner, DataTable dtPrizeWinnerDtl)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInPrizeWinner");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objPrizeWinner.DataUniqueId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objPrizeWinner.SaveStatus);
                cmd.Parameters.AddWithValue("@InJudgesName1", objPrizeWinner.JudgesName1);
                cmd.Parameters.AddWithValue("@InJudgesName2", objPrizeWinner.JudgesName2);
                cmd.Parameters.AddWithValue("@InJudgesName3", objPrizeWinner.JudgesName3);
                cmd.Parameters.AddWithValue("@InPlayingAddress", objPrizeWinner.PlayingAddress);
                cmd.Parameters.AddWithValue("@InDrawTime", objPrizeWinner.DrawTime);                
                cmd.Parameters.AddWithValue("@InUserId", objPrizeWinner.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objPrizeWinner.IpAdd);
                cmd.Parameters.AddWithValue("@InTblPrizeWinnerDtl", dtPrizeWinnerDtl);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }



        public DataTable GetWinneListDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetWinneListDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        //public DataTable GetPrizeWinnerDtlById(Int64 InDataUniqueId)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SpGetPrizeWinnerDtlById");
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
        //        DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
        //        return ObjDataTable;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //    finally { }
        //}
        //public DataTable GetWinneEntryDtlByPrizeWinnerId(Int64 InDataUniqueId)        {
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SpGetWinneEntryDtlByPrizeWinnerId");
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
        //        DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
        //        return ObjDataTable;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //    finally { }
        //}
        public DataTable GetWinneEntryDtlByRequisitionId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetWinneEntryDtlByRequisitionId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        //public DataTable GetLotteryDtlOfPrizeByLotteryTypeId(Int64 InDataUniqueId)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SpGetLotteryDtlOfPrizeByLotteryTypeId");
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
        //        DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
        //        return ObjDataTable;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //    finally { }
        //}

        //public DataTable IsRecordExistForWinnersForLottery(Int64 InLotteryID, DateTime InDrawDate)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("SpIsRecordExistForWinnersForLottery");
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@InLotteryID", InLotteryID);
        //        cmd.Parameters.AddWithValue("@InDrawDate",InDrawDate);
        //        DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
        //        return ObjDataTable;
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //    finally { }
        //}

        public DataTable GetWinnePrizeWinningSlNoDtlById(Int64 InDataUniqueId, Int16 RowNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetWinnePrizeWinningSlNoDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                cmd.Parameters.AddWithValue("@InRowNo", RowNo);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataSet GetWinnigSlNoDtlByRequisitionId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetWinnigSlNoDtlByRequisitionId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataSet ObjDataTable = objDbUlility.GetDataSet(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion

        #region Dealer Deposit
        public bool InsertInDealerDeposit(ClsDealerDeposit objDealerDeposit)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInDealerDeposit");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InRequisitionId", objDealerDeposit.DataUniqueId);
                cmd.Parameters.AddWithValue("@InDepositDate", objDealerDeposit.DepositDate);
                cmd.Parameters.AddWithValue("@InDepositAmount", objDealerDeposit.DepositAmount);
                cmd.Parameters.AddWithValue("@InDepositId", objDealerDeposit.DepositId);
                cmd.Parameters.AddWithValue("@InDepositMethodId", objDealerDeposit.DepositMethodId);
                cmd.Parameters.AddWithValue("@InDepositToId", objDealerDeposit.DepositToId);
                cmd.Parameters.AddWithValue("@InBankName", objDealerDeposit.BankName);
                cmd.Parameters.AddWithValue("@InSaveStatus", objDealerDeposit.SaveStatus);
                cmd.Parameters.AddWithValue("@InBGValidityDays", objDealerDeposit.BGValidityDay);
                cmd.Parameters.AddWithValue("@InRemarks", objDealerDeposit.Remarks);
                cmd.Parameters.AddWithValue("@InUserId", objDealerDeposit.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objDealerDeposit.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateInDealerDeposit(ClsDealerDeposit objDealerDeposit)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInDealerDeposit");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objDealerDeposit.DataUniqueId);
                cmd.Parameters.AddWithValue("@InDepositDate", objDealerDeposit.DepositDate);
                cmd.Parameters.AddWithValue("@InDepositAmount", objDealerDeposit.DepositAmount);
                cmd.Parameters.AddWithValue("@InDepositId", objDealerDeposit.DepositId);
                cmd.Parameters.AddWithValue("@InDepositToId", objDealerDeposit.DepositToId);
                cmd.Parameters.AddWithValue("@InDepositMethodId", objDealerDeposit.DepositMethodId);
                cmd.Parameters.AddWithValue("@InBankName", objDealerDeposit.BankName);
                cmd.Parameters.AddWithValue("@InBGValidityDays", objDealerDeposit.BGValidityDay);
                cmd.Parameters.AddWithValue("@InRemarks", objDealerDeposit.Remarks);
                cmd.Parameters.AddWithValue("@InSaveStatus", objDealerDeposit.BGValidityDay);
                cmd.Parameters.AddWithValue("@InUserId", objDealerDeposit.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objDealerDeposit.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool DeleteInDealerDeposit(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInDealerDeposit");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);               
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetDealerDepositViewDtl(clsInputParameter objInputParameter)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDealerDepositViewDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetDealerDepositViewForApprovalDtl(clsInputParameter objInputParameter,int DepositToId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDealerDepositViewForApprovalDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                cmd.Parameters.AddWithValue("@InDepositToId", DepositToId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetDealerDepositDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDealerDepositDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetDealerDepositDtlByReqId(Int64 InRequsitionId, Int16 status)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDealerDepositDtlByReqId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InRequsitionId", InRequsitionId);
                cmd.Parameters.AddWithValue("@InStatus", status);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable SpGetDealerDepositReconDtlByStatus(string InSaveStatus)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDealerDepositReconDtlByStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InSaveStatus", InSaveStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateApprovalInDealerDeposit(ClsDealerDeposit objDealerDeposit, string ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInDealerDeposit");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objDealerDeposit.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objDealerDeposit.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objDealerDeposit.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateReconInDealerDeposit(ClsDealerDeposit objDealerDeposit)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateReconInDealerDeposit");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objDealerDeposit.DataUniqueId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objDealerDeposit.BGValidityDay);
                cmd.Parameters.AddWithValue("@InReconRemarks", objDealerDeposit.ReconRemarks);                
                cmd.Parameters.AddWithValue("@InUserId", objDealerDeposit.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objDealerDeposit.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataSet GetDealerDepositInHandDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDealerDepositInHandDtlById");
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                cmd.CommandType = CommandType.StoredProcedure;
                DataSet ObjDataSet = objDbUlility.GetDataSet(cmd);
                return ObjDataSet;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateApprovalInReqDealerDeposit(ClsRequisition objRequisition, string ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInReqDealerDeposit");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InDataUniqueId", objRequisition.DataUniqueId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion

        #region Email To
        public DataTable GetEmailToDtlByType(string EmailType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetEmailToDtlByType");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InEmailType", EmailType);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion

        #region TicketInventory
        public bool InsertInTicketInventory(ClsTicketInventory objTicketInventory)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInsertInTicketInventory");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InLotteryId", objTicketInventory.LotteryId);
                cmd.Parameters.AddWithValue("@InDrawDate", objTicketInventory.DrawDate);
                cmd.Parameters.AddWithValue("@InDrawNo", objTicketInventory.DrawNo);
                cmd.Parameters.AddWithValue("@InFnStart", objTicketInventory.FnStart);
                cmd.Parameters.AddWithValue("@InFnEnd", objTicketInventory.FnEnd);
                cmd.Parameters.AddWithValue("@InAlphabetSeries", objTicketInventory.AlphabetSeries);
                cmd.Parameters.AddWithValue("@InTnStart", objTicketInventory.TnStart);
                cmd.Parameters.AddWithValue("@InTnEnd", objTicketInventory.TnEnd);
                cmd.Parameters.AddWithValue("@InSaveStatus", objTicketInventory.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objTicketInventory.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objTicketInventory.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateInTicketInventory(ClsTicketInventory objTicketInventory)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInTicketInventory");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objTicketInventory.DataUniqueId);
                cmd.Parameters.AddWithValue("@InDrawDate", objTicketInventory.DrawDate);
                cmd.Parameters.AddWithValue("@InDrawNo", objTicketInventory.DrawNo);
                cmd.Parameters.AddWithValue("@InFnStart", objTicketInventory.FnStart);
                cmd.Parameters.AddWithValue("@InFnEnd", objTicketInventory.FnEnd);
                cmd.Parameters.AddWithValue("@InAlphabetSeries", objTicketInventory.AlphabetSeries);
                cmd.Parameters.AddWithValue("@InTnStart", objTicketInventory.TnStart);
                cmd.Parameters.AddWithValue("@InTnEnd", objTicketInventory.TnEnd);
                cmd.Parameters.AddWithValue("@InSaveStatus", objTicketInventory.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objTicketInventory.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objTicketInventory.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool GenerateLotteryNumberDtlById(Int64 InDataUniqueId, int TicketStatus)
        {          
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateTicketGenerateStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                cmd.Parameters.AddWithValue("@InTicketStatus", TicketStatus);
                objDbUlility.ExNonQuery(cmd);
                return true;
                
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        
        #endregion
            
        #region Requisition 
        public bool InsertInRequisition(ClsRequisition objRequisition, out string TransactionNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInRequisition");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InReqCode", objRequisition.ReqCode);
                cmd.Parameters.AddWithValue("@InGovermentOrderId", objRequisition.GovermentOrderId);
                cmd.Parameters.AddWithValue("@InDrawDate", objRequisition.DrawDate);
                cmd.Parameters.AddWithValue("@InPressDeliveryDate", objRequisition.PressDeliveryDate);
                cmd.Parameters.AddWithValue("@InDrawNo", objRequisition.DrawNo);    
                cmd.Parameters.AddWithValue("@InQty", objRequisition.Qty);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                cmd.Parameters.AddWithValue("@SlabLimit", objRequisition.SlabLimit);
                cmd.Parameters.Add("@TransactionNo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;                
                objDbUlility.ExNonQuery(cmd);
                TransactionNo = cmd.Parameters["@TransactionNo"].Value.ToString();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateInRequisition(ClsRequisition objRequisition)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInRequisition");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objRequisition.DataUniqueId);
                cmd.Parameters.AddWithValue("@InGovermentOrderId", objRequisition.GovermentOrderId);
                cmd.Parameters.AddWithValue("@InDrawDate", objRequisition.DrawDate);
                cmd.Parameters.AddWithValue("@InPressDeliveryDate", objRequisition.PressDeliveryDate);
                cmd.Parameters.AddWithValue("@InDrawNo", objRequisition.DrawNo);  
                cmd.Parameters.AddWithValue("@InQty", objRequisition.Qty);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                cmd.Parameters.AddWithValue("@SlabLimit", objRequisition.SlabLimit);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateCloseTransactionInRequisition(ClsRequisition objRequisition)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateCloseTransactionInRequisition");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objRequisition.DataUniqueId);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateSendForAdjustment(ClsRequisition objRequisition, string ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateSendForAdjustment");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objRequisition.DataUniqueId);
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetSentAdjustmentDtlByTranNo(Int32 DataUniqueId, int TransactionNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetSentAdjustmentDtlByTranNo");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", DataUniqueId);
                cmd.Parameters.AddWithValue("@InTransactionNo", TransactionNo);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
       
        
        public bool UpdateApprovalInRequisition(ClsRequisition objRequisition, string ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInRequisition");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateApprovalInRequisitionUnSoldTicket(ClsRequisition objRequisition, string ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInRequisitionUnSoldTicket");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUploadStatus", objRequisition.UploadStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateApprovalInRequisitionUnClaimTicket(ClsRequisition objRequisition, string ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInRequisitionUnClaimTicket");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InDataUniqueId", objRequisition.DataUniqueId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUploadStatus", objRequisition.UploadStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateApprovalInReqWinnerPrize(ClsRequisition objRequisition, string ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInReqWinnerPrize");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool DeleteInRequisition(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInRequisition");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetRequisitionDtl(clsInputParameter objInputParameter)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetRequisitionDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetRequisitionDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetRequisitionDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetRequisitionDtlByStatus(clsInputParameter objInputParameter)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetRequisitionDtlByStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetRequisitiondataForDDL(int SaveStatus, int Opmode)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_LoadRequisitionDDLByStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SaveStatus", SaveStatus);
                cmd.Parameters.AddWithValue("@Opmode", Opmode);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetBankGuranteeLedger(clsInputParameter objInputParameter)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetBankGuranteeLedger");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLastDrawDateAndNoDtlByLotteryId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLastDrawDateAndNoDtlByLotteryId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLotteryDtlFromRequisitionDtl(Int64 InLotteryId, int? InDrawNo, DateTime? InDrawDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryDtlFromRequisitionDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InLotteryId", InLotteryId);


                if (InDrawNo != null)
                {
                    cmd.Parameters.AddWithValue("@InDrawNo", InDrawNo);
                }
                else if (InDrawNo == null)
                {
                    cmd.Parameters.AddWithValue("@InDrawNo", DBNull.Value);
                }  

                if (InDrawDate != null){
                    cmd.Parameters.AddWithValue("@InDrawDate", InDrawDate);
                }
                else if (InDrawDate == null){
                     cmd.Parameters.AddWithValue("@InDrawDate", DBNull.Value);
                }               
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataSet GetLotteryDtlInClaimAndUnSold(Int64 InDataUniqueId, string InLotteryNo,int FnNo, string Alphabet,Int64 TnNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryDtlInClaimAndUnSold");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                cmd.Parameters.AddWithValue("@InLotteryNo", InLotteryNo);
                cmd.Parameters.AddWithValue("@InFnNo", FnNo);
                cmd.Parameters.AddWithValue("@InAlphabet", Alphabet);
                cmd.Parameters.AddWithValue("@InTnNo", TnNo);
                DataSet ObjDataTable = objDbUlility.GetDataSet(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetUnSoldSummaryById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetUnSoldSummaryById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetPrintOrderDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetPrintOrderDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        
        #endregion
        
        #region Requisition Dealer
        public bool InsertInRequisitionDealer(ClsRequisition objRequisition,DataTable tblDirectorRequisitionDtl, out string TransactionNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInRequisitionDealer");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InReqCode", objRequisition.ReqCode);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                cmd.Parameters.AddWithValue("@InTblDirectorRequisitionDtl", tblDirectorRequisitionDtl);
                cmd.Parameters.Add("@TransactionNo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                objDbUlility.ExNonQuery(cmd);
                TransactionNo = cmd.Parameters["@TransactionNo"].Value.ToString();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateInRequisitionDealer(ClsRequisition objRequisition, DataTable tblDirectorRequisitionDtl)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInRequisitionDealer");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objRequisition.DataUniqueId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                cmd.Parameters.AddWithValue("@InTblDirectorRequisitionDtl", tblDirectorRequisitionDtl);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateApprovalInRequisitionDealer(ClsRequisition objRequisition, string ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInRequisitionDealer");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool DeleteInRequisitionDealer(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInRequisitionDealer");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetRequisitionDealerDtl(clsInputParameter objInputParameter)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetRequisitionDealerDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetRequisitionDealerDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetRequisitionDealerDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetRequisitionDealerDtlByStatus(clsInputParameter objInputParameter)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetRequisitionDealerDtlByStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetRequisitionDtlByStatusForDealerReq(int InStatus)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetRequisitionDtlByStatusForDealerReq");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InStatus", InStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetDirectorRequisitionDtlByDealerReqId(Int64 DataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDirectorRequisitionDtlByDealerReqId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", DataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        
        #endregion

        #region User Role Type
        public bool InsertInUserRole(ClsUserRole objUserRole, DataTable dtUserAccessMenuDtl)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInUserRole");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InUserRole", objUserRole.UserRole);
                cmd.Parameters.AddWithValue("@InUserId", objUserRole.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objUserRole.IpAdd);
                cmd.Parameters.AddWithValue("@UserAccessMenuDtl", dtUserAccessMenuDtl);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateInUserRole(ClsUserRole objUserRole, DataTable dtUserAccessMenuDtl)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInUserRole");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objUserRole.DataUniqueId);
                cmd.Parameters.AddWithValue("@InUserRole", objUserRole.UserRole);
                cmd.Parameters.AddWithValue("@InUserId", objUserRole.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objUserRole.IpAdd);
                cmd.Parameters.AddWithValue("@UserAccessMenuDtl", dtUserAccessMenuDtl);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool DeleteInUserRole(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInUserRole");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetUserRoleDtl()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetUserRoleDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetUserRoleDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetUserRoleDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetGetMenuListForUserRole(Int64 InUserRoleID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetMenuListForUserRole");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InUserRoleID", InUserRoleID);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetNavMenuListForUserRoleId(Int64 InUserRoleID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetNavMenuListForUserRoleId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InUserRoleID", InUserRoleID);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        
        #endregion
        
        #region User 
        public bool InsertInUser(ClsUser objUser)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInUser");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InTrxUserId", objUser.TrxUserId);
                cmd.Parameters.AddWithValue("@InUserPassword", objUser.UserPassword);
                cmd.Parameters.AddWithValue("@InDisplayName", objUser.DisplayName);
                cmd.Parameters.AddWithValue("@InEmailId", objUser.EmailId);
                cmd.Parameters.AddWithValue("@InLocked", objUser.Locked);
                cmd.Parameters.AddWithValue("@InIsFirstTime", objUser.IsFirstTime);
                cmd.Parameters.AddWithValue("@InMobileNo", objUser.MobileNo);
                cmd.Parameters.AddWithValue("@InUserRole", objUser.UserRoleId);
                cmd.Parameters.AddWithValue("@InUserId", objUser.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objUser.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateInUser(ClsUser objUser)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInUser");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objUser.DataUniqueId);
                cmd.Parameters.AddWithValue("@InTrxUserId", objUser.TrxUserId);
                cmd.Parameters.AddWithValue("@InUserPassword", objUser.UserPassword);
                cmd.Parameters.AddWithValue("@InDisplayName", objUser.DisplayName);
                cmd.Parameters.AddWithValue("@InEmailId", objUser.EmailId);
                cmd.Parameters.AddWithValue("@InLocked", objUser.Locked);
                cmd.Parameters.AddWithValue("@InIsFirstTime", objUser.IsFirstTime);
                cmd.Parameters.AddWithValue("@InMobileNo", objUser.MobileNo);
                cmd.Parameters.AddWithValue("@InUserRole", objUser.UserRoleId);
                cmd.Parameters.AddWithValue("@InUserId", objUser.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objUser.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateUserPassword(ClsUser objUser)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateUserPassword");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objUser.DataUniqueId);
                cmd.Parameters.AddWithValue("@InUserPassword", objUser.UserPassword);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool DeleteInUser(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInUser");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetUserDtl()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetUserDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetUserDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetUserDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetUserDtlByUserId(string InUserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetUserDtlByUserId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InUserId", InUserId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetIsValidUser(string UserId, string UserPassword)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpIsValidUser");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InUserId", UserId);
                cmd.Parameters.AddWithValue("@InUserPassword", UserPassword);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetUserDtlByEmailOrMobile(string EmailId, string MobileNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetUserDtlByEmailOrMobile");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InEmailId", EmailId);
                cmd.Parameters.AddWithValue("@InMobileNo", MobileNo);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetIsMenuAccessAvailable(Int64 InUserRoleId, string InMenuCode)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("[SpIsMenuAccessAvailable]");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InUserRoleId", InUserRoleId);
                cmd.Parameters.AddWithValue("@InMenuCode", InMenuCode);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        #endregion

        #region Lottery Claim Entry
        public bool InserInLotteryClaimEntry(ClsLotteryClaimDetails objClsLotteryClaimDetails, out string TransactionNo)
        {
            try
            {               

                SqlCommand cmd = new SqlCommand("SpInserInLotteryClaimEntry");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InLotteryId", objClsLotteryClaimDetails.LotteryId);
                cmd.Parameters.AddWithValue("@InLotteryRequisitionId", objClsLotteryClaimDetails.DataUniqueId);
                //cmd.Parameters.AddWithValue("@InDrawDate", objClsLotteryClaimDetails.DrawDate);
                cmd.Parameters.AddWithValue("@InMobileNo", objClsLotteryClaimDetails.MobileNo);
                cmd.Parameters.AddWithValue("@InEmailId", objClsLotteryClaimDetails.EmailId);
                cmd.Parameters.AddWithValue("@InLotteryNo", objClsLotteryClaimDetails.LotteryNo);
                cmd.Parameters.AddWithValue("@InClaimType", objClsLotteryClaimDetails.ClaimType);
                cmd.Parameters.AddWithValue("@InName", objClsLotteryClaimDetails.Name);
                cmd.Parameters.AddWithValue("@InSoDoWo", objClsLotteryClaimDetails.SoDoWo);
                cmd.Parameters.AddWithValue("@InSurety", objClsLotteryClaimDetails.Surety);
                cmd.Parameters.AddWithValue("@InProprietorOf", objClsLotteryClaimDetails.ProprietorOf);   
                cmd.Parameters.AddWithValue("@InFatherOrGuardianName", objClsLotteryClaimDetails.FatherOrGuardianName);
                cmd.Parameters.AddWithValue("@InAddress", objClsLotteryClaimDetails.Address);
                cmd.Parameters.AddWithValue("@InAadharNo", objClsLotteryClaimDetails.AadharNo);
                cmd.Parameters.AddWithValue("@InPanNo", objClsLotteryClaimDetails.PanNo);
                cmd.Parameters.AddWithValue("@InBankName", objClsLotteryClaimDetails.BankName);
                cmd.Parameters.AddWithValue("@InBankAccountNo", objClsLotteryClaimDetails.BankAccountNo);
                cmd.Parameters.AddWithValue("@InIFSCCode", objClsLotteryClaimDetails.IFSCCode);
                cmd.Parameters.AddWithValue("@InCaptcha", objClsLotteryClaimDetails.Captcha);
                cmd.Parameters.AddWithValue("@InOTP", objClsLotteryClaimDetails.OTP);
                cmd.Parameters.AddWithValue("@InPanFile", objClsLotteryClaimDetails.Pan);
                cmd.Parameters.AddWithValue("@InAadharFile", objClsLotteryClaimDetails.Aadhar);
                cmd.Parameters.AddWithValue("@InBankDtl", objClsLotteryClaimDetails.BankDtl);
                cmd.Parameters.AddWithValue("@InPhoto", objClsLotteryClaimDetails.Photo);
                cmd.Parameters.AddWithValue("@InPwtTicket", objClsLotteryClaimDetails.PwtTicket);
                cmd.Parameters.AddWithValue("@InUserId", objClsLotteryClaimDetails.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objClsLotteryClaimDetails.IpAdd);
                cmd.Parameters.Add("@TransactionNo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                objDbUlility.ExNonQuery(cmd);
                TransactionNo = cmd.Parameters["@TransactionNo"].Value.ToString();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLotteryClaimEntryDtlByReqCode(string ReqCode)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryClaimEntryDtlByReqCode");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InReqCode", ReqCode);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLotteryClaimEntryDtlByStatus(clsInputParameter objInputParameter, int InClaimType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryClaimEntryDtlByStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                cmd.Parameters.AddWithValue("@InClaimType", InClaimType);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateApprovalInLotteryClaimEntry(ClsLotteryClaimApprovalDetails objClsLotteryClaimApprovalDetails)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInLotteryClaimEntry");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objClsLotteryClaimApprovalDetails.DataUniqueId);
                cmd.Parameters.AddWithValue("@InPayableAmount", objClsLotteryClaimApprovalDetails.PayableAmount);
                cmd.Parameters.AddWithValue("@InSaveStatus", objClsLotteryClaimApprovalDetails.SaveStatus);
                cmd.Parameters.AddWithValue("@InRemarks", objClsLotteryClaimApprovalDetails.Remarks);
                cmd.Parameters.AddWithValue("@InUserId", objClsLotteryClaimApprovalDetails.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objClsLotteryClaimApprovalDetails.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateApprovalByStausInLotteryClaimEntry(ClsRequisition objRequisition, string ReqId, int ClaimType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalByStausInLotteryClaimEntry");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InClaimType", ClaimType);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetLotteryClaimSendToGovDtlByStatus(clsInputParameter objInputParameter, int InClaimType,int GovId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryClaimSendToGovDtlByStatus");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                cmd.Parameters.AddWithValue("@InClaimType", InClaimType);
                cmd.Parameters.AddWithValue("@InGovIdId", GovId);                
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetClaimSendToGovApprovedDtl(clsInputParameter objInputParameter)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetClaimSendToGovApprovedDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InFromDate", objInputParameter.InFromDate);
                cmd.Parameters.AddWithValue("@InToDate", objInputParameter.InToDate);
                cmd.Parameters.AddWithValue("@InStatus", objInputParameter.InStatus);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateLotteryClaimEntrySendToGov(ClsRequisition objRequisition, string ReqId, out string TransactionNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateLotteryClaimEntrySendToGov");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InClaimType", objRequisition.ClaimType);
                cmd.Parameters.AddWithValue("@InGovermentOrderId", objRequisition.GovermentOrderId);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                cmd.Parameters.Add("@TransactionNo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                objDbUlility.ExNonQuery(cmd);
                TransactionNo = cmd.Parameters["@TransactionNo"].Value.ToString();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetSendToGovAnnextureIIIByID(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetSendToGovAnnextureIIIByID");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        #endregion

        #region Ticket Inventory Claimed
        public bool InserInTicketInventoryClaimed(ClsLottery objClsLottery, DataTable IntblTicketNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInTicketInventoryClaimed");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objClsLottery.DataUniqueId);
                cmd.Parameters.AddWithValue("@InUserId", objClsLottery.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objClsLottery.IpAdd);
                cmd.Parameters.AddWithValue("@IntblTicketNo", IntblTicketNo);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataSet GetLotteryClaimEntryDtlByReqId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetLotteryClaimEntryDtlByReqId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataSet ObjDataTable = objDbUlility.GetDataSet(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetVariableClaimPrizeById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetVariableClaimPrizeById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetVariableClaimByClaimId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetVariableClaimByClaimId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetVariableClaimVoucherDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetVariableClaimVoucherDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool DeleteInVariableClaimByVoucherId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInVariableClaimByVoucherId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateApprovalInVariableClaim(ClsRequisition objRequisition, string ReqId, DataTable IntblTicketNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInVariableClaim");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objRequisition.SaveStatus);
                cmd.Parameters.AddWithValue("@InIpAdd", objRequisition.IpAdd);
                cmd.Parameters.AddWithValue("@InUserId", objRequisition.UserId);
                cmd.Parameters.AddWithValue("@InTblVariablePrizeWinnerDtl", IntblTicketNo);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion

        #region Ticket Inventory Un-Sold
        public bool InserInTicketInventoryUnSold(ClsLottery objClsLottery, DataTable IntblTicketNo,int SaveStatus)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInsertInTicketInventoryUnSold");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InSaveStatus", SaveStatus);
                cmd.Parameters.AddWithValue("@InDataUniqueId", objClsLottery.DataUniqueId);
                cmd.Parameters.AddWithValue("@InUserId", objClsLottery.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objClsLottery.IpAdd);
                cmd.Parameters.AddWithValue("@IntblTicketNo", IntblTicketNo);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public DataTable GetLotteryUnsold(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUnsoldReport");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion

        #region Ticket Transaction
        public DataTable GetRequisitionTransactionDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetRequisitionTransactionDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion

        #region Update In Requisition Challan
        public bool UpdateInRequisitionChallan(ClsChallan objChallan)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateInRequisitionChallan");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", objChallan.RequisitionId);               
                cmd.Parameters.AddWithValue("@InSaveStatus", objChallan.SaveStatus);
                cmd.Parameters.AddWithValue("@InUserId", objChallan.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objChallan.IpAdd);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool InserInRequisitionChallan(ClsChallan objChallan,out string TransactionNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpInserInChallan");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InRequisitionId", objChallan.RequisitionId);
                cmd.Parameters.AddWithValue("@InTransporterName", objChallan.TransporterName);         
                cmd.Parameters.AddWithValue("@InConsignmentNo", objChallan.ConsignmentNo);
                cmd.Parameters.AddWithValue("@InCustomerOrderNo", objChallan.CustomerOrderNo);
                cmd.Parameters.AddWithValue("@InCustomerOrdeDate", objChallan.CustomerOrdeDate);
                cmd.Parameters.AddWithValue("@InVehicleNo", objChallan.VehicleNo);
                cmd.Parameters.AddWithValue("@InSubject", objChallan.Subject);
                cmd.Parameters.AddWithValue("@InSACCode", objChallan.SACCode);
                cmd.Parameters.AddWithValue("@InNo", objChallan.No);
                cmd.Parameters.AddWithValue("@InDeliveredQuantity", objChallan.DeliveredQuantity);
                cmd.Parameters.AddWithValue("@InNoOfBoxBundle", objChallan.NoOfBoxBundle);
                cmd.Parameters.AddWithValue("@InRemarks", objChallan.Remarks);
                cmd.Parameters.AddWithValue("@InSaveStatus", objChallan.SaveStatus);
                cmd.Parameters.AddWithValue("@InChallanStatus", objChallan.ChallanStatus);
                cmd.Parameters.AddWithValue("@InUserId", objChallan.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objChallan.IpAdd);
                cmd.Parameters.Add("@TransactionNo", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                objDbUlility.ExNonQuery(cmd);
                TransactionNo = cmd.Parameters["@TransactionNo"].Value.ToString();
                
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetRequisitionChallanDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetRequisitionChallanDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetChallanDtlById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetChallanDtlById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool DeleteInChallanById(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpDeleteInChallanById");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateApprovalInRequisitionChallan(ClsChallan objChallan, string ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpUpdateApprovalInRequisitionChallan");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@InSaveStatus", objChallan.SaveStatus);
                cmd.Parameters.AddWithValue("@InChallanStatus", objChallan.ChallanStatus);
                cmd.Parameters.AddWithValue("@InUserId", objChallan.UserId);
                cmd.Parameters.AddWithValue("@InIpAdd", objChallan.IpAdd);
                cmd.Parameters.AddWithValue("@InRequisitionId", objChallan.RequisitionId);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        
        #endregion

        #region MIS Report
        public DataTable GetVariableIncentiveByReqId(Int64 InDataUniqueId, string ClaimType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetVariableIncentiveByReqId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                cmd.Parameters.AddWithValue("@InClaimType", ClaimType);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataSet GetDealerTransactionDtlByReqId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDealerTransactionDtlByReqId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataSet ObjDataSet = objDbUlility.GetDataSet(cmd);
                return ObjDataSet;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetDealerListOfTransactionDtl()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetDealerListOfTransactionDtl");
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetAdjustmentDtlByRequisitionId(Int32 DataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetAdjustmentDtlByRequisitionId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InRequisitionId", DataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetProfitLossDtlByReqId(Int64 InDataUniqueId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SpGetProfitLossDtlByReqId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InDataUniqueId", InDataUniqueId);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        #endregion

        #region [New Series Generation]
        public bool InsertInSeriesGeneration(ClsSeriesGeneration obj, out string InsertedID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_SeriesGeneration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Series1Start", obj.Series1Start);
                cmd.Parameters.AddWithValue("@Series1End", obj.Series1End);
                cmd.Parameters.AddWithValue("@Series2Start", obj.Series2Start);
                cmd.Parameters.AddWithValue("@Series2End", obj.Series2End);
                cmd.Parameters.AddWithValue("@NumStart", obj.NumStart);
                cmd.Parameters.AddWithValue("@NumEnd", obj.NumEnd);
                cmd.Parameters.AddWithValue("@ReqId", obj.ReqId);
                cmd.Parameters.AddWithValue("@Opmode", 2);
                cmd.Parameters.Add("@InsertedID", SqlDbType.NVarChar, 500).Direction = ParameterDirection.Output;
                objDbUlility.ExNonQuery(cmd);
                InsertedID = cmd.Parameters["@InsertedID"].Value.ToString();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool UpdateInSeriesGeneration(ClsSeriesGeneration obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_SeriesGeneration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", obj.ID);
                cmd.Parameters.AddWithValue("@Series1Start", obj.Series1Start);
                cmd.Parameters.AddWithValue("@Series1End", obj.Series1End);
                cmd.Parameters.AddWithValue("@Series2Start", obj.Series2Start);
                cmd.Parameters.AddWithValue("@Series2End", obj.Series2End);
                cmd.Parameters.AddWithValue("@NumStart", obj.NumStart);
                cmd.Parameters.AddWithValue("@NumEnd", obj.NumEnd);
                cmd.Parameters.AddWithValue("@ReqId", obj.ReqId);
                cmd.Parameters.AddWithValue("@Opmode", 3);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool DeleteInSeriesGeneration(long ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_SeriesGeneration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Opmode", 4);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetSeriesGenerationAll()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_SeriesGeneration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Opmode", default(int));
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetSeriesGenerationById(long ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_SeriesGeneration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Opmode", 1);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetSeriesGenerationByReqId(int ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_SeriesGeneration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@Opmode", 5);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetSeriesGenerationByReqIdSpecific(int ReqId, long Id)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_SeriesGeneration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", Id);
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@Opmode", 8);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetSeriesGenerationListByReqId(int ReqId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_SeriesGeneration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@Opmode", 7);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetSeriesGenerationByReqIdAndSr1End(int ReqId, long Sr1End)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_SeriesGeneration");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReqId", ReqId);
                cmd.Parameters.AddWithValue("@Series1End", Sr1End);
                cmd.Parameters.AddWithValue("@Opmode", 6);
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion [New Series Generation]

        #region [ZipProgress]
        public bool InsertInZipProgress(ZipProgressMaster objZipProgress, out string ZipID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_ZipProgress");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", objZipProgress.UserId);
                cmd.Parameters.AddWithValue("@ReqCode", objZipProgress.ReqCode);
                cmd.Parameters.AddWithValue("@Opmode", 1);
                cmd.Parameters.Add("@ReturnID", SqlDbType.NVarChar, 2048).Direction = ParameterDirection.Output;
                objDbUlility.ExNonQuery(cmd);
                ZipID = cmd.Parameters["@ReturnID"].Value.ToString();
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }

        public bool UpdateInZipProgress(ZipProgressMaster objZipProgress)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_ZipProgress");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", objZipProgress.ID);
                cmd.Parameters.AddWithValue("@ZipProgress", objZipProgress.ZipProgress);
                cmd.Parameters.AddWithValue("@Opmode", 2);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public bool DeleteInZipProgress(long ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_ZipProgress");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Opmode", 3);
                objDbUlility.ExNonQuery(cmd);
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        public DataTable GetZipProgressById(long ID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_ZipProgress");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Opmode", default(int));
                DataTable ObjDataTable = objDbUlility.GetDataTable(cmd);
                return ObjDataTable;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            finally { }
        }
        #endregion [ZipProgress]
    }
}