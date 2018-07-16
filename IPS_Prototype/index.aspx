<%@ Page Title="" Language="C#" MasterPageFile="~/IPS.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="IPS_Prototype.index" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div id="MemberSearch">
              <button type="button" class="btn btn-primary" id="MemberSearchIcon"><i class="fas fa-search"></i></button>
              
              <div id="MemberHover">
              <input type="text" id="MemberSearchTxtbox" />

               <a id="dLabel" role="button" data-toggle="dropdown" class="btn btn-primary dropdown-toggle">ALL</a>
                                <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu">
                                    <li><a class="dropdown-item" href="#" id="MemberDropdownSelected">ALL</a></li>
                                    <li><a class="dropdown-item" href="#">ORGANISATION</a></li>
                                    <li><a class="dropdown-item" href="#">INDIVIDUAL</a></li>
                                    <li class="dropdown-divider"></li>
                                    <li class="dropdown-submenu">
                                        <a class="dropdown-item" tabindex="-1" href="#">
                                            CATEGORY
                                        </a>
                                        <ul class="dropdown-menu">
                                            <li><a class="dropdown-item" tabindex="-1" href="#">EDUCATION</a></li>
                                            <li><a class="dropdown-item" tabindex="-1" href="#">MINISTRY</a></li>
                                            <li class="dropdown-submenu">
                                            </li>
                                            <li><a class="dropdown-item" href="#">DONATION</a></li>
                                            
                                        </ul>
                                        </li>
                                    </ul>

              </div>
          </div>
        <div id="MemberContent">
        <table class="table table-hover">
  <thead>
    <tr>
      <th scope="col">Name</th>
      <th scope="col">Donation</th>
      <th scope="col">Member</th>
      <th scope="col">Expiry</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <th scope="row">Neo Kai Jie</th>
      <td>$1500</td>
      <td>Indi</td>
      <td>5/20/2016<button type="button" id="MemberExpiryRefresh" class="btn"><i class="fas fa-sync-alt MemberExpiryRefresh"></i></button></td>
    </tr>
    <tr>
      <th scope="row">Tan Xiao Wei</th>
      <td>$3100</td>
      <td>CA</td>
      <td>5/20/2019</td>
    </tr>
    <tr>
      <th scope="row">Dominic Lim</th>
      <td>$1900</td>
      <td>CA</td>
      <td>5/20/2020</td>
    </tr>
      <tr>
      <th scope="row">Hong Yan Feng</th>
      <td>$2000</td>
      <td>Indi</td>
      <td>5/20/2021</td>
    </tr>
      <tr>
      <th scope="row">Alvin Chu</th>
      <td>$1100</td>
      <td>Indi</td>
      <td>5/20/2022</td>
    </tr>
   
  </tbody>
</table>
     
   </div>
</asp:Content>
