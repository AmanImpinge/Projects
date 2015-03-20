<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YodelParcel.aspx.cs" Inherits="YodelParcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/style.css" media="screen" type="text/css" />
    <%--   <script type='text/javascript' src='http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js'></script>--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <!--Load Script and Stylesheet -->
    <script src="Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <link type="text/css" href="css/jquery.simple-dtpicker.css" rel="stylesheet" />
    <script src="Scripts/jquery.simple-dtpicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="editor-wrapper">
        <!--editor-wrap-->
        <div class="header">
            <h2>
                Yodel</h2>
            <input type="text" id="date_foo" value="">
        </div>
        <!--header-end-->
        <div class="editor-cont">
            <!--edit-cont-->
            <div class="edit-mid-containor">
                <div class="adult-infowrap">
                    <!--adult-infowrap-->
                    <h1>
                        Information</h1>
                    <div class="adult-info-leftbox">
                        <!--adult-infoleftbox-->
                        <div class="adult-info-leftbox-inner">
                            <label>
                                JobType
                            </label>
                            <input class="adultinfo-input" value="HT" readonly="readonly"></input>
                            <label>
                                Tariff Code
                            </label>
                            <%--<input class="adultinfo-input" value="HDN" readonly="readonly"></input>--%>
                            <asp:DropDownList class="adultinfo-input" ID="ddlTariffCode" runat="server">
                                <asp:ListItem Text="DIA" Value="1234568"></asp:ListItem>
                                <asp:ListItem Text="NOON" Value="1234569"></asp:ListItem>
                                <asp:ListItem Text="SAT" Value="1234570"></asp:ListItem>
                                <asp:ListItem Text="1EXP" Value="1234571"></asp:ListItem>
                                <asp:ListItem Text="ISLE" Value="1234572"></asp:ListItem>
                                <asp:ListItem Text="STD" Value="1234573"></asp:ListItem>
                                <asp:ListItem Text="1EB" Value="1234574"></asp:ListItem>
                                <asp:ListItem Text="1EU" Value="1234575"></asp:ListItem>
                                <asp:ListItem Text="ECO" Value="1234576"></asp:ListItem>
                                <asp:ListItem Text="NIS" Value="1234577"></asp:ListItem>
                                <asp:ListItem Text="STSA" Value="1234578"></asp:ListItem>
                                <asp:ListItem Text="HDN" Value="1234569"></asp:ListItem>
                                <asp:ListItem Text="1HB" Value="1234570"></asp:ListItem>
                                <asp:ListItem Text="HECO" Value="1234571"></asp:ListItem>
                                <asp:ListItem Text="2HB" Value="1234572"></asp:ListItem>
                                <asp:ListItem Text="2HN" Value="1234573"></asp:ListItem>
                                <asp:ListItem Text="HDPA" Value="1234574"></asp:ListItem>
                                <asp:ListItem Text="HIPA" Value="1234575"></asp:ListItem>
                                <asp:ListItem Text="5HCT" Value="1234576"></asp:ListItem>
                                <asp:ListItem Text="HSAT" Value="1234577"></asp:ListItem>
                                <asp:ListItem Text="PACK" Value="1234578"></asp:ListItem>
                            </asp:DropDownList>
                            <label>
                                Service Code
                            </label>
                            <input class="adultinfo-input" value="ON" readonly="readonly"></input>
                            <label>
                                Pick Up DateTime
                            </label>
                            <asp:TextBox ID="txtPickUpDate" class="adultinfo-input" runat="server"></asp:TextBox>
                            <%-- <label>
                                Pick Up Time
                            </label>
                            <input class="adultinfo-input" value="NA" readonly="readonly"></input>--%>
                            <label>
                                Your Reference(Optional)
                            </label>
                            <asp:TextBox ID="txtReference" class="adultinfo-input" runat="server"></asp:TextBox>
                            <label>
                                Second Customer Reference(Optional)
                            </label>
                            <asp:TextBox ID="txtSecondReference" class="adultinfo-input" runat="server"></asp:TextBox>
                            <label>
                                Order Date Time
                            </label>
                            <asp:TextBox ID="txtOrderDateTime" class="adultinfo-input" runat="server"></asp:TextBox>
                            <%--  <label>
                                Order Time
                            </label>
                            <input class="adultinfo-input" value="NA" readonly="readonly"></input>--%>
                            <label>
                                Customer Contact details via SMS / Email
                            </label>
                            <label>
                                Name
                            </label>
                            <asp:TextBox ID="txtCustomerName" class="adultinfo-input" runat="server"></asp:TextBox>
                            <label>
                                Contact Mobile Number
                            </label>
                            <asp:TextBox ID="txtCustomerMobile" class="adultinfo-input" runat="server"></asp:TextBox>
                            <label>
                                Email
                            </label>
                            <asp:TextBox ID="txtCustomerEmail" class="adultinfo-input" runat="server"></asp:TextBox>
                            <label>
                                Additional Information
                            </label>
                            <label style="float: none!important">
                                Best Service
                            </label>
                            <asp:CheckBox class="adultinfo-input" ID="chkBstService" Style="padding-right: 167px"
                                runat="server" />
                        </div>
                    </div>
                    <div class="adult-info-rightbox">
                        <!--adult-inforightbox-->
                        <div class="adult-info-rightbox-inner">
                            <div class="info-row">
                                <div class="info-column">
                                    <label>
                                        Address Detail</label></div>
                            </div>
                            <div class="info-row">
                                <div class="info-column">
                                    <label>
                                        Company
                                    </label>
                                    <input class="adultinfo-input" value="SENDER COMPANY NAME" readonly="readonly" />
                                    <label>
                                        Street
                                    </label>
                                    <input class="adultinfo-input" value="CIM DEPARTMENT" readonly="readonly" />
                                    <label>
                                        Locality
                                    </label>
                                    <input class="adultinfo-input" value="FROBISHER WAY" readonly="readonly" />
                                    <label>
                                        Town
                                    </label>
                                    <input class="adultinfo-input" value="HATFIELD BUS PK" readonly="readonly" />
                                </div>
                                <div class="info-column">
                                    <label>
                                        Building
                                    </label>
                                    <input class="adultinfo-input" value="YODEL SORT CENTRE" readonly="readonly"></input>
                                    <label>
                                        County
                                    </label>
                                    <input class="adultinfo-input" value="HATFIELD" readonly="readonly" />
                                    <label>
                                        Zip
                                    </label>
                                    <input class="adultinfo-input" value="AL10 9TR" readonly="readonly" />
                                    <label>
                                        Country
                                    </label>
                                    <input class="adultinfo-input" value="GB" readonly="readonly" />
                                </div>
                            </div>
                            <div class="info-row">
                                <div class="info-column">
                                    <label>
                                        Collection Contact Details
                                    </label>
                                </div>
                            </div>
                            <div class="info-row">
                                <div class="info-column">
                                    <label>
                                        Name
                                    </label>
                                    <asp:TextBox ID="txtCollectionContactName" class="adultinfo-input" runat="server"></asp:TextBox>
                                </div>
                                <div class="info-column">
                                    <label>
                                        Telephone
                                    </label>
                                    <asp:TextBox ID="txtCollectionContactNumber" class="adultinfo-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="info-row">
                                <div class="info-column">
                                    <label>
                                        Delivery Address Information
                                    </label>
                                </div>
                            </div>
                            <div class="info-row">
                                <div class="info-column">
                                    <label>
                                        Company
                                    </label>
                                    <asp:TextBox ID="txtDelCompanyName" class="adultinfo-input" runat="server"></asp:TextBox>
                                    <label>
                                        Street
                                    </label>
                                    <asp:TextBox ID="txtDelStreet" class="adultinfo-input" runat="server"></asp:TextBox>
                                    <label>
                                        Locality
                                    </label>
                                    <asp:TextBox ID="txtDelLocality" class="adultinfo-input" runat="server"></asp:TextBox>
                                    <label>
                                        Town
                                    </label>
                                    <asp:TextBox ID="txtDelTown" class="adultinfo-input" runat="server"></asp:TextBox>
                                </div>
                                <div class="info-column">
                                    <label>
                                        Building
                                    </label>
                                    <asp:TextBox ID="txtDelBuilding" class="adultinfo-input" runat="server"></asp:TextBox>
                                    <label>
                                        County
                                    </label>
                                    <asp:TextBox ID="txtDelCounty" class="adultinfo-input" runat="server"></asp:TextBox>
                                    <label>
                                        Zip
                                    </label>
                                    <asp:TextBox ID="txtDelZip" class="adultinfo-input" runat="server"></asp:TextBox>
                                    <label>
                                        Country
                                    </label>
                                    <asp:TextBox ID="txtDelCountry" class="adultinfo-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="info-row">
                                <div class="info-column">
                                    <label>
                                        Other Information
                                    </label>
                                </div>
                            </div>
                            <div class="info-row">
                                <div class="info-column">
                                    <label>
                                        Number Of items(Pieces)
                                    </label>
                                    <asp:TextBox ID="txtNumberOfItems" class="adultinfo-input" runat="server"></asp:TextBox>
                                </div>
                                <div class="info-column">
                                    <label>
                                        Total Weight in KG
                                    </label>
                                    <asp:TextBox ID="txtTotalWeight" class="adultinfo-input" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                         <asp:Button ID="btnsubmit" class="button-save" runat="server" Text="Submit" />
                    </div>
                   
                </div>
            </div>
        </div>
    </div>
    <script>
        $('#txtOrderDateTime').appendDtpicker({ "autodateOnStart": false,
            "futureOnly": true
        });
        $('#txtPickUpDate').appendDtpicker({ "autodateOnStart": false,
            "futureOnly": true
        });
         
    </script>
    </form>
</body>
</html>
