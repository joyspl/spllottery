<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrxLotteryClaimEntryPrint.aspx.cs" Inherits="LTMSWebApp.TrxLotteryClaimEntryPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="aspctr" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <%-- <link href="StartPageResource/css/bootstrap.min.css" rel="stylesheet" />


    <link href="Content/AppStyleSheet.css" rel="stylesheet" />--%>
    <title></title>
    <style type="text/css">

        table.divDataEntry {
            padding: 2px 2px 2px 2px;
            font-family:'Times New Roman',serif;
            /*font-family: sans-serif,Calibri,Arial;*/ 
            font-size: 14px;
        }

            /*table.divDataEntry td, th {
                padding: 2px 2px 2px 2px;
            }*/

        .spanClass {
            font-weight:bold;
            display: inline-block; 
            border-bottom: dotted 1px;            
            text-align:center;
        }


        table.DocUpload td, th {
            padding: 4px 4px 4px 4px;
            vertical-align: top;
            border-bottom: 1px solid;
            border-right: 1px groove;
        }

        .hidden {
            display: none;
        }

        .visible {
            display: block;
        }

        .button {
            background-color: #b200ff;
            border: none;
            color: white;
            padding: 6px 14px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
        }
    </style>
    <script>

        function printDiv(divName) {
            var NottoPrintDiv = document.getElementById('dvNotToPrint');
            NottoPrintDiv.setAttribute('class', 'hidden');
            window.print();
            NottoPrintDiv.setAttribute('class', 'visible');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <aspctr:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></aspctr:ToolkitScriptManager>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <div class="container-fluid">
                    <div style="margin: 0 auto; text-align: left; width: 800px; font-family: Calibri,Arial; border-bottom: solid 1px;" id="dvNotToPrint">
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <input type="button" width="150px" onclick="printDiv('printableArea')" value="Print" class="button" /></td>
                                    <td style="text-align: right; padding-left: 675px;">
                                        <asp:Button ID="btnClose" runat="server" class="button" Text="Close" OnClick="btnClose_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="margin: 0 auto; text-align: left; width: 800px; font-family: sans-serif,Calibri,Arial; font-size: 14px;">
                        <div style="width: 800px; margin: 0 auto;" id="dvPwtAnnexI" runat="server">
                            <table border="0" class="divDataEntry" style="width: 100%;">
                                <tr>
                                    <td style="text-align: center;padding-left:100px; font-weight: bold;">Annexure–I</td>
                                    <td style="width: 20%;" rowspan="6">
                                        <asp:Image ID="imgPhoto" runat="server" Height="140px" Width="120px" BackColor="Beige" Visible="false" BorderStyle="Solid" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;padding-left:100px; font-weight: bold;">Form for claim and Pre-receipt </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;padding-left:100px; font-weight: bold;">(For prizes above Rs. 10000/-)</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">To</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">The Director,</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">West Bengal State Lotteries,</td>
                                </tr>
                               
                                <tr>
                                    <td style="font-family: Verdana, sans-serif 16px #4e1b31; text-align: right; color: #582239; font-weight: bold;" colspan="2">
                                        <asp:Label ID="lblApplicationId" runat="server" Text="Application Id :"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-family: Verdana, sans-serif 16px #4e1b31; font-size: 11px; font-style: italic; text-align: right" colspan="2">
                                        <asp:Label ID="lblNote" runat="server" Text="(Please Keep the Application Id for your futere reference)"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table border="0" class="divDataEntry" style="width: 100%;">
                                <tr>
                                    <td style="width: 3%; font-weight: bold;">1</td>
                                    <td style="width: 37%; font-weight: bold;">Name of the Prize Winner/Claimant : *</td>
                                    <td style="width: 60%; border-bottom: dotted 1px;">
                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">2</td>
                                    <td style="font-weight: bold;">Name of Father/Husband/Guardian : *</td>
                                    <td style="border-bottom: dotted 1px;"><asp:Label ID="lblFatherOrGuardianName" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">3</td>
                                    <td style="font-weight: bold;">Postal Address with Pin Code : *</td>
                                    <td style="border-bottom: dotted 1px;">
                                        <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" colspan="2">&nbsp;</td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 20%; font-weight: bold;">Mobile No. *</td>
                                                <td style="width: 100%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblMobileNo" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">4</td>
                                    <td style="font-weight: bold;">Email Id : *</td>
                                    <td style="border-bottom: dotted 1px;">
                                        <asp:Label ID="lblEmailId" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">5</td>
                                    <td style="font-weight: bold;">Prize Winning Ticket No. with Series : *</td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 35%; font-weight: bold; border-bottom: dotted 1px; text-align: center;">
                                                    <asp:Label ID="lblLotteryNo" runat="server" Text=""></asp:Label></td>
                                                <td style="width: 30%; text-align: right; font-weight: bold;">Date of Draw</td>
                                                <td style="width: 35%; border-bottom: dotted 1px; text-align: center;">
                                                    <asp:Label ID="lblDrawDate" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">6</td>
                                    <td style="font-weight: bold;">Name and Number of Draw :</td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 60%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblLotteryName" runat="server" Text=""></asp:Label>
                                                    &nbsp;&nbsp; <b>Type: </b>
                                                    <asp:Label ID="lblLotteryType" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 20%;">Draw No</td>
                                                <td style="width: 20%; border-bottom: dotted 1px; text-align: center;">
                                                    <asp:Label ID="lblDrawNo" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">7</td>
                                    <td style="font-weight: bold;">Category of Prize : *</td>
                                    <td style="border-bottom: dotted 1px;">
                                        <asp:Label ID="lblNameOfPrize" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">8</td>
                                    <td style="font-weight: bold;">Amount *: Rs</td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 20%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblPrizeAmount" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 15%;">(Rupees)</td>
                                                <td style="width: 65%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblRupees" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; vertical-align: top;">9</td>
                                    <td style="font-weight: bold;">Name & Address of Banker with IFSC and Account Number *</td>
                                    <td style="border-bottom: dotted 1px;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 45%; font-weight: bold; ">Bank Name & Branch Name *</td>
                                                <td style="width: 55%; ">
                                                    <asp:Label ID="lblBankName" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" colspan="2">&nbsp;</td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 30%; font-weight: bold; border-bottom: dotted 1px;">Bank Account No *</td>
                                                <td style="width: 20%; ">
                                                    <asp:Label ID="lblBankAccountNo" runat="server" Text=""></asp:Label></td>
                                                <td style="width: 20%; font-weight: bold; border-bottom: dotted 1px;">IFSC Code *</td>
                                                <td style="width: 30%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblIFSCCode" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td style="font-weight: bold;">10</td>
                                    <td style="font-weight: bold;">Permanent Account No. (PAN). : *</td>
                                    <td style="border-bottom: dotted 1px;">
                                        <asp:Label ID="lblPanCard" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">11</td>
                                    <td style="font-weight: bold;">Aadhaar No. / Passport No. : *</td>
                                    <td style="border-bottom: dotted 1px;">
                                        <asp:Label ID="lblAadharNo" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; vertical-align: top;">12</td>
                                    <td style="vertical-align: top; text-align: justify;" colspan="2">I, hereby, declare that I am the owner of the above said prize winning lottery ticket and submit the prize-winning
                                        ticket duly singed by me on the backside for payment of the net prize amount after deduction of the Administrative
                                        Charge as mentioned on the overleaf of the ticket and Income Tax as admissible. It is also certified that the information
                                        provided above are correct.
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" colspan="3">
                                        <div style="margin-top: 30px;">Signature of Witness:</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 60%; font-weight: bold;">Name</td>
                                                <td style="width: 40%; border-bottom: solid 1px;">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 60%; font-weight: bold;">Address:</td>
                                                <td style="width: 40%; text-align: center; font-weight: bold;">Signature of Claimant</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div style="margin-top: 20px;">Advance receipt with Revenue Stamp acknowledging payment</div>
                                    </td>
                                </tr>
                            </table>
                             <div style="margin-top: 5px;">&nbsp;</div>
                             <table border="0" class="divDataEntry" style="width: 100%;">
                                <tr>
                                    <td style="text-align: center;width:45%;" >&nbsp;</td>
                                    <td style="width:10%; border:solid 1px ;" rowspan="3">

                                    </td>
                                     <td style="text-align: center;width:45%;" >&nbsp;</td>
                                </tr>
                                 <tr>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align:right;">Full Signature of Claimant</td>
                                </tr>
                                 <tr>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align:right;">With Revenue Stamp</td>
                                </tr>
                                 <tr>
                                    <td style="text-align: justify; font-weight:bold;" colspan="3">
                                        Note :– Enclose the copy of PAN, Aadhaar etc. , First page of Pass book, a cancelled cheque leaf & duly notarized Indemnity Bond.
                                    </td>
                                </tr>
                                <tr><td style="font-size:11px;" colspan="3">*These fields are mandatory.</td></tr>
                                <tr><td style=" font-weight:bold; text-align:center;" colspan="3">Office Use only</td></tr>
                                <tr><td style="text-align: justify;" colspan="3">Verified with reference to the sold tickets, official result sheet and found correct. Hence may be passed for payment.</td></tr>
                                <tr>
                                    <td colspan="3" >
                                        <table border="0" style="width:100%;">
                                            <tr>
                                                <td style="border-bottom: dotted 1px; width:25%;">&nbsp;</td>
                                                <td style="width:12%;">&nbsp;</td>
                                                <td style="border-bottom: dotted 1px;width:25%;">&nbsp;</td>
                                                <td style="width:13%;">&nbsp;</td>
                                                <td style="border-bottom: dotted 1px;width:25%;">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="3" >
                                        <table border="0" style="width:100%;">
                                            <tr>
                                                <td style="width:25%; text-align:center;">DA</td>
                                                <td style="width:12%;">&nbsp;</td>
                                                <td style="width:25%;text-align:center;">Sr. Accountant/Supervisor</td>
                                                <td style="width:13%;">&nbsp;</td>
                                                <td style="width:25%;text-align:center;">DDSL/JDSL</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="3" >
                                        <table border="0" style="width:100%;">
                                            <tr>
                                                <td style="width:25%;">Passed for payment of Rs</td>
                                                <td style="width:20%;border-bottom: dotted 1px;">&nbsp;</td>
                                                <td style="width:15%;text-align:center;">Rupees</td>
                                                <td style="width:30%;border-bottom: dotted 1px;">&nbsp;</td>
                                                <td style="width:10%;text-align:center;">only.</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="3" >
                                        <table border="0" style="width:100%;">
                                            <tr>
                                                <td style="width:40%;border-bottom: dotted 1px;">&nbsp;</td>
                                                <td style="width:20%;">&nbsp;</td>
                                                <td style="width:45%;border-bottom: dotted 1px;">&nbsp;</td>                                               
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="3" >
                                        <table border="0" style="width:100%;">
                                            <tr>
                                                <td style="width:40%;font-weight:bold;text-align:center;">Deputy Director (A)</td>
                                                <td style="width:20%;">&nbsp;</td>
                                                <td style="width:45%;font-weight:bold;text-align:center;">DDO/Joint Director (A & A)</td>                                               
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                             </table>
                        </div>
                         <div style="page-break-before: always; margin-top:1px; " id="dvPageBreak" runat="server"></div>
                        <div style="width: 800px; margin: 0 auto;" id="dvPwtBondAnnexI" runat="server">                 
                             <table border="0" class="divDataEntry" style="width: 100%;">
                                <tr>
                                    <td style="text-align: center; font-weight: bold;">Annexure–II</td>
                                </tr>
                                  <tr>
                                    <td style="text-align: center; font-weight: bold;">Bond of Indemnity</td>
                                </tr>
                             </table>          
                             <table border="0" class="divDataEntry" style="width: 100%;">
                                 <tr>
                                     <td style="text-align:justify;">
                                        <p style="font-weight:bold;">For the  <span style="width:100px;" class="spanClass" ><asp:Label ID="lblNameOfPrizeAnxII" runat="server" Text=""></asp:Label></span>	
                                         Prize of <span style="width:200px;" class="spanClass" ><asp:Label ID="lblLotteryNameAndDrawNoAnxII" runat="server" Text=""></asp:Label></span>(No. & Name of Draw) of West Bengal State Lottery. </p>

                                        <p><b>KNOW ALL MEN </b>by these presents that We (a)<span style="width:200px;" class="spanClass" ><asp:Label ID="lblNameAnxII" runat="server" Text=""></asp:Label></span>
                                        the son/daughter/wife of<span style="width:400px;" class="spanClass" ><asp:Label ID="lblSoDoWoAnxII" runat="server" Text=""></asp:Label></span>
                                        resident of<span style="width:400px;" class="spanClass" ><asp:Label ID="lblAddressAnxII" runat="server" Text=""></asp:Label></span>
                                        (hereinafter called the "Claimant" and (b) S/o, D/o, W/o<span style="width:300px;" class="spanClass" ><asp:Label ID="lblSuretyAnxII" runat="server" Text=""></asp:Label></span>
                                        resident of<span style="width:200px;" class="spanClass" ><asp:Label ID="lblSuretyAddressAnxII" runat="server" Text=""></asp:Label></span>	, the Surety for and 
                                        on behalf of the "Claimant" (hereinafter called the Surety) are held firmly bound to the Governor of West Bengal 
                                        (hereinafter called "the Government") for the sum of Rs.<span style="width:200px;" class="spanClass" ><asp:Label ID="lblPrizeAmountAnxII" runat="server" Text=""></asp:Label></span>
                                        (Rupees <span style="width:300px;" class="spanClass" ><asp:Label ID="lblRupeesAnxII" runat="server" Text=""></asp:Label></span>) 
                                        equivalent to the amount of<span style="width:300px;" class="spanClass" ><asp:Label ID="lblEquivalentNameOfPrizeAnxII" runat="server" Text=""></asp:Label></span>
                                        prize payable against ticket no. <span style="width:150px;" class="spanClass" ><asp:Label ID="lblLotteryNoAnxII" runat="server" Text=""></asp:Label></span>
                                        of<span style="width:300px;" class="spanClass" ><asp:Label ID="lblLotteryNameAndDrawNoAnxII_2" runat="server" Text=""></asp:Label></span>	(No. & Name of Draw) of West Bengal State Lottery and every sum being well and truly to be paid to the Government on demand and without a demur, 
                                        for which payment we bind ourselves and our respective heirs, executors, administrators, legal representatives, successors and assigns by these presents.</p>

                                        Signed this<span style="width:200px;" class="spanClass" ><asp:Label ID="Label11" runat="server" Text=""></asp:Label></span>	
                                        day of<span style="width:200px;" class="spanClass" ><asp:Label ID="Label12" runat="server" Text=""></asp:Label></span>
                                        Two thousand<span style="width:200px;" class="spanClass" ><asp:Label ID="Label13" runat="server" Text=""></asp:Label></span>	.
                                        <p><b>WHEREAS</b> the Ticket No.<span style="width:200px;" class="spanClass" ><asp:Label ID="lblLotteryNoAnxII_2" runat="server" Text=""></asp:Label></span>
                                            of	(No. & Name of Draw) of West Bengal State Lottery has won the<span style="width:200px;" class="spanClass" ><asp:Label ID="lblNameOfPrizeAnxII_2" runat="server" Text=""></asp:Label></span>
                                            Prize of the<span style="width:200px;" class="spanClass" ><asp:Label ID="lblLotteryNameAndDrawNoAnxII_3" runat="server" Text=""></asp:Label></span>(No. & Name of Draw) of 
                                            West Bengal State Lottery conducted by the Directorate of State Lotteries, Government of West Bengal valued Rs.<span style="width:200px;" class="spanClass" ><asp:Label ID="lblPrizeAmountAnxII_3" runat="server" Text=""></asp:Label></span></p>
                                        <p><b>AND WHEREAS</b> the claimant has represented that being the purchaser and owner of the above mentioned prize
                                            <span style="width:200px;" class="spanClass" ><asp:Label ID="lblLotteryNoAnxII_3" runat="server" Text=""></asp:Label></span>  
                                            wining ticket he/she is entitled to the aforesaid
                                             <span style="width:200px;" class="spanClass" ><asp:Label ID="lblNameOfPrizeAnxII_3" runat="server" Text=""></asp:Label></span>	
                                            prize as per condition pointed on the ticket and has formally put forward his/her claim for the same.</p>
                                        <p><b>AND WHEREAS</b> the Director of State Lotteries, being the authorized officer of the Government has agreed to make payment of the said sum of Rs.	
                                            <span style="width:200px;" class="spanClass" ><asp:Label ID="lblPrizeAmountAnxII_4" runat="server" Text=""></asp:Label></span>	
                                            to the Claimant upon the Claimant and the Surety entering into a Bond in the above mentioned sum to indemnify the Government against all claim to the amount so  due  to  the  bonafide	
                                            <span style="width:200px;" class="spanClass" ><asp:Label ID="lblNameOfPrizeAnxII_4" runat="server" Text=""></asp:Label></span>	
                                            prize winner in the above said lottery :</p>
                                        <p><b>AND WHEREAS</b> the Claimant and at his/her request the Surety have agreed to execute the Bond in the terms and manner hereinafter contained;</p>
                                        <p><b>NOW THE CONDITION OF THIS BOND</b> is such that if after Payment has been made, to the Claimant, the Claimant and/or the Surety shall in the event of a claim being made,
                                             by any other person, against the Government with respect to the aforesaid sum of Rs.
                                            <span style="width:200px;" class="spanClass" ><asp:Label ID="lblPrizeAmountAnxII_6" runat="server" Text=""></asp:Label></span>	
                                            (Rupees	
                                            <span style="width:200px;" class="spanClass" ><asp:Label ID="lblRupeesAnxII_6" runat="server" Text=""></asp:Label></span>	
                                            ) 
                                            then refund to the Director, State Lotteries the said sum of Rs.
                                            <span style="width:200px;" class="spanClass" ><asp:Label ID="lblPrizeAmountAnxII_7" runat="server" Text=""></asp:Label></span>	
                                            (Rupees	
                                            <span style="width:200px;" class="spanClass" ><asp:Label ID="lblRupeesAnxII_7" runat="server" Text=""></asp:Label></span>	
                                            ) and shall otherwise, indemnify and keep the Government harmless and indemnified against and from all liabilities in respect of the aforesaid sum and all costs incurred in consequence of the claim thereto THEN the above written BOND or obligation shall be void but otherwise the same shall remain in full force effect and virtue.</p>
                                        <p><b>AND THESE PRESENTS ALSO WITNESS</b> that the liability of the Surety herein under shall not be impaired or discharged by reason of time being granted 
                                            by or any forbearance act or omission of the Government whether with or without the knowledge or consent of the Surety in respect of or in relation to the obligations or conditions to be performed or discharged by the Claimant or by any other method or thing whatsoever which under the law relating to Sureties, would but for this provision shall have no effect of so releasing the Surety from such liability nor shall it be necessary for the Government to see the Claimant before seeing the Surety or either of them for the amount due hereunder.</p>
                                        <p><b>IN WITNESS WHEREOF</b> the Claimant and the Surety hereto have set and subscribed their respective hands hereunto on the day, month and year above written.</p>
                                     </td>
                                 </tr> 
                                 <tr>
                                     <td>
                                         <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 60%; font-weight: bold;">&nbsp;</td>
                                                <td style="width: 40%; border-bottom: solid 1px;">&nbsp;</td>
                                            </tr>
                                        </table>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                          <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 60%; font-weight: bold;">&nbsp;</td>
                                                <td style="width: 40%; text-align: center; font-weight: bold;">Signed by the above named "Claimant"</td>
                                            </tr>
                                        </table>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                         <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 60%; font-weight: bold;">&nbsp;</td>
                                                <td style="width: 40%; border-bottom: solid 1px;">&nbsp;</td>
                                            </tr>
                                        </table>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td>
                                          <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 60%; font-weight: bold;">&nbsp;</td>
                                                <td style="width: 40%; text-align: center; font-weight: bold;">Signed by the above named "Surety"</td>
                                            </tr>
                                        </table>
                                     </td>
                                 </tr>                      
                             </table>
                            <table style="width: 100%;">
                                 <tr>
                                     <td colspan="3">In presence of</td>
                                 </tr>
                                 <tr>
                                     <td colspan="3">1.&nbsp;&nbsp;<span style="width:300px;" class="spanClass" >&nbsp;</span></td>
                                 </tr>
                                <tr>
                                    <td style="width: 5%;">&nbsp;</td>
                                    <td style="width: 50%;">Accepted for and on behalf of the Governor of West Bengal by</td>
                                    <td style="width: 45%; text-align: center; border-bottom: dotted 1px;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td >&nbsp;</td>
                                    <td >(Name and Designation of the Officer) in presence of</td>
                                    <td style=" border-bottom: dotted 1px;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td >&nbsp;</td>
                                    <td >(Name and Designation of the Witness)</td>
                                    <td style="border-bottom: dotted 1px;">&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 800px; margin: 0 auto;font-family:'Times New Roman',serif;font-size:14px;" id="dvPwtAnnexIII" runat="server">
                            <table border="0" class="divDataEntry" style="width: 100%;">
                                <tr>
                                    <td style="text-align: center; padding-left:100px; font-weight: bold;">Annexure–III</td>
                                    <td style="width: 20%;" rowspan="6">
                                        <asp:Image ID="imgAnnextureIII" runat="server" Height="140px" Width="120px" BackColor="Beige" Visible="false" BorderStyle="Solid" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;padding-left:100px; font-weight: bold;">Form for claim and Pre-receipt </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;padding-left:100px; font-weight: bold;">(For Super/Special Prize)</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">To</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">The Director,</td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">West Bengal State Lotteries,</td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; font-weight: bold;" colspan="2">The Required details are as follows:</td>
                                </tr>                               
                                <tr>
                                    <td style="font-family: Verdana, sans-serif 16px #4e1b31; text-align: right; color: #582239; font-weight: bold;" colspan="2">
                                        <asp:Label ID="lblApplicationIdAnxIII" runat="server" Text="Application Id :"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table border="0"  style="width: 100%;">
                                <tr>
                                    <td style="width: 3%; font-weight: bold;">1</td>
                                    <td style="width: 35%; font-weight: bold;">Name of the Claimant (Agent/Seller): </td>
                                    <td style="width: 62%; border-bottom: dotted 1px;">
                                        <asp:Label ID="lblNameAnxIII" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">2</td>
                                    <td style="font-weight: bold;">Postal Address with Pin Code : </td>
                                    <td style="border-bottom: dotted 1px;">
                                        <asp:Label ID="lblAddressAnxIII" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" colspan="2">&nbsp;</td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 20%; font-weight: bold;">Mobile No. </td>
                                                <td style="width: 80%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblMobileNoAnxIII" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">3</td>
                                    <td style="font-weight: bold;">Prize Winning Ticket No. with Series : </td>
                                    <td>
                                        <table style="width: 100%;" border="0">
                                            <tr>
                                                <td style="width: 100%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblLotteryNoAnxIII" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">4</td>
                                    <td style="font-weight: bold;">Category of Prize : </td>
                                    <td>
                                        <table style="width: 100%;" border="0">
                                            <tr>
                                                <td style="width: 100%; font-weight: bold; border-bottom: dotted 1px; text-align: center;">
                                                    <asp:Label ID="lblNameOfPrizeAnxIII" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">5</td>
                                    <td style="font-weight: bold;">Name and Number of Draw :</td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 60%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblLotteryNameAndDrawNoAnxIII" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">6</td>
                                    <td style="font-weight: bold;">Date of Draw : </td>
                                    <td style="border-bottom: dotted 1px;">
                                        <asp:Label ID="lblDrawDateAnxIII" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">7</td>
                                    <td style="font-weight: bold;">Super/Special Prize Amount Rs. </td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 20%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblPrizeAmountAnxIII" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td style="width: 20%; border-bottom: dotted 1px;">&nbsp;</td>
                                                <td style="width: 15%; text-align: center;border-bottom: dotted 1px;">(Rupees</td>
                                                <td style="width: 40%; border-bottom: dotted 1px;"><asp:Label ID="lblRupeesAnxIII" runat="server" Text=""></asp:Label>)</td>
                                                
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; vertical-align: top;">8</td>
                                    <td style="font-weight: bold;">Name & Address of Banker with IFSC and Account Number </td>
                                    <td style="border-bottom: dotted 1px;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 45%; font-weight: bold;">Bank Name & Branch Name </td>
                                                <td style="width: 55%;">
                                                    <asp:Label ID="lblBankNameAnxIII" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;" colspan="2">&nbsp;</td>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 30%; font-weight: bold; border-bottom: dotted 1px;">Bank Account No </td>
                                                <td style="width: 20%;">
                                                    <asp:Label ID="lblBankAccountNoAnxIII" runat="server" Text=""></asp:Label></td>
                                                <td style="width: 20%; font-weight: bold; border-bottom: dotted 1px;">IFSC Code </td>
                                                <td style="width: 30%; border-bottom: dotted 1px;">
                                                    <asp:Label ID="lblIFSCCodeAnxIII" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold;">9</td>
                                    <td style="font-weight: bold;">Permanent Account No. (PAN) : </td>
                                    <td style="border-bottom: dotted 1px;">
                                        <asp:Label ID="lblPanCardAnxIII" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; vertical-align: top;">10</td>
                                    <td style="vertical-align: top; text-align: justify;" colspan="2">
                                        I, hereby, declare that I am the owner/proprietor of
                                        <span style="width:320px;" class="spanClass" ><asp:Label ID="lblProprietorName" runat="server" Text=""></asp:Label></span>	
                                        (Name of agency)<br />
                                        and I am the Agent/Seller of the lottery ticket No.
                                        <span style="width:150px;" class="spanClass" ><asp:Label ID="lblLotteryNoAnxIII_2" runat="server" Text=""></asp:Label></span>	
                                        of Draw
                                        <span style="width:180px;" class="spanClass" ><asp:Label ID="lblLotteryNameAndDrawNoAnxIII_2" runat="server" Text=""></asp:Label></span>	
                                        held<br />
                                        on
                                        <span style="width:200px;" class="spanClass" ><asp:Label ID="lblDrawDateAnxIII_2" runat="server" Text=""></asp:Label></span>	
                                        which has won the	
                                        <span style="width:200px;" class="spanClass" ><asp:Label ID="lblNameOfPrizeAnxIII_2" runat="server" Text=""></asp:Label></span>	
                                        prize .
                                    </td>
                                </tr>                   
                            </table>
                            <div style="margin-top: 5px;">&nbsp;</div>
                            <table border="0" class="divDataEntry" style="width: 100%;"> 
                               
                                <tr>
                                    <td style="text-align: center;width:45%;">&nbsp;</td>
                                    <td style="text-align: center;width:10%;">&nbsp;</td>
                                    <td style="text-align: center;width:45%; font-weight:bold;">Full Signature of Claimant (Agent/Seller)</td>
                                </tr> 
                                <tr>
                                    <td style="text-align: center;width:45%;">Advance receipt with Revenue Stamp acknowledging payment</td>
                                    <td style="text-align: center;width:10%;">&nbsp;</td>
                                    <td style="text-align: center;width:45%;">&nbsp;</td>
                                </tr>  
                               
                            </table>                             
                             <div style="margin-top: 1px;">&nbsp;</div>
                            <table border="0" class="divDataEntry" style="width: 100%;">
                                <tr>
                                    <td style="text-align: center;width:43%;">&nbsp;</td>
                                    <td style="text-align: center;width:10%;">&nbsp;</td>
                                    <td style="text-align: center;width:2%;">&nbsp;</td>
                                    <td style="text-align: center;width:45%;">&nbsp;</td>
                                </tr> 
                                 <tr>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align: center; border:2px solid;" rowspan="4">&nbsp;</td>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align: center;">Received payment</td>
                                </tr> 
                                 <tr>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align: center;">&nbsp;</td>
                                </tr> 
                                 <tr>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align: center; border-bottom:1px solid;">&nbsp;</td>
                                </tr> 
                                <tr>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align: center;">&nbsp;</td>
                                    <td style="text-align: center;">Full Signature of Claimant (Agent/Seller)</td>
                                </tr> 
                                 <tr>
                                    <td style="text-align:justify; border-top:1px solid; font-size:14px;" colspan="4">
                                        Certified that the claim and particulars given above are verified with all related documents and found correct.
                                        The Xerox copies of related vouchers/ money receipts are also submitted in support of the claim. The payment of
                                        Super/Special Prize for agent/seller in respect of<span style="width:320px;" class="spanClass" ><asp:Label ID="lblNameOfPrizeAnxIII_3" runat="server" Text=""></asp:Label></span>	 
                                        Prize of <span style="width:320px;" class="spanClass" ><asp:Label ID="lblLotteryNameAndDrawNoAnxIII_3" runat="server" Text=""></asp:Label></span>	
                                        Draw may be paid to<span style="width:320px;" class="spanClass" ><asp:Label ID="lblNameAnxIII_2" runat="server" Text=""></asp:Label></span>	
                                    </td>
                                </tr> 
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                </tr> 
                                 <tr>
                                    <td style="width:43%; border-bottom:1px solid;" colspan="3">Date</td>
                                    <td style="text-align: center;width:45%;font-weight:bold;border-bottom:1px solid;">(Signature of the Distributor with seal)</td>
                                </tr> 
                                <tr>
                                    <td style="text-align: center;width:45%;font-weight:bold;" colspan="4">Office Use only</td>
                                </tr>
                                <tr>
                                    <td style="text-align:justify;width:45%;font-weight:bold;" colspan="4">The claim is verified with reference to the sold tickets, official result sheet and found correct. Hence may be passed for payment.</td>
                                </tr> 
                                 <tr>
                                    <td style="text-align: center;width:43%;">&nbsp;</td>
                                    <td style="text-align: center;width:10%;">&nbsp;</td>
                                    <td style="text-align: center;width:2%;">&nbsp;</td>
                                    <td style="text-align: center;width:45%;">&nbsp;</td>
                                </tr>  
                            </table>
                             <table border="0" class="divDataEntry" style="width: 100%;">
                                <tr>
                                    <td colspan="3" >
                                        <table border="0" style="width:100%;">
                                            <tr>
                                                <td style="border-bottom: dotted 1px; width:25%;">&nbsp;</td>
                                                <td style="width:12%;">&nbsp;</td>
                                                <td style="border-bottom: dotted 1px;width:25%;">&nbsp;</td>
                                                <td style="width:13%;">&nbsp;</td>
                                                <td style="border-bottom: dotted 1px;width:25%;">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="3" >
                                        <table border="0" style="width:100%;">
                                            <tr>
                                                <td style="width:25%; text-align:center;">Dealing Assistant</td>
                                                <td style="width:12%;">&nbsp;</td>
                                                <td style="width:25%;text-align:center;">Sr. Accountant/Supervisor</td>
                                                <td style="width:13%;">&nbsp;</td>
                                                <td style="width:25%;text-align:center;">DDSL/JDSL</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                 <tr>
                                    <td colspan="3" >
                                       Passed for payment of Rs. 
                                        <span style="width:120px;" class="spanClass" ><asp:Label ID="lblPrizeAmountAnxIII_2" runat="server" Text=""></asp:Label></span>	
                                         (Rupees<span style="width:400px;" class="spanClass" ><asp:Label ID="lblRupeesAnxIII_2" runat="server" Text=""></asp:Label></span>	) only.
                                    </td>
                                </tr>
                                 <tr>
                                     <td style="width: 40%; border-bottom: dotted 1px;">&nbsp;</td>
                                     <td style="width: 20%;">&nbsp;</td>
                                     <td style="width: 45%; border-bottom: dotted 1px;">&nbsp;</td>
                                 </tr>
                                <tr>
                                    <td style="width:40%;font-weight:bold;text-align:center;">Deputy Director (A)</td>
                                    <td style="width:20%;">&nbsp;</td>
                                    <td style="width:45%;font-weight:bold;text-align:center;">DDO/Joint Director (A & A)</td>                                               
                                </tr>
                                 <tr><td colspan="3">&nbsp;</td></tr>
                                 <tr>
                                    <td style="text-align:center;border-top:1px solid;" colspan="3" >
                                       Published by Finance Department (Revenue), West Bengal and printed at Saraswaty Press Ltd.
                                       <br />
                                       (Government of West Bengal Enterprise), Kolkata 700 056.
                                    </td>
                                </tr>
                             </table>
                        </div>                        
                        <div style="width: 800px; margin: 0 auto;">

                            <div style="page-break-before: always; margin-top:100px;"></div>
                            <table border="0" class="divDataEntry" style="width: 100%; border-top: 2px groove #808080;">
                                <tr> <td style="font-weight: bold;background-color:#808080;">Prize Winning Ticket</td></tr>
                                <tr id="trPwtTicket" runat="server" visible="false"><td><asp:Image ID="imgPwtTicket" runat="server" Height="400px" Width="600px" BackColor="Beige" Visible="false" BorderStyle="Solid" /></td></tr>
                                <tr> <td style="font-weight: bold;background-color:#808080;">Pan Card</td></tr>
                                <tr><td><asp:Image ID="imgPan" runat="server" Height="400px" Width="600px" BackColor="Beige" Visible="false" BorderStyle="Solid" /></td></tr>
                                <tr> <td style="font-weight: bold;background-color:#808080;">Aadhar Card</td></tr>
                                <tr><td><asp:Image ID="imgAadharPic" runat="server" Height="400px" Width="600px" BackColor="Beige" Visible="false" BorderStyle="Solid" /></td></tr>
                                <tr> <td style="font-weight: bold;background-color:#808080;">Bank Details</td></tr>
                                <tr><td><asp:Image ID="imgBankDtl" runat="server" Height="400px" Width="600px" BackColor="Beige" Visible="false" BorderStyle="Solid" /></td></tr>

                            </table>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
